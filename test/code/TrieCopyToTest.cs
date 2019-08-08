using System;
using System.Collections.Generic;
using Xunit;

namespace TrieLookup.Test
{
	public class TrieCopyToTest
	{
		private class TrieCopyToTestData : TheoryData<List<string>>
		{
			public TrieCopyToTestData()
			{
				// source list

				Add(new List<string>() { });
				Add(new List<string>() { "a" });
				Add(new List<string>() { "abcdef" });
				Add(new List<string>() { "a", "b", "c" });
				Add(new List<string>() { "aaa", "aab", "aac", "aaaaa" });
			}
		}

		[Fact]
		public void Test1()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.CopyTo(null, 0));

			Assert.Throws<ArgumentNullException>(a);
		}

		[Fact]
		public void Test2()
		{
			Trie t = new Trie();

			Action a = new Action(() => t.CopyTo(new string[] { }, -1));

			Assert.Throws<ArgumentOutOfRangeException>(a);
		}

		[Fact]
		public void Test3()
		{
			Trie t = new Trie(new string[] { "a", "b", "c" });

			string[] copy = new string[1];
			Action a = new Action(() => t.CopyTo(copy, 0));

			Assert.Throws<ArgumentException>(a);
		}

		[Theory]
		[ClassData(typeof(TrieCopyToTestData))]
		public void Test4(IList<string> strings)
		{
			Trie t = new Trie(strings);

			string[] copy = new string[strings.Count];
			t.CopyTo(copy, 0);

			Assert.All(strings, x => Assert.Contains(x, copy));
		}
	}
}
