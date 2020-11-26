using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyExpressionStatementNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			child = AddChild("expression", treeNode.GetMappedChildNodes()[0]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (child?.Evaluate(thread) is not ExpressionNode expr) throw new ParserException();

			thread.CurrentNode = Parent;
			return new ExpressionStatementNode(expr);
		}

		private AstNode? child;
	}
}
