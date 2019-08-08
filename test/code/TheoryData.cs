using System.Collections;
using System.Collections.Generic;

namespace TrieLookup.Test
{
	public abstract class TheoryData : IEnumerable<object[]>
	{
		private readonly List<object[]> data = new List<object[]>();

		protected void AddData(params object[] values)
		{
			data.Add(values);
		}

		public IEnumerator<object[]> GetEnumerator()
		{
			return data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class TheoryData<T1> : TheoryData
	{
		public void Add(T1 value1)
		{
			AddData(value1);
		}
	}

	public class TheoryData<T1, T2> : TheoryData
	{
		public void Add(T1 value1, T2 value2)
		{
			AddData(value1, value2);
		}
	}

	public class TheoryData<T1, T2, T3> : TheoryData
	{
		public void Add(T1 value1, T2 value2, T3 value3)
		{
			AddData(value1, value2, value3);
		}
	}

	public class TheoryData<T1, T2, T3, T4> : TheoryData
	{
		public void Add(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			AddData(value1, value2, value3, value4);
		}
	}
}
