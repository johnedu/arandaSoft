using ArandaSoft.Core.Domain;
using ArandaSoft.Core.Model.ValueObjects;
using ArandaSoft.Model.Consts;
using ArandaSoft.Model.InputModels;
using ArandaSoft.Model.OutputModels;
using ArandaSoft.Model.ValueObjects;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ArandaSoft.Controllers
{
    public class HomeController : Controller
    {
        public IAccountDomainService _accountDomainService;
        private readonly MapperConfiguration config = new AutoMapperConfig().Configure();
        public IMapper _mapper;

        public HomeController()
        {
            _accountDomainService = new AccountDomainService();
            _mapper = config.CreateMapper();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Aranda Software";
            ViewData["userSession"] = GetUserSession();

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserSession"] = null;

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Users(string messageError)
        {
            UserSession userSession = GetUserSession();
            if (userSession.Permissions.Count == 0 || userSession.Permissions.All(x => x != ArandaSoftConsts.UserListPermission))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["userSession"] = userSession;
                List<AppUserModel> userList = _accountDomainService.GetAppUsers();
                ViewBag.Error = messageError;

                return View(_mapper.Map<List<AppUserOutput>>(userList));
            }
        }

        public ActionResult CreateUser()
        {
            UserSession userSession = GetUserSession();
            if (userSession.Permissions.Count == 0 || userSession.Permissions.All(x => x != ArandaSoftConsts.CreateUserPermission))
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<AppRoleModel> roleList = _accountDomainService.GetAppRoles();
                List<SelectListItem> selectRoleList = new List<SelectListItem>();
                selectRoleList.AddRange(roleList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }));
                ViewBag.RoleList = selectRoleList;
                ViewData["userSession"] = userSession;

                return View();
            }
        }

        public ActionResult UpdateUser(int appUserId)
        {
            UserSession userSession = GetUserSession();
            if (userSession.Permissions.Count == 0 || userSession.Permissions.All(x => x != ArandaSoftConsts.EditUserPermission))
            {
                return RedirectToAction("Index");
            }
            else
            {
                AppUserModel appUserModel = _accountDomainService.GetUserById(appUserId);
                List<AppRoleModel> roleList = _accountDomainService.GetAppRoles();
                List<SelectListItem> selectRoleList = new List<SelectListItem>();
                selectRoleList.AddRange(roleList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }));
                ViewBag.RoleList = selectRoleList;
                ViewData["userSession"] = userSession;

                return View(_mapper.Map<AppUserOutput>(appUserModel));
            }
        }

        public ActionResult SaveNewUser(AppUserInput appUserInput)
        {
            AppUserModel appUserModel = _mapper.Map<AppUserModel>(appUserInput);
            _accountDomainService.InsertUser(appUserModel);

            return RedirectToAction("Users");
        }

        public ActionResult SaveUpdatedUser(AppUserInput appUserInput)
        {
            UserSession userSession = GetUserSession();
            string errorMessage = "";
            if (userSession.Id == appUserInput.Id)
            {
                errorMessage = "No se puede modificar el rol del usuario actual en sesión";
            }
            else
            {
                AppUserModel appUserModel = _mapper.Map<AppUserModel>(appUserInput);
                _accountDomainService.UpdateUser(appUserModel);
            }

            return RedirectToAction("Users", new { messageError = errorMessage });
        }

        public ActionResult DeleteUser(int appUserId)
        {
            UserSession userSession = GetUserSession();
            if (userSession.Permissions.Count == 0 || userSession.Permissions.All(x => x != ArandaSoftConsts.DeleteUserPermission))
            {
                return RedirectToAction("Index");
            }
            else
            {
                string errorMessage = "";
                if (userSession.Id == appUserId)
                {
                    errorMessage = "No se puede eliminar el usuario actual en sesión";
                }
                else
                {
                    AppUserModel appUserModel = _accountDomainService.GetUserById(appUserId);
                    if (appUserModel.RoleName == ArandaSoftConsts.AdmonRole) {
                        errorMessage = "No se puede eliminar un usuario con rol Administrador";
                    }
                    else
                    {
                        _accountDomainService.DeleteUser(appUserId);
                    }
                }

                return RedirectToAction("Users", new { messageError = errorMessage });
            }
        }

        private UserSession GetUserSession()
        {
            return (UserSession)Session["UserSession"];
        }
    }
}
