using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jess.Controllers
{
	public class ManageController : ApiController
	{
		public HttpResponseMessage Post(string type, string id, [FromBody] string json)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public HttpResponseMessage Delete(string type, string id)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public HttpResponseMessage Get(string type, string id)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public HttpResponseMessage Delete(string type)
		{
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
	}
}
