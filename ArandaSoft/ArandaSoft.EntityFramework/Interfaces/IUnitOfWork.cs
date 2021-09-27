using ArandaSoft.EntityFramework.Model;

namespace ArandaSoft.EntityFramework.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<AppUser> AppUsers { get; }
        IRepository<AppRole> AppRoles { get; }
        IRepository<AppRolePermission> AppUserRoles { get; }
        IRepository<AppPermission> AppPermissions { get; }
        void Commit();
    }
}