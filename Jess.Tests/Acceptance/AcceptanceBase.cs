using System;
using Jess.Caches;
using Jess.Tests.Util;

namespace Jess.Tests.Acceptance
{
	public class AcceptanceBase : IDisposable
	{
		public HydratorHost Hydrator { get; private set; }
		public RemoteHost Remote { get; private set; }

		public AcceptanceBase()
		{
			Remote = new RemoteHost();
			Hydrator = new HydratorHost(Remote, new DefaultCache());
		}

		public void Dispose()
		{
			Hydrator.Dispose();
			Remote.Dispose();
		}
	}
}
