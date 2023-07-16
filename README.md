﻿
<div align='center'>
<img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/gitLogo.png" height=100 />

<div align="center"> 

[![NuGet(ThingsGateway)](https://img.shields.io/nuget/v/ThingsGateway.Foundation.svg?label=ThingsGateway.Foundation)](https://www.nuget.org/packages/kimdiego/)
[![License](https://img.shields.io/badge/license-Apache%202-4EB1BA.svg)](https://www.apache.org/licenses/LICENSE-2.0.html)
[![star](https://gitee.com/dotnetchina/ThingsGateway/badge/star.svg)](https://gitee.com/dotnetchina/ThingsGateway/stargazers) 
[![star](https://gitee.com/dotnetchina/ThingsGateway/badge/fork.svg)](https://gitee.com/dotnetchina/ThingsGateway/members) 
![star](https://img.shields.io/badge/QQ群-605534569-blue)

</div>  
</div>

#### 介绍

基于Net6/7+Blazor Server的跨平台边缘采集网关，支持南北端插件式开发，
并拥有较完善的北端Rpc权限管理。

[Github地址](https://github.com/kimdiego2098/ThingsGateway)

<div >
如果对您有帮助，请点击右上角⭐Star关注，感谢支持开源！
</div>

#### 开源说明

Apache 2.0+[附加协议](https://diego2098.gitee.io/thingsgateway/docs/)

Apache 2.0 开源协议的核心内容是以保护和尊重原作者的著作权为主要目的。对使用，复制，修改，商用不做过多限制，但必须包含原著的License信息。

####  功能亮点

- Blazor Server架构，开发部署更简单
- 采集/上传配置完全支持Excel导入导出
- 插件式驱动，方便驱动二次开发
- 支持采集通道冗余，上传离线缓存
- 时序数据库存储
- 实时/历史报警(Sql转储)，支持布尔/高低限值

#### 演示

http://120.24.62.140:5000/

默认账户密码：superAdmin 111111


#### 社区版采集插件
> 支持分包解析/订阅
- Modbus(Rtu/Tcp/Udp)
- OPCDAClient（支持导入节点）
- OPCUAClient（支持导入节点，动态类型）
- 西门子S7协议

#### 社区版上传插件
> 支持Rpc写入
- Modbus Server
- OPCUA Server (支持历史查询)
- Mqtt Server (支持自定义json)
- Mqtt Client (支持自定义json)
- IotSharp Client (IotSharp网关插件，Rpc待测试)

> 不支持Rpc
- RabbitMQ (支持自定义json)
- Kafka

#### nuget

- Modbus库，支持ModbusTcp、ModbusRtu、ModbusRtuOverTcp、ModbusUdp、ModbusServer等
``` powershell
 dotnet add package ThingsGateway.Foundation.Adapter.Modbus
```
- OPCDA客户端库，支持X64，支持NetCore，支持检测重连
``` powershell
 dotnet add package ThingsGateway.Foundation.Adapter.OPCDA
```
- OPCUA客户端库
``` powershell
 dotnet add package ThingsGateway.Foundation.Adapter.OPCUA
```

- S7库
``` powershell
 dotnet add package ThingsGateway.Foundation.Adapter.Siemens
```

####  效果图
 <table>
    <tr>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/1.png"/></td>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/2.png"/></td>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/3.png"/></td>
    </tr>
    <tr>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/4.png"/></td>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/5.png"/></td>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/6.png"/></td>
    </tr>
        <tr>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/7.png"/></td>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/8.png"/></td>
        <td><img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/9.png"/></td>
    </tr>
 </table>


 ####  文档

 使用前请查看Gitee Pages [文档站点](https://diego2098.gitee.io/thingsgateway/)

 #### 特别鸣谢
-  Furion：[https://dotnetchina.gitee.io/furion](https://dotnetchina.gitee.io/furion)
-  SqlSugar：[https://gitee.com/dotnetchina/SqlSugar](https://gitee.com/dotnetchina/SqlSugar)
-  Simple.Admin：[https://gitee.com/zxzyjs/SimpleAdmin](https://gitee.com/zxzyjs/SimpleAdmin)
-  Masa.Blazor：[https://www.masastack.com/blazor](https://www.masastack.com/blazor)
-  MiniExcel：[https://gitee.com/dotnetchina/MiniExcel](https://gitee.com/dotnetchina/MiniExcel)
-  TouchSocket：[https://gitee.com/rrqm_home/touchsocket](https://gitee.com/rrqm_home/touchsocket)
-  IdGenerator：[https://github.com/yitter/idgenerator](https://github.com/yitter/idgenerator)
-  CodingSeb.ExpressionEvaluator：[https://github.com/codingseb/ExpressionEvaluator](https://github.com/codingseb/ExpressionEvaluator)
-  Hardware.Info：[https://github.com/Jinjinov/Hardware.Info](https://github.com/Jinjinov/Hardware.Info)
-  UAParser：[https://github.com/ua-parser/uap-csharp](https://github.com/ua-parser/uap-csharp)
-  OPCUAWebPlatformUniCT：[https://github.com/OPCUAUniCT/OPCUAWebPlatformUniCT](https://github.com/OPCUAUniCT/OPCUAWebPlatformUniCT)

#### 补充说明
* 使用OPC相关插件时请遵循OPC基金会的授权规则
* 使用OPCDA插件时，需安装OPC核心库，[文件地址](https://gitee.com/dotnetchina/ThingsGateway/attach_files)

 

####  支持作者
 如果对您有帮助，请点击右上角⭐Star关注，感谢支持开源！

 若希望捐赠项目，请查看以下捐赠码或使用Gitee捐赠功能

<img src="https://gitee.com/dotnetchina/ThingsGateway/raw/master/Image/pay.png" height=180 />

####  联系作者
 * QQ群：605534569
 * 邮箱：2248356998@qq.com

