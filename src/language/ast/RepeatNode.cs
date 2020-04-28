using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class RepeatNode : AstNode
	{
		public AstNode Statement { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 1)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: repeat node doesn't have a child");
			}

			// Parse statements
			Statement = AddChild("statement", children[0]);

			base.Init(ctx, node);
		}
	}
}
