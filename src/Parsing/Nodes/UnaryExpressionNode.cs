namespace Ebister.Parsing.Node
{
	public class UnaryExpressionNode : ExpressionNode
	{
		public string Operator { get; }
		public ExpressionNode Terminal { get; }

		public UnaryExpressionNode(string op, ExpressionNode terminal) => (Operator, Terminal) = (op, terminal);

		public override string ToString() => $"({Operator} {Terminal})";
	}
}
