using BackupManager.Domain.Interfaces;
using BackupManager.Domain.Services;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace BackupManager.Domain.Entities
{
    public sealed class SettingsCustomer : BindableBase, ISettingsCustomer
    {
        private string _id;
        private string _name;

        public SettingsCustomer(string id, string name)
        {
            _id = id;
            _name = name;
            Hash = HashService.GenerateHash(id);
        }

        internal SettingsCustomer()
        {
        }

        [JsonIgnore]
        public string Hash { get; private set; }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value, () => { Hash = HashService.GenerateHash(_id); });
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
    }
}