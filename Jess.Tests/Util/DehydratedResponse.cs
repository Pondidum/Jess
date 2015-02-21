namespace Jess.Tests.Util
{
	public class DehydratedResponse : JsonResponse
	{
		public DehydratedResponse(string json) 
			: base(json)
		{
			Headers.Add("X-Hydrate", "!ref");
		}
	}
}
