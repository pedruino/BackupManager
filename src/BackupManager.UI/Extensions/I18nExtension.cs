using BackupManager.Infra.I18n;
using System;
using System.Windows.Markup;

namespace BackupManager.UI.Extensions
{
    public class InternationalizationExtension : MarkupExtension
    {
        private readonly IResourceTranslator _resourceTranslator;

        public InternationalizationExtension()
        {

        }
        public InternationalizationExtension(IResourceTranslator resourceTranslator)
        {
            _resourceTranslator = resourceTranslator;
        }

        public string Key { get; set; }
        public string Locale { get; set; } 

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _resourceTranslator.Translate(Locale, Key);
        }
    }
}