using System.Linq;
using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class GroupNode : AstNode
	{
		public string Name { get; private set; }
		public AstNode[] Statements { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 2)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: func node doesn't have 2 children");
			}

			Name = children[0].Token.ValueString;
			Statements = children[1].GetMappedChildNodes().Select(child => AddChild("statements", child)).ToArray();

			base.Init(ctx, node);
		}
	}
}
