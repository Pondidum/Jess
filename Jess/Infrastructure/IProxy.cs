using System.Net.Http;

namespace Jess.Infrastructure
{
	public interface IProxy
	{
		HttpResponseMessage MakeRequest(HttpRequestMessage request);
	}
}
