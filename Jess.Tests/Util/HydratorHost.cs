using System;
using System.Net.Http;
using System.Web.Http;
using Jess.Infrastructure;
using Microsoft.Owin.Testing;
using Owin;

namespace Jess.Tests.Util
{
	public class HydratorHost : IDisposable
	{
		private readonly TestServer _server;

		public HydratorHost(IProxy proxy, ICache cache)
		{
			_server = TestServer.Create(appBuilder =>
			{
				var config = new HttpConfiguration();

				WebApiConfig.Register(config, proxy, cache);

				appBuilder.UseWebApi(config);

			});
		}

		public HttpResponseMessage MakeRequest(string relativeUri, HttpRequestMessage request)
		{
			request.RequestUri = new Uri(relativeUri, UriKind.Relative);

			return _server
				.HttpClient
				.SendAsync(request).Result;
		}

		public void Dispose()
		{
			_server.Dispose();
		}
	}
}
