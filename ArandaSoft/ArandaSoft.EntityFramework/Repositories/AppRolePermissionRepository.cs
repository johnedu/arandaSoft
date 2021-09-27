using ArandaSoft.EntityFramework.Interfaces;
using ArandaSoft.EntityFramework.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ArandaSoft.EntityFramework.Repositories
{
    public class AppRolePermissionRepository : IAppRolePermissionRepository
    {
        private readonly ArandaSoftModel arandaSoftModel;

        public AppRolePermissionRepository(ArandaSoftModel arandaSoftModel)
        {
            this.arandaSoftModel = arandaSoftModel;
        }

        public async Task<List<AppRolePermission>> GetAllByRole(int roleId)
        {
            return await arandaSoftModel.AppRolePermission.Where(x => x.RoleId == roleId).ToListAsync();
        }

        public void Save()
        {
            arandaSoftModel.SaveChanges();
        }
    }
}