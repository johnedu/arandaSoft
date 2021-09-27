using ArandaSoft.EntityFramework.Interfaces;
using ArandaSoft.EntityFramework.Model;
using ArandaSoft.EntityFramework.Repositories;

namespace ArandaSoft.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private ArandaSoftModel _dbContext;
        private BaseRepository<AppUser> _appUserRepository;
        private BaseRepository<AppRole> _appRoleRepository;
        private BaseRepository<AppRolePermission> _appUserRoleRepository;
        private BaseRepository<AppPermission> _appPermissionRepository;

        public UnitOfWork()
        {
            _dbContext = new ArandaSoftModel();
        }

        public IRepository<AppUser> AppUsers
        {
            get
            {
                return _appUserRepository ?? (_appUserRepository = new BaseRepository<AppUser>(_dbContext));
            }
        }

        public IRepository<AppRole> AppRoles
        {
            get
            {
                return _appRoleRepository ?? (_appRoleRepository = new BaseRepository<AppRole>(_dbContext));
            }
        }

        public IRepository<AppRolePermission> AppUserRoles
        {
            get
            {
                return _appUserRoleRepository ?? (_appUserRoleRepository = new BaseRepository<AppRolePermission>(_dbContext));
            }
        }

        public IRepository<AppPermission> AppPermissions
        {
            get
            {
                return _appPermissionRepository ?? (_appPermissionRepository = new BaseRepository<AppPermission>(_dbContext));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
