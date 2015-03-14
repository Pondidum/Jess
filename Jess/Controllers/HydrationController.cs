using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jess.Controllers
{
	public class HydrationController : ApiController
	{
		private readonly ResponseHydrator _hydrator;

		public HydrationController(ResponseHydrator hydrator)
		{
			_hydrator = hydrator;
		}

		public HttpResponseMessage Get(HttpRequestMessage request)
		{
			var client = new HttpClient();

			var proxyRequest = new HttpRequestMessage
			{
				RequestUri = ModifyUri(request.RequestUri),
				Method = request.Method,
			};

			request.Headers.ToList().ForEach(header => proxyRequest.Headers.Add(header.Key, header.Value));

			var response = client
				.SendAsync(proxyRequest)
				.Result;

			if (response.Headers.Contains("X-Hydrate"))
			{
				response.Content = new PushStreamContent((stream, httpContent, transportContext) =>
				{
					_hydrator.Hydrate(response.Content.ReadAsStreamAsync().Result, stream);

					stream.Flush();
					stream.Close();
				}, response.Content.Headers.ContentType.MediaType);
			}

			return response;
		}

		public Uri ModifyUri(Uri request)
		{
			var builder = new UriBuilder(request);
			builder.Port = 48654;

			return builder.Uri;
		}
	}
}
