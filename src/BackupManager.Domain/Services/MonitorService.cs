using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using NLog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BackupManager.Domain.Services
{
    public class ApplicationService
    {
        private readonly IBackupService _backupService;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly ISettings _settings;

        public ApplicationService(ISettings settings, IBackupService backupService) : this(ApplicationState.Running, settings, backupService)
        {
        }

        public ApplicationService(ApplicationState initialState, ISettings settings, IBackupService backupService)
        {
            _settings = settings;
            _backupService = backupService;
            ApplicationServiceState = initialState;
        }

        public ApplicationState ApplicationServiceState { get; set; }

        public async Task<bool> Run()
        {
            if (string.IsNullOrEmpty(_settings.BackupContent.FullPath)) return true;

            var file = await _backupService.CreateBackupFileAsync();
            if (!file.Exists) return false;

            var success = await _backupService.UploadFile(file, _settings.Customer.Hash);

            if (success)
            {
                _logger.Info("A new backup uploaded to FTP.");
                try
                {
                    File.Delete(file.FullName);
                }
                catch (Exception)
                {
                    _logger.Error("Unable to delete backup file on temp folder.");
                }
            }
            else
            {
                _logger.Error("Unable to upload files to FTP. Check internet connection.");
            }

            return success;
        }
    }
}