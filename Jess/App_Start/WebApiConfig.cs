using System.Web.Http;
using Jess.Caches;
using Jess.Infrastructure;
using StructureMap;
using StructureMap.Graph;

namespace Jess
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			var container = new Container(c =>
			{
				c.Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});

				c.For<ICache>().Use<DefaultCache>();
			});

			config.DependencyResolver = new StructureMapDependencyResolver(container);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "Manage",
				routeTemplate: "manage/{type}/{id}/",
				defaults: new { controller = "Manage", id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "Default",
				routeTemplate: "{*url}",
				defaults: new { controller = "Hydration", action = "get" }
			);
		}
	}
}
