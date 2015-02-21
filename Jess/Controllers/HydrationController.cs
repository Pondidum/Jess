using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Jess.Controllers
{
	public class HydrationController : ApiController
	{
		public HttpResponseMessage Get(HttpRequestMessage request)
		{
			var client = new HttpClient();

			var proxyRequest = new HttpRequestMessage
			{
				RequestUri = ModifyUri(request.RequestUri),
				Method = request.Method,
			};

			request.Headers.ToList().ForEach(header => proxyRequest.Headers.Add(header.Key, header.Value));

			var response =client
				.SendAsync(proxyRequest)
				.Result;

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
