using System;
using System.Linq;
using System.Windows.Markup;
using BackupManager.Domain.Infra.Globalization;
using BackupManager.UI.Properties;

namespace BackupManager.UI.Extensions
{
    public class EnumerationExtension : MarkupExtension
    {
        private Type _enumType;

        public EnumerationExtension(Type enumType)
        {
            EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
        }

        public Type EnumType
        {
            get => _enumType;
            private set
            {
                if (_enumType == value)
                    return;

                var enumType = Nullable.GetUnderlyingType(value) ?? value;

                if (enumType.IsEnum == false)
                    throw new ArgumentException("Type must be an Enum.");

                _enumType = value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType).Cast<object>()
                       .Select(enumValue => new EnumerationMember
                       {
                           Value = enumValue,
                           Description = GetDescription(enumValue)

                       })
                       .ToArray();
        }

        private string GetDescription(object enumValue)
        {
            return EnumType.GetField(enumValue.ToString())
                           .GetCustomAttributes(typeof(LocalizedDescriptionKeyAttribute), false)
                           .FirstOrDefault() is LocalizedDescriptionKeyAttribute localizedDescriptionKeyAttribute
                ? EnumResources.ResourceManager.GetString(localizedDescriptionKeyAttribute.Description)
                : enumValue.ToString();
        }

        public class EnumerationMember
        {
            public string Description { get; set; }
            public object Value { get; set; }
        }
    }
}