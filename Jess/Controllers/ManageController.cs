using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace Jess.Controllers
{
	public class ManageController : ApiController
	{
		private readonly ICache _cache;

		public ManageController(ICache cache)
		{
			_cache = cache;
		}

		public HttpResponseMessage Get()
		{
			var cacheStats = _cache.GetCacheStats();
			var json = JsonConvert.SerializeObject(cacheStats);

			return new HttpResponseMessage
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
		}

		public HttpResponseMessage Get(string type, string id)
		{
			var json = _cache.Get(type, id);

			return new HttpResponseMessage
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json")
			};
		}

		public HttpResponseMessage Post(string type, string id, [FromBody] string json)
		{
			_cache.Add(type, id, json);
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public HttpResponseMessage Delete(string type, string id)
		{
			_cache.Remove(type, id);
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		public HttpResponseMessage Delete(string type)
		{
			_cache.Clear(type);
			return new HttpResponseMessage(HttpStatusCode.OK);
		}
	}
}
