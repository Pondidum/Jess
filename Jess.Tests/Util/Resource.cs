using System.IO;
using System.Text;

namespace Jess.Tests.Util
{
	public class Resource
	{
		public static string AsText(string resourceName)
		{
			using (var stream = typeof(Resource).Assembly.GetManifestResourceStream(resourceName))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		public static string PersonWithOneRef  { get { return AsText(""); } }

	}
}
