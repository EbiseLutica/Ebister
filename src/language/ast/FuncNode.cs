using System.Linq;
using Irony;
using Irony.Ast;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	public class FuncNode : AstNode
	{
		public string Name { get; private set; }
		public string[] Parameters { get; private set; }
		public AstNode[] Statements { get; private set; }

		public override void Init(AstContext ctx, ParseTreeNode node)
		{
			var children = node.GetMappedChildNodes();

			if (children.Count != 3)
			{
				ctx.AddMessage(ErrorLevel.Error, SourceLocation.Empty, "bug: func node doesn't have 3 children");
			}

			Name = children[0].Token.ValueString;
			Parameters = children[1].GetMappedChildNodes().Select(child => child.Token.ValueString).ToArray();
			Statements = children[2].GetMappedChildNodes()[0].GetMappedChildNodes().Select(child => AddChild("statements", child)).ToArray();

			base.Init(ctx, node);
		}
	}
}
