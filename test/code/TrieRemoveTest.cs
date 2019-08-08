using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieRemoveTest
	{
		private class TrieRemoveTestData : TheoryData<List<string>, string, bool>
		{
			public TrieRemoveTestData()
			{
				// source list, item, removed (true/false)

				Add(new List<string>() { }, "a", false);
				Add(new List<string>() { "a" }, "a", true);
				Add(new List<string>() { "a" }, "b", false);
				Add(new List<string>() { "a" }, "bcde", false);
				Add(new List<string>() { "abc", "abe" }, "abc", true);
				Add(new List<string>() { "abcdef" }, "abcdef", true);
				Add(new List<string>() { "abcdef", "abc" }, "abcdef", true);
				Add(new List<string>() { "aaa", "aab", "aac", "mno" }, "aa", false);
				Add(new List<string>() { "aaa", "aab", "aac", "mno" }, "aab", true);
				Add(new List<string>() { "aaa", "aab", "aac", "mno" }, "aaab", false);
			}
		}

		[Fact]
		public void Test1()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.Remove(null));

			Assert.Throws<ArgumentException>(a);
		}

		[Fact]
		public void Test2()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.Remove(""));

			Assert.Throws<ArgumentException>(a);
		}

		[Fact]
		public void Test3()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.Remove("     "));

			Assert.Throws<ArgumentException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieRemoveTestData))]
		public void Test4(IEnumerable<string> strings, string item, bool isRemoved)
		{
			Trie t = new Trie(strings);

			bool removed = t.Remove(item);
			var list = t.ToList();

			Assert.Equal(isRemoved, removed);
			Assert.DoesNotContain(item, list);
		}
	}
}
