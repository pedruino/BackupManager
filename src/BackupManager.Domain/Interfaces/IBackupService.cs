using BackupManager.Domain.Entities;
using System.Collections.Generic;

namespace BackupManager.Domain.Interfaces
{
    public interface IBackupService
    {
        /// <summary>
        /// Downloads a selected backup file.
        /// </summary>
        /// <param name="backupFile">The file to be downloaded.</param>
        /// <param name="localPath">The local path to save downloaded file.</param>
        void DownloadFile(BackupFile backupFile, string localPath);

        /// <summary>
        /// Retrieves a backup files from remote directory.
        /// </summary>
        /// <param name="remotePath">The path to remote directory.</param>
        /// <returns>If remote path is valid path and contains files, returns a enumeration of backup files, otherwise a empty enumeration.</returns>
        IEnumerable<BackupFile> GetBackupFiles(string remotePath);

        /// <summary>
        /// Retrieves a backup files from remote directory.
        /// </summary>
        /// <returns>If remote path is valid path and contains files, returns a enumeration of backup files, otherwise a empty enumeration.</returns>
        IEnumerable<BackupFile> GetBackupFiles();
    }
}