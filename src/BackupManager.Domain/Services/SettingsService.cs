using BackupManager.Domain.Entities;
using BackupManager.Domain.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace BackupManager.Domain.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly bool _createIfNotExists;
        private readonly string _fullPathFileName;
        private readonly string _path;

        public SettingsService(string path, string settingsFileName, bool createIfNotExists)
        {
            _createIfNotExists = createIfNotExists;
            _path = path;
            _fullPathFileName = Path.Combine(_path, settingsFileName);
        }

        public SettingsService(string path, string settingsFileName) : this(path, settingsFileName, true)
        {
        }

        public ISettings Load()
        {
            var json = string.Empty;

            if (!File.Exists(_fullPathFileName) && _createIfNotExists)
            {
                try
                {
                    Directory.CreateDirectory(_path);
                    var settings = new Settings();

                    Save(settings);
                }
                catch (System.Exception)
                {
                    Debug.WriteLine($"Error writing settings. Check your permissions.");
                }
            }
            else
            {
                json = File.ReadAllText(_fullPathFileName);
            }

            return JsonConvert.DeserializeObject<Settings>(json);
        }

        public void Save(ISettings settings)
        {
            File.WriteAllText(_fullPathFileName, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
    }
}