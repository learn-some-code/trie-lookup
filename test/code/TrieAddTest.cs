using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieAddTest
	{
		private class TrieAddTestData : TheoryData<List<string>, string, List<string>>
		{
			public TrieAddTestData()
			{
				// source list, item, resulting list

				Add(new List<string>() { }, null, new List<string>() { });
				Add(new List<string>() { }, "", new List<string>() { });
				Add(new List<string>() { }, "    ", new List<string>() { });
				Add(new List<string>() { "a", "b" }, "    ", new List<string>() { "a", "b" });
				Add(new List<string>() { }, "abc", new List<string>() { "abc" });
				Add(new List<string>() { "abc" }, "abc", new List<string>() { "abc" });
				Add(new List<string>() { "abb" }, "abc", new List<string>() { "abb", "abc" });
				Add(new List<string>() { "abc" }, "abcdef", new List<string>() { "abc", "abcdef" });
			}
		}

		[Theory]
		[ClassData(typeof(TrieAddTestData))]
		public void Test1(IEnumerable<string> strings, string item, IEnumerable<string> result)
		{
			Trie t = new Trie(strings);

			t.Add(item);
			var list = t.ToList();

			Assert.All(result, x => Assert.Contains(x, list));
		}
	}
}
