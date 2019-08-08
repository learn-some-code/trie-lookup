using System.Collections.Generic;

namespace TrieLookup
{
	internal class TrieNode
	{
		Dictionary<char, TrieNode> nodes;
		bool isItem;

		// Constructor
		public TrieNode()
		{
			nodes = new Dictionary<char, TrieNode>();
			isItem = false;
		}

		// Adds a string to the node
		public bool Add(string s)
		{
			TrieNode n = this;
			foreach (char c in s)
			{
				n = n.AddChar(c);
			}

			// Item already exists
			if (n.isItem)
			{
				return false;
			}
			// New item added
			else
			{
				n.isItem = true;
				return true;
			}
		}

		// Adds a node for the specified character and returns it
		private TrieNode AddChar(char c)
		{
			if (nodes.ContainsKey(c))
			{
				return nodes[c];
			}
			else
			{
				TrieNode n = new TrieNode();
				nodes.Add(c, n);
				return n;
			}
		}



		// Counts the number of items in the specified node
		public int Count()
		{
			// End node and item
			if (this.nodes.Count == 0 && this.isItem)
			{
				return 1;
			}

			// Count all items in child nodes
			int total = 0;
			foreach (var child in this.nodes)
			{
				total += child.Value.Count();
			}

			// If current node is an item, count it
			if (this.isItem)
			{
				total++;
			}

			return total;
		}

		// Checks if the specified string exists as an item in the trie
		public bool Contains(string s)
		{
			TrieNode n = this;
			foreach (char c in s)
			{
				if (!n.nodes.ContainsKey(c))
				{
					return false;
				}

				n = n.nodes[c];
			}

			return n.isItem;
		}

		// Checks if the specified string prefix exists in the trie
		public bool ContainsPrefix(string s)
		{
			TrieNode n = this;
			foreach (char c in s)
			{
				if (!n.nodes.ContainsKey(c))
				{
					return false;
				}

				n = n.nodes[c];
			}

			return true;
		}

		// Gets all the string items recursively
		public IList<string> GetAllItems(string prefix = "")
		{
			List<string> items = new List<string>();
			if (isItem)
			{
				items.Add(prefix);
			}

			foreach (var child in nodes)
			{
				items.AddRange(child.Value.GetAllItems(prefix + child.Key));
			}

			return items;
		}

		// Gets the node by the specified prefix
		public TrieNode GetNodeByPrefix(string prefix)
		{
			TrieNode node = this;
			foreach (char c in prefix)
			{
				if (!node.nodes.ContainsKey(c))
				{
					return null;
				}
				else
				{
					node = node.nodes[c];
				}
			}

			return node;
		}

		// Removes an item if exists
		public bool Remove(string item)
		{
			if (item == "")
			{
				if (this.isItem)
				{
					this.isItem = false;
					return true;
				}
				else
				{
					return false;
				}
			}

			if (this.nodes.ContainsKey(item[0]))
			{
				TrieNode n = this.nodes[item[0]];
				if (n.Remove(item.Substring(1)))
				{
					if (n.nodes.Count == 0)
					{
						this.nodes.Remove(item[0]);
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		// Remove all items with the specified prefix
		public int RemoveByPrefix(string prefix)
		{
			TrieNode parent = GetNodeByPrefix(prefix.Remove(prefix.Length - 1));
			if (parent == null)
			{
				return 0;
			}

			char last = prefix[prefix.Length - 1];
			if (!parent.nodes.ContainsKey(last))
			{
				return 0;
			}

			TrieNode child = parent.nodes[last];
			int removed = child.Count();
			parent.nodes.Remove(last);
			return removed;
		}
	}
}
