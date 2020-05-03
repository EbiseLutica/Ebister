using System;
using System.Collections;

namespace Ebister.Runtime
{
	public class EbiValue
	{
		public static readonly EbiValue Null = new EbiValue(null);

		public EbiType Type { get; }

		public EbiValue(object? dotnetObject)
		{
			obj = dotnetObject;

			// 正規化
			obj = obj switch
			{
				byte i => (double)i,
				sbyte i => (double)i,
				short i => (double)i,
				ushort i => (double)i,
				int i => (double)i,
				uint i => (double)i,
				long i => (double)i,
				ulong i => (double)i,
				float i => (double)i,
				decimal i => (double)i,
				string s => s,
				bool b => b,
				EbiFunction f => f,
				EbiObject o => o,
				EbiArray a => a,
				null => null,
				IList a => new EbiArray(a),
				object o => new EbiObject(o),
			};

			Type = obj switch
			{
				string _ => EbiType.String,
				double _ => EbiType.Number,
				bool _ => EbiType.Boolean,
				EbiArray _ => EbiType.Array,
				EbiFunction _ => EbiType.Function,
				EbiObject _ => EbiType.Object,
				null => EbiType.Null,
				_ => throw new ArgumentException($"Type {obj.GetType().Name} cannot be converted to a Ebister value."),
			};
		}

		public T ToDotnetObject<T>()
		{
#pragma warning disable CS8603 // ignore because
			return (T)obj;
#pragma warning restore
		}

		public override string? ToString()
		{
			return obj switch
			{
				string s => s,
				double d => d.ToString(),
				EbiArray a => a.ToString(),
				EbiObject o => o.ToString(),
				bool b => b ? "true" : "false",
				EbiFunction _ => "[Function]",
				null => null,
				_ => throw new InvalidOperationException(),
			};
		}

		private readonly object? obj;
	}
}
