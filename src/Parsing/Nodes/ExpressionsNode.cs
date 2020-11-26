using System.Collections.Generic;

namespace Ebister.Parsing.Node
{
	public class ExpressionsNode : EbisterNode
	{
		public ExpressionNode[] Expressions { get; }
		public ExpressionsNode(ExpressionNode[] exprs) => Expressions = exprs;

		public override string ToString() => $"(exprs {string.Join(" ", Expressions as IEnumerable<ExpressionNode>)});";
	}
}
