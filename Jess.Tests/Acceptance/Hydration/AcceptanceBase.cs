using System;
using Jess.Tests.Util;

namespace Jess.Tests.Acceptance.Hydration
{
	public class AcceptanceBase : IDisposable
	{
		public HydratorHost Hydrator { get; private set; }
		public RemoteHost Remote { get; private set; }

		public AcceptanceBase()
		{
			Hydrator = new HydratorHost();
			Remote = new RemoteHost();

			Hydrator.Start();
			Remote.Start();
		}

		public void Dispose()
		{
			Hydrator.Stop();
			Remote.Stop();
		}
	}
}
