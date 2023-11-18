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

//------------------------------------------------------------------------------
//  此代码版权（除特别声明或在XREF结尾的命名空间的代码）归作者本人若汝棋茗所有
//  源代码使用协议遵循本仓库的开源协议及附加协议，若本仓库没有设置，则按MIT开源协议授权
//  CSDN博客：https://blog.csdn.net/qq_40374647
//  哔哩哔哩视频：https://space.bilibili.com/94253567
//  Gitee源代码仓库：https://gitee.com/RRQM_Home
//  Github源代码仓库：https://github.com/RRQM
//  API首页：http://rrqm_home.gitee.io/touchsocket/
//  交流QQ群：234762506
//  感谢您的下载和使用
//------------------------------------------------------------------------------
//------------------------------------------------------------------------------
using System.Net;

namespace ThingsGateway.Foundation.Sockets
{
    /// <summary>
    /// 具有Udp终结点的发送
    /// </summary>
    public interface IUdpClientSender : ISender
    {
        /// <summary>
        /// 同步组合发送数据。
        /// <para>该发送会经过适配器封装，具体封装内容由适配器决定。</para>
        /// </summary>
        /// <param name="endPoint">远程终结点</param>
        /// <param name="transferBytes">组合数据</param>
        /// <exception cref="NotConnectedException">客户端没有连接</exception>
        /// <exception cref="OverlengthException">发送数据超长</exception>
        /// <exception cref="Exception">其他异常</exception>
        void Send(EndPoint endPoint, IList<ArraySegment<byte>> transferBytes);

        /// <summary>
        /// 异步组合发送数据。
        /// <para>该发送会经过适配器封装，具体封装内容由适配器决定。</para>
        /// </summary>
        /// <param name="endPoint">远程终结点</param>
        /// <param name="transferBytes">组合数据</param>
        /// <exception cref="NotConnectedException">客户端没有连接</exception>
        /// <exception cref="OverlengthException">发送数据超长</exception>
        /// <exception cref="Exception">其他异常</exception>
        Task SendAsync(EndPoint endPoint, IList<ArraySegment<byte>> transferBytes);

        /// <summary>
        /// 同步组合发送数据。
        /// <para>该发送会经过适配器封装，具体封装内容由适配器决定。</para>
        /// </summary>
        /// <param name="endPoint">远程终结点</param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <exception cref="OverlengthException">发送数据超长</exception>
        /// <exception cref="Exception">其他异常</exception>
        void Send(EndPoint endPoint, byte[] buffer, int offset, int length);

        /// <summary>
        /// 异步组合发送数据。
        /// <para>该发送会经过适配器封装，具体封装内容由适配器决定。</para>
        /// </summary>
        /// <param name="endPoint">远程终结点</param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <exception cref="OverlengthException">发送数据超长</exception>
        /// <exception cref="Exception">其他异常</exception>
        Task SendAsync(EndPoint endPoint, byte[] buffer, int offset, int length);
    }
}