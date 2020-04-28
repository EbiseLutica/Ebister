using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class ForNode : AstNode
	{
		public string VariableName { get; private set; }
		public AstNode Iterable { get; private set; }
		public AstNode Statement { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 3)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: for node doesn't have 3 children");
			}

			VariableName = children[0].Token.ValueString;
			Iterable = AddChild("iterable", children[1]);
			Statement = AddChild("statement", children[2]);

			base.Init(ctx, node);
		}
	}
}
