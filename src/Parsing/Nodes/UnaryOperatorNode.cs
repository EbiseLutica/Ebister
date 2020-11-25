using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyUnaryOperatorNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			unaryOperatorNode = AddChild("unaryOperator", treeNode.GetMappedChildNodes()[0]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			return unaryOperatorNode?.Term.ToString() ?? "";
		}

		private AstNode? unaryOperatorNode;
	}
}
