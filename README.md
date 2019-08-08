# TrieLookup

A trie data structure for fast string lookups.<br>
The trie is an excellent structure to look up strings based on a prefix.
This allows you to add intellisense / auto-complete functionality to your applications.

## NuGet Package

[Get it here](https://www.nuget.org/packages/TrieLookup/)

## Compatibility

- .NET Standard 2.0
- .NET 4.0
- .NET 4.5
- .NET 4.6
- .NET 4.7
- .NET 4.8

## Example

```csharp
Trie trie = new Trie();
trie.Add("abc");
trie.Add("abcdef");
trie.Add("azz");

Console.WriteLine("String starting with ab:");
foreach (string s in trie.ToListByPrefix("ab"))
	Console.WriteLine(s);
```

**Output:**

```
Strings starting with ab:
abc
abcdef
```

## Performance

You can test the performance of the trie by going to the `./bench` folder and running the .NET Core CLI command:

```
dotnet run
```

## Unit Tests

You can execute the unit tests by running this command in the root folder:

```
dotnet test
```

## Notes

- This implementation of the trie is case-sensitive but does NOT allow duplicates. For example the following code will output a '1'

```csharp
Trie trie = new Trie();
trie.Add("abc");
trie.Add("abc");
Console.WriteLine(trie.Count);
```

- The `Clear()` method has a close to 0ms execution time. It does not remove items from the trie one by one, but instead drops the link to all items, letting the GC (Garbage Collector) free up the memory. The `RemoveByPrefix(string prefix)` method works in a similar way.
