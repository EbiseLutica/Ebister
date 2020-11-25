using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class IronyUnaryExpressionNode : AstNode
	{
		public override void Init(AstContext context, ParseTreeNode treeNode)
		{
			base.Init(context, treeNode);

			var children = treeNode.GetMappedChildNodes();

			unaryOperatorNode = AddChild("unaryOperator", children[0]);
			terminalNode = AddChild("terminal", children[1]);
		}

		protected override object DoEvaluate(ScriptThread thread)
		{
			thread.CurrentNode = this;

			if (unaryOperatorNode?.Evaluate(thread) is not string op) throw new ParserException("invalid unary operator");
			if (terminalNode?.Evaluate(thread) is not EbisterNode term) throw new ParserException("invalid terminal");

			return new UnaryExpressionNode(op, term);
		}

		private AstNode? unaryOperatorNode;
		private AstNode? terminalNode;

	}

	public class UnaryExpressionNode : ExpressionNode
	{
		public string Operator { get; }
		public EbisterNode Terminal { get; }

		public UnaryExpressionNode(string op, EbisterNode terminal) => (Operator, Terminal) = (op, terminal);

		public override string ToString() => $"({Operator} {Terminal})";
	}
}
