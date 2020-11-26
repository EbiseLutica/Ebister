using System.Collections.Generic;
using System.Linq;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyExpressionsNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			children = treeNode.GetMappedChildNodes().Select(node => AddChild("expressions", node)).ToArray();
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (children == null) throw new ParserException();
			var list = new ExpressionNode[children.Length];

			foreach (var (node, i) in children.Select((node, i) => (node, i)))
			{
				if (node.Evaluate(thread) is not ExpressionNode expr) throw new ParserException();
				list[i] = expr;
			}

			thread.CurrentNode = Parent;
			return new ExpressionsNode(list);
		}

		private AstNode[]? children;
	}

	public class ExpressionsNode : EbisterNode
	{
		public ExpressionNode[] Expressions { get; }
		public ExpressionsNode(ExpressionNode[] exprs) => Expressions = exprs;

		public override string ToString() => $"(exprs {string.Join(" ", Expressions as IEnumerable<ExpressionNode>)});";
	}
}
