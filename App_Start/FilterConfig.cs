﻿using System.Web;
using System.Web.Mvc;
using Buho.Filters;

namespace Buho
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new VerificarSession());
        }
    }
}
