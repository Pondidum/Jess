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
		private readonly string _token;

		public ResponseHydrator(ICache cache, string token)
		{
			_cache = cache;
			_token = token;
		}

		public void Hydrate(Stream input, Stream output)
		{
			using (var inputReader = new StreamReader(input))
			{
				var content = inputReader.ReadToEnd();

				var tokenIndex = -1;

				while ((tokenIndex = content.IndexOf(_token)) >= 0)
				{
					content = ReplaceContent(content, tokenIndex);
				}

				var bytes = Encoding.UTF8.GetBytes(content);
				output.Write(bytes, 0, bytes.Length);
			}
		}

		private string ReplaceContent(string content, int tokenIndex)
		{
			var startIndex = content.IndexOf("{", tokenIndex + _token.Length);

			if (startIndex == -1)
			{
				return content;
			}

			var finishIndex = content.IndexOf("}", startIndex + _token.Length);

			if (finishIndex == -1)
			{
				return content;
			}

			var sourceJson = content.Substring(startIndex, finishIndex - startIndex + 1);
			var reference = JsonConvert.DeserializeObject<Reference>(sourceJson);

			var data =_cache.Get(reference);

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
	}
}
