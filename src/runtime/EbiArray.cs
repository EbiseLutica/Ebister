using System.Collections;
using System.Collections.Generic;

namespace Ebister.Runtime
{
	public class EbiArray : IList<EbiValue>
	{
		public EbiArray(IList dotnetArray)
		{
			foreach (var i in dotnetArray)
			{
				list.Add(new EbiValue(i));
			}
		}

		public EbiValue this[int index] { get => list[index]; set => list[index] = value; }

		public int Count => list.Count;

		public bool IsReadOnly => false;

		public void Add(EbiValue item) => list.Add(item);

		public void Clear() => list.Clear();

		public bool Contains(EbiValue item) => list.Contains(item);

		public void CopyTo(EbiValue[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

		public IEnumerator<EbiValue> GetEnumerator() => list.GetEnumerator();

		public int IndexOf(EbiValue item) => list.IndexOf(item);

		public void Insert(int index, EbiValue item) => list.Insert(index, item);

		public bool Remove(EbiValue item) => list.Remove(item);

		public void RemoveAt(int index) => list.RemoveAt(index);

		IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

		private readonly List<EbiValue> list = new List<EbiValue>();

		public override string ToString() => $"[ {string.Join(", ", this)} ]";
	}


}
