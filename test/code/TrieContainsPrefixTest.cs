using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieContainsPrefixTest
	{
		private class TrieContainsPrefixTestData : TheoryData<List<string>, string, bool>
		{
			public TrieContainsPrefixTestData()
			{
				// source list, prefix, contains (true/false)

				Add(new List<string>() { }, "a", false);
				Add(new List<string>() { "a" }, "a", true);
				Add(new List<string>() { "a", "b", "c" }, "a", true);
				Add(new List<string>() { "a", "b", "c" }, "d", false);
				Add(new List<string>() { "abcde" }, " a", false);
				Add(new List<string>() { "abcde" }, "a", true);
				Add(new List<string>() { "abcde" }, "abc", true);
				Add(new List<string>() { "abcde" }, "abcde", true);
				Add(new List<string>() { "abcde" }, "abm", false);
				Add(new List<string>() { "abc", "abd" }, "ab", true);
			}
		}


		[Fact]
		public void Test1()
		{
			Trie t = new Trie(new string[] { "a", "b" });

			Action a = new Action(() => t.ContainsPrefix(null));

			Assert.Throws<ArgumentException>(a);
		}

		[Fact]
		public void Test2()
		{
			Trie t = new Trie(new string[] { "a", "b" });

			Action a = new Action(() => t.ContainsPrefix(""));

			Assert.Throws<ArgumentException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieContainsPrefixTestData))]
		public void Test3(IEnumerable<string> strings, string target, bool exists)
		{
			Trie t = new Trie(strings);

			bool found = t.ContainsPrefix(target);

			Assert.Equal(exists, found);
		}
	}
}
