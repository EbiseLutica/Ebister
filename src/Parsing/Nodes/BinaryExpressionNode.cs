using System;

namespace Ebister.Parsing.Node
{
	public class BinaryExpressionNode : ExpressionNode
	{
		public string Operator { get; }
		public ExpressionNode TerminalLeft { get; }
		public ExpressionNode TerminalRight { get; }

		public BinaryExpressionNode(string op, ExpressionNode left, ExpressionNode right) => (Operator, TerminalLeft, TerminalRight) = (op, left, right);

		public override string ToString() => $"({Operator} {TerminalLeft} {TerminalRight})";
	}
}
