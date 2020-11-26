namespace Ebister.Parsing.Node
{
	public class BlockStatementNode : StatementNode
	{
		public ProgramNode Statements { get; }

		public BlockStatementNode(ProgramNode children) => Statements = children;

		public override string ToString() => $"(block {Statements})";
	}
}
