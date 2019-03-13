using BackupManager.Domain.Entities;
using BackupManager.Domain.Interfaces;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace BackupManager.Domain.Services
{
    public class BackupService : IBackupService
    {
        private readonly ISettings _settings;

        public BackupService(ISettings settings)
        {
            _settings = settings;
        }

        public void DownloadFile(BackupFile backupFile, string localPath)
        {
            if (!TryGetFtpClient(out var ftpClient)) return;

            using (ftpClient)
            {
                ftpClient.DownloadFile(Path.Combine(localPath, backupFile.Name), backupFile.Uri, FtpLocalExists.Overwrite, FtpVerify.None);
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
                        Debug.WriteLine(e);
                    }
                }
            }

            return Enumerable.Empty<BackupFile>();
        }

        public IEnumerable<BackupFile> GetBackupFiles()
        {
            return Enumerable.Empty<BackupFile>();

            return GetBackupFiles(_settings.Customer.Hash);
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
                Debug.WriteLine(e);
            }

            return false;
        }
    }
}