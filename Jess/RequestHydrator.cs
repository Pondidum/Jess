using System;
using System.IO;
using System.Text;

namespace Jess
{
	public class ResponseHydrator
	{
		public void Hydrate(Stream input, Stream output)
		{
			using (var inputReader = new StreamReader(input))
			{
				string line;
				while ((line = inputReader.ReadLine()) != null)
				{
					var bytes = Encoding.UTF8.GetBytes(line + Environment.NewLine);
					output.Write(bytes, 0, bytes.Length);
					output.Flush();
				}
			}
		}

	}
}