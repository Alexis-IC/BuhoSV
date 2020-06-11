using Buho.Controllers;
using Buho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buho.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        private Usuarios objUsuario;


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            try
            {
                objUsuario = (Usuarios)HttpContext.Current.Session["Usuario"];

                if (objUsuario == null)
                {
                    if (filterContext.Controller is HomeController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Home/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Login");
            }
            
        }
    }
}