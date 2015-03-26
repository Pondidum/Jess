using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Jess.Tests.Util;
using Shouldly;
using Xunit;

namespace Jess.Tests
{
	public class RemoteHostTests : IDisposable
	{
		private readonly RemoteHost _remote;

		public RemoteHostTests()
		{
			_remote = new RemoteHost();
		}

		[Fact]
		public void Remote_logs_values()
		{
			_remote
				.MakeRequest(new HttpRequestMessage());

			_remote.Recieved.ShouldNotBeEmpty();
		}

		[Fact]
		public void Remote_can_respond_to_routes()
		{
			var response = new HttpResponseMessage(HttpStatusCode.OK);

			_remote.RespondsTo("/test/some/route", req => response);

			_remote
				.MakeRequest(new HttpRequestMessage { RequestUri = new Uri("http://remote/test/some/route") })
				.StatusCode
				.ShouldBe(HttpStatusCode.OK);

			_remote.Recieved.First().Response.ShouldBe(response);
		}

		public void Dispose()
		{
			try
			{
				_remote.Dispose();
			}
			catch (Exception)
			{
				//if it failed to start probably...
			}
		}
	}
}
