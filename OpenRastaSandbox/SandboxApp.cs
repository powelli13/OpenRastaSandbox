using OpenRasta.Codecs;
using OpenRasta.Codecs.Newtonsoft.Json;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.DI.Windsor;
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenRastaSandbox
{
    public class SandboxApp : IConfigurationSource, IDependencyResolverAccessor
    {
        public IDependencyResolver Resolver { get; } = new WindsorDependencyResolver();

        public class PlanetData
        {
            public string Name { get; set; }

            public class Handler
            {
                public OperationResult Get()
                {
                    return new OperationResult.OK
                    { 
                        ResponseResource = new PlanetData() { Name = "Earth" }
                    };
                }
            }
        }

        public class RootResponse
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
                .ResourcesOfType<IEnumerable<RootResponse>>()
                .AtUri("/")
                .Named("root")
                .HandledBy<RootResponse.RootHandler>()
                .TranscodedBy<NewtonsoftJsonCodec>()
                .ForMediaType("application/json");

            ResourceSpace.Has
                .ResourcesOfType<IEnumerable<PlanetData>>()
                .AtUri("/planets")
                .HandledBy<PlanetData.Handler>()
                //.AsXmlDataContract()
                //.And.AsJsonDataContract();
                .TranscodedBy<TextPlainCodec>()
                .ForMediaType("text/plain");
        }
    }
}
