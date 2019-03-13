using System;

namespace BackupManager.Domain.Entities
{
    public sealed class BackupFile
    {
        public BackupFile(string name, string uri, long size, DateTime dateCreated, string checksum)
        {
            Id = Guid.NewGuid();
            Name = name;
            Uri = uri;
            Size = size;
            DateCreated = dateCreated;
        }

        public BackupFile(string name, string uri, long size, DateTime dateCreated)
            : this(name, uri, size, dateCreated, null)
        {
        }

        public DateTime DateCreated { get; }
        public Guid Id { get; }
        public string Name { get; }
        public long Size { get; }
        public string Uri { get; }
    }
}