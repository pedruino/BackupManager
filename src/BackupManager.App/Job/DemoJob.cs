﻿using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BackupManager.App.Job
{
    public class DemoJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"DemoJob: {DateTime.Now}");
            Debug.WriteLine($"JobKey: {context.JobDetail.Key}");
            return Task.CompletedTask;
        }
    }
}