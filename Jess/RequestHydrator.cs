using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace Jess
{
	public class ResponseHydrator
	{
		private readonly ICache _cache;

		public ResponseHydrator(ICache cache)
		{
			_cache = cache;
		}

		public void Hydrate(string token, Stream input, Stream output)
		{
			using (var inputReader = new StreamReader(input))
			{
				var content = inputReader.ReadToEnd();

				var tokenIndex = -1;

				while ((tokenIndex = content.IndexOf(token)) >= 0)
				{
					content = ReplaceContent(token, content, tokenIndex);
				}

				var bytes = Encoding.UTF8.GetBytes(content);
				output.Write(bytes, 0, bytes.Length);
			}
		}

		private string ReplaceContent(string token, string content, int tokenIndex)
		{
			var startIndex = content.IndexOf("{", tokenIndex + token.Length);

			if (startIndex == -1)
			{
				return content;
			}

			var finishIndex = content.IndexOf("}", startIndex + token.Length);

			if (finishIndex == -1)
			{
				return content;
			}

			var sourceJson = content.Substring(startIndex, finishIndex - startIndex + 1);
			var reference = JsonConvert.DeserializeObject<Reference>(sourceJson);

			var data =_cache.Get(reference.Type, reference.ID);

			if (string.IsNullOrEmpty(data))
			{
				return content;
			}

			var pre = content.Substring(0, tokenIndex-1);
			var post = content.Substring(finishIndex+1);

			return pre + TrimBraces(data) + post;
		}

		private string TrimBraces(string json)
		{
			return json.TrimStart(' ', '{').TrimEnd(' ', '}');
		}

		private class Reference
		{
			public string ID { get; set; }
			public string Type { get; set; }
		}
	}
}
