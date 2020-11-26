using System;
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
				var e = children[0].Term switch
				{
					KeyTerm key => key.Text == "true" ? true : key.Text == "false" ? false : key.Text == "null" ? null : throw new ParserException("Invalid"),
					IdentifierTerminal => new IdentifierNode(children[0].AsString),
					_ => children[0].Evaluate(thread),
				};
				thread.CurrentNode = Parent;

				// EbisterNodeでなければおそらくリテラルの即値（としておく）
				return e is EbisterNode node ? node : new LiteralNode(EbiValueBase.ToEbiObject(e));
			}
			else if (children.Length == 3)
			{
				if (children[0]?.Evaluate(thread) is not ExpressionNode left) throw new ParserException();
				if (children[1].Term is not KeyTerm t) throw new ParserException();
				if (children[2]?.Evaluate(thread) is not ExpressionNode right) throw new ParserException();
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
		public ExpressionNode TerminalLeft { get; }
		public ExpressionNode TerminalRight { get; }

		public BinaryExpressionNode(string op, ExpressionNode left, ExpressionNode right) => (Operator, TerminalLeft, TerminalRight) = (op, left, right);

		public override string ToString() => $"({Operator} {TerminalLeft} {TerminalRight})";
	}


}
