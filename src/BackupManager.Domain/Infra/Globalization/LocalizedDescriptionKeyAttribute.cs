using System.ComponentModel;

namespace BackupManager.Domain.Infra.Globalization
{
    public class LocalizedDescriptionKeyAttribute : DescriptionAttribute
    {
        public LocalizedDescriptionKeyAttribute(string resourceKey)
        {
            Description = resourceKey;
        }

        public override string Description { get; }
    }
}