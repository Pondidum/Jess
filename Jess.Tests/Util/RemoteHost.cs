using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Jess.Infrastructure;
using Microsoft.Owin.Testing;
using Owin;
using StructureMap;
using StructureMap.Graph;

namespace Jess.Tests.Util
{
	public class RemoteHost : IProxy, IDisposable
	{
		public IEnumerable<RequestInfo> Recieved { get { return _wrapper.Recieved; } }

		private Wrapper _wrapper;
		private readonly TestServer _server;

		public RemoteHost()
		{
			_server = TestServer.Create(appBuilder =>
			{

				var config = new HttpConfiguration();

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

				appBuilder.UseWebApi(config);
			});

		}

		public void RespondsTo(string route, Func<HttpRequestMessage, HttpResponseMessage> response)
		{
			if (route.StartsWith("/") == false)
				route = "/" + route;

			_wrapper.Routes[route] = response;
		}

		public IProxy GetProxy()
		{
			return this;
		}

		public HttpResponseMessage MakeRequest(HttpRequestMessage request)
		{
			return _server
				.HttpClient
				.SendAsync(request)
				.Result;
		}

		public void Dispose()
		{
			_server.Dispose();
		}
	}
}
