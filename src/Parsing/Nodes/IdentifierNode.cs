namespace Ebister.Parsing.Node
{
	public class IdentifierNode : ExpressionNode
	{
		public string Name { get; }
		public IdentifierNode(string name) => Name = name;

		public override string ToString() => $"(id {Name})";
	}
}
