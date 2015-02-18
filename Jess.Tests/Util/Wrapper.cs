using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Jess.Tests.Util
{
	public class Wrapper
	{
		public List<RequestInfo> Recieved { get; private set; }
		public Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>> Routes { get; private set; }

		public Wrapper()
		{
			Recieved = new List<RequestInfo>();
			Routes = new Dictionary<string, Func<HttpRequestMessage, HttpResponseMessage>>();
		}
	}
}