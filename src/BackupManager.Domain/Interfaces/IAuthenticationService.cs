using System.Threading.Tasks;

namespace BackupManager.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ISettingsCustomer> AuthenticateUser(string username, string clearedPassword);

        bool IsAuthorized();
    }
}