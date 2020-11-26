using System.Collections.Generic;
using System.Linq;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyCallExpressionNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			var nodes = treeNode.GetMappedChildNodes();
			callee = AddChild("callee", nodes[0]);
			parameters = AddChild("parameters", nodes[2]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (callee?.Evaluate(thread) is not ExpressionNode expr) throw new ParserException();
			if (parameters?.Evaluate(thread) is not ExpressionsNode exprs) throw new ParserException();

			thread.CurrentNode = Parent;
			return new CallExpressionNode(expr, exprs);
		}

		private AstNode? callee;
		private AstNode? parameters;
	}

	public class CallExpressionNode : ExpressionNode
	{
		public ExpressionNode Callee { get; }
		public ExpressionsNode Parameters { get; }
		public CallExpressionNode(ExpressionNode callee, ExpressionsNode p) => (Callee, Parameters) = (callee, p);

		public override string ToString() => $"(call {Callee} {Parameters});";
	}
}
