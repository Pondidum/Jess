using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Jess.Infrastructure
{
	public static class Extensions
	{
		public static IEnumerable<string> Get(this HttpHeaders headers, string name)
		{
			return headers
				.Where(h => string.Equals(h.Key, name, StringComparison.OrdinalIgnoreCase))
				.SelectMany(h => h.Value);
		}
	}
}