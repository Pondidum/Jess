using System.Net;
using System.Net.Http;
using Shouldly;
using Xunit;

namespace Jess.Tests.Acceptance.Management
{
	public class AllRequestTypes : AcceptanceBase
	{
		[Fact]
		public void When_handling_a_get_with_type_and_id()
		{
			var response = Hydrator.MakeRequest(
				"/manage/statement/abc", 
				new HttpRequestMessage {Method = HttpMethod.Get});

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public void When_handling_a_post_with_type_id_and_json()
		{
			var response = Hydrator.MakeRequest(
				"/manage/statement/abc",
				new HttpRequestMessage {Method = HttpMethod.Post, Content = new StringContent("")});

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public void When_handling_a_delete_with_type_and_id()
		{
			var response = Hydrator.MakeRequest(
				"/manage/statement/abc", 
				new HttpRequestMessage { Method = HttpMethod.Delete });

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public void When_handling_a_delete_with_type_only()
		{
			var response = Hydrator.MakeRequest(
				"/manage/statement",
				new HttpRequestMessage { Method = HttpMethod.Delete });

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}
	}
}
