using System.Linq;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyProgramNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			children = treeNode.GetMappedChildNodes().Select(node => AddChild("statements", node)).ToArray();
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (children == null) throw new ParserException();
			var list = new StatementNode[children.Length];

			foreach (var (node, i) in children.Select((node, i) => (node, i)))
			{
				if (node.Evaluate(thread) is not StatementNode statement) throw new ParserException();
				list[i] = statement;
			}

			thread.CurrentNode = Parent;
			return new ProgramNode(list);
		}

		private AstNode[]? children;
	}
}
