using System;
using System.Collections.Generic;

namespace TrieLookup.Bench
{
	/// <summary>
	/// A utility class that can generate sets of random strings.
	/// </summary>
	public static class BenchData
	{
		private static readonly string SOURCE = "abcdefghijklmnopqrstuvwxyz0123456789";
		private static Random rng = new Random();

		/// <summary>
		/// Generates a set of random strings.
		/// </summary>
		/// <param name="sampleSize">The number of strings to generate.</param>
		/// <param name="stringLength">The length of each string.</param>
		/// <returns>A set of random strings.</returns>
		public static IEnumerable<string> Generate(int sampleSize, int stringLength)
		{
			HashSet<string> strings = new HashSet<string>();

			while (strings.Count < sampleSize)
			{
				string s = GenerateRandomString(stringLength);
				if (!strings.Contains(s))
					strings.Add(s);
			}

			return strings;
		}

		/// <summary>
		/// Generates a single random string with the specified length.
		/// </summary>
		/// <param name="length">The length of the string.</param>
		/// <returns>The randomly generated string.</returns>
		private static string GenerateRandomString(int length)
		{
			char[] chars = new char[length];

			for (int i = 0; i < length; i++)
				chars[i] = SOURCE[rng.Next(SOURCE.Length)];

			return new string(chars);
		}
	}
}
