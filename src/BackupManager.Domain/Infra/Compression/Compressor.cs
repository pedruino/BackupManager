using System;
using System.Diagnostics;
using System.IO;
using System.IO.Abstractions;
using BackupManager.Domain.Interfaces;
using NLog;
using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;

namespace BackupManager.Domain.Infra.Compression
{
    public sealed class Compressor : ICompressor
    {
        private const string DEFAULT_SEARCH_PATTERN = "*.*";
        private const string SUPPORTED_EXTENSION = ".tar.gz";
        private readonly IFileSystem _fileSystem;
        private readonly ILogger _logger;

        public Compressor(IFileSystem fileSystem, ILogger logger)
        {
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public bool Compress(string sourceDirectory, string targetFilePath)
        {
            return Compress(sourceDirectory, targetFilePath, DEFAULT_SEARCH_PATTERN);
        }

        public bool Compress(string sourceDirectory, string targetFilePath, string searchPattern)
        {
            if (string.IsNullOrEmpty(sourceDirectory))
                throw new ArgumentNullException(nameof(sourceDirectory));

            if (string.IsNullOrEmpty(targetFilePath))
                throw new ArgumentNullException(nameof(sourceDirectory));

            if (!targetFilePath.EndsWith(SUPPORTED_EXTENSION))
                throw new ArgumentException("Unsupported file extension.", nameof(targetFilePath));

            try
            {
                using (var stream = _fileSystem.File.OpenWrite(targetFilePath))
                {
                    using (var writer = WriterFactory.Open(stream, ArchiveType.Tar, CompressionType.GZip))
                    {
                        writer.WriteAll(sourceDirectory, searchPattern, SearchOption.AllDirectories);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error compressing content '{sourceDirectory}' to file '{targetFilePath}'.");
            }

            return false;
        }

        public bool Decompress(string sourceFilePath, string targetDirectory)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
                throw new ArgumentNullException(nameof(sourceFilePath));

            if (string.IsNullOrEmpty(targetDirectory))
                throw new ArgumentNullException(nameof(targetDirectory));

            if (!sourceFilePath.EndsWith(SUPPORTED_EXTENSION))
                throw new ArgumentException("Unsupported file extension.", nameof(sourceFilePath));

            try
            {
                using (var stream = _fileSystem.File.OpenRead(sourceFilePath))
                {
                    using (var reader = ReaderFactory.Open(stream))
                    {
                        while (reader.MoveToNextEntry())
                        {
                            Debug.WriteLine(reader.Entry.Key);
                            reader.WriteEntryToDirectory(targetDirectory, new ExtractionOptions()
                            {
                                ExtractFullPath = true,
                                Overwrite = true
                            });
                        }

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error decompressing content from '{sourceFilePath}' to directory '{targetDirectory}'.");
            }

            return false;
        }
    }
}