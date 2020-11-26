using System;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyKeywordStatementNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			keyword = AddChild("keyword", treeNode.GetMappedChildNodes()[0]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;
			Console.WriteLine(keyword?.AsString);
			var s = keyword?.AsString switch
			{
				"break" => new BreakStatementNode() as StatementNode,
				"continue" => new ContinueStatementNode(),
				"return" => new ReturnStatementNode(),
				_ => throw new ParserException(),
			};
			thread.CurrentNode = Parent;

			return s;
		}

		private AstNode? keyword;
	}
}
