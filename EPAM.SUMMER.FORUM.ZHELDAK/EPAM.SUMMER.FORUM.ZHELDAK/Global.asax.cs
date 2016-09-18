using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure;
using EPAM.SUMMER.FORUM.ZHELDAK.Providers;

namespace EPAM.SUMMER.FORUM.ZHELDAK
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(NinjectConfig.StandartKernel));
            ModelMetadataProviders.Current = new ConventionProvider();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string urlError = @"^4\d{2}$";
                string serverError = @"^5\d{2}$";
                if (Regex.IsMatch(httpException.GetHttpCode().ToString(), urlError, RegexOptions.IgnoreCase))
                    Response.Redirect($"~/Error/NotFound/?message={exception.Message}");
                if (Regex.IsMatch(httpException.GetHttpCode().ToString(), serverError, RegexOptions.IgnoreCase))
                    Response.Redirect($"~/Error/ServerError/?message={exception.Message}");
                else
                    Response.Redirect($"~/Error/DefaultError/?message={exception.Message}");
                Server.ClearError();

            }
        }
    }
}
