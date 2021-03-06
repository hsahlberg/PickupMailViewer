﻿using PickupMailViewer.Helpers;
using PickupMailViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PickupMailViewer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MailWatcher.Init();
            Task.Factory.StartNew(FillCache);
        }

        private void FillCache()
        {
            var mailPaths = MailHelper.ListMailFiles(Properties.Settings.Default.MailDir);
            foreach (var mailPath in mailPaths.Reverse()) // Fill cache from reverse to not run from the same direction as HomeController.GetMailListModel
            {
                MailHelper.ReadMessage(mailPath);
            }
        }
    }
}
