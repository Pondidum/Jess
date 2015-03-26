using System.Linq;
using System.Net;
using System.Net.Http;
using Shouldly;
using Xunit;

namespace Jess.Tests.Acceptance.Hydration
{
	public class OnRequest : AcceptanceBase
	{
		public OnRequest()
		{
			Remote.RespondsTo("/some/endpoint/234", request => new HttpResponseMessage(HttpStatusCode.OK));
		}

		[Fact]
		public void When_requesting_a_valid_remote_url()
		{
			var response = Hydrator.MakeRequest("some/endpoint/234", BuildMessage(new HttpRequestMessage()));

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
			Remote.Recieved.Count().ShouldBe(1);
		}

		[Fact]
		public void When_requesting_an_invalid_remote_url()
		{
			var response = Hydrator.MakeRequest("some/endpoint/789", BuildMessage(new HttpRequestMessage()));

			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
			Remote.Recieved.Count().ShouldBe(1);
		}
	}
}
