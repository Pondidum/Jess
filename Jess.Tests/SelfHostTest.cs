using System.Net;
using System.Net.Http;
using Shouldly;
using Xunit;

namespace Jess.Tests
{
	public class SelfHostTest
	{
		[Fact]
		public void Self_hosting_respeonds()
		{
			var self = new SelfHost(48321);
			self.Start();

			self
				.MakeRequest("", new HttpRequestMessage())
				.StatusCode
				.ShouldBe(HttpStatusCode.OK);

			self.Stop();
		}
	}
}
