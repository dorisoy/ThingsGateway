﻿#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人Diego所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/diego2098/ThingsGateway
//  Github源代码仓库：https://github.com/kimdiego2098/ThingsGateway
//  使用文档：https://diego2098.gitee.io/thingsgateway-docs/
//  QQ群：605534569
//------------------------------------------------------------------------------
#endregion

using LiteDB;

using Microsoft.Extensions.Logging;

using MQTTnet;

using ThingsGateway.Foundation.Extension.ConcurrentQueue;
using ThingsGateway.Foundation.Sockets;

namespace ThingsGateway.Plugin.Mqtt;

/// <summary>
/// IotSharpClient
/// </summary>
public partial class IotSharpClient : UpLoadBaseWithCache<DeviceData, VariableData>
{
    /// <inheritdoc/>
    public override Type DriverDebugUIType => null;
    protected override bool _device => false;
    public override Type DriverUIType => null;

    /// <inheritdoc/>
    public override VariablePropertyBase VariablePropertys => _variablePropertys;
    protected override IReadWrite _readWrite => null;

    protected override UploadPropertyWithCache _uploadPropertyWithCache => _driverPropertys;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override bool IsConnected() => _mqttClient?.IsConnected == true;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $" {nameof(IotSharpClient)} IP:{_driverPropertys.IP} Port:{_driverPropertys.Port}";
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        try
        {
            _mqttClient?.SafeDispose();
            base.Dispose(disposing);
        }
        catch (Exception ex)
        {
            LogMessage?.LogError(ex);
        }
    }

    protected override void Init(ISenderClient client = null)
    {
        base.Init(client);
        var mqttFactory = new MqttFactory();
        _mqttClientOptions = mqttFactory.CreateClientOptionsBuilder()
           .WithClientId(Guid.NewGuid().ToString())
           .WithCredentials(_driverPropertys.Accesstoken)//账密
           .WithTcpServer(_driverPropertys.IP, _driverPropertys.Port)//服务器
           .WithCleanSession(true)
           .WithKeepAlivePeriod(TimeSpan.FromSeconds(120.0))
           .Build();
        _mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(
                f =>
                {
                    f.WithTopic($"devices/+/rpc/request/+/+");//RPC控制请求，需要订阅
                })

            .Build();
        _mqttClient = mqttFactory.CreateMqttClient();
        _mqttClient.ConnectedAsync += MqttClient_ConnectedAsync;
        _mqttClient.ApplicationMessageReceivedAsync += MqttClient_ApplicationMessageReceivedAsync;

    }


    protected override async Task ProtectedBeforStartAsync(CancellationToken cancellationToken)
    {
        await base.ProtectedBeforStartAsync(cancellationToken);
        if (_mqttClient != null)
        {
            var result = await TryMqttClientAsync(cancellationToken);
            if (!result.IsSuccess)
            {
                LogMessage?.LogWarning($"{ToString()}-连接MqttServer失败：{result.Message}");
            }
        }
    }


    protected override async Task ProtectedExecuteAsync(CancellationToken cancellationToken)
    {

        var clientResult = await TryMqttClientAsync(cancellationToken);
        if (!clientResult.IsSuccess)
        {
            if (success != clientResult.IsSuccess)
            {
                if (!clientResult.IsSuccess)
                    LogMessage.Warning(clientResult.Message);
                success = clientResult.IsSuccess;
            }
            await Delay(_driverPropertys.CycleInterval, cancellationToken);
            return;
        }

        var cacheItems = new List<CacheItem>();

        try
        {
            var list = _collectVariableRunTimes.ToListWithDequeue();
            if (list?.Count != 0)
            {
                var data = list.GroupBy(a => a.DeviceName).ToList();
                foreach (var item in data)
                {
                    try
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            Dictionary<string, object> nameValueDict = new();
                            foreach (var pair in item)
                            {
                                nameValueDict.AddOrUpdate(pair.Name, pair.Value);
                            }
                            var json = nameValueDict.ToJsonString();
                            var result = await MqttUpAsync($"devices/{item.Key}/telemetry", json, cancellationToken);
                            if (success != result.IsSuccess)
                            {
                                if (!result.IsSuccess)
                                    LogMessage.Warning(result.ToString());
                                success = result.IsSuccess;
                            }
                            if (!result.IsSuccess)
                            {
                                AddVarCahce(cacheItems, item.Key, json);

                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage?.LogWarning(ex);
                    }

                }
            }

        }
        catch (Exception ex)
        {
            LogMessage?.LogWarning(ex);
        }


        if (cacheItems.Count > 0)
            CacheDb.Cache.Insert(cacheItems);

        List<long> successIds = new();
        try
        {
            var varList = CacheDb.Cache.Find(a => a.Type == varType, 0, 100).ToList();
            {
                foreach (var item in varList)
                {
                    try
                    {
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            var result = await MqttUpAsync($"devices/{item.Key}/telemetry", item.Value, cancellationToken);
                            if (success != result.IsSuccess)
                            {
                                if (!result.IsSuccess)
                                    LogMessage.Warning(result.ToString());
                                success = result.IsSuccess;
                            }
                            if (result.IsSuccess)
                                successIds.Add(item.Id);
                        }
                        else
                        {
                            break;
                        }

                    }
                    catch (Exception ex)
                    {
                        LogMessage?.LogWarning(ex);
                    }
                }

            }
        }
        catch (Exception ex)
        {
            LogMessage?.LogWarning(ex);
        }

        if (successIds.Count > 0)
            CacheDb.Cache.DeleteMany(a => successIds.Contains(a.Id));

        await Delay(_driverPropertys.CycleInterval, cancellationToken);
    }


}