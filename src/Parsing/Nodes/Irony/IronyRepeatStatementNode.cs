using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyRepeatStatementNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			statement = AddChild("statement", treeNode.GetMappedChildNodes()[1]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;
			if (statement?.Evaluate(thread) is not StatementNode stt) throw new ParserException();
			thread.CurrentNode = Parent;

			return new RepeatStatementNode(stt) ?? throw new ParserException();
		}

		private AstNode? statement;
	}
}
