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

using Newtonsoft.Json.Linq;

using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.ComplexTypes;
using Opc.Ua.Configuration;


//修改自https://github.com/dathlin/OpcUaHelper 与OPC基金会net库

namespace ThingsGateway.Foundation.Adapter.OPCUA;
/// <summary>
/// 订阅委托
/// </summary>
/// <param name="value"></param>
public delegate void DataChangedEventHandler((VariableNode variableNode, DataValue dataValue, JToken jToken) value);
/// <summary>
/// OPCUAClient
/// </summary>
public class OPCUAClient : IDisposable
{

    #region 属性，变量等
    /// <summary>
    /// 当前配置
    /// </summary>
    public OPCNode OPCNode;

    /// <summary>
    /// ProductUri
    /// </summary>
    public string ProductUri = "https://diego2098.gitee.io/thingsgateway-docs/";

    /// <summary>
    /// 当前保存的变量名称列表
    /// </summary>
    public List<string> Variables = new();

    private readonly Action<byte, object, string, Exception> _logAction;

    /// <summary>
    /// 当前的变量名称/OPC变量节点
    /// </summary>
    private readonly Dictionary<string, VariableNode> _variableDicts = new();
    private readonly object checkLock = new();
    /// <summary>
    /// 当前的订阅组，组名称/组
    /// </summary>
    private readonly Dictionary<string, Subscription> dic_subscriptions = new();
    private readonly ApplicationInstance m_application = new();

    private readonly ApplicationConfiguration m_configuration;
    private SessionReconnectHandler m_reConnectHandler;

    private ISession m_session;

    /// <summary>
    /// 默认的构造函数，实例化一个新的OPC UA类
    /// </summary>
    public OPCUAClient(Action<byte, object, string, Exception> log)
    {
        _logAction = log;
        var certificateValidator = new CertificateValidator();
        certificateValidator.CertificateValidation += CertificateValidation;

        // 构建应用程序配置
        m_configuration = new ApplicationConfiguration
        {
            ApplicationName = OPCUAName,
            ApplicationType = ApplicationType.Client,
            CertificateValidator = certificateValidator,
            ApplicationUri = Utils.Format(@"urn:{0}:{1}", System.Net.Dns.GetHostName(), OPCUAName),
            ProductUri = ProductUri,

            ServerConfiguration = new ServerConfiguration
            {
                MaxSubscriptionCount = 100000,
                MaxMessageQueueSize = 1000000,
                MaxNotificationQueueSize = 1000000,
                MaxPublishRequestCount = 10000000,

            },

            SecurityConfiguration = new SecurityConfiguration
            {
                UseValidatedCertificates = true,
                AutoAcceptUntrustedCertificates = true,//自动接受证书
                RejectSHA1SignedCertificates = false,
                MinimumCertificateKeySize = 1024,
                SuppressNonceValidationErrors = true,

                ApplicationCertificate = new CertificateIdentifier
                {
                    StoreType = CertificateStoreType.X509Store,
                    StorePath = "CurrentUser\\" + OPCUAName,
                    SubjectName = $"CN={OPCUAName}, C=CN, S=GUANGZHOU, O=ThingsGateway, DC=" + System.Net.Dns.GetHostName(),
                },

                TrustedIssuerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = AppContext.BaseDirectory + @"OPCUAClientCertificate\pki\trustedIssuer",
                },
                TrustedPeerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = AppContext.BaseDirectory + @"OPCUAClientCertificate\pki\trustedPeer",
                },
                RejectedCertificateStore = new CertificateStoreIdentifier
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = AppContext.BaseDirectory + @"OPCUAClientCertificate\pki\rejected",
                },
                UserIssuerCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = AppContext.BaseDirectory + @"OPCUAClientCertificate\pki\issuerUser",
                },
                TrustedUserCertificates = new CertificateTrustList
                {
                    StoreType = CertificateStoreType.Directory,
                    StorePath = AppContext.BaseDirectory + @"OPCUAClientCertificate\pki\trustedUser",
                }


            },

            TransportQuotas = new TransportQuotas
            {
                OperationTimeout = 6000000,
                MaxStringLength = int.MaxValue,
                MaxByteStringLength = int.MaxValue,
                MaxArrayLength = 65535,
                MaxMessageSize = 419430400,
                MaxBufferSize = 65535,
                ChannelLifetime = -1,
                SecurityTokenLifetime = -1
            },
            ClientConfiguration = new ClientConfiguration
            {
                DefaultSessionTimeout = -1,
                MinSubscriptionLifetime = -1,
            },
            DisableHiResClock = true
        };

        certificateValidator.Update(m_configuration);

        m_configuration.Validate(ApplicationType.Client);
        m_application.ApplicationConfiguration = m_configuration;


    }

    /// <summary>
    /// 订阅
    /// </summary>
    public event DataChangedEventHandler DataChangedHandler;

    /// <summary>
    /// 配置信息
    /// </summary>
    public ApplicationConfiguration AppConfig => m_configuration;

    /// <summary>
    /// 连接状态
    /// </summary>
    public bool Connected => m_session?.Connected == true;

    /// <summary>
    /// OPCUAClient
    /// </summary>
    public string OPCUAName { get; set; } = "ThingsGateway";

    /// <summary>
    /// SessionReconnectHandler
    /// </summary>
    public SessionReconnectHandler ReConnectHandler => m_reConnectHandler;
    /// <summary>
    /// 当前活动会话。
    /// </summary>
    public ISession Session => m_session;

    #endregion

    #region 订阅

    /// <summary>
    /// 新增订阅，需要指定订阅组名称，订阅的tag名数组
    /// </summary>
    public async Task AddSubscriptionAsync(string subscriptionName, string[] items)
    {
        Subscription m_subscription = new(m_session.DefaultSubscription)
        {
            PublishingEnabled = true,
            PublishingInterval = 0,
            KeepAliveCount = uint.MaxValue,
            LifetimeCount = uint.MaxValue,
            MaxNotificationsPerPublish = uint.MaxValue,
            Priority = 100,
            DisplayName = subscriptionName
        };
        List<MonitoredItem> monitoredItems = new();
        for (int i = 0; i < items.Length; i++)
        {
            try
            {
                var variableNode = await ReadNodeAsync(items[i], false);
                var item = new MonitoredItem
                {
                    StartNodeId = variableNode.NodeId,
                    AttributeId = Attributes.Value,
                    DisplayName = items[i],
                    Filter = OPCNode.DeadBand == 0 ? null : new DataChangeFilter() { DeadbandValue = OPCNode.DeadBand, DeadbandType = (int)DeadbandType.Absolute, Trigger = DataChangeTrigger.StatusValue },
                    SamplingInterval = OPCNode?.UpdateRate ?? 1000,
                };
                item.Notification += Callback;
                monitoredItems.Add(item);
            }
            catch (Exception ex)
            {
                _logAction?.Invoke(3, this, $"初始化{items[i]}变量订阅失败", ex);
            }
        }
        m_subscription.AddItems(monitoredItems);

        m_session.AddSubscription(m_subscription);
        m_subscription.Create();
        foreach (var item in m_subscription.MonitoredItems.Where(a => a.Status.Error != null && StatusCode.IsBad(a.Status.Error.StatusCode)))
        {
            item.Filter = OPCNode.DeadBand == 0 ? null : new DataChangeFilter() { DeadbandValue = OPCNode.DeadBand, DeadbandType = (int)DeadbandType.None, Trigger = DataChangeTrigger.StatusValue };
        }
        m_subscription.ApplyChanges();

        var isError = m_subscription.MonitoredItems.Any(a => a.Status.Error != null && StatusCode.IsBad(a.Status.Error.StatusCode));
        if (isError)
        {
            _logAction?.Invoke(3, this, $"创建以下变量订阅失败：{Environment.NewLine}{m_subscription.MonitoredItems.Where(
                a => a.Status.Error != null && StatusCode.IsBad(a.Status.Error.StatusCode))
                .Select(a => a.StartNodeId.ToString() + "：" + a.Status.Error.ToString()).ToJsonString()}", null);
        }

        lock (dic_subscriptions)
        {
            if (dic_subscriptions.ContainsKey(subscriptionName))
            {
                // remove
                dic_subscriptions[subscriptionName].Delete(true);
                m_session.RemoveSubscription(dic_subscriptions[subscriptionName]);
                try { dic_subscriptions[subscriptionName].Dispose(); } catch { }
                dic_subscriptions[subscriptionName] = m_subscription;
            }
            else
            {
                dic_subscriptions.Add(subscriptionName, m_subscription);
            }
        }
    }

    /// <summary>
    /// 移除所有的订阅消息
    /// </summary>
    public void RemoveAllSubscription()
    {
        lock (dic_subscriptions)
        {
            foreach (var item in dic_subscriptions)
            {
                item.Value.Delete(true);
                m_session.RemoveSubscription(item.Value);
                try { item.Value.Dispose(); } catch { }

            }
            dic_subscriptions.Clear();
        }
    }

    /// <summary>
    /// 移除订阅消息
    /// </summary>
    /// <param name="subscriptionName">组名称</param>
    public void RemoveSubscription(string subscriptionName)
    {
        lock (dic_subscriptions)
        {
            if (dic_subscriptions.ContainsKey(subscriptionName))
            {
                // remove
                dic_subscriptions[subscriptionName].Delete(true);
                m_session.RemoveSubscription(dic_subscriptions[subscriptionName]);
                try { dic_subscriptions[subscriptionName].Dispose(); } catch { }
                dic_subscriptions.RemoveWhere(a => a.Key == subscriptionName);
            }
        }

    }

    private async void Callback(MonitoredItem monitoreditem, MonitoredItemNotificationEventArgs monitoredItemNotificationEventArgs)
    {
        try
        {
            var variableNode = await ReadNodeAsync(monitoreditem.StartNodeId.ToString(), false);
            foreach (var value in monitoreditem.DequeueValues())
            {
                if (value.Value != null)
                {
                    var data = JsonUtils.Encode(m_session.MessageContext, TypeInfo.GetBuiltInType(variableNode.DataType, m_session.SystemContext.TypeTable), value.Value);
                    if (data == null && value.Value != null)
                    {
                        _logAction?.Invoke(3, this, $"{monitoreditem.StartNodeId}转换出错，原始值String为{value.Value}", null);
                        var data1 = JsonUtils.Encode(m_session.MessageContext, TypeInfo.GetBuiltInType(variableNode.DataType, m_session.SystemContext.TypeTable), value.Value);
                    }
                    DataChangedHandler?.Invoke((variableNode, value, data));
                }
                else
                {
                    var data = JValue.CreateNull();
                    DataChangedHandler?.Invoke((variableNode, value, data));
                }

            }
        }
        catch (Exception ex)
        {
            _logAction?.Invoke(3, this, $"{monitoreditem.StartNodeId}订阅处理错误", ex);
        }

    }

    #endregion

    #region 其他方法

    /// <summary>
    /// 浏览一个节点的引用
    /// </summary>
    /// <param name="tag">节点值</param>
    /// <returns>引用节点描述</returns>
    public async Task<ReferenceDescription[]> BrowseNodeReferenceAsync(string tag)
    {
        NodeId sourceId = new(tag);

        // 该节点可以读取到方法
        BrowseDescription nodeToBrowse1 = new()
        {
            NodeId = sourceId,
            BrowseDirection = BrowseDirection.Forward,
            ReferenceTypeId = ReferenceTypeIds.Aggregates,
            IncludeSubtypes = true,
            NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method),
            ResultMask = (uint)BrowseResultMask.All
        };

        // find all nodes organized by the node.
        BrowseDescription nodeToBrowse2 = new()
        {
            NodeId = sourceId,
            BrowseDirection = BrowseDirection.Forward,
            ReferenceTypeId = ReferenceTypeIds.Organizes,
            IncludeSubtypes = true,
            NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable),
            ResultMask = (uint)BrowseResultMask.All
        };

        BrowseDescriptionCollection nodesToBrowse = new()
        {
            nodeToBrowse1,
            nodeToBrowse2
        };

        // fetch references from the server.
        ReferenceDescriptionCollection references = await FormUtils.BrowseAsync(m_session, nodesToBrowse, false);

        return references.ToArray();
    }

    /// <summary>
    /// 调用服务器的方法
    /// </summary>
    /// <param name="tagParent">方法的父节点tag</param>
    /// <param name="tag">方法的节点tag</param>
    /// <param name="args">传递的参数</param>
    /// <returns>输出的结果值</returns>
    public object[] CallMethodByNodeId(string tagParent, string tag, params object[] args)
    {
        if (m_session == null)
        {
            return null;
        }

        IList<object> outputArguments = m_session.Call(
            new NodeId(tagParent),
            new NodeId(tag),
            args);

        return outputArguments.ToArray();
    }

    /// <summary>
    /// 读取历史数据
    /// </summary>
    /// <param name="tag">节点的索引</param>
    /// <param name="start">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="count">读取的个数</param>
    /// <param name="containBound">是否包含边界</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns>读取的数据列表</returns>
    public async Task<List<DataValue>> ReadHistoryRawDataValues(string tag, DateTime start, DateTime end, uint count = 1, bool containBound = false, CancellationToken cancellationToken = default)
    {
        HistoryReadValueId m_nodeToContinue = new()
        {
            NodeId = new NodeId(tag),
        };

        ReadRawModifiedDetails m_details = new()
        {
            StartTime = start,
            EndTime = end,
            NumValuesPerNode = count,
            IsReadModified = false,
            ReturnBounds = containBound
        };

        HistoryReadValueIdCollection nodesToRead = new()
        {
            m_nodeToContinue
        };

        var result = await m_session.HistoryReadAsync(
             null,
             new ExtensionObject(m_details),
             TimestampsToReturn.Both,
             false,
             nodesToRead,
             cancellationToken);
        var results = result.Results;
        var diagnosticInfos = result.DiagnosticInfos;
        ClientBase.ValidateResponse(results, nodesToRead);
        ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);

        if (StatusCode.IsBad(results[0].StatusCode))
        {
            throw new ServiceResultException(results[0].StatusCode);
        }

        HistoryData values = ExtensionObject.ToEncodeable(results[0].HistoryData) as HistoryData;
        return values.DataValues;
    }

    #endregion


    #region 连接
    private ComplexTypeSystem typeSystem;

    /// <summary>
    /// 连接到服务器
    /// </summary>
    public async Task ConnectAsync()
    {
        await ConnectAsync(OPCNode.OPCUrl);
        _logAction?.Invoke(1, this, $"连接成功", null);
    }

    /// <summary>
    /// 断开连接。
    /// </summary>
    public void Disconnect()
    {
        PrivateDisconnect();
        // disconnect any existing session.
        if (m_session != null)
        {
            _logAction?.Invoke(1, this, $"主动断开连接", null);
            m_session = null;
        }
    }
    /// <summary>
    /// Creates a new session.
    /// </summary>
    /// <returns>The new session object.</returns>
    private async Task<ISession> ConnectAsync(string serverUrl)
    {
        PrivateDisconnect();

        if (m_configuration == null)
        {
            throw new ArgumentNullException("未初始化配置");
        }
        var useSecurity = OPCNode?.IsUseSecurity ?? true;
        EndpointDescription endpointDescription = CoreClientUtils.SelectEndpoint(m_configuration, serverUrl, useSecurity, 10000);
        EndpointConfiguration endpointConfiguration = EndpointConfiguration.Create(m_configuration);
        ConfiguredEndpoint endpoint = new(null, endpointDescription, endpointConfiguration);
        UserIdentity userIdentity;
        if (!string.IsNullOrEmpty(OPCNode.UserName))
        {
            userIdentity = new UserIdentity(OPCNode.UserName, OPCNode.Password);
        }
        else
        {
            userIdentity = new UserIdentity(new AnonymousIdentityToken());
        }
        //创建本地证书
        await m_application.CheckApplicationInstanceCertificate(true, 0, 1200);
        m_session = await Opc.Ua.Client.Session.Create(
     m_configuration,
    endpoint,
    false,
    OPCNode.CheckDomain,
    (string.IsNullOrEmpty(OPCUAName)) ? m_configuration.ApplicationName : OPCUAName,
    60000,
    userIdentity,
    Array.Empty<string>());

        typeSystem = new ComplexTypeSystem(m_session);

        m_session.KeepAliveInterval = OPCNode.KeepAliveInterval == 0 ? 60000 : OPCNode.KeepAliveInterval;
        m_session.KeepAlive += new KeepAliveEventHandler(Session_KeepAlive);

        //如果是订阅模式，连接时添加订阅组
        if (OPCNode.ActiveSubscribe)
            await AddSubscriptionAsync(Guid.NewGuid().ToString(), Variables.ToArray());
        return m_session;
    }

    private void PrivateDisconnect()
    {
        if (m_reConnectHandler != null)
        {
            try { m_reConnectHandler.Dispose(); } catch { }
            m_reConnectHandler = null;
        }
        if (m_session != null)
        {
            m_session.KeepAlive -= Session_KeepAlive;
            m_session.Close(10000);
        }


    }
    #endregion

    #region 读取/写入

    /// <summary>
    /// 从服务器读取值
    /// </summary>
    public async Task<List<(string, DataValue, JToken)>> ReadJTokenValueAsync(string[] tags, CancellationToken cancellationToken = default)
    {
        var result = await ReadJTokenValueAsync(tags.Select(a => new NodeId(a)).ToArray(), cancellationToken);
        return result;
    }

    /// <summary>
    /// 异步写opc标签
    /// </summary>
    public async Task<Dictionary<string, Tuple<bool, string>>> WriteNodeAsync(Dictionary<string, JToken> writeInfoLists, CancellationToken cancellationToken = default)
    {
        Dictionary<string, Tuple<bool, string>> results = new();
        try
        {
            WriteValueCollection valuesToWrite = new();
            foreach (var item in writeInfoLists)
            {
                WriteValue valueToWrite = new()
                {
                    NodeId = new NodeId(item.Key),
                    AttributeId = Attributes.Value,
                };
                var variableNode = await ReadNodeAsync(item.Key, false, cancellationToken);
                var dataValue = JsonUtils.Decode(
                    m_session.MessageContext,
                    variableNode.DataType,
                    TypeInfo.GetBuiltInType(variableNode.DataType, m_session.SystemContext.TypeTable),
                    item.Value.CalculateActualValueRank(),
                    item.Value
                    );
                valueToWrite.Value = dataValue;

                valuesToWrite.Add(valueToWrite);
            }


            var result = await m_session.WriteAsync(
     requestHeader: null,
     nodesToWrite: valuesToWrite, cancellationToken);

            ClientBase.ValidateResponse(result.Results, valuesToWrite);
            ClientBase.ValidateDiagnosticInfos(result.DiagnosticInfos, valuesToWrite);


            var keys = writeInfoLists.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (!StatusCode.IsGood(result.Results[i]))
                    results.Add(keys[i], Tuple.Create(true, result.Results[i].ToString()));
                else
                {
                    results.Add(keys[i], Tuple.Create(false, "成功"));
                }
            }

            return results;
        }
        catch (Exception ex)
        {
            var keys = writeInfoLists.Keys.ToList();
            foreach (var item in keys)
            {
                results.Add(item, Tuple.Create(true, ex.Message));
            }
            return results;
        }

    }

    /// <summary>
    /// 从服务器读取值
    /// </summary>
    private async Task<List<(string, DataValue, JToken)>> ReadJTokenValueAsync(NodeId[] nodeIds, CancellationToken cancellationToken = default)
    {
        if (m_session == null)
        {
            throw new("服务器未初始化连接");
        }
        ReadValueIdCollection nodesToRead = new();
        for (int i = 0; i < nodeIds.Length; i++)
        {
            nodesToRead.Add(new ReadValueId()
            {
                NodeId = nodeIds[i],
                AttributeId = Attributes.Value
            });
        }

        // 读取当前的值
        var result = await m_session.ReadAsync(
             null,
             0,
             TimestampsToReturn.Neither,
             nodesToRead,
             cancellationToken);
        var results = result.Results;
        var diagnosticInfos = result.DiagnosticInfos;
        ClientBase.ValidateResponse(results, nodesToRead);
        ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
        List<(string, DataValue, JToken)> jTokens = new();
        for (int i = 0; i < results.Count; i++)
        {
            var variableNode = await ReadNodeAsync(nodeIds[i].ToString(), false, cancellationToken);
            var type = TypeInfo.GetBuiltInType(variableNode.DataType, m_session.SystemContext.TypeTable);
            var jToken = JsonUtils.Encode(m_session.MessageContext, type, results[i].Value);
            jTokens.Add((variableNode.NodeId.ToString(), results[i], jToken));
        }
        return jTokens.ToList();
    }

    /// <summary>
    /// 从服务器或缓存读取节点
    /// </summary>
    private async Task<VariableNode> ReadNodeAsync(string nodeIdStr, bool isOnlyServer = true, CancellationToken cancellationToken = default)
    {
        if (!isOnlyServer)
        {
            if (_variableDicts.TryGetValue(nodeIdStr, out var value))
            {
                return value;
            }
        }

        NodeId nodeToRead = new(nodeIdStr);
        var node = (VariableNode)await ReadNodeAsync(nodeToRead, NodeClass.Unspecified, false, cancellationToken);
        await typeSystem.LoadType(node.DataType, true, false);
        _variableDicts.AddOrUpdate(nodeIdStr, node);
        return node;
    }




    #endregion
    #region session

    /// <inheritdoc/>
    public Task<Node> ReadNodeAsync(
        NodeId nodeId,
        CancellationToken ct = default)
    {
        return ReadNodeAsync(nodeId, NodeClass.Unspecified, true, ct);
    }

    /// <inheritdoc/>
    public async Task<Node> ReadNodeAsync(
        NodeId nodeId,
        NodeClass nodeClass,
        bool optionalAttributes = true,
        CancellationToken ct = default)
    {
        // build list of attributes.
        var attributes = CreateAttributes(nodeClass, optionalAttributes);

        // build list of values to read.
        ReadValueIdCollection itemsToRead = new ReadValueIdCollection();
        foreach (uint attributeId in attributes.Keys)
        {
            ReadValueId itemToRead = new ReadValueId
            {
                NodeId = nodeId,
                AttributeId = attributeId
            };
            itemsToRead.Add(itemToRead);
        }

        // read from server.
        ReadResponse readResponse = await m_session.ReadAsync(
            null,
            0,
            TimestampsToReturn.Neither,
            itemsToRead, ct).ConfigureAwait(false);

        DataValueCollection values = readResponse.Results;
        DiagnosticInfoCollection diagnosticInfos = readResponse.DiagnosticInfos;

        ClientBase.ValidateResponse(values, itemsToRead);
        ClientBase.ValidateDiagnosticInfos(diagnosticInfos, itemsToRead);

        return ProcessReadResponse(readResponse.ResponseHeader, attributes, itemsToRead, values, diagnosticInfos);
    }

    /// <summary>
    /// Creates a Node based on the read response.
    /// </summary>
    private Node ProcessReadResponse(
        ResponseHeader responseHeader,
        IDictionary<uint, DataValue> attributes,
        ReadValueIdCollection itemsToRead,
        DataValueCollection values,
        DiagnosticInfoCollection diagnosticInfos)
    {
        // process results.
        int? nodeClass = null;

        for (int ii = 0; ii < itemsToRead.Count; ii++)
        {
            uint attributeId = itemsToRead[ii].AttributeId;

            // the node probably does not exist if the node class is not found.
            if (attributeId == Attributes.NodeClass)
            {
                if (!DataValue.IsGood(values[ii]))
                {
                    throw ServiceResultException.Create(values[ii].StatusCode, ii, diagnosticInfos, responseHeader.StringTable);
                }

                // check for valid node class.
                nodeClass = values[ii].Value as int?;

                if (nodeClass == null)
                {
                    throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Node does not have a valid value for NodeClass: {0}.", values[ii].Value);
                }
            }
            else
            {
                if (!DataValue.IsGood(values[ii]))
                {
                    // check for unsupported attributes.
                    if (values[ii].StatusCode == StatusCodes.BadAttributeIdInvalid)
                    {
                        continue;
                    }

                    // ignore errors on optional attributes
                    if (StatusCode.IsBad(values[ii].StatusCode))
                    {
                        if (attributeId == Attributes.AccessRestrictions ||
                            attributeId == Attributes.Description ||
                            attributeId == Attributes.RolePermissions ||
                            attributeId == Attributes.UserRolePermissions ||
                            attributeId == Attributes.UserWriteMask ||
                            attributeId == Attributes.WriteMask)
                        {
                            continue;
                        }
                    }

                    // all supported attributes must be readable.
                    if (attributeId != Attributes.Value)
                    {
                        throw ServiceResultException.Create(values[ii].StatusCode, ii, diagnosticInfos, responseHeader.StringTable);
                    }
                }
            }

            attributes[attributeId] = values[ii];
        }

        Node node;
        DataValue value;
        switch ((NodeClass)nodeClass.Value)
        {
            default:
                {
                    throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Node does not have a valid value for NodeClass: {0}.", nodeClass.Value);
                }

            case NodeClass.Object:
                {
                    ObjectNode objectNode = new ObjectNode();

                    value = attributes[Attributes.EventNotifier];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Object does not support the EventNotifier attribute.");
                    }

                    objectNode.EventNotifier = (byte)value.GetValue(typeof(byte));
                    node = objectNode;
                    break;
                }

            case NodeClass.ObjectType:
                {
                    ObjectTypeNode objectTypeNode = new ObjectTypeNode();

                    value = attributes[Attributes.IsAbstract];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "ObjectType does not support the IsAbstract attribute.");
                    }

                    objectTypeNode.IsAbstract = (bool)value.GetValue(typeof(bool));
                    node = objectTypeNode;
                    break;
                }

            case NodeClass.Variable:
                {
                    VariableNode variableNode = new VariableNode();

                    // DataType Attribute
                    value = attributes[Attributes.DataType];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Variable does not support the DataType attribute.");
                    }

                    variableNode.DataType = (NodeId)value.GetValue(typeof(NodeId));

                    // ValueRank Attribute
                    value = attributes[Attributes.ValueRank];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Variable does not support the ValueRank attribute.");
                    }

                    variableNode.ValueRank = (int)value.GetValue(typeof(int));

                    // ArrayDimensions Attribute
                    value = attributes[Attributes.ArrayDimensions];

                    if (value != null)
                    {
                        if (value.Value == null)
                        {
                            variableNode.ArrayDimensions = Array.Empty<uint>();
                        }
                        else
                        {
                            variableNode.ArrayDimensions = (uint[])value.GetValue(typeof(uint[]));
                        }
                    }

                    // AccessLevel Attribute
                    value = attributes[Attributes.AccessLevel];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Variable does not support the AccessLevel attribute.");
                    }

                    variableNode.AccessLevel = (byte)value.GetValue(typeof(byte));

                    // UserAccessLevel Attribute
                    value = attributes[Attributes.UserAccessLevel];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Variable does not support the UserAccessLevel attribute.");
                    }

                    variableNode.UserAccessLevel = (byte)value.GetValue(typeof(byte));

                    // Historizing Attribute
                    value = attributes[Attributes.Historizing];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Variable does not support the Historizing attribute.");
                    }

                    variableNode.Historizing = (bool)value.GetValue(typeof(bool));

                    // MinimumSamplingInterval Attribute
                    value = attributes[Attributes.MinimumSamplingInterval];

                    if (value != null)
                    {
                        variableNode.MinimumSamplingInterval = Convert.ToDouble(attributes[Attributes.MinimumSamplingInterval].Value);
                    }

                    // AccessLevelEx Attribute
                    value = attributes[Attributes.AccessLevelEx];

                    if (value != null)
                    {
                        variableNode.AccessLevelEx = (uint)value.GetValue(typeof(uint));
                    }

                    node = variableNode;
                    break;
                }

            case NodeClass.VariableType:
                {
                    VariableTypeNode variableTypeNode = new VariableTypeNode();

                    // IsAbstract Attribute
                    value = attributes[Attributes.IsAbstract];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "VariableType does not support the IsAbstract attribute.");
                    }

                    variableTypeNode.IsAbstract = (bool)value.GetValue(typeof(bool));

                    // DataType Attribute
                    value = attributes[Attributes.DataType];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "VariableType does not support the DataType attribute.");
                    }

                    variableTypeNode.DataType = (NodeId)value.GetValue(typeof(NodeId));

                    // ValueRank Attribute
                    value = attributes[Attributes.ValueRank];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "VariableType does not support the ValueRank attribute.");
                    }

                    variableTypeNode.ValueRank = (int)value.GetValue(typeof(int));

                    // ArrayDimensions Attribute
                    value = attributes[Attributes.ArrayDimensions];

                    if (value != null && value.Value != null)
                    {
                        variableTypeNode.ArrayDimensions = (uint[])value.GetValue(typeof(uint[]));
                    }

                    node = variableTypeNode;
                    break;
                }

            case NodeClass.Method:
                {
                    MethodNode methodNode = new MethodNode();

                    // Executable Attribute
                    value = attributes[Attributes.Executable];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Method does not support the Executable attribute.");
                    }

                    methodNode.Executable = (bool)value.GetValue(typeof(bool));

                    // UserExecutable Attribute
                    value = attributes[Attributes.UserExecutable];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Method does not support the UserExecutable attribute.");
                    }

                    methodNode.UserExecutable = (bool)value.GetValue(typeof(bool));

                    node = methodNode;
                    break;
                }

            case NodeClass.DataType:
                {
                    DataTypeNode dataTypeNode = new DataTypeNode();

                    // IsAbstract Attribute
                    value = attributes[Attributes.IsAbstract];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "DataType does not support the IsAbstract attribute.");
                    }

                    dataTypeNode.IsAbstract = (bool)value.GetValue(typeof(bool));

                    // DataTypeDefinition Attribute
                    value = attributes[Attributes.DataTypeDefinition];

                    if (value != null)
                    {
                        dataTypeNode.DataTypeDefinition = value.Value as ExtensionObject;
                    }

                    node = dataTypeNode;
                    break;
                }

            case NodeClass.ReferenceType:
                {
                    ReferenceTypeNode referenceTypeNode = new ReferenceTypeNode();

                    // IsAbstract Attribute
                    value = attributes[Attributes.IsAbstract];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "ReferenceType does not support the IsAbstract attribute.");
                    }

                    referenceTypeNode.IsAbstract = (bool)value.GetValue(typeof(bool));

                    // Symmetric Attribute
                    value = attributes[Attributes.Symmetric];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "ReferenceType does not support the Symmetric attribute.");
                    }

                    referenceTypeNode.Symmetric = (bool)value.GetValue(typeof(bool));

                    // InverseName Attribute
                    value = attributes[Attributes.InverseName];

                    if (value != null && value.Value != null)
                    {
                        referenceTypeNode.InverseName = (LocalizedText)value.GetValue(typeof(LocalizedText));
                    }

                    node = referenceTypeNode;
                    break;
                }

            case NodeClass.View:
                {
                    ViewNode viewNode = new ViewNode();

                    // EventNotifier Attribute
                    value = attributes[Attributes.EventNotifier];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "View does not support the EventNotifier attribute.");
                    }

                    viewNode.EventNotifier = (byte)value.GetValue(typeof(byte));

                    // ContainsNoLoops Attribute
                    value = attributes[Attributes.ContainsNoLoops];

                    if (value == null)
                    {
                        throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "View does not support the ContainsNoLoops attribute.");
                    }

                    viewNode.ContainsNoLoops = (bool)value.GetValue(typeof(bool));

                    node = viewNode;
                    break;
                }
        }

        // NodeId Attribute
        value = attributes[Attributes.NodeId];

        if (value == null)
        {
            throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Node does not support the NodeId attribute.");
        }

        node.NodeId = (NodeId)value.GetValue(typeof(NodeId));
        node.NodeClass = (NodeClass)nodeClass.Value;

        // BrowseName Attribute
        value = attributes[Attributes.BrowseName];

        if (value == null)
        {
            throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Node does not support the BrowseName attribute.");
        }

        node.BrowseName = (QualifiedName)value.GetValue(typeof(QualifiedName));

        // DisplayName Attribute
        value = attributes[Attributes.DisplayName];

        if (value == null)
        {
            throw ServiceResultException.Create(StatusCodes.BadUnexpectedError, "Node does not support the DisplayName attribute.");
        }

        node.DisplayName = (LocalizedText)value.GetValue(typeof(LocalizedText));

        // all optional attributes follow

        // Description Attribute
        if (attributes.TryGetValue(Attributes.Description, out value) &&
            value != null && value.Value != null)
        {
            node.Description = (LocalizedText)value.GetValue(typeof(LocalizedText));
        }

        // WriteMask Attribute
        if (attributes.TryGetValue(Attributes.WriteMask, out value) &&
            value != null)
        {
            node.WriteMask = (uint)value.GetValue(typeof(uint));
        }

        // UserWriteMask Attribute
        if (attributes.TryGetValue(Attributes.UserWriteMask, out value) &&
            value != null)
        {
            node.UserWriteMask = (uint)value.GetValue(typeof(uint));
        }

        // RolePermissions Attribute
        if (attributes.TryGetValue(Attributes.RolePermissions, out value) &&
            value != null)
        {
            ExtensionObject[] rolePermissions = value.Value as ExtensionObject[];

            if (rolePermissions != null)
            {
                node.RolePermissions = new RolePermissionTypeCollection();

                foreach (ExtensionObject rolePermission in rolePermissions)
                {
                    node.RolePermissions.Add(rolePermission.Body as RolePermissionType);
                }
            }
        }

        // UserRolePermissions Attribute
        if (attributes.TryGetValue(Attributes.UserRolePermissions, out value) &&
            value != null)
        {
            ExtensionObject[] userRolePermissions = value.Value as ExtensionObject[];

            if (userRolePermissions != null)
            {
                node.UserRolePermissions = new RolePermissionTypeCollection();

                foreach (ExtensionObject rolePermission in userRolePermissions)
                {
                    node.UserRolePermissions.Add(rolePermission.Body as RolePermissionType);
                }
            }
        }

        // AccessRestrictions Attribute
        if (attributes.TryGetValue(Attributes.AccessRestrictions, out value) &&
            value != null)
        {
            node.AccessRestrictions = (ushort)value.GetValue(typeof(ushort));
        }

        return node;
    }


    /// <summary>
    /// Create a dictionary of attributes to read for a nodeclass.
    /// </summary>
    private IDictionary<uint, DataValue> CreateAttributes(NodeClass nodeclass = NodeClass.Unspecified, bool optionalAttributes = true)
    {
        // Attributes to read for all types of nodes
        var attributes = new SortedDictionary<uint, DataValue>() {
                { Attributes.NodeId, null },
                { Attributes.NodeClass, null },
                { Attributes.BrowseName, null },
                { Attributes.DisplayName, null },
            };

        switch (nodeclass)
        {
            case NodeClass.Object:
                attributes.Add(Attributes.EventNotifier, null);
                break;

            case NodeClass.Variable:
                attributes.Add(Attributes.DataType, null);
                attributes.Add(Attributes.ValueRank, null);
                attributes.Add(Attributes.ArrayDimensions, null);
                attributes.Add(Attributes.AccessLevel, null);
                attributes.Add(Attributes.UserAccessLevel, null);
                attributes.Add(Attributes.Historizing, null);
                attributes.Add(Attributes.MinimumSamplingInterval, null);
                attributes.Add(Attributes.AccessLevelEx, null);
                break;

            case NodeClass.Method:
                attributes.Add(Attributes.Executable, null);
                attributes.Add(Attributes.UserExecutable, null);
                break;

            case NodeClass.ObjectType:
                attributes.Add(Attributes.IsAbstract, null);
                break;

            case NodeClass.VariableType:
                attributes.Add(Attributes.IsAbstract, null);
                attributes.Add(Attributes.DataType, null);
                attributes.Add(Attributes.ValueRank, null);
                attributes.Add(Attributes.ArrayDimensions, null);
                break;

            case NodeClass.ReferenceType:
                attributes.Add(Attributes.IsAbstract, null);
                attributes.Add(Attributes.Symmetric, null);
                attributes.Add(Attributes.InverseName, null);
                break;

            case NodeClass.DataType:
                attributes.Add(Attributes.IsAbstract, null);
                attributes.Add(Attributes.DataTypeDefinition, null);
                break;

            case NodeClass.View:
                attributes.Add(Attributes.EventNotifier, null);
                attributes.Add(Attributes.ContainsNoLoops, null);
                break;

            default:
                // build complete list of attributes.
                attributes = new SortedDictionary<uint, DataValue> {
                        { Attributes.NodeId, null },
                        { Attributes.NodeClass, null },
                        { Attributes.BrowseName, null },
                        { Attributes.DisplayName, null },
                        //{ Attributes.Description, null },
                        //{ Attributes.WriteMask, null },
                        //{ Attributes.UserWriteMask, null },
                        { Attributes.DataType, null },
                        { Attributes.ValueRank, null },
                        { Attributes.ArrayDimensions, null },
                        { Attributes.AccessLevel, null },
                        { Attributes.UserAccessLevel, null },
                        { Attributes.MinimumSamplingInterval, null },
                        { Attributes.Historizing, null },
                        { Attributes.EventNotifier, null },
                        { Attributes.Executable, null },
                        { Attributes.UserExecutable, null },
                        { Attributes.IsAbstract, null },
                        { Attributes.InverseName, null },
                        { Attributes.Symmetric, null },
                        { Attributes.ContainsNoLoops, null },
                        { Attributes.DataTypeDefinition, null },
                        //{ Attributes.RolePermissions, null },
                        //{ Attributes.UserRolePermissions, null },
                        //{ Attributes.AccessRestrictions, null },
                        { Attributes.AccessLevelEx, null }
                    };
                break;
        }

        if (optionalAttributes)
        {
            attributes.Add(Attributes.Description, null);
            attributes.Add(Attributes.WriteMask, null);
            attributes.Add(Attributes.UserWriteMask, null);
            attributes.Add(Attributes.RolePermissions, null);
            attributes.Add(Attributes.UserRolePermissions, null);
            attributes.Add(Attributes.AccessRestrictions, null);
        }

        return attributes;
    }

    #endregion

    #region 特性

    /// <summary>
    /// 读取一个节点的所有属性
    /// </summary>
    public async Task<List<OPCNodeAttribute>> ReadNoteAttributeAsync(string tag, uint attributesId, CancellationToken cancellationToken = default)
    {
        BrowseDescriptionCollection nodesToBrowse = new();
        ReadValueIdCollection nodesToRead = new();
        NodeId sourceId = new(tag);

        ReadValueId nodeToRead = new()
        {
            NodeId = sourceId,
            AttributeId = attributesId
        };
        nodesToRead.Add(nodeToRead);
        BrowseDescription nodeToBrowse = new()
        {
            NodeId = sourceId,
            BrowseDirection = BrowseDirection.Forward,
            ReferenceTypeId = ReferenceTypeIds.HasProperty,
            IncludeSubtypes = true,
            NodeClassMask = 0,
            ResultMask = (uint)BrowseResultMask.All
        };
        nodesToBrowse.Add(nodeToBrowse);

        var result1 = await ReadNoteAttributeAsync(nodesToBrowse, nodesToRead, cancellationToken);

        var result2 = result1.Values.FirstOrDefault();
        return result2;
    }

    /// <summary>
    /// 读取节点的所有属性
    /// </summary>
    public async Task<Dictionary<string, List<OPCNodeAttribute>>> ReadNoteAttributeAsync(List<string> tags, CancellationToken cancellationToken)
    {
        BrowseDescriptionCollection nodesToBrowse = new();
        ReadValueIdCollection nodesToRead = new();
        foreach (var tag in tags)
        {
            NodeId sourceId = new(tag);

            for (uint ii = Attributes.NodeClass; ii <= Attributes.UserExecutable; ii++)
            {
                ReadValueId nodeToRead = new()
                {
                    NodeId = sourceId,
                    AttributeId = ii
                };
                nodesToRead.Add(nodeToRead);
            }
            BrowseDescription nodeToBrowse = new()
            {
                NodeId = sourceId,
                BrowseDirection = BrowseDirection.Forward,
                ReferenceTypeId = ReferenceTypeIds.HasProperty,
                IncludeSubtypes = true,
                NodeClassMask = 0,
                ResultMask = (uint)BrowseResultMask.All
            };
            nodesToBrowse.Add(nodeToBrowse);

        }

        return await ReadNoteAttributeAsync(nodesToBrowse, nodesToRead, cancellationToken);
    }

    /// <summary>
    /// 读取一个节点的所有属性
    /// </summary>
    /// <param name="tag">节点信息</param>
    /// <returns>节点的特性值</returns>
    public OPCNodeAttribute[] ReadNoteAttributes(string tag)
    {
        NodeId sourceId = new(tag);
        ReadValueIdCollection nodesToRead = new();

        for (uint ii = Attributes.NodeClass; ii <= Attributes.UserExecutable; ii++)
        {
            ReadValueId nodeToRead = new()
            {
                NodeId = sourceId,
                AttributeId = ii
            };
            nodesToRead.Add(nodeToRead);
        }

        int startOfProperties = nodesToRead.Count;

        // find all of the pror of the node.
        BrowseDescription nodeToBrowse1 = new()
        {
            NodeId = sourceId,
            BrowseDirection = BrowseDirection.Forward,
            ReferenceTypeId = ReferenceTypeIds.HasProperty,
            IncludeSubtypes = true,
            NodeClassMask = 0,
            ResultMask = (uint)BrowseResultMask.All
        };

        BrowseDescriptionCollection nodesToBrowse = new()
        {
            nodeToBrowse1
        };

        // fetch property references from the server.
        ReferenceDescriptionCollection references = FormUtils.Browse(m_session, nodesToBrowse, false);

        if (references == null)
        {
            return Array.Empty<OPCNodeAttribute>();
        }

        for (int ii = 0; ii < references.Count; ii++)
        {
            // ignore external references.
            if (references[ii].NodeId.IsAbsolute)
            {
                continue;
            }

            ReadValueId nodeToRead = new()
            {
                NodeId = (NodeId)references[ii].NodeId,
                AttributeId = Attributes.Value
            };
            nodesToRead.Add(nodeToRead);
        }

        // read all values.

        m_session.Read(
            null,
            0,
            TimestampsToReturn.Neither,
            nodesToRead,
            out DataValueCollection results,
            out DiagnosticInfoCollection diagnosticInfos);

        ClientBase.ValidateResponse(results, nodesToRead);
        ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);

        // process results.

        List<OPCNodeAttribute> nodeAttribute = new();
        for (int ii = 0; ii < results.Count; ii++)
        {
            OPCNodeAttribute item = new();

            // process attribute value.
            if (ii < startOfProperties)
            {
                // ignore attributes which are invalid for the node.
                if (results[ii].StatusCode == StatusCodes.BadAttributeIdInvalid)
                {
                    continue;
                }

                // get the name of the attribute.
                item.Name = Attributes.GetBrowseName(nodesToRead[ii].AttributeId);

                // display any unexpected error.
                if (StatusCode.IsBad(results[ii].StatusCode))
                {
                    item.Type = Utils.Format("{0}", Attributes.GetDataTypeId(nodesToRead[ii].AttributeId));
                    item.Value = Utils.Format("{0}", results[ii].StatusCode);
                }

                // display the value.
                else
                {
                    TypeInfo typeInfo = TypeInfo.Construct(results[ii].Value);

                    item.Type = typeInfo.BuiltInType.ToString();

                    if (typeInfo.ValueRank >= ValueRanks.OneOrMoreDimensions)
                    {
                        item.Type += "[]";
                    }

                    item.Value = results[ii].Value;//Utils.Format("{0}", results[ii].Value);
                }
            }

            // process property value.
            else
            {
                // ignore properties which are invalid for the node.
                if (results[ii].StatusCode == StatusCodes.BadNodeIdUnknown)
                {
                    continue;
                }

                // get the name of the property.
                item.Name = Utils.Format("{0}", references[ii - startOfProperties]);

                // display any unexpected error.
                if (StatusCode.IsBad(results[ii].StatusCode))
                {
                    item.Type = String.Empty;
                    item.Value = Utils.Format("{0}", results[ii].StatusCode);
                }

                // display the value.
                else
                {
                    TypeInfo typeInfo = TypeInfo.Construct(results[ii].Value);

                    item.Type = typeInfo.BuiltInType.ToString();

                    if (typeInfo.ValueRank >= ValueRanks.OneOrMoreDimensions)
                    {
                        item.Type += "[]";
                    }

                    item.Value = results[ii].Value;
                }
            }

            nodeAttribute.Add(item);
        }

        return nodeAttribute.ToArray();
    }
    #endregion




    /// <inheritdoc/>
    public void Dispose()
    {
        Disconnect();
    }

    #region 私有方法

    private void CertificateValidation(CertificateValidator sender, CertificateValidationEventArgs eventArgs)
    {
        if (ServiceResult.IsGood(eventArgs.Error))
            eventArgs.Accept = true;
        else if (eventArgs.Error.StatusCode.Code == StatusCodes.BadCertificateUntrusted)
            eventArgs.Accept = true;
        else
            throw new Exception(string.Format("验证证书失败，错误代码:{0}: {1}", eventArgs.Error.Code, eventArgs.Error.AdditionalInfo));
    }


    private async Task<Dictionary<string, List<OPCNodeAttribute>>> ReadNoteAttributeAsync(BrowseDescriptionCollection nodesToBrowse, ReadValueIdCollection nodesToRead, CancellationToken cancellationToken)
    {
        int startOfProperties = nodesToRead.Count;

        ReferenceDescriptionCollection references = await FormUtils.BrowseAsync(m_session, nodesToBrowse, false, cancellationToken);

        if (references == null)
        {
            throw new("浏览失败");
        }

        for (int ii = 0; ii < references.Count; ii++)
        {
            if (references[ii].NodeId.IsAbsolute)
            {
                continue;
            }

            ReadValueId nodeToRead = new()
            {
                NodeId = (NodeId)references[ii].NodeId,
                AttributeId = Attributes.Value
            };
            nodesToRead.Add(nodeToRead);
        }

        var result = await m_session.ReadAsync(
            null,
            0,
            TimestampsToReturn.Neither,
            nodesToRead, cancellationToken);

        ClientBase.ValidateResponse(result.Results, nodesToRead);
        ClientBase.ValidateDiagnosticInfos(result.DiagnosticInfos, nodesToRead);

        Dictionary<string, List<OPCNodeAttribute>> nodeAttributes = new();
        for (int ii = 0; ii < result.Results.Count; ii++)
        {
            DataValue nodeValue = result.Results[ii];
            var nodeToRead = nodesToRead[ii];
            OPCNodeAttribute item = new();
            if (ii < startOfProperties)
            {
                if (nodeValue.StatusCode == StatusCodes.BadAttributeIdInvalid)
                {
                    continue;
                }

                item.Name = Attributes.GetBrowseName(nodesToRead[ii].AttributeId);
                if (StatusCode.IsBad(nodeValue.StatusCode))
                {
                    item.Type = Utils.Format("{0}", Attributes.GetDataTypeId(nodesToRead[ii].AttributeId));
                    item.Value = Utils.Format("{0}", nodeValue.StatusCode);
                }
                else
                {
                    TypeInfo typeInfo = TypeInfo.Construct(nodeValue.Value);
                    item.Type = typeInfo.BuiltInType.ToString();

                    if (typeInfo.ValueRank >= ValueRanks.OneOrMoreDimensions)
                    {
                        item.Type += "[]";
                    }
                    if (item.Name == nameof(Attributes.NodeClass))
                    {
                        item.Value = ((NodeClass)nodeValue.Value).ToString();
                    }
                    else if (item.Name == nameof(Attributes.EventNotifier))
                    {
                        item.Value = ((EventNotifierType)nodeValue.Value).ToString();
                    }
                    else
                        item.Value = nodeValue.Value;
                }
            }


            if (nodeAttributes.ContainsKey(nodeToRead.NodeId.ToString()))
            {
                nodeAttributes[nodeToRead.NodeId.ToString()].Add(item);
            }
            else
            {
                nodeAttributes.Add(nodeToRead.NodeId.ToString(), new() { item });
            }
        }
        return nodeAttributes;
    }

    /// <summary>
    /// 连接处理器连接事件处理完成。
    /// </summary>
    private void Server_ReconnectComplete(object sender, EventArgs e)
    {
        if (!Object.ReferenceEquals(sender, m_reConnectHandler))
        {
            return;
        }

        m_session = m_reConnectHandler.Session;
        m_reConnectHandler = null;

    }

    private void Session_KeepAlive(ISession session, KeepAliveEventArgs e)
    {
        lock (checkLock)
        {

            if (!Object.ReferenceEquals(session, m_session))
            {
                return;
            }

            if (ServiceResult.IsBad(e.Status))
            {
                _logAction?.Invoke(3, this, $"心跳检测错误：{e.Status}", null);

                if (m_reConnectHandler == null)
                {
                    m_reConnectHandler = new SessionReconnectHandler();
                    m_reConnectHandler.BeginReconnect(m_session, m_session.KeepAliveInterval, Server_ReconnectComplete);
                }

                return;
            }


        }
    }



    #endregion
}