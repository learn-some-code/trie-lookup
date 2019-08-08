using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieContainsTest
	{
		private class TrieContainsTestData : TheoryData<List<string>, string, bool>
		{
			public TrieContainsTestData()
			{
				// source list, item, contains (true/false)

				Add(new List<string>() { }, "a", false);
				Add(new List<string>() { "a" }, "a", true);
				Add(new List<string>() { "a" }, " a", false);
				Add(new List<string>() { "a", "b", "c" }, "a", true);
				Add(new List<string>() { "a", "b", "c" }, "d", false);
				Add(new List<string>() { "a", "aa", "aaa" }, "a", true);
				Add(new List<string>() { "a", "aa", "aaa" }, "aa", true);
				Add(new List<string>() { "a", "aa", "aaa" }, "aaa", true);
				Add(new List<string>() { "a", "aa", "aaa" }, "aab", false);
				Add(new List<string>() { "abcde" }, "abcde", true);
				Add(new List<string>() { "abcde" }, "abc", false);
			}
		}


		[Fact]
		public void Test1()
		{
			Trie t = new Trie(new string[] { "a", "b" });

			Action a = new Action(() => t.Contains(null));

			Assert.Throws<ArgumentException>(a);
		}

		[Fact]
		public void Test2()
		{
			Trie t = new Trie(new string[] { "a", "b" });

			Action a = new Action(() => t.Contains(""));

			Assert.Throws<ArgumentException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieContainsTestData))]
		public void Test3(IEnumerable<string> strings, string target, bool exists)
		{
			Trie t = new Trie(strings);

			bool found = t.Contains(target);

			Assert.Equal(exists, found);
		}
	}
}
