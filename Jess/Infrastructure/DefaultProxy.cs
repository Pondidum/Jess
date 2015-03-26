using System.Net.Http;

namespace Jess.Infrastructure
{
	public class DefaultProxy : IProxy
	{
		private readonly  HttpClient _client;

		public DefaultProxy()
		{
			_client = new HttpClient();
		}

		public HttpResponseMessage MakeRequest(HttpRequestMessage request)
		{
			return _client
				.SendAsync(request)
				.Result;
		}
	}
}
