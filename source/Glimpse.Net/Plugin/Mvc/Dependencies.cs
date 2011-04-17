using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glimpse.Net.Plumbing;
using Glimpse.Protocol;

namespace Glimpse.Net.Plugin.Mvc
{
    [GlimpsePlugin(ShouldSetupInInit = true)]
    public class Dependencies : IGlimpsePlugin
    {
        public object GetData(HttpApplication application)
        {
            var calls = application.Context.Items[GlimpseConstants.Dependencies] as IList<GlimpseDependencyMetadata>;
            if(calls == null)
            {
                return null;
            }
            var header = new[] { "Call", "Requested Type", "Returned Types" };
            var values = calls.Select(
                data => new[]
                            {
                                data.Call,
                                data.RequestedType.FullName,
                                String.Join(", ", data.ReturnedTypes.Select(t => t.FullName)),
                            });

            return new [] { header }.Concat(values).ToArray();
        }

        public void SetupInit(HttpApplication application)
        {
            var setResolver = DependencyResolver.Current;
            if (setResolver != null && !(setResolver is GlimpseDependencyResolver))
            {
                DependencyResolver.SetResolver(new GlimpseDependencyResolver(setResolver));
            }
        }

        public string Name
        {
            get { return "Dependencies"; }
        }
    }
}