using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class ReturnNode : AstNode
	{
		public AstNode? Expression { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count > 1)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: return node can have only one child");
			}

			if (children.Count == 1)
			{
				Expression = AddChild("initialExpression", children[0]);
			}

			base.Init(ctx, node);
		}
	}
}
