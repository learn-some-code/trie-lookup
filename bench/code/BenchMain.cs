using System;
using System.Linq;
using System.Diagnostics;

namespace TrieLookup.Bench
{
	class BenchMain
	{
		static double baselineMemory;

		static void Main(string[] args)
		{
			// Get the sample size
			int sampleSize = GetSampleSize();
			if (sampleSize == 0)
			{
				Console.WriteLine("Invalid selection");
				return;
			}

			// Get the string length
			int stringLength = GetStringLength();
			if (stringLength == 0)
			{
				Console.WriteLine("Invalid selection");
				return;
			}

			// Generate randomized source data
			Console.WriteLine("\nGenerating data...");
			var data = BenchData.Generate(sampleSize, stringLength);
			baselineMemory = GetMemoryUsageInMB();
			Console.WriteLine("Baseline memory: {0} MB", baselineMemory.ToString("0.##"));

			// Create the trie and the relevant method calls to benchmark
			Trie trie = new Trie();
			Action addSet = new Action(() => BenchMethods.AddSet(trie, data));
			Action toList = new Action(() => BenchMethods.ToList(trie));
			Action toListByPrefix1 = new Action(() => BenchMethods.ToListByPrefix(trie, "a"));
			Action toListByPrefix2 = new Action(() => BenchMethods.ToListByPrefix(trie, "ab"));
			Action remove = new Action(() => BenchMethods.Remove(trie, data.First()));
			Action removeByPrefix = new Action(() => BenchMethods.RemoveByPrefix(trie, "a"));
			Action removeByPrefix2 = new Action(() => BenchMethods.RemoveByPrefix(trie, "b"));
			Action add = new Action(() => BenchMethods.Add(trie, data.First()));

			// Benchmark each method call
			Console.WriteLine($"\nBenchmarking {sampleSize} strings of length {stringLength}");
			Console.WriteLine("---------------------------------------------------------------");
			Console.WriteLine("{0,-26} | {1,-14} | {2,-10}", "Method", "Execution (ms)", "Delta memory (MB)");
			Console.WriteLine("---------------------------------------------------------------");
			WriteBenchLine("Add (all)", addSet);
			WriteBenchLine("ToList", toList);
			WriteBenchLine("ToListByPrefix(\"a\")", toListByPrefix1);
			WriteBenchLine("ToListByPrefix(\"ab\")", toListByPrefix2);
			WriteBenchLine("Remove", remove);
			WriteBenchLine("RemoveByPrefix(\"a\")", removeByPrefix);
			WriteBenchLine("RemoveByPrefix(\"b\")", removeByPrefix2);
			WriteBenchLine("Add (one)", add);
			Console.WriteLine("---------------------------------------------------------------");

			// End
		}


		// Query user for the sample size
		static int GetSampleSize()
		{
			int sampleSize = 0;

			Console.WriteLine();
			Console.WriteLine("Select sample size:");
			Console.WriteLine("(1) 1,000");
			Console.WriteLine("(2) 10,000");
			Console.WriteLine("(3) 100,000");
			Console.WriteLine("(4) 1,000,000");
			Console.WriteLine("(5) 10,000,000");

			var selection = Console.ReadKey();
			Console.WriteLine();

			switch (selection.Key)
			{
				case ConsoleKey.D1:
					sampleSize = 1000;
					break;

				case ConsoleKey.D2:
					sampleSize = 10000;
					break;

				case ConsoleKey.D3:
					sampleSize = 100000;
					break;

				case ConsoleKey.D4:
					sampleSize = 1000000;
					break;

				case ConsoleKey.D5:
					sampleSize = 10000000;
					break;

				default:
					sampleSize = 0;
					break;
			}

			return sampleSize;
		}


		// Query user for the string length
		static int GetStringLength()
		{
			int stringLength = 0;

			Console.WriteLine();
			Console.WriteLine("Select string length:");

			Console.WriteLine("(1) 8");
			Console.WriteLine("(2) 16");
			Console.WriteLine("(3) 32");
			Console.WriteLine("(4) 64");
			Console.WriteLine("(5) 128");

			var selection = Console.ReadKey();
			Console.WriteLine();

			switch (selection.Key)
			{
				case ConsoleKey.D1:
					stringLength = 8;
					break;

				case ConsoleKey.D2:
					stringLength = 16;
					break;

				case ConsoleKey.D3:
					stringLength = 32;
					break;

				case ConsoleKey.D4:
					stringLength = 64;
					break;

				case ConsoleKey.D5:
					stringLength = 128;
					break;

				default:
					stringLength = 0;
					break;
			}

			return stringLength;
		}

		// Measures the execution time of the specified Action
		static long ExecutionTimeInMS(Action action)
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			action.Invoke();
			timer.Stop();
			return timer.ElapsedMilliseconds;
		}

		// Gets the memory usage of the current process in MB
		static double GetMemoryUsageInMB()
		{
			return ((double)Process.GetCurrentProcess().WorkingSet64 / 1000000);
		}

		// Gets the memory above baseline used by the current process in MB
		static double GetDeltaMemoryUsageInMB()
		{
			return GetMemoryUsageInMB() - baselineMemory;
		}

		// Writes a benchmark result line to the console
		static void WriteBenchLine(string method, Action action)
		{
			Console.WriteLine($"Running {method}...");
			Console.SetCursorPosition(0, Console.CursorTop - 1);
			Console.WriteLine("{0,-26} | {1,-14} | {2,-10}",
							method, ExecutionTimeInMS(action), GetDeltaMemoryUsageInMB().ToString("0.##"));
		}
	}
}
