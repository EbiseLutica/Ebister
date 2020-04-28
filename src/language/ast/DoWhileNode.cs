using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class DoWhileNode : AstNode
	{
		public AstNode Condition { get; private set; }
		public AstNode Statement { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 2)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: do-while node doesn't have 2 children");
			}

			Statement = AddChild("statement", children[0]);
			Condition = AddChild("condition", children[1]);

			base.Init(ctx, node);
		}
	}
}
