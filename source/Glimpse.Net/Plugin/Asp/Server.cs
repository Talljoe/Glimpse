﻿using System;
using System.Web;
using Glimpse.Net.Extensibility;
using Glimpse.Net.Extentions;

namespace Glimpse.Net.Plugin.ASP
{
    [GlimpsePlugin]
    internal class Server : IGlimpsePlugin
    {
        public string Name
        {
            get { return "Server"; }
        }

        public object GetData(HttpApplication application)
        {
            return application.Request.ServerVariables.Flatten();
        }

        public void SetupInit(HttpApplication application)
        {
            throw new NotImplementedException();
        }
    }
}
