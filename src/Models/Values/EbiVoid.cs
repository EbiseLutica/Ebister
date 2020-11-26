using System;

namespace Ebister
{
	public class EbiVoid : EbiValueBase
	{
		public override string TypeName => "void";
		public override EbiType Type => EbiType.Void;

		public override string ToString() => "void";

		public override EbiString ToEbiString()
		{
			throw new NotSupportedException();
		}
	}
}
