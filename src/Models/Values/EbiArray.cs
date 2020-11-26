using System.Collections.Generic;
using System.Linq;

namespace Ebister
{
	public class EbiArray : EbiValueBase
	{
		public override string TypeName => "array";
		public override EbiType Type => EbiType.Array;

		public List<EbiValueBase> Items => value;

		public EbiArray(IEnumerable<object> array)
		{
			value.AddRange(array.Select(e => ToEbiObject(e)));
		}

		public EbiArray(params object[] array) : this(array as IEnumerable<object>) { }

		public EbiArray() { }

		private readonly List<EbiValueBase> value = new();
		public override string ToString() => "[" + string.Join(',', Items) + " ]";
	}
}
