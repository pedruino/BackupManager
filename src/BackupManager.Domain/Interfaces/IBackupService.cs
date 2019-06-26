using BackupManager.Domain.Entities;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace BackupManager.Domain.Interfaces
{
    public interface IBackupService
    {
        Task<IFileInfo> CreateBackupFileAsync();

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

        Task<bool> UploadFile(IFileInfo fileInfo, string remotePath);
    }
}