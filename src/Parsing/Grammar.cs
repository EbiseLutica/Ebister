using Ebister.Parsing.Node;
using Irony.Interpreter;
using Irony.Parsing;

namespace Ebister.Parsing
{
	[Language("Ebister", "1.0.0", "A ebister language grammar")]
	public class EbisterGrammar : InterpretedLanguageGrammar
	{
		public EbisterGrammar() : base(true)
		{
			// 0. Comments
			var commentSingleLine = new CommentTerminal("commentSingleLine", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
			var commentMultiLine = new CommentTerminal("commentMultiLine", "/*", "*/");
			NonGrammarTerminals.Add(commentSingleLine);
			NonGrammarTerminals.Add(commentMultiLine);

			// 1. Literals and Operators

			var literalNumber = new NumberLiteral("number",
				NumberOptions.AllowSign |
				NumberOptions.AllowStartEndDot |
				NumberOptions.AllowUnderscore
			);
			literalNumber.AddPrefix("0x", NumberOptions.Hex);
			literalNumber.AddPrefix("0b", NumberOptions.Binary);

			var literalString = new StringLiteral("string", "\"", StringOptions.AllowsAllEscapes);
			literalString.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

			var identifier = new IdentifierTerminal("identifier");

			var operatorPlus = ToTerm("+");
			var operatorMinus = ToTerm("-");
			var operatorAsterisk = ToTerm("*");
			var operatorSlash = ToTerm("/");
			var operatorPercent = ToTerm("%");
			var operatorParenLeft = ToTerm("(");
			var operatorParenRight = ToTerm(")");
			var operatorSemicolon = ToTerm(";");

			// 2. Non-Terminals
			var program = new NonTerminal("program", typeof(IronyProgramNode));
			var statement = new NonTerminal("statement", typeof(IronyExpressionStatementNode));

			var exprAddSub = new NonTerminal("exprAddSub", typeof(IronyExpressionNode));
			var exprMulDiv = new NonTerminal("exprMulDiv", typeof(IronyExpressionNode));
			var expr = new NonTerminal("expr", typeof(IronyExpressionNode));
			var exprParen = new NonTerminal("exprParen", typeof(IronyParenExpressionNode));
			var exprUnary = new NonTerminal("exprUnary", typeof(IronyUnaryExpressionNode));
			var operatorUnary = new NonTerminal("operatorUnary", typeof(IronyUnaryOperatorNode));

			// 3. BNF Definition
			program.Rule = MakeStarRule(program, statement);
			statement.Rule = exprAddSub + operatorSemicolon;
			exprAddSub.Rule = exprMulDiv
				| exprAddSub + operatorPlus + exprAddSub
				| exprAddSub + operatorMinus + exprAddSub;
			exprMulDiv.Rule = expr
				| exprMulDiv + operatorAsterisk + exprMulDiv
				| exprMulDiv + operatorSlash + exprMulDiv
				| exprMulDiv + operatorPercent + exprMulDiv;
			expr.Rule = exprParen
				| exprUnary
				| literalNumber
				| literalString
				| "true"
				| "false"
				| "null"
				| identifier;
			exprParen.Rule = operatorParenLeft + exprAddSub + operatorParenRight;
			exprUnary.Rule = operatorUnary + expr;
			operatorUnary.Rule = operatorPlus | operatorMinus;

			Root = program;
			LanguageFlags = LanguageFlags.CreateAst;
		}
	}
}
