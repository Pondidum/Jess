using System.Net;
using System.Net.Http;
using Jess.Tests.Util;
using Shouldly;
using Xunit;

namespace Jess.Tests
{
	public class SelfHostTest
	{
		[Fact]
		public void Self_hosting_respeonds()
		{
			var self = new HydratorHost();
			self.Start();

			self
				.MakeRequest("", new HttpRequestMessage())
				.StatusCode
				.ShouldBe(HttpStatusCode.OK);

			self.Stop();
		}

		[Fact]
		public void Remote_logs_values()
		{
			var remote = new RemoteHost();
			remote.Start();

			remote
				.MakeRequest("", new HttpRequestMessage())
				.StatusCode
				.ShouldBe(HttpStatusCode.OK);

			remote.Stop();

			remote.Recieved.ShouldNotBeEmpty();
		}
	}
}
