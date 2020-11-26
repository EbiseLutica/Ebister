namespace Ebister
{
	public class EbiVariable : EbiConstant
	{
		public EbiVariable(EbiValueBase value) : base(value) { }
		public EbiVariable(EbiValueBase value, EbiType suitableType) : base(value, suitableType) { }

		public void Set(EbiValueBase value) => Value = value;
	}
}
