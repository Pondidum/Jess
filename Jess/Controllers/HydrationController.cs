using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Jess.Infrastructure;

namespace Jess.Controllers
{
	public class HydrationController : ApiController
	{
		private readonly IProxy _proxy;
		private readonly ResponseHydrator _hydrator;

		public HydrationController(IProxy proxy, ResponseHydrator hydrator)
		{
			_proxy = proxy;
			_hydrator = hydrator;
		}

		public HttpResponseMessage Get(HttpRequestMessage request)
		{
			var response = _proxy.MakeRequest(CreateUpstreamRequest(request));

			var token = response
				.Headers
				.Get(Headers.Hydrate)
				.FirstOrDefault();

			if (token == null)
			{
				return response;
			}

			var inputStream = response
				.Content
				.ReadAsStreamAsync()
				.Result;

			response.Content = new PushStreamContent((stream, httpContent, transportContext) =>
			{
				_hydrator.Hydrate(token, inputStream, stream);

				stream.Flush();
				stream.Close();

			}, response.Content.Headers.ContentType.MediaType);

			return response;
		}

		public HttpRequestMessage CreateUpstreamRequest(HttpRequestMessage inboundRequest)
		{
			var upstream = inboundRequest
				.Headers
				.Get(Headers.Upstream)
				.Select(val => new Uri(val))
				.FirstOrDefault();

			if (upstream == null)
			{
				throw new MissingHeaderException(Headers.Upstream);
			}

			inboundRequest.RequestUri = new Uri(upstream, inboundRequest.RequestUri);
			inboundRequest.Headers.Remove(Headers.Upstream);

			return inboundRequest;
		}
	}
}
