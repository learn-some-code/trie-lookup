using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieCountTest
	{
		private class TrieCountTestData : TheoryData<List<string>, int>
		{
			public TrieCountTestData()
			{
				// source list, item count

				Add(new List<string>() { }, 0);
				Add(new List<string>() { null }, 0);
				Add(new List<string>() { "" }, 0);
				Add(new List<string>() { "   " }, 0);


				Add(new List<string>() { "a" }, 1);
				Add(new List<string>() { "abc" }, 1);
				Add(new List<string>() { "a", null, "" }, 1);
				Add(new List<string>() { "a", "a", "a" }, 1);


				Add(new List<string>() { "a", "b", "c" }, 3);
				Add(new List<string>() { "a", "aa", "aaa" }, 3);
				Add(new List<string>() { "abc", "bcd", "cde" }, 3);
				Add(new List<string>() { "aaa", "aab", "aac" }, 3);
			}
		}

		[Fact]
		public void Test1()
		{
			Action a = new Action(() => { Trie t = new Trie(null); });

			Assert.Throws<ArgumentNullException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieCountTestData))]
		public void Test2(IEnumerable<string> strings, int count)
		{
			Trie t = new Trie(strings);

			int testCount = t.Count;

			Assert.Equal(testCount, count);
		}
	}
}
