﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace FindLoveNowMaybe
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;
            string action;
            if (httpException != null)
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        action = "PageNotFound";
                        break;
                    case 500:
                        action = "ServerIssues";
                        break;
                    default:
                        action = "GeneralIssue";
                        break;
                }
                Server.ClearError();
                Response.Redirect(String.Format("~/Error/{0}/?exceptionMessage={1}", action, exception.Message));
            }
        }

    }
}