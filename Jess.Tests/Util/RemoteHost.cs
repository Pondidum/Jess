using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using StructureMap;
using StructureMap.Graph;

namespace Jess.Tests.Util
{
	public class RemoteHost : SelfHost
	{
		public IEnumerable<RequestInfo> Recieved { get { return _wrapper.Recieved;  } }

		private Wrapper _wrapper;

		public RemoteHost()
			: base(48654)
		{

			Configure(config =>
			{
				config.Routes.MapHttpRoute(
					name: "Default",
					routeTemplate: "{*url}",
					defaults: new { controller = "All", action = "get" }
				);

				var container = new Container(c =>
				{
					c.Scan(a =>
					{
						a.TheCallingAssembly();
						a.WithDefaultConventions();
					});

					c.For<Wrapper>().Singleton();
				});

				_wrapper = container.GetInstance<Wrapper>();

				config.DependencyResolver = new StructureMapDependencyResolver(container);

			});
		}

		public void RespondsTo(string route, Func<HttpRequestMessage, HttpResponseMessage> response)
		{
			_wrapper.Routes[route] = response;
		}
	}
}
