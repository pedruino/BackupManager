namespace BackupManager.Domain.Interfaces
{
    public interface ICompressor
    {
        bool Compress(string sourceDirectory, string targetFilePath);

        bool Compress(string fileInfoDirectoryName, string targetFilePath, string searchPattern);

        bool Decompress(string sourceFilePath, string targetDirectory);
    }
}