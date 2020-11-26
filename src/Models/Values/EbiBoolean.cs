namespace Ebister
{
	public class EbiBoolean : EbiPrimitive<bool>
	{
		public override string TypeName => "boolean";
		public override EbiType Type => EbiType.Boolean;

		public EbiBoolean(bool b) : base(b) { }
		public override string ToString() => value ? "true" : "false";
	}
}
