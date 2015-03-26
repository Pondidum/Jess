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

			var upstream = request.Headers.GetValues("X-Upstream").First();

			var proxyRequest = new HttpRequestMessage
			{
				RequestUri = ModifyUri(upstream, request.RequestUri),
				Method = request.Method,
			};

			request.Headers.ToList().ForEach(header => proxyRequest.Headers.Add(header.Key, header.Value));

			var response = client
				.SendAsync(proxyRequest)
				.Result;

			if (response.Headers.Contains("X-Hydrate"))
			{
				var token = response.Headers.GetValues("X-Hydrate").First();
				var inputStream = response.Content.ReadAsStreamAsync().Result;

				response.Content = new PushStreamContent((stream, httpContent, transportContext) =>
				{
					_hydrator.Hydrate(token, inputStream, stream);

					stream.Flush();
					stream.Close();
				}, response.Content.Headers.ContentType.MediaType);
			}

			return response;
		}

		public Uri ModifyUri(string upstream, Uri request)
		{
			var builder = new UriBuilder(upstream);
			builder.Path = request.PathAndQuery;

			return builder.Uri;
		}
	}
}
