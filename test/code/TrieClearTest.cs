using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieClearTest
	{
		private class TrieClearTestData : TheoryData<List<string>>
		{
			public TrieClearTestData()
			{
				// source list

				Add(new List<string>() { });
				Add(new List<string>() { null });
				Add(new List<string>() { "a" });
				Add(new List<string>() { "a", "b", "c" });
			}
		}

		[Theory]
		[ClassData(typeof(TrieClearTestData))]
		public void Test1(IEnumerable<string> strings)
		{
			Trie t = new Trie(strings);

			t.Clear();
			var count = t.Count;

			Assert.Equal(0, count);
		}
	}
}
