using System;
using System.Net;
using System.Net.Http;
using Jess.Tests.Util;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Jess.Tests.Acceptance.Hydration
{
	public class WhenHydrating : AcceptanceBase
	{
		public WhenHydrating()
		{
			Remote.RespondsTo(
				"/candidate/ref/123",
				request => new DehydratedResponse(Resource.PersonWithOneRef));

			Remote.RespondsTo(
				"/candidate/ref/456",
				request => new JsonResponse(Resource.PersonWithOneRef));

		}

		[Fact]
		public void Without_a_hydrate_header()
		{
			var response = Hydrator.MakeRequest("/candidate/ref/456", BuildMessage(new HttpRequestMessage()));

			var body = response.Content.ReadAsStringAsync().Result;

			body.ShouldNotBeEmpty();
			body.ShouldBe(Resource.PersonWithOneRef);
		}

		[Fact]
		public void When_the_request_has_no_upstream_header()
		{
			var response = Hydrator.MakeRequest("/candidate/ref/456", new HttpRequestMessage());

			response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
			//in-memory doesnt give you the full exception back?
		}
	}
}
