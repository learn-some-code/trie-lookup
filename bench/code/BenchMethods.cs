using System.Collections.Generic;

namespace TrieLookup.Bench
{
	/// <summary>
	/// A utility class containing specific method calls for the Trie class.
	/// </summary>
	public static class BenchMethods
	{
		/// <summary>
		/// Called the Add(string item) method of the Trie object.
		/// </summary>
		/// <param name="trie">The Trie object.</param>
		/// <param name="item">The string to add.</param>
		public static void Add(Trie trie, string item)
		{
			trie.Add(item);
		}

		/// <summary>
		/// Calls the Add(string item) method of the Trie object.
		/// </summary>
		/// <param name="trie">The Trie object to add strings to.</param>
		/// <param name="strings">The set of strings to add.</param>
		public static void AddSet(Trie trie, IEnumerable<string> strings)
		{
			trie.Add(strings);
		}

		/// <summary>
		/// Calls the ToList() method of the Trie object.
		/// </summary>
		/// <param name="trie">The Trie object.</param>
		public static void ToList(Trie trie)
		{
			trie.ToList();
		}

		/// <summary>
		/// Calls the ToListByPrefix(string prefix) method of the Trie object.
		/// </summary>
		/// <param name="trie">The Trie object.</param>
		/// <param name="prefix">The prefix string.</param>
		public static void ToListByPrefix(Trie trie, string prefix)
		{
			trie.ToListByPrefix(prefix);
		}

		/// <summary>
		/// Calls the Remove(string item) method of the Trie object.
		/// </summary>
		/// <param name="trie">The Trie object.</param>
		/// <param name="item">The string to remove.</param>
		public static void Remove(Trie trie, string item)
		{
			trie.Remove(item);
		}

		/// <summary>
		/// Calls the RemoveByPrefix(string prefix) method of the Trie object.
		/// </summary>
		/// <param name="trie">The Trie object.</param>
		/// <param name="prefix">The prefix string.</param>
		public static void RemoveByPrefix(Trie trie, string prefix)
		{
			trie.RemoveByPrefix(prefix);
		}
	}
}
