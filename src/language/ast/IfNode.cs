using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class IfNode : AstNode
	{
		public AstNode Condition { get; private set; }
		public AstNode TrueStatement { get; private set; }
		public AstNode? FalseStatement { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 2 && children.Count != 3)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: if node doesn't have 2 or 3 children");
			}

			Condition = AddChild("trueStatement", children[0]);
			TrueStatement = AddChild("trueStatement", children[1]);
			if (children.Count == 3)
			{
				FalseStatement = AddChild("falseStatement", children[2]);
			}

			base.Init(ctx, node);
		}
	}
}
