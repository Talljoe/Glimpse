using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glimpse.Net.Plumbing
{
    public class GlimpseDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver resolver;

        public GlimpseDependencyResolver(IDependencyResolver resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            var result = this.resolver.GetService(serviceType);
            Store.Add(new GlimpseDependencyMetadata
                {
                    Call = "GetService",
                    RequestedType = serviceType,
                    ReturnedTypes = result == null ? Enumerable.Empty<Type>() : new [ ] { result.GetType() },
                });
            return BuildUp(serviceType, result);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var services = this.resolver.GetServices(serviceType);
            Store.Add(new GlimpseDependencyMetadata
            {
                Call = "GetServices",
                RequestedType = serviceType,
                ReturnedTypes = (services ?? Enumerable.Empty<object>()).Select(s => s.GetType()),
            });
            return services == null ? null : services.Select(o => BuildUp(serviceType, o));
        }

        public IList<GlimpseDependencyMetadata> Store
        {
            get
            {
                var items = HttpContext.Current.Items;
                var store = items[GlimpseConstants.Dependencies] as IList<GlimpseDependencyMetadata>;
                if (store == null) items[GlimpseConstants.Dependencies] = store = new List<GlimpseDependencyMetadata>();

                return store;
            }
        }

        private static object BuildUp(Type serviceType, object o)
        {
            //TODO: Wrap with Glimpse objects
            return o;
        }
    }
}