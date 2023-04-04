﻿using Furion.Schedule;

namespace ThingsGateway.Web.Core;

/// <summary>
/// 清理日志作业任务
/// </summary>
[JobDetail("job_log", Description = "清理访问/操作日志", GroupName = "default", Concurrent = false)]
[Daily(TriggerId = "trigger_log", Description = "清理访问/操作日志", RunOnStart = true)]
public class LogJob : IJob
{
    private readonly IServiceProvider _serviceProvider;
    /// <summary>
    /// <inheritdoc cref="LogJob"/>
    /// </summary>
    /// <param name="serviceProvider"></param>
    public LogJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    /// <inheritdoc/>
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        var db = DbContext.Db.CopyNew();
        var daysAgo = 30; // 删除30天以前
        await db.Deleteable<DevLogVisit>().Where(u => (DateTime)u.CreateTime < DateTime.UtcNow.AddDays(-daysAgo)).ExecuteCommandAsync(); // 删除访问日志
        await db.Deleteable<DevLogOperate>().Where(u => (DateTime)u.CreateTime < DateTime.UtcNow.AddDays(-daysAgo)).ExecuteCommandAsync(); // 删除操作日志
    }
}