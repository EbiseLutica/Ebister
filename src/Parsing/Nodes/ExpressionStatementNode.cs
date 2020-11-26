using System.Linq.Expressions;

namespace Ebister.Parsing.Node
{
	public class ExpressionStatementNode : StatementNode
	{
		public ExpressionNode Expression { get; }
		public ExpressionStatementNode(ExpressionNode expr) => Expression = expr;

		public override string ToString() => $"{Expression};";
	}
}
