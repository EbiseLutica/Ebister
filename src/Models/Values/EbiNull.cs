namespace Ebister
{
	public class EbiNull : EbiValueBase
	{
		public override string TypeName => "null";
		public override EbiType Type => EbiType.Null;

		public override string ToString() => "null";

		public override EbiString ToEbiString()
		{
			return new EbiString("");
		}
	}
}
