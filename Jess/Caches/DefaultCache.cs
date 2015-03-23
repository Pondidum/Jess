namespace Jess.Caches
{
	public class DefaultCache : ICache
	{
		public string Get(string type, string id)
		{
			return "";
		}

		public void Add(string json)
		{
			throw new System.NotImplementedException();
		}

		public void Remove(string type, string id)
		{
			throw new System.NotImplementedException();
		}

		public void Clear(string type)
		{
			throw new System.NotImplementedException();
		}
	}
}
