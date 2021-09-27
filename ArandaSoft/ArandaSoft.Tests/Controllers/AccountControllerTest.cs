using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArandaSoft.Controllers;
using System.Threading.Tasks;
using ArandaSoft.Core.Domain;
using Moq;
using System.Collections.Generic;
using ArandaSoft.Core.Model.ValueObjects;
using AutoMapper;
using ArandaSoft.Model.ValueObjects;

namespace ArandaSoft.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void Login()
        {
            // Disponer
            AccountController controller = new AccountController();

            // Actuar
            ViewResult result = controller.Login() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task LoginPostOk()
        {
            // Disponer
            AppUserModel appUserModel = new AppUserModel
            {
                Id = 1,
                Name = "admon",
                Password = "123",
                FullName = "Administrador",
                EmailAddress = "admon@arandasoft.com",
                Address = "Dg. 97 # 17-60",
                PhoneNumber = "7563000",
                Age = 20,
                RoleId = 1,
                RoleName = "Administrador",
                Permissions = new List<string> {
                    "UserListPermission",
                    "EditUserPermission",
                    "DeleteUserPermission",
                    "CreateUserPermission"
                }
            };
            UserSession userSession = new UserSession {
                Id = 1,
                Name = "admon",
                RoleId = 1,
                RoleName = "Administrador",
                Permissions = new List<string> {
                    "UserListPermission",
                    "EditUserPermission",
                    "DeleteUserPermission",
                    "CreateUserPermission"
                }
            };
            AccountController controller = new AccountController();
            Mock<IAccountDomainService> accountDomainServiceMock = new Mock<IAccountDomainService>();
            accountDomainServiceMock.SetupAllProperties();
            accountDomainServiceMock.Setup(m => m.LoginUser("admon", "123")).Returns(Task.FromResult(appUserModel));
            controller._accountDomainService = accountDomainServiceMock.Object;
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            mapperMock.SetupAllProperties();
            mapperMock.Setup(m => m.Map<UserSession>(It.IsAny<AppUserModel>())).Returns(userSession);
            controller._mapper = mapperMock.Object;

            // Actuar
            ViewResult result = (await controller.Login("admon", "123")) as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task LoginPostUserInvalid()
        {
            // Disponer
            AccountController controller = new AccountController();
            Mock<IAccountDomainService> accountDomainServiceMock = new Mock<IAccountDomainService>();
            accountDomainServiceMock.SetupAllProperties();
            accountDomainServiceMock.Setup(m => m.LoginUser("admon", "111")).Returns(Task.FromResult((AppUserModel)null));
            controller._accountDomainService = accountDomainServiceMock.Object;

            // Actuar
            ViewResult result = (await controller.Login("admon", "111")) as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Usuario o contraseña invalida", result.ViewBag.Error);
        }
    }
}
