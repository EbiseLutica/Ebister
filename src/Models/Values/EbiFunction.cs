namespace Ebister
{
	public class EbiFunction : EbiValueBase
	{
		public override string TypeName => "function";
		public override EbiType Type => EbiType.Function;

		public string Name { get; }

		public EbiFunction(string name)
		{
			Name = name;
		}
		public override string ToString() => $"[Function {Name}]";
	}
}
