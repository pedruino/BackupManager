using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BackupManager.Domain.Services;
using CommonServiceLocator;
using Quartz;

namespace BackupManager.App.Job
{
    public class BackupJob : IJob
    {
        private readonly ApplicationService _applicationService;

        public BackupJob() : this(ServiceLocator.Current.GetInstance<ApplicationService>())
        {
        }

        public BackupJob(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"BackupJob: {DateTime.Now}");
            Debug.WriteLine($"JobKey: {context.JobDetail.Key}");
            return _applicationService.Run();
        }
    }
}