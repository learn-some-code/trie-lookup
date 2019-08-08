using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrieLookup
{
	/// <summary>
	/// A trie data structure for storing and looking up strings.
	/// </summary>
	public class Trie : ICollection<string>
	{
		// The root of the trie
		TrieNode root;
		int count;
		bool isReadOnly;


		/// <summary>
		/// Creates a trie object.
		/// </summary>
		public Trie()
		{
			this.root = new TrieNode();
			this.count = 0;
			this.isReadOnly = false;
		}

		/// <summary>
		/// Creates a trie object and populates it with a collection of strings.
		/// </summary>
		/// <param name="items">A collection of strings to store in the trie.</param>
		/// <exception cref="System.ArgumentNullException">items is null.</exception>
		public Trie(IEnumerable<string> items) : this()
		{
			Add(items);
		}

		/// <summary>
		/// Gets the number of strings in the trie.
		/// </summary>
		/// <value>The number of strings as an integer.</value>
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>
		/// Gets the read-only state of the trie.
		/// </summary>
		/// <value>True if read-only. False otherwise.</value>
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		/// <summary>
		/// Adds a new string to the trie.
		/// </summary>
		/// <param name="item">The string to add to the trie.</param>
		public void Add(string item)
		{
			if (string.IsNullOrWhiteSpace(item))
			{
				return;
			}

			if (this.root.Add(item.Trim()))
			{
				this.count++;
			}
		}

		/// <summary>
		/// Adds a set of strings to the trie.
		/// </summary>
		/// <param name="items">A collection of strings to store in the trie.</param>
		/// <exception cref="System.ArgumentNullException">items is null.</exception>
		public void Add(IEnumerable<string> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items is null");
			}

			foreach (string s in items)
			{
				Add(s);
			}
		}

		/// <summary>
		/// Removes all items from the trie.
		/// </summary>
		public void Clear()
		{
			this.root = new TrieNode();
			this.count = 0;
			//Task.Factory.StartNew(() => GC.Collect());
		}

		/// <summary>
		/// Determines whether the trie contains the specified string.
		/// </summary>
		/// <param name="item">The string to look for in the trie.</param>
		/// <returns>True if found. False otherwise.</returns>
		/// <exception cref="System.ArgumentException">item is null or empty.</exception>
		public bool Contains(string item)
		{
			if (string.IsNullOrWhiteSpace(item))
			{
				throw new ArgumentException("item is null or empty");
			}

			return this.root.Contains(item);
		}

		/// <summary>
		/// Determines whether the trie contains the specified prefix string.
		/// </summary>
		/// <param name="prefix">The prefix to look for in the trie.</param>
		/// <returns>True if found. False otherwise.</returns>
		/// <exception cref="System.ArgumentException">prefix is null or empty.</exception>
		public bool ContainsPrefix(string prefix)
		{
			if (string.IsNullOrWhiteSpace(prefix))
			{
				throw new ArgumentException("prefix is null or empty");
			}

			return this.root.ContainsPrefix(prefix);
		}


		/// <summary>
		/// Copies all string from the trie to the specified string array
		/// </summary>
		/// <param name="array">The array to copy string to.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		/// <exception cref="System.ArgumentNullException">array is null.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">arrayIndex is less than 0.</exception>
		/// <exception cref="System.ArgumentException">
		/// The number of elements in the trie is greater than the available space
		/// from arrayIndex to the end of the destination array.
		/// </exception>
		public virtual void CopyTo(string[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array is null");
			}
			else if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex is less than 0");
			}

			// Not enough space in destination array
			if (array.Length - arrayIndex < this.count)
			{
				throw new ArgumentException("The number of elements in the trie is greater than " +
											"the available space from arrayIndex to the end of the destination array");
			}

			foreach (string s in this)
			{
				array[arrayIndex] = s;
				arrayIndex++;
			}
		}

		/// <summary>
		/// Gets the enumerator that can iterate through the trie.
		/// </summary>
		/// <returns>The enumerator object.</returns>
		public virtual IEnumerator<string> GetEnumerator()
		{
			return ToList().GetEnumerator();
		}

		/// <summary>
		/// Removes a string from the trie.
		/// </summary>
		/// <param name="item">The string to remove from the trie.</param>
		/// <returns>True if string was removed successfully. False if string was not found in the trie.</returns>
		/// <exception cref="System.ArgumentException">item is null or empty.</exception>
		public bool Remove(string item)
		{
			if (string.IsNullOrWhiteSpace(item))
			{
				throw new ArgumentException("item is null or empty");
			}

			if (this.root.Remove(item))
			{
				this.count--;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Removes all strings starting with the prefix.
		/// </summary>
		/// <param name="prefix">The prefix to use.</param>
		/// <returns>The number of strings removed.</returns>
		/// <exception cref="System.ArgumentNullException">prefix is null.</exception>
		public int RemoveByPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix is null");
			}

			int removed = 0;
			if (prefix == "")
			{
				removed = this.count;
				this.Clear();
				return removed;
			}

			removed = this.root.RemoveByPrefix(prefix);
			this.count -= removed;
			//Task.Factory.StartNew(() => GC.Collect());
			return removed;
		}

		/// <summary>
		/// Gets all the strings in the trie as a list.
		/// </summary>
		/// <returns>A list of strings.</returns>
		public IList<string> ToList()
		{
			return this.root.GetAllItems();
		}

		/// <summary>
		/// Gets all the strings that start with the prefix.
		/// </summary>
		/// <param name="prefix">The prefix to look for.</param>
		/// <returns>A list of strings.</returns>
		/// <exception cref="System.ArgumentNullException">prefix is null.</exception>
		public IList<string> ToListByPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix is null");
			}

			TrieNode node = this.root.GetNodeByPrefix(prefix);
			return (node == null) ? new List<string>() : node.GetAllItems(prefix);
		}

		/// <summary>
		/// Gets all the strings in the trie as an array.
		/// </summary>
		/// <returns>An array of strings.</returns>
		public string[] ToArray()
		{
			return ToList().ToArray();
		}


		/// <summary>
		/// Gets all the strings that start with the prefix.
		/// </summary>
		/// <param name="prefix">The prefix to look for.</param>
		/// <returns>An array of strings.</returns>
		/// <exception cref="System.ArgumentNullException">prefix is null.</exception>
		public string[] ToArrayByPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix is null");
			}

			return ToListByPrefix(prefix).ToArray();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ToList().GetEnumerator();
		}
	}
}
