using Irony.Ast;
using Irony.Interpreter;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Ebister.Parsing.Node
{
	public class LiteralNode : ExpressionNode
	{
		public object Value { get; }
		public LiteralNode(object value) => Value = value;

		public override string ToString() => Value.ToString() ?? "null";
	}
}
