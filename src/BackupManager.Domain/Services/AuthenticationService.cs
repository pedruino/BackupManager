using System;
using System.Net.Http;
using System.Threading.Tasks;
using BackupManager.Domain.Entities;
using BackupManager.Domain.Interfaces;
using BackupManager.Domain.Response;
using Newtonsoft.Json;

namespace BackupManager.Domain.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        
        private readonly Uri _baseAddress = new Uri("http://crm.inovatechcaxias.com.br");

        public async Task<ISettingsCustomer> AuthenticateUser(string username, string clearedPassword)
        {
            ISettingsCustomer customer;
            using (var httpClient = new HttpClient() { BaseAddress = _baseAddress })
            {
                var data = new
                {
                    username,
                    password = HashService.GenerateSaltedHash(clearedPassword),
                };

                var response = await httpClient.PostAsJsonAsync("/clientes/auth", data).ConfigureAwait(false);
                var json = await response.Content.ReadAsStringAsync();
                var settings = JsonConvert.DeserializeObject<CustomerAuthReponse>(json);

                customer = settings.Cliente.BackupSettings.Customer;

                //TODO: Criar o arquivo de configurações
                new SettingsService().Save(settings.Cliente.BackupSettings);
            }
            return customer;
        }

        public bool IsAuthorized()
        {
            using (var httpClient = new HttpClient() { BaseAddress = _baseAddress })
            {
                //httpClient.PutAsJsonAsync("/clientes/auth", new { username, password });
            }
            return true;
        }
    }
}

namespace BackupManager.Domain.Response
{
    using Newtonsoft.Json;

    public partial class CustomerAuthReponse
    {
        [JsonProperty("Cliente")]
        public Cliente Cliente { get; set; }
    }

    public partial class Cliente
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cpfcnpj")]
        public string Cpfcnpj { get; set; }

        [JsonProperty("fantasia")]
        public string Fantasia { get; set; }

        [JsonProperty("razaosocial")]
        public string Razaosocial { get; set; }

        [JsonProperty("ie")]
        public string Ie { get; set; }

        [JsonProperty("backup_is_active")]
        public bool BackupIsActive { get; set; }

        [JsonProperty("backup_settings")]
        public Settings BackupSettings { get; set; }
    }
}
