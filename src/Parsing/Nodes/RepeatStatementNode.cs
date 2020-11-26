namespace Ebister.Parsing.Node
{
	public class RepeatStatementNode : StatementNode
	{
		public StatementNode Statement { get; }

		public RepeatStatementNode(StatementNode statement) => Statement = statement;

		public override string ToString() => $"(repeat {Statement})";
	}
}
