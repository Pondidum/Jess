using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jess.Tests.Util
{
	public class AllController : ApiController
	{
		private readonly Wrapper _wrapper;

		public AllController(Wrapper wrapper)
		{
			_wrapper = wrapper;
		}

		public HttpResponseMessage Get(HttpRequestMessage request)
		{
			Func<HttpRequestMessage, HttpResponseMessage> responseBuilder;

			_wrapper.Routes.TryGetValue(request.RequestUri.AbsolutePath, out responseBuilder);

			var response = responseBuilder != null
				? responseBuilder(request)
				: new HttpResponseMessage(HttpStatusCode.NotFound);

			_wrapper.Recieved.Add(new RequestInfo(request.RequestUri, request, response));

			return response;
		}
	}
}