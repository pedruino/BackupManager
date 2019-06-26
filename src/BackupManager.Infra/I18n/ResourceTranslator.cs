using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace BackupManager.Infra.I18n
{
    public sealed class ResourceTranslator : IResourceTranslator
    {
        public const string DEFAULT_LOCALE = "pt-BR";
        private readonly string _resourceDirectory;
        private readonly IDictionary<string, IDictionary<string, string>> _resourceLocales = new Dictionary<string, IDictionary<string, string>>();

        private ResourceTranslator(string resourceDirectory)
        {
            _resourceDirectory = resourceDirectory;
        }

        /// <inheritdoc />
        public string Translate(string resourceKey)
        {
            return Translate(resourceKey, DEFAULT_LOCALE);
        }

        /// <inheritdoc />
        public string Translate(string resourceKey, string locale)
        {
            if (_resourceLocales.TryGetValue(locale, out var resources))
            {
                return Translate(resourceKey, resources);
            }

            resources = LoadResourceJson(locale);

            return Translate(resourceKey, resources);
        }

        private static string Translate(string resourceKey, IDictionary<string, string> resources)
        {
            return resources.TryGetValue(resourceKey, out var translatedValue) ? translatedValue : resourceKey;
        }

        private IDictionary<string, string> LoadResourceJson(string locale)
        {
            if (_resourceLocales.TryGetValue(locale, out var resources))
            {
                Debug.WriteLine("Resource already loaded.");
                return resources;
            }

            var jsonFilePath = $"{_resourceDirectory}/{locale}.json";

            var jsonText = System.IO.File.ReadAllText(jsonFilePath);
            resources = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);

            _resourceLocales.Add(locale, resources);

            return resources;
        }
    }
}