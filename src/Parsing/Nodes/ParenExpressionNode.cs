using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyParenExpressionNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			expression = AddChild("expression", treeNode.GetMappedChildNodes()[1]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (expression?.Evaluate(thread) is not EbisterNode expr) throw new ParserException("invalid terminal");

			thread.CurrentNode = Parent;
			return expr;
		}

		private AstNode? expression;
	}
}
