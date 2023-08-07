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

using Mapster;

using Microsoft.AspNetCore.Components;

using ThingsGateway.Admin.Blazor.Core;
using ThingsGateway.Admin.Core;
using ThingsGateway.Application;

namespace ThingsGateway.Blazor;
/// <summary>
/// 后台日志页面
/// </summary>
public partial class BackendLogPage
{
    private IAppDataTable _datatable;
    private readonly BackendLogPageInput search = new();

    [Inject]
    AjaxService AjaxService { get; set; }

    private async Task ClearClickAsync()
    {
        var confirm = await PopupService.OpenConfirmDialogAsync("删除", "确定 ?");
        if (confirm)
        {
            await BackendLogService.DeleteAsync();
            await _datatable?.QueryClickAsync();
        }
    }
    async Task DownExportAsync(BackendLogPageInput input = null)
    {
        try
        {
            await AjaxService.DownFileAsync("gatewayFile/backendLog", SysDateTimeExtensions.CurrentDateTime.ToFileDateTimeFormat(), input.Adapt<BackendLogInput>());
        }
        finally
        {
        }
    }



    private async Task<SqlSugarPagedList<BackendLog>> QueryCallAsync(BackendLogPageInput input)
    {
        var data = await BackendLogService.PageAsync(input);
        return data;
    }
}