using System;
using System.Collections.Generic;
using Jess.Infrastructure;

namespace Jess.Caches
{
	public class DefaultCache : ICache
	{
		private readonly Cache<string, Dictionary<string, string>> _caches;

		public DefaultCache()
		{
			//oh for some F# types :(
			_caches = new Cache<string, Dictionary<string, string>>(
				new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase),
				key => new Dictionary<string, string>());
		}

		public string Get(string type, string id)
		{
			var cache = _caches[type];

			string json;
			return cache.TryGetValue(id, out json)
				? json
				: string.Empty;
		}

		public void Add(string type, string id, string json)
		{
			_caches[type][id] = json;
		}

		public void Remove(string type, string id)
		{
			_caches[type].Remove(id);
		}

		public void Clear(string type)
		{
			_caches[type].Clear();
		}
	}
}
