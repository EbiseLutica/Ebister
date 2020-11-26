using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ebister.Parsing.Node
{
	public class ProgramNode : EbisterNode
	{
		public StatementNode[] Nodes { get; }

		public ProgramNode(StatementNode[] nodes) => Nodes = nodes;

		public override string ToString() => string.Join('\n', Nodes as IEnumerable<StatementNode>);
	}
}
