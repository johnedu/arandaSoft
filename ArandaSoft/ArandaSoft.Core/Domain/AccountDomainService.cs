using ArandaSoft.Core.Model.ValueObjects;
using ArandaSoft.EntityFramework.Interfaces;
using ArandaSoft.EntityFramework.Model;
using ArandaSoft.EntityFramework.Repositories;
using ArandaSoft.EntityFramework.UnitOfWork;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArandaSoft.Core.Domain
{
    public class AccountDomainService : IAccountDomainService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppRolePermissionRepository _appRolePermissionRepository;

        private readonly IMapper _mapper;

        public AccountDomainService()
        {
            MapperConfiguration config = new AutoMapperCoreConfig().Configure();
            ArandaSoftModel arandaSoftModel = new ArandaSoftModel();
            _appUserRepository = new AppUserRepository(arandaSoftModel);
            _appRolePermissionRepository = new AppRolePermissionRepository(arandaSoftModel);
            _mapper = config.CreateMapper();
        }

        public async Task<AppUserModel> LoginUser(string userName, string password)
        {
            AppUserModel appUserModel = null;
            AppUser appUser = await _appUserRepository.LoginUser(userName, password);
            if (appUser != null)
            {
                List<AppRolePermission> permissionsRole = await _appRolePermissionRepository.GetAllByRole(appUser.RoleId);
                appUserModel = _mapper.Map<AppUserModel>(appUser);
                appUserModel.Permissions = _mapper.Map<List<string>>(permissionsRole.Select(x => x.AppPermission.Name));
            }

            return appUserModel;
        }

        public List<AppUserModel> GetAppUsers() {
            IEnumerable<AppUser> appUsers = _unitOfWork.AppUsers.Get();

            return _mapper.Map<List<AppUserModel>>(appUsers);
        }

        public AppUserModel GetUserById(int satelliteId)
        {
            AppUser appUser = _unitOfWork.AppUsers.GetByID(satelliteId);

            return _mapper.Map<AppUserModel>(appUser);
        }

        public void InsertUser(AppUserModel appUserModel)
        {
            _unitOfWork.AppUsers.Insert(_mapper.Map<AppUser>(appUserModel));
            _unitOfWork.Commit();
        }

        public void UpdateUser(AppUserModel appUserModel)
        {
            AppUser appUser = _mapper.Map<AppUserModel, AppUser>(appUserModel);
            _unitOfWork.AppUsers.Update(appUser);
            _unitOfWork.Commit();
        }

        public void DeleteUser(int appUserId)
        {
            _unitOfWork.AppUsers.Delete(appUserId);
            _unitOfWork.Commit();
        }

        public List<AppRoleModel> GetAppRoles()
        {
            IEnumerable<AppRole> appRoles = _unitOfWork.AppRoles.Get();

            return _mapper.Map<List<AppRoleModel>>(appRoles);
        }

    }
}