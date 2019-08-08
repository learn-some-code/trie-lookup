using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieToListTest
	{
		private class TrieToListTestData : TheoryData<List<string>>
		{
			public TrieToListTestData()
			{
				// source list

				Add(new List<string>() { });
				Add(new List<string>() { "a" });
				Add(new List<string>() { "abcdef" });
				Add(new List<string>() { "a", "b", "c" });
				Add(new List<string>() { "aaa", "aab", "aac", "aaaaa" });
			}
		}

		[Theory]
		[ClassData(typeof(TrieToListTestData))]
		public void Test1(IEnumerable<string> strings)
		{
			Trie t = new Trie(strings);

			var list = t.ToList();

			Assert.All(strings, x => Assert.Contains(x, list));
		}
	}
}
