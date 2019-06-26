using BackupManager.Domain.Interfaces;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using BackupManager.App.Job;
using DayOfWeek = BackupManager.Domain.Enumerations.DayOfWeek;

namespace BackupManager.App
{
    public partial class App
    {
        private const int DEFAULT_INTERVAL = 30;

        private static IEnumerable<System.DayOfWeek> GetSystemDaysOfWeek(DayOfWeek flaDaysOfWeek)
        {
            foreach (var day in flaDaysOfWeek.ToString().Split(','))
            {
                if (Enum.TryParse<System.DayOfWeek>(day, out var dayOfWeek))
                    yield return dayOfWeek;
            }
        }

        private void CreateRoutine(IScheduler scheduler, ISettings settings)
        {
            //TODO: Alterar para settings.Schedule.Time
            var time = DateTime.Now.AddSeconds(DEFAULT_INTERVAL);
            var days = GetSystemDaysOfWeek(settings.Schedule.DaysOfWeek).ToArray();

            var scheduleBuilder = CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(time.Hour, time.Minute, days);

            var job = JobBuilder
                .Create<BackupJob>()
                .WithIdentity(typeof(BackupJob).Name, SchedulerConstants.DefaultGroup)
                .Build();

            var triggerName = string.Join(",", days);

            var trigger = TriggerBuilder.Create()
                .WithIdentity("demotrigger2", "demogroup2")
                .StartNow()
                .WithSchedule(scheduleBuilder)
                .Build();

            scheduler.ScheduleJob(job, trigger).Wait();
        }

        private void ScheduleJobs(IScheduler scheduler, ISettings settings)
        {
            //var schedule = settings.Schedule;

            //IJobDetail job = JobBuilder.Create<DemoJob>().WithIdentity("demoJob", "demoGroup").Build();
            //// Trigger para que o job seja executado imediatamente, e repetidamente a cada 15 segundos
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("demoTrigger", "demoGroup")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(15)
            //        .RepeatForever())
            //    .Build();
            //scheduler.ScheduleJob(job, trigger).Wait();

            CreateRoutine(scheduler, settings);
        }

        private void StartScheduler(ISettings settings)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler().Result;
            scheduler.Start().Wait();

            ScheduleJobs(scheduler, settings);
        }
    }
}