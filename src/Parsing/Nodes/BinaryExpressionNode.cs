using System.Linq;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyExpressionNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			children = treeNode.GetMappedChildNodes().Select(node => AddChild("children", node)).ToArray();
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (children == null) throw new ParserException();

			if (children.Length == 1)
			{
				var e = children[0].Evaluate(thread);
				thread.CurrentNode = Parent;

				// EbisterNodeでなければおそらくリテラルの即値（としておく）
				return e is EbisterNode node ? node : new LiteralNode(e);
			}
			else if (children.Length == 3)
			{
				if (children[0]?.Evaluate(thread) is not EbisterNode left) throw new ParserException();
				if (children[1].Term is not KeyTerm t) throw new ParserException();
				if (children[2]?.Evaluate(thread) is not EbisterNode right) throw new ParserException();
				thread.CurrentNode = Parent;
				return new BinaryExpressionNode(t.Text, left, right);
			}
			else
			{
				throw new ParserException("invalid children size");
			}
		}

		private AstNode[]? children;
	}

	public class BinaryExpressionNode : ExpressionNode
	{
		public string Operator { get; }
		public EbisterNode TerminalLeft { get; }
		public EbisterNode TerminalRight { get; }

		public BinaryExpressionNode(string op, EbisterNode left, EbisterNode right) => (Operator, TerminalLeft, TerminalRight) = (op, left, right);

		public override string ToString() => $"({Operator} {TerminalLeft} {TerminalRight})";
	}


}
