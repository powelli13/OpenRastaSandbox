using OpenRasta.Codecs.Newtonsoft.Json;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.DI.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenRastaSandbox
{
    public class SandboxApp : IConfigurationSource, IDependencyResolverAccessor
    {
        public IDependencyResolver Resolver { get; } = new WindsorDependencyResolver();

        class PlanetData
        {
            public string Name { get; set; }

            public class Handler
            {
                public PlanetData Get() => 
                    new PlanetData() { Name = "Earth" };
            }
        }

        class RootResponse
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public class RootHandler
            {
                public RootResponse Get() =>
                    new RootResponse() { Name = "Hi there", Age = 100 };
            }
        }

        public void Configure()
        {
            ResourceSpace.Has
                .ResourcesOfType<RootResponse>()
                .AtUri("/")
                .Named("root")
                .HandledBy<RootResponse.RootHandler>()
                .TranscodedBy<NewtonsoftJsonCodec>()
                .ForMediaType("application/json");

            ResourceSpace.Has
                .ResourcesOfType<PlanetData>()
                .AtUri("/planets")
                .Named("planets")
                .HandledBy<PlanetData.Handler>()
                .TranscodedBy<NewtonsoftJsonCodec>()
                .ForMediaType("application/json");
        }
    }
}
