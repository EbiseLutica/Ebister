using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class VarNode : AstNode
	{
		public string Name { get; private set; }
		public AstNode? InitialExpression { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 1 && children.Count != 3)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: var node doesn't have 1 or 2 children");
			}

			Name = children[0].Token.ValueString;
			if (children.Count == 3)
			{
				InitialExpression = AddChild("initialExpression", children[2]);
			}

			base.Init(ctx, node);
		}
	}

	public class Func : AstNode
	{
		public string Name { get; private set; }
		public AstNode? InitialExpression { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 1 && children.Count != 2)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: var node doesn't have 1 or 2 children");
			}

			Name = children[0].Token.ValueString;
			if (children.Count == 2)
			{
				InitialExpression = AddChild("initialExpression", children[1]);
			}

			base.Init(ctx, node);
		}
	}
}
