using System.Collections.Generic;

namespace Jess
{
	public interface ICache
	{
		string Get(string type, string id);

		void Add(string type, string id, string json);
		void Remove(string type, string id);
		void Clear(string type);

		IEnumerable<CacheStat> GetCacheStats();
	}
}
