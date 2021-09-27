using ArandaSoft.EntityFramework.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArandaSoft.EntityFramework.Interfaces
{
    public interface IAppRolePermissionRepository
    {
        Task<List<AppRolePermission>> GetAllByRole(int roleId);
        void Save();
    }
}