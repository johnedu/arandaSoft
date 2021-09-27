using ArandaSoft.Core.Domain;
using ArandaSoft.Core.Model.ValueObjects;
using ArandaSoft.Model.ValueObjects;
using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArandaSoft.Controllers
{
    public class AccountController : Controller
    {
        public IAccountDomainService _accountDomainService;

        private readonly MapperConfiguration config = new AutoMapperConfig().Configure();
        public IMapper _mapper;

        public AccountController()
        {
            _accountDomainService = new AccountDomainService();
            _mapper = config.CreateMapper();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string userName, string password)
        {
            try
            {
                AppUserModel appUserModel = await _accountDomainService.LoginUser(userName, password);
                if (appUserModel == null)
                {
                    ViewBag.Error = "Usuario o contraseña invalida";
                    return View();
                }
                else
                {
                    Session["UserSession"] = _mapper.Map<UserSession>(appUserModel);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}