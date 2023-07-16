"use strict";(self.webpackChunkthingsgateway=self.webpackChunkthingsgateway||[]).push([[3477],{3905:(e,t,n)=>{n.d(t,{Zo:()=>p,kt:()=>d});var i=n(7294);function r(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function a(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);t&&(i=i.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,i)}return n}function l(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?a(Object(n),!0).forEach((function(t){r(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):a(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function c(e,t){if(null==e)return{};var n,i,r=function(e,t){if(null==e)return{};var n,i,r={},a=Object.keys(e);for(i=0;i<a.length;i++)n=a[i],t.indexOf(n)>=0||(r[n]=e[n]);return r}(e,t);if(Object.getOwnPropertySymbols){var a=Object.getOwnPropertySymbols(e);for(i=0;i<a.length;i++)n=a[i],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(r[n]=e[n])}return r}var o=i.createContext({}),s=function(e){var t=i.useContext(o),n=t;return e&&(n="function"==typeof e?e(t):l(l({},t),e)),n},p=function(e){var t=s(e.components);return i.createElement(o.Provider,{value:t},e.children)},u={inlineCode:"code",wrapper:function(e){var t=e.children;return i.createElement(i.Fragment,{},t)}},m=i.forwardRef((function(e,t){var n=e.components,r=e.mdxType,a=e.originalType,o=e.parentName,p=c(e,["components","mdxType","originalType","parentName"]),m=s(n),d=r,g=m["".concat(o,".").concat(d)]||m[d]||u[d]||a;return n?i.createElement(g,l(l({ref:t},p),{},{components:n})):i.createElement(g,l({ref:t},p))}));function d(e,t){var n=arguments,r=t&&t.mdxType;if("string"==typeof e||r){var a=n.length,l=new Array(a);l[0]=m;var c={};for(var o in t)hasOwnProperty.call(t,o)&&(c[o]=t[o]);c.originalType=e,c.mdxType="string"==typeof e?e:r,l[1]=c;for(var s=2;s<a;s++)l[s]=n[s];return i.createElement.apply(null,l)}return i.createElement.apply(null,n)}m.displayName="MDXCreateElement"},6969:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>o,contentTitle:()=>l,default:()=>u,frontMatter:()=>a,metadata:()=>c,toc:()=>s});var i=n(7462),r=(n(7294),n(3905));const a={id:"pluginmqttserver",title:"MqttServer"},l=void 0,c={unversionedId:"pluginmqttserver",id:"pluginmqttserver",title:"MqttServer",description:"\u4e00\u3001\u8bbe\u5907\u6269\u5c55\u5c5e\u6027",source:"@site/docs/pluginmqttserver.mdx",sourceDirName:".",slug:"/pluginmqttserver",permalink:"/thingsgateway/docs/pluginmqttserver",draft:!1,editUrl:"https://gitee.com/dotnetchina/ThingsGateway/tree/master/handbook/docs/pluginmqttserver.mdx",tags:[],version:"current",frontMatter:{id:"pluginmqttserver",title:"MqttServer"},sidebar:"docs",previous:{title:"ModbusServer",permalink:"/thingsgateway/docs/pluginmodbusserver"},next:{title:"MqttClient",permalink:"/thingsgateway/docs/pluginmqttclient"}},o={},s=[{value:"\u4e00\u3001\u8bbe\u5907\u6269\u5c55\u5c5e\u6027",id:"\u4e00\u8bbe\u5907\u6269\u5c55\u5c5e\u6027",level:2},{value:"\u4e8c\u3001\u53d8\u91cf\u914d\u7f6e",id:"\u4e8c\u53d8\u91cf\u914d\u7f6e",level:2},{value:"\u542f\u7528",id:"\u542f\u7528",level:3},{value:"\u5141\u8bb8\u5199\u5165",id:"\u5141\u8bb8\u5199\u5165",level:3},{value:"\u4e09\u3001\u5b9e\u4f53\u811a\u672c",id:"\u4e09\u5b9e\u4f53\u811a\u672c",level:2},{value:"\u8bf4\u660e",id:"\u8bf4\u660e",level:3},{value:"DTO\u5b9a\u4e49",id:"dto\u5b9a\u4e49",level:3}],p={toc:s};function u(e){let{components:t,...a}=e;return(0,r.kt)("wrapper",(0,i.Z)({},p,a,{components:t,mdxType:"MDXLayout"}),(0,r.kt)("h2",{id:"\u4e00\u8bbe\u5907\u6269\u5c55\u5c5e\u6027"},"\u4e00\u3001\u8bbe\u5907\u6269\u5c55\u5c5e\u6027"),(0,r.kt)("p",null,(0,r.kt)("img",{src:n(111).Z,width:"602",height:"1396"})),(0,r.kt)("table",null,(0,r.kt)("thead",{parentName:"table"},(0,r.kt)("tr",{parentName:"thead"},(0,r.kt)("th",{parentName:"tr",align:null},"\u5c5e\u6027"),(0,r.kt)("th",{parentName:"tr",align:null},"\u8bf4\u660e"),(0,r.kt)("th",{parentName:"tr",align:null},"\u9ed8\u8ba4\u503c/\u5907\u6ce8"))),(0,r.kt)("tbody",{parentName:"table"},(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"IP"),(0,r.kt)("td",{parentName:"tr",align:null},"ServerIP,\u4e3a\u7a7a\u65f6\u6307\u4efb\u610fIP"),(0,r.kt)("td",{parentName:"tr",align:null})),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u7aef\u53e3"),(0,r.kt)("td",{parentName:"tr",align:null},"\u8fde\u63a5\u7aef\u53e3"),(0,r.kt)("td",{parentName:"tr",align:null},"1883")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u5141\u8bb8\u8fde\u63a5\u7684ID\u524d\u7f00"),(0,r.kt)("td",{parentName:"tr",align:null},"\u8fc7\u6ee4ID\uff0c\u4f8b\u5982Client\u7aefID\u5fc5\u987b\u662fThingsGatewayId123456"),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGatewayId")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u5141\u8bb8Rpc\u5199\u5165"),(0,r.kt)("td",{parentName:"tr",align:null},"\u662f\u5426\u5141\u8bb8\u5199\u5165\u53d8\u91cf"),(0,r.kt)("td",{parentName:"tr",align:null},"True")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u7ebf\u7a0b\u5faa\u73af\u95f4\u9694"),(0,r.kt)("td",{parentName:"tr",align:null},"\u4e0a\u4f20\u7ebf\u7a0b\u7684\u5faa\u73af\u95f4\u9694,\u4e00\u822c\u4e0d\u9700\u8981\u66f4\u6539(ms)"),(0,r.kt)("td",{parentName:"tr",align:null},"1000")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u5217\u8868\u5206\u5272\u5927\u5c0f"),(0,r.kt)("td",{parentName:"tr",align:null},"\u53d1\u5e03\u7684\u5b9e\u4f53\u5217\u8868\u6309\u5927\u5c0f\u5206\u5272"),(0,r.kt)("td",{parentName:"tr",align:null},"1000")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u8bbe\u5907Topic"),(0,r.kt)("td",{parentName:"tr",align:null},"\u8bbe\u5907\u4fe1\u606f\u8ba2\u9605Topic"),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGateway/Device")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"\u53d8\u91cfTopic"),(0,r.kt)("td",{parentName:"tr",align:null},"\u53d8\u91cf\u4fe1\u606f\u8ba2\u9605Topic"),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGateway/Variable")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"Rpc\u8fd4\u56deTopic"),(0,r.kt)("td",{parentName:"tr",align:null},"\u5ba2\u6237\u7aef\u5199\u5165\u53d8\u91cf\u7684\u8ba2\u9605Topic\uff0c\u8fd4\u56de\u5199\u5165\u7ed3\u679c\u4fe1\u606f"),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGateway/RpcSub")),(0,r.kt)("tr",{parentName:"tbody"},(0,r.kt)("td",{parentName:"tr",align:null},"Rpc\u5199\u5165Topic"),(0,r.kt)("td",{parentName:"tr",align:null},"\u5ba2\u6237\u7aef\u5199\u5165\u53d8\u91cf\u7684\u53d1\u5e03Topic"),(0,r.kt)("td",{parentName:"tr",align:null},"ThingsGateway/RpcWrite")))),(0,r.kt)("admonition",{type:"tip"},(0,r.kt)("p",{parentName:"admonition"}," \u767b\u5f55\u7f51\u5173\u7684MqttServer\u8fd8\u9700\u8981\u914d\u7f6e\u7b2c\u4e09\u65b9\u6388\u6743User\uff0c\u5e76\u4e14\u5728\u5ba2\u6237\u7aef\u586b\u5199\u5bf9\u5e94\u7684UserName\u4e0ePassword"),(0,r.kt)("p",{parentName:"admonition"},(0,r.kt)("img",{src:n(7352).Z,width:"2561",height:"1245"}))),(0,r.kt)("h2",{id:"\u4e8c\u53d8\u91cf\u914d\u7f6e"},"\u4e8c\u3001\u53d8\u91cf\u914d\u7f6e"),(0,r.kt)("img",{src:n(5311).Z,width:"400"}),(0,r.kt)("h3",{id:"\u542f\u7528"},"\u542f\u7528"),(0,r.kt)("p",null,"\u542f\u7528\u53d8\u91cf\u540e\u624d\u80fd\u8fdb\u884c\u4e0a\u4f20"),(0,r.kt)("h3",{id:"\u5141\u8bb8\u5199\u5165"},"\u5141\u8bb8\u5199\u5165"),(0,r.kt)("p",null,"\u5355\u72ec\u914d\u7f6e\u53d8\u91cf\u662f\u5426\u5141\u8bb8\u5199\u5165"),(0,r.kt)("h2",{id:"\u4e09\u5b9e\u4f53\u811a\u672c"},"\u4e09\u3001\u5b9e\u4f53\u811a\u672c"),(0,r.kt)("h3",{id:"\u8bf4\u660e"},"\u8bf4\u660e"),(0,r.kt)("p",null,"\u811a\u672c\u529f\u80fd\u7531cs-script\u5b9e\u73b0"),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre"},' //\u63d0\u4f9b\u8fd9\u4e2a\u4f8b\u5b50\n\n //\u5b9a\u4e49\u8fd4\u56deList\n  List<dynamic> newModelList=new List<dynamic>(); \n  //input\u4e3a\u56fa\u5b9a\u4f20\u5165\u503c\uff0c\u5728\u53d8\u91cf\u811a\u672c\u4e2d\u4e3a\u53d8\u91cf\u5b9e\u4f53\u7c7bList\uff0c\u5728\u8bbe\u5907\u811a\u672c\u4e2d\u4e3a\u8bbe\u5907\u5b9e\u4f53\u7c7bList\uff0c\u67e5\u770b\u4e0a\u9762\u7684json\u8bf4\u660e\n  foreach (var item in input)\n  {\n    //\u6dfb\u52a0\u81ea\u5b9a\u4e49\u503c\n    newModelList.Add(new\n  {\n      customName=item.name,//\u53d8\u91cf\u540d\u79f0\n      customValue=item.value,//\u53d8\u91cf\u503c\n      customCollectTime=item.collectTime.ToString("yyyy-MM-dd HH:mm:ss fffffff"), //\u91c7\u96c6\u65f6\u95f4\uff0c\u8fd9\u91cc\u683c\u5f0f\u5316\u4e3a\u81ea\u5b9a\u4e49\u65f6\u95f4\u683c\u5f0f\n  });\n\n  }\n  return newModelList;\n\n')),(0,r.kt)("img",{src:n(8460).Z,width:"400"}),(0,r.kt)("p",null,"\u53ef\u4ee5\u770b\u5230mqtt\u4e0a\u4f20\u5185\u5bb9\u5df2\u7ecf\u6539\u4e3a\u4ee5\u4e0a\u811a\u672c\u8fd4\u56de\u503c"),(0,r.kt)("h3",{id:"dto\u5b9a\u4e49"},"DTO\u5b9a\u4e49"),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre"},'#region copyright\n//------------------------------------------------------------------------------\n//  \u6b64\u4ee3\u7801\u7248\u6743\u58f0\u660e\u4e3a\u5168\u6587\u4ef6\u8986\u76d6\uff0c\u5982\u6709\u539f\u4f5c\u8005\u7279\u522b\u58f0\u660e\uff0c\u4f1a\u5728\u4e0b\u65b9\u624b\u52a8\u8865\u5145\n//  \u6b64\u4ee3\u7801\u7248\u6743\uff08\u9664\u7279\u522b\u58f0\u660e\u5916\u7684\u4ee3\u7801\uff09\u5f52\u4f5c\u8005\u672c\u4ebaDiego\u6240\u6709\n//  \u6e90\u4ee3\u7801\u4f7f\u7528\u534f\u8bae\u9075\u5faa\u672c\u4ed3\u5e93\u7684\u5f00\u6e90\u534f\u8bae\u53ca\u9644\u52a0\u534f\u8bae\n//  Gitee\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1ahttps://gitee.com/diego2098/ThingsGateway\n//  Github\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1ahttps://github.com/kimdiego2098/ThingsGateway\n//  \u4f7f\u7528\u6587\u6863\uff1ahttps://diego2098.gitee.io/thingsgateway-docs/\n//  QQ\u7fa4\uff1a605534569\n//------------------------------------------------------------------------------\n#endregion\n\n\nusing ThingsGateway.Core;\nnamespace ThingsGateway.Web.Foundation;\n/// <summary>\n/// \u4e0a\u4f20DTO\n/// </summary>\npublic class VariableData\n{\n    /// <inheritdoc cref="PrimaryIdEntity.Id"/>\n    public long Id { get; set; }\n    /// <inheritdoc cref="MemoryVariable.Name"/>\n    public string Name { get; set; }\n    /// <inheritdoc cref="MemoryVariable.Description"/>\n    public string Description { get; set; }\n    /// <inheritdoc cref="DeviceVariableRunTime.DeviceName"/>\n    public string DeviceName { get; set; }\n    /// <inheritdoc cref="DeviceVariableRunTime.RawValue"/>\n    public object RawValue { get; set; }\n    /// <inheritdoc cref="DeviceVariableRunTime.Value"/>\n    public object Value { get; set; }\n    /// <inheritdoc cref="DeviceVariableRunTime.ChangeTime"/>\n    public DateTime ChangeTime { get; set; }\n    /// <inheritdoc cref="DeviceVariableRunTime.CollectTime"/>\n    public DateTime CollectTime { get; set; }\n    /// <inheritdoc cref="DeviceVariableRunTime.IsOnline"/>\n    public bool IsOnline { get; set; }\n    /// <inheritdoc cref="MemoryVariable.ReadExpressions"/>\n    public string ReadExpressions { get; set; }\n    /// <inheritdoc cref="DeviceVariable.WriteExpressions"/>\n    public string WriteExpressions { get; set; }\n    /// <inheritdoc cref="DeviceVariable.IntervalTime"/>\n    public int IntervalTime { get; set; }\n    /// <inheritdoc cref="DeviceVariable.OtherMethod"/>\n    public string OtherMethod { get; set; }\n    /// <inheritdoc cref="DeviceVariable.VariableAddress"/>\n    public string VariableAddress { get; set; }\n\n    /// <inheritdoc cref="MemoryVariable.ProtectTypeEnum"/>\n    public int ProtectTypeEnum { get; set; }\n    /// <inheritdoc cref="MemoryVariable.DataTypeEnum"/>\n    public int DataTypeEnum { get; set; }\n}\n\n')),(0,r.kt)("pre",null,(0,r.kt)("code",{parentName:"pre"},'#region copyright\n//------------------------------------------------------------------------------\n//  \u6b64\u4ee3\u7801\u7248\u6743\u58f0\u660e\u4e3a\u5168\u6587\u4ef6\u8986\u76d6\uff0c\u5982\u6709\u539f\u4f5c\u8005\u7279\u522b\u58f0\u660e\uff0c\u4f1a\u5728\u4e0b\u65b9\u624b\u52a8\u8865\u5145\n//  \u6b64\u4ee3\u7801\u7248\u6743\uff08\u9664\u7279\u522b\u58f0\u660e\u5916\u7684\u4ee3\u7801\uff09\u5f52\u4f5c\u8005\u672c\u4ebaDiego\u6240\u6709\n//  \u6e90\u4ee3\u7801\u4f7f\u7528\u534f\u8bae\u9075\u5faa\u672c\u4ed3\u5e93\u7684\u5f00\u6e90\u534f\u8bae\u53ca\u9644\u52a0\u534f\u8bae\n//  Gitee\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1ahttps://gitee.com/diego2098/ThingsGateway\n//  Github\u6e90\u4ee3\u7801\u4ed3\u5e93\uff1ahttps://github.com/kimdiego2098/ThingsGateway\n//  \u4f7f\u7528\u6587\u6863\uff1ahttps://diego2098.gitee.io/thingsgateway-docs/\n//  QQ\u7fa4\uff1a605534569\n//------------------------------------------------------------------------------\n#endregion\n\n\nusing ThingsGateway.Core;\nnamespace ThingsGateway.Web.Foundation;\n/// <summary>\n/// \u8bbe\u5907\u4e0a\u4f20DTO\n/// </summary>\npublic class DeviceData\n{\n    /// <inheritdoc cref="PrimaryIdEntity.Id"/>\n    public long Id { get; set; }\n    /// <inheritdoc cref="CollectDeviceRunTime.PluginName"/>\n    public string PluginName { get; set; }\n    /// <inheritdoc cref="CollectDeviceRunTime.DeviceVariableCount"/>\n    public int DeviceVariablesCount { get; set; }\n    /// <inheritdoc cref="CollectDeviceRunTime.ActiveTime"/>\n    public DateTime ActiveTime { get; set; }\n    /// <inheritdoc cref="CollectDeviceRunTime.DeviceStatus"/>\n    public int DeviceStatus { get; set; }\n    /// <inheritdoc cref="CollectDeviceRunTime.LastErrorMessage"/>\n    public string LastErrorMessage { get; set; }\n    /// <inheritdoc cref="UploadDevice.Name"/>\n    public string Name { get; set; }\n    /// <inheritdoc cref="UploadDevice.Description"/>\n    public string Description { get; set; }\n    /// <inheritdoc cref="CollectDeviceRunTime.KeepRun"/>\n    public bool KeepRun { get; set; }\n\n}\n\n\n')))}u.isMDXComponent=!0},5311:(e,t,n)=>{n.d(t,{Z:()=>i});const i=n.p+"assets/images/pluginmqttserver-3-bb7ff75232f26fb73f8ee97624fd0821.png"},8460:(e,t,n)=>{n.d(t,{Z:()=>i});const i=n.p+"assets/images/script2-bb71dfdd3ef867c7ec87d60968e71f88.png"},7352:(e,t,n)=>{n.d(t,{Z:()=>i});const i=n.p+"assets/images/pluginmqttserver-1-2ca9923b7eac0595fbcb1c61bb4ac778.png"},111:(e,t,n)=>{n.d(t,{Z:()=>i});const i=n.p+"assets/images/pluginmqttserver-2-eb3c1e4bbca6c6bf16c2503a5035c6e1.png"}}]);