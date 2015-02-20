using System.Net.Http;
using System.Text;

namespace Jess.Tests.Util
{
	public class JsonResponse : HttpResponseMessage
	{
		public JsonResponse(string json)
		{
			Content = new StringContent(json, Encoding.UTF8, "application/json");
		}
	}
}
