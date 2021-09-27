using ArandaSoft.Controllers;
using ArandaSoft.Model.ValueObjects;
using System;
using System.Web;
using System.Web.Mvc;

namespace ArandaSoft.Security
{
    public class VerificationSession : ActionFilterAttribute
    {
        private UserSession userSession;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                userSession = (UserSession)HttpContext.Current.Session["UserSession"];
                if (userSession == null)
                {

                    if (filterContext.Controller is AccountController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Account/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }

        }
    }
}