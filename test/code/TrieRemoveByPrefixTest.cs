using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieRemoveByPrefixTest
	{
		private class TrieRemoveByPrefixTestData : TheoryData<List<string>, string, int, List<string>>
		{
			public TrieRemoveByPrefixTestData()
			{
				// source list, prefix, items removed, resulting list

				Add(new List<string>() { }, "", 0, new List<string>() { });
				Add(new List<string>() { "a" }, "", 1, new List<string>() { });
				Add(new List<string>() { "a" }, "a", 1, new List<string>() { });
				Add(new List<string>() { "a" }, "b", 0, new List<string>() { "a" });
				Add(new List<string>() { "a" }, "bcde", 0, new List<string>() { "a" });
				Add(new List<string>() { "aaa", "aab", "aac", "mno" }, "aa", 3, new List<string>() { "mno" });
				Add(new List<string>() { "aaa", "aaae", "abccc", "aa" }, "aa", 3, new List<string>() { "abccc" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "", 4, new List<string>() { });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "a", 4, new List<string>() { });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "abc", 2, new List<string>() { "abxx", "abxk" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "abx", 2, new List<string>() { "abcde", "abcmz" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "c", 0, new List<string>() { "abcde", "abxx", "abcmz", "abxk" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "cccccccc", 0, new List<string>() { "abcde", "abxx", "abcmz", "abxk" });
			}
		}

		[Fact]
		public void Test1()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.RemoveByPrefix(null));

			Assert.Throws<ArgumentNullException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieRemoveByPrefixTestData))]
		public void Test2(IEnumerable<string> strings, string prefix, int itemsRemoved, IEnumerable<string> result)
		{
			Trie t = new Trie(strings);

			int removed = t.RemoveByPrefix(prefix);
			var list = t.ToList();

			Assert.Equal(itemsRemoved, removed);
			Assert.All(list, x => Assert.Contains(x, result));
			
		}
	}
}
