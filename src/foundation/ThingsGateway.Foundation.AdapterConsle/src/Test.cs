#region copyright

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

using ThingsGateway.Foundation.Modbus;

using TouchSocket.Core;

namespace ThingsGateway.Foundation
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            List<ModbusMaster> masterList = new List<ModbusMaster>();
            for (int i = 0; i < 2000; i++)
            {
                //---------------ModbusMaster----------------//

                var clientConfig = new TouchSocketConfig();
                //创建通道，也可以通过TouchSocketConfig.GetChannel扩展获取
                var clientChannel = clientConfig.GetTcpClientWithIPHost("127.0.0.1:504");

                //创建modbus客户端，传入通道
                ModbusMaster modbusMaster = new(clientChannel)
                {
                    //modbus协议格式
                    //ModbusType = Modbus.ModbusTypeEnum.ModbusRtu,
                    ModbusType = Modbus.ModbusTypeEnum.ModbusTcp,
                };
                masterList.Add(modbusMaster);
                //---------------ModbusMaster----------------//
            }
            List<Task> tasks = new List<Task>();
            foreach (var modbusMaster in masterList)
            {
                Task task = Task.Run(async () =>
                {
                    try
                    {
                        await modbusMaster.Channel.ConnectAsync(5000, CancellationToken.None);
                        //读写对应数据类型
                        var result = await modbusMaster.ReadInt32Async("40001", 2);
                        if (!result.IsSuccess)
                        {
                            Console.WriteLine(result.ErrorMessage);
                        }
                        else { Console.WriteLine(1); }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks.ToArray());

            Console.ReadKey();
        }
    }
}