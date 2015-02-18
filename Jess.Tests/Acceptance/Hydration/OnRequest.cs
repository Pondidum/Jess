using System.Net;
using System.Net.Http;
using Shouldly;
using Xunit;

namespace Jess.Tests.Acceptance.Hydration
{
	public class OnRequest : AcceptanceBase
	{

		[Fact]
		public void A_request_to_a_server_with_no_hydration_needed()
		{
			var response = Hydrator.MakeRequest("some/endpoint/234", new HttpRequestMessage());

			response.StatusCode.ShouldBe(HttpStatusCode.OK);

		}
	}
}
