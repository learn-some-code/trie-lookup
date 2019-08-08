using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieToListByPrefixTest
	{
		private class TrieToListByPrefixTestData : TheoryData<List<string>, string, List<string>>
		{
			public TrieToListByPrefixTestData()
			{
				// source list, prefix, filtered list
				
				Add(new List<string>() { }, "", new List<string>() { });
				Add(new List<string>() { "a" }, "", new List<string>() { "a" });
				Add(new List<string>() { "a" }, "a", new List<string>() { "a" });
				Add(new List<string>() { "aaa", "aab", "aac", "mno" }, "aa", new List<string>() { "aaa", "aab", "aac" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "", new List<string>() { "abcde", "abxx", "abcmz", "abxk" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "a", new List<string>() { "abcde", "abxx", "abcmz", "abxk" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "ab", new List<string>() { "abcde", "abxx", "abcmz", "abxk" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "abc", new List<string>() { "abcde", "abcmz" });
				Add(new List<string>() { "abcde", "abxx", "abcmz", "abxk" }, "abx", new List<string>() { "abxx", "abxk" });
			}
		}

		[Fact]
		public void Test1()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.ToListByPrefix(null));

			Assert.Throws<ArgumentNullException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieToListByPrefixTestData))]
		public void Test2(IEnumerable<string> strings, string prefix, IEnumerable<string> filtered)
		{
			Trie t = new Trie(strings);

			var list = t.ToListByPrefix(prefix);

			Assert.All(filtered, x => Assert.Contains(x, list));
		}
	}
}
