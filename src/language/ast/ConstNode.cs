using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class ConstNode : AstNode
	{
		public string Name { get; private set; }
		public AstNode InitialExpression { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 3)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: const node doesn't have 2 children");
			}

			Name = children[0].Token.ValueString;
			InitialExpression = AddChild("initialExpression", children[2]);

			base.Init(ctx, node);
		}
	}
}
