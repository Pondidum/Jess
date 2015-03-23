using Jess.Caches;
using Shouldly;
using Xunit;

namespace Jess.Tests.Caches
{
	public class DefaultCacheTests
	{
		[Fact]
		public void When_getting_from_a_non_existing_cache()
		{
			var cache = new DefaultCache();

			cache.Get("test", "new").ShouldBe("");
		}

		[Fact]
		public void When_adding_to_a_new_type_cache()
		{
			var cache = new DefaultCache();

			cache.Add("test", "new", "{'type':'old value'}");
			cache.Get("test", "new").ShouldBe("{'type':'old value'}");
		}

		[Fact]
		public void When_adding_to_an_existing_type_cache()
		{
			var cache = new DefaultCache();

			cache.Add("test", "old", "{'type':'old value'}");
			cache.Add("test", "new", "{'type':'new value'}");

			cache.Get("test", "new").ShouldBe("{'type':'new value'}");
		}

		[Fact]
		public void When_adding_to_a_type_cache_with_an_existing_id()
		{
			var cache = new DefaultCache();

			cache.Add("test", "new", "{'type':'old value'}");
			cache.Add("test", "new", "{'type':'new value'}");

			cache.Get("test", "new").ShouldBe("{'type':'new value'}");
		}

		[Fact]
		public void When_removing_from_a_cache()
		{
			var cache = new DefaultCache();

			cache.Add("test", "new", "{'type':'old value'}");
			cache.Remove("test", "new");

			cache.Get("test", "new").ShouldBe("");
		}

		[Fact]
		public void When_clearing_a_cache()
		{
			var cache = new DefaultCache();

			cache.Add("test", "old", "{'type':'old value'}");
			cache.Add("test", "new", "{'type':'new value'}");

			cache.Clear("test");

			cache.Get("test", "old").ShouldBe("");
			cache.Get("test", "new").ShouldBe("");
		}
	}
}
