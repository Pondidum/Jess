using System.Collections.Generic;

namespace Jess.Tests.Util
{
	public class Wrapper
	{
		public List<RequestInfo> Recieved { get; private set; }

		public Wrapper()
		{
			Recieved = new List<RequestInfo>();
		}
	}
}