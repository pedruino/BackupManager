using BackupManager.Domain.Entities;
using BackupManager.Domain.Enumerations;
using BackupManager.Domain.Interfaces;
using FluentFTP;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackupManager.Domain.Services
{
    public class BackupService : IBackupService
    {
        private const string FILE_EXTENSION = ".tar.gz";
        private readonly ICompressor _compressor;
        private readonly IFileSystem _fileSystem;
        private readonly IFirebirdService _firebirdService;
        private readonly ILogger _logger;
        private readonly ISettings _settings;

        public BackupService(IFileSystem fileSystem, ILogger logger, ICompressor compressor, ISettings settings, IFirebirdService firebirdService)
        {
            _logger = logger;
            _fileSystem = fileSystem;
            _settings = settings;
            _compressor = compressor;
            _firebirdService = firebirdService;
        }

        public Task<IFileInfo> CreateBackupFileAsync()
        {
            return Task.Run(() =>
            {
                var sourcePath = _settings.BackupContent.FullPath;
                var fileName = $"{Guid.NewGuid().ToString()}{FILE_EXTENSION}";
                var targetFilePath = _fileSystem.Path.Combine(Path.GetTempPath(), fileName);

                if (_settings.BackupContent.ContentType == ContentType.Database)
                {
                    var fileInfo = _firebirdService.GenerateFbk(_settings.BackupContent);

                    if (!_compressor.Compress(sourcePath, targetFilePath, fileInfo.Name))
                    {
                        _logger.Error($"Backup file '{fileInfo.FullName}' can not be created at '{targetFilePath}'.");
                    }
                }
                else if (!_compressor.Compress(sourcePath, targetFilePath))
                {
                    _logger.Error($"Backup file '{sourcePath}' can not be created at '{targetFilePath}'.");
                }

                return _fileSystem.FileInfo.FromFileName(targetFilePath);
            });
        }

        public void DownloadFile(BackupFile backupFile, string localPath)
        {
            if (!TryGetFtpClient(out var ftpClient)) return;

            using (ftpClient)
            {
                ftpClient.DownloadFile(_fileSystem.Path.Combine(localPath, backupFile.Name), backupFile.Uri, FtpLocalExists.Overwrite, FtpVerify.None);
            }
        }

        public IEnumerable<BackupFile> GetBackupFiles(string remotePath)
        {
            if (TryGetFtpClient(out var ftpClient))
            {
                using (ftpClient)
                {
                    try
                    {
                        if (!ftpClient.DirectoryExists(remotePath))
                        {
                            ftpClient.CreateDirectory(remotePath);
                        }

                        return ftpClient.GetListing(remotePath)
                            .Select(ftpListItem => new BackupFile(ftpListItem.Name,
                                ftpListItem.FullName,
                                ftpListItem.Size,
                                ftpListItem.Created))
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e, "Error on listing files.");
                    }
                }
            }

            return Enumerable.Empty<BackupFile>();
        }

        public IEnumerable<BackupFile> GetBackupFiles()
        {
            return GetBackupFiles(_settings.Customer.Hash);
        }

        public async Task<bool> UploadFile(IFileInfo fileInfo, string remotePath)
        {
            if (!TryGetFtpClient(out var ftpClient)) return false;

            using (ftpClient)
            {
                using (var fs = fileInfo.OpenRead())
                {
                    return await ftpClient.UploadAsync(fs, Path.Combine(remotePath, fileInfo.Name), FtpExists.NoCheck, true);
                }
            }
        }

        private bool TryGetFtpClient(out FtpClient ftpClient)
        {
            try
            {
                ftpClient = new FtpClient(_settings.Ftp.Host, new NetworkCredential(_settings.Ftp.Username, _settings.Ftp.Password));
                return true;
            }
            catch (Exception e)
            {
                ftpClient = null;
                _logger.Error(e, "Error trying to get ftp client.");
            }

            return false;
        }
    }
}