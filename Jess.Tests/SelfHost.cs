using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Jess.Tests
{
	public class SelfHost : IDisposable
	{
		private HttpSelfHostServer _host;
		private readonly Uri _url;

		public SelfHost(int port)
		{
			_url = new UriBuilder(Uri.UriSchemeHttp, "localhost", port).Uri;
		}

		public void Configure(Action<HttpConfiguration> configure)
		{
			var config = new HttpSelfHostConfiguration(_url);

			configure(config);

			_host = new HttpSelfHostServer(config);
		}

		public void Start()
		{
			_host.OpenAsync().Wait();
		}

		public void Stop()
		{
			_host.CloseAsync().Wait();
		}

		public HttpResponseMessage MakeRequest(string relativeUri, HttpRequestMessage request)
		{
			request.RequestUri = new Uri(_url, relativeUri);

			return new HttpClient()
				.SendAsync(request)
				.Result;
		}

		public void Dispose()
		{
			Stop();
		}
	}
}
