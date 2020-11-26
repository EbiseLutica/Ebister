using System;
using System.Collections;
using System.Linq;

namespace Ebister
{
	public abstract class EbiValueBase
	{
		public virtual string TypeName => "object";
		public abstract EbiType Type { get; }

		public static EbiValueBase ToEbiObject(object? obj)
		{
			return obj switch
			{
				EbiValueBase ebi => ebi,
				string s => new EbiString(s),
				// TODO: 適切な数値型に変換する
				double n => new EbiDouble(n),
				float n => new EbiDouble(n),
				long n => new EbiDouble(n),
				int n => new EbiDouble(n),
				short n => new EbiDouble(n),
				byte n => new EbiDouble(n),
				ulong n => new EbiDouble(n),
				uint n => new EbiDouble(n),
				ushort n => new EbiDouble(n),
				sbyte n => new EbiDouble(n),
				bool b => new EbiBoolean(b),
				null => new EbiNull(),
				IEnumerable e => new EbiArray(e),
				_ => throw new NotSupportedException(),
			};
		}

		public virtual EbiString ToEbiString()
		{
			return new EbiString(ToString() ?? "");
		}
	}
}
