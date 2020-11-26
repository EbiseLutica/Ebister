namespace Ebister
{
	public class EbiString : EbiPrimitive<string>
	{
		public override string TypeName => "string";
		public override EbiType Type => EbiType.String;

		public EbiString(string s) : base(s) { }
		public override string ToString() => '"' + value + '"';
		public static EbiString operator +(EbiString left, EbiValueBase right)
		{
			var rightValue = right switch
			{
				EbiString s => s.value,
				EbiBoolean b => b.ToDotNetObject() ? "true" : "false",
				_ => right.ToString(),
			};
			return new EbiString(left.value + rightValue);
		}

		public override EbiString ToEbiString()
		{
			return this;
		}
	}
}
