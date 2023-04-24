﻿using Microsoft.Extensions.Logging;

using System.Threading;

using ThingsGateway.Foundation;

using TouchSocket.Core;

namespace ThingsGateway.Web.Foundation;
/// <summary>
/// <para></para>
/// 采集插件，继承实现不同PLC通讯
/// <para></para>
/// 读取字符串，DateTime等等不确定返回字节数量的方法属性特殊方法，需使用<see cref="DeviceMethodAttribute"/>特性标识
/// </summary>
public abstract class CollectBase : DriverBase
{
    /// <summary>
    /// <see cref="TouchSocketConfig"/> 
    /// </summary>
    public TouchSocketConfig TouchSocketConfig;

    /// <inheritdoc cref="CollectBase"/>
    public CollectBase(IServiceScopeFactory scopeFactory) : base(scopeFactory)
    {
        TouchSocketConfig = new TouchSocketConfig();
        TouchSocketConfig.ConfigureContainer(a => a.RegisterSingleton<ILog>(new EasyLogger(Log_Out)));
    }
    /// <summary>
    /// 导入变量UI Type，继承实现<see cref="DriverImportUIBase"/>后，返回继承类的Type，如果不存在，返回null
    /// </summary>
    public virtual Type DriverImportUIType { get; }

    /// <summary>
    /// 数据转换器
    /// </summary>
    /// <returns></returns>
    public abstract IThingsGatewayBitConverter ThingsGatewayBitConverter { get; }

    /// <summary>
    /// 结束通讯后执行的方法
    /// </summary>
    /// <returns></returns>
    public abstract void AfterStop();

    /// <summary>
    /// 开始通讯前执行的方法
    /// </summary>
    /// <returns></returns>
    public abstract Task BeforStartAsync();

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(ILogger logger, CollectDeviceRunTime device, object client = null)
    {
        _logger = logger;
        IsLogOut = device.IsLogOut;
        Init(device, client);
    }

    /// <summary>
    /// 返回是否支持读取
    /// </summary>
    /// <returns></returns>
    public abstract bool IsSupportAddressRequest();

    /// <summary>
    /// 连读分包，返回实际通讯包信息<see cref="DeviceVariableSourceRead"/> 
    /// <br></br>每个驱动分包方法不一样，所以需要实现这个接口
    /// </summary>
    /// <param name="deviceVariables">设备下的全部通讯点位</param>
    /// <returns></returns>
    public abstract OperResult<List<DeviceVariableSourceRead>> LoadSourceRead(List<CollectVariableRunTime> deviceVariables);

    /// <summary>
    /// 采集驱动读取
    /// </summary>
    public virtual async Task<OperResult<byte[]>> ReadSourceAsync(DeviceVariableSourceRead deviceVariableSourceRead, CancellationToken cancellationToken)
    {
        ushort length;
        if (!ushort.TryParse(deviceVariableSourceRead.Length, out length))
            return new OperResult<byte[]>("解析失败 长度[" + deviceVariableSourceRead.Length + "] 解析失败 :");
        OperResult<byte[]> read = await ReadAsync(deviceVariableSourceRead.Address, length, cancellationToken);
        if (!read.IsSuccess)
            deviceVariableSourceRead.DeviceVariables.ForEach(it => it.SetValue(null));
        return ReadWriteHelpers.DealWithReadResult(read, content =>
        ReadWriteHelpers.PraseStructContent(content, deviceVariableSourceRead.DeviceVariables));
    }

    /// <summary>
    /// 写入变量值
    /// </summary>
    /// <param name="deviceVariable">变量实体</param>
    /// <param name="value">变量写入值</param>
    /// <returns></returns>
    public abstract Task<OperResult> WriteValueAsync(CollectVariableRunTime deviceVariable, string value);

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="device">设备</param>
    /// <param name="client">链路对象，如TCPClient</param>
    protected abstract void Init(CollectDeviceRunTime device, object client = null);

    /// <summary>
    /// 返回全部内容字节数组
    /// <br></br>
    /// 通常使用<see cref="IReadWrite.ReadAsync(string, int, System.Threading.CancellationToken)"/>可以直接返回正确信息
    /// </summary>
    protected abstract Task<OperResult<byte[]>> ReadAsync(string address, int length, CancellationToken cancellationToken);
}