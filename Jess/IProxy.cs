using System.Net.Http;

namespace Jess
{
	public interface IProxy
	{
		HttpResponseMessage MakeRequest(HttpRequestMessage request);
	}
}
