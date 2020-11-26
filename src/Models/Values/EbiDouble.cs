namespace Ebister
{
	public class EbiDouble : EbiPrimitive<double>
	{
		public override string TypeName => "double";
		public override EbiType Type => EbiType.Double;

		public EbiDouble(double n) : base(n) { }

		public static EbiDouble operator -(EbiDouble d) => new EbiDouble(-d.value);
		public static EbiDouble operator +(EbiDouble d) => d;
		public static EbiDouble operator +(EbiDouble left, EbiDouble right) => new EbiDouble(left.value + right.value);
		public static EbiDouble operator -(EbiDouble left, EbiDouble right) => new EbiDouble(left.value - right.value);
		public static EbiDouble operator *(EbiDouble left, EbiDouble right) => new EbiDouble(left.value * right.value);
		public static EbiDouble operator /(EbiDouble left, EbiDouble right) => new EbiDouble(left.value / right.value);
		public static EbiDouble operator %(EbiDouble left, EbiDouble right) => new EbiDouble(left.value % right.value);

		public override string ToString() => value.ToString();
	}
}
