using System.Collections.Generic;
using System.Linq;

namespace Ebister
{
	public class EbiMap : EbiValueBase
	{
		public override string TypeName => "map";
		public override EbiType Type => EbiType.Map;

		public Dictionary<string, EbiValueBase> Items => value;

		public EbiMap() { }

		private readonly Dictionary<string, EbiValueBase> value = new();
		public override string ToString() => "{" + string.Join(',', Items.Select(i => $" {i.Key}: {i.Value}")) + " }";
	}
}
