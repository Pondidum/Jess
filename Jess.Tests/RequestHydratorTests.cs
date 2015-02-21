using System.IO;
using Jess.Tests.Util;
using Shouldly;
using Xunit;

namespace Jess.Tests
{
	public class RequestHydratorTests
	{
		[Fact]
		public void An_input_with_no_replacements_is_unmodified()
		{
			using (var input = StreamFrom(Resource.PersonWithOneRefHydrated))
			using (var output = new MemoryStream())
			{
				var hydrator = new ResponseHydrator();
				hydrator.Hydrate(input, output);

				output.Position = 0;

				StringFrom(output).ShouldBe(Resource.PersonWithOneRefHydrated);
			}
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