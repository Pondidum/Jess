namespace Jess
{
	public interface ICache
	{
		string Get(string type, string id);

		void Add(string json);
		void Remove(string type, string id);
		void Clear(string type);
	}
}
