using ArandaSoft.Core.Model.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArandaSoft.Core.Domain
{
    public interface IAccountDomainService
    {
        Task<AppUserModel> LoginUser(string userName, string password);
        List<AppUserModel> GetAppUsers();
        AppUserModel GetUserById(int satelliteId);
        void InsertUser(AppUserModel appUserModel);
        void UpdateUser(AppUserModel appUserModel);
        void DeleteUser(int appUserId);
        List<AppRoleModel> GetAppRoles();
    }
}