using System.Collections.Generic;
using System.Linq;

namespace Ebister.Parsing.Node
{
	public class CallExpressionNode : ExpressionNode
	{
		public ExpressionNode Callee { get; }
		public ExpressionsNode Parameters { get; }
		public CallExpressionNode(ExpressionNode callee, ExpressionsNode p) => (Callee, Parameters) = (callee, p);

		public override string ToString() => $"(call {Callee} {Parameters});";
	}
}
