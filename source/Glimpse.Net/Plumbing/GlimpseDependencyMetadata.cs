using System;
using System.Collections.Generic;

namespace Glimpse.Net.Plumbing
{
    public class GlimpseDependencyMetadata
    {
        public string Call { get; set; }
        public Type RequestedType { get; set; }
        public IEnumerable<Type> ReturnedTypes { get; set; }
    }
}