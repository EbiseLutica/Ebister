using System.Linq;
using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class ProgramNode : AstNode
	{
		public AstNode[] Statements { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 2)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: program node doesn't have 2 children");
			}

			// Parse options
			children[0].GetMappedChildNodes().ForEach(child =>
			{
				switch (child.Token.ValueString)
				{
					case "strict":
						ctx.Values["strict"] = true;
						break;
				}
			});

			// Parse statements
			Statements = children[1]
				.GetMappedChildNodes()
				.Select(child => AddChild("statements", child))
				.ToArray();

			base.Init(ctx, node);
		}
	}
}