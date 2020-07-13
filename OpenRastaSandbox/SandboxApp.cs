using OpenRasta.Configuration;
using OpenRasta.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenRastaSandbox
{
    public class SandboxApp : IConfigurationSource, IDependencyResolverAccessor
    {
        public IDependencyResolver Resolver => throw new NotImplementedException();

        public void Configure()
        {
            throw new NotImplementedException();
        }
    }
}
