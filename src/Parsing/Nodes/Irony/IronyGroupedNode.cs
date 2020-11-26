using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyGroupedNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			child = AddChild("child", treeNode.GetMappedChildNodes()[0]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;
			var c = child?.Evaluate(thread);
			thread.CurrentNode = Parent;

			return c ?? throw new ParserException();
		}

		private AstNode? child;
	}
}
