using System;
using System.IO;
using Jess.Tests.Util;
using Newtonsoft.Json;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Jess.Tests
{
	public class RequestHydratorTests : IDisposable
	{
		private readonly ResponseHydrator _hydrator;
		private readonly MemoryStream _output;
		private MemoryStream _input;

		public RequestHydratorTests()
		{
			var cache = Substitute.For<ICache>();

			cache
				.Get("statement", "abc")
				.Returns("{\"id\": \"abc\", \"type\": \"statement\", \"signedon\": \"2015-02-20:14:37:44\", \"signedby\": \"dave grohl\"}");

			cache
				.Get("statement", "def")
				.Returns("{\"id\": \"def\", \"type\": \"statement\", \"signedon\": \"2015-02-20:14:37:44\", \"signedby\": \"dave grohl\"}");

			cache
				.Get("statement", "ghi")
				.Returns("{\"id\": \"ghi\", \"type\": \"statement\", \"signedon\": \"2015-02-20:14:37:44\", \"signedby\": \"dave grohl\"}");


			_hydrator = new ResponseHydrator(cache);
			_output = new MemoryStream();
		}

		[Fact]
		public void An_input_with_no_replacements_is_unmodified()
		{
			_input = StreamFrom(Resource.PersonWithOneRefHydrated);
			_hydrator.Hydrate("!ref", _input, _output);

			_output.Position = 0;

			var output = JsonConvert.DeserializeObject(StringFrom(_output));
			var expected = JsonConvert.DeserializeObject(Resource.PersonWithOneRefHydrated);

			expected.ShouldBe(output);
		}

		[Fact]
		public void An_input_with_a_replacement_is_replaced()
		{
			_input = StreamFrom(Resource.PersonWithOneRef);
			_hydrator.Hydrate("!ref", _input, _output);

			_output.Position = 0;

			var output = JsonConvert.DeserializeObject(StringFrom(_output));
			var expected = JsonConvert.DeserializeObject(Resource.PersonWithOneRefHydrated);

			output.ShouldBe(expected);
		}

		[Fact]
		public void An_input_with_multiple_refs_get_replaced()
		{
			_input = StreamFrom(Resource.PersonWithMutlipleRefs);
			_hydrator.Hydrate("!ref", _input, _output);

			_output.Position = 0;

			var output = JsonConvert.DeserializeObject(StringFrom(_output));
			var expected = JsonConvert.DeserializeObject(Resource.PersonWithMutlipleRefsHydrated);

			output.ShouldBe(expected);
		}

		public void Dispose()
		{
			_output.Dispose();
		}

		private MemoryStream StreamFrom(string input)
		{
			var ms = new MemoryStream();
			var writer = new StreamWriter(ms);
			writer.Write(input);
			writer.Flush();

			ms.Position = 0;
			return ms;
		}

		private string StringFrom(Stream input)
		{
			using (var reader = new StreamReader(input))
			{
				return reader.ReadToEnd();
			}
		}

	}
}