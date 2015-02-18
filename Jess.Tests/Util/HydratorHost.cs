namespace Jess.Tests.Util
{
	public class HydratorHost : SelfHost
	{
		public HydratorHost() : base(48321)
		{
			Configure(WebApiConfig.Register);
		}
	}
}
