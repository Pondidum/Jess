using System;
using System.Net.Http;

namespace Jess.Tests.Util
{
	public class RequestInfo
	{
		public Uri Url { get; private set; }
		public HttpRequestMessage Request { get; private set; }
		public HttpResponseMessage Response { get; private set; }

		public RequestInfo(Uri url, HttpRequestMessage request, HttpResponseMessage response)
		{
			Url = url;
			Request = request;
			Response = response;
		}
	}
}