using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Jess.Tests.Util
{
	public class AllController : ApiController
	{
		private readonly List<RequestInfo> _recieved;

		public AllController(Wrapper wrapper)
		{
			_recieved = wrapper.Recieved;
		}

		public HttpResponseMessage Get(HttpRequestMessage request)
		{
			var response = new HttpResponseMessage();

			_recieved.Add(new RequestInfo(request.RequestUri, request, response));

			return response;
		}
	}
}