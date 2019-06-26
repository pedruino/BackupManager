using BackupManager.Domain.Interfaces;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Services;
using NLog;
using System;
using System.IO.Abstractions;

namespace BackupManager.Domain.Services
{
    public sealed class FirebirdService : IFirebirdService
    {
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        public FirebirdService(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public IFileInfo GenerateFbk(ISettingsBackupContent settingsBackupContent)
        {
            var cs = new FbConnectionStringBuilder
            {
                UserID = settingsBackupContent.User,
                Password = settingsBackupContent.Password,
                Database = settingsBackupContent.FullPath
            };

            var backupSvc = new FbBackup { ConnectionString = cs.ToString() };

            var file = _fileSystem.FileInfo.FromFileName(settingsBackupContent.FullPath);
            var fbkName = $"{Guid.NewGuid().ToString()}.fbk";
            var fullFileNameFbk = _fileSystem.Path.Combine(file.DirectoryName, fbkName);

            backupSvc.BackupFiles.Add(new FbBackupFile(fullFileNameFbk, 4096));
            backupSvc.Verbose = true;

            backupSvc.Options = FbBackupFlags.IgnoreLimbo;

            backupSvc.ServiceOutput += (sender, args) =>
            {
                _logger.Debug(args.Message);
            };

            backupSvc.Execute();

            return _fileSystem.FileInfo.FromFileName(fullFileNameFbk);
        }
    }
}