
using System;
using Irony.Parsing;

namespace Citrine.Scripting
{
	[Language("CitrineScript", "1.0", "Citrine Script")]
	public class CitrineScriptGrammar : Grammar
	{
		public CitrineScriptGrammar()
		{
			// Terminals
			var keyTrue = ToTerm("true", "true");
			var keyFalse = ToTerm("false", "false");

			var keyIf = ToTerm("if", "if");
			var keyElse = ToTerm("else", "else");
			var keyFunc = ToTerm("func", "func");
			var keyGroup = ToTerm("group", "group");
			var keyReturn = ToTerm("return", "return");
			var keyFor = ToTerm("for", "for");
			var keyWhile = ToTerm("while", "while");
			var keyRepeat = ToTerm("repeat", "repeat");
			var keySwitch = ToTerm("switch", "switch");

			var keyNull = ToTerm("null", "null");

			var keyPlus = ToTerm("+", "plus");
			var keyMinus = ToTerm("-", "minus");
			var keyAsterisk = ToTerm("*", "asterisk");
			var keySlash = ToTerm("/", "slash");
			var keyPercent = ToTerm("%", "percent");

			var keyShiftLeft = ToTerm("<<", "shift_left");
			var keyShiftRight = ToTerm(">>", "shift_right");

			var keyGreaterThan = ToTerm(">", "greater_than");
			var keyGreaterEqual = ToTerm(">=", "greater_equal");
			var keyLesserThan = ToTerm("<", "lesser_than");
			var keyLesserEqual = ToTerm("<=", "lesser_equal");

			var keyEqual2 = ToTerm("==", "equal_equal");
			var keyNotEqual = ToTerm("!=", "not_equal");

			var keyAllowRight = ToTerm("->", "allow_right");
			var keyAtmark = ToTerm("@", "atmark");


			var keyEqual = ToTerm("=", "equal");
			var keyCoronEqual = ToTerm(":=", "coron_equal");
			var keyPlusEqual = ToTerm("+=", "coron_equal");
			var keyMinusEqual = ToTerm("-=", "coron_equal");
			var keyAsteriskEqual = ToTerm("*=", "coron_equal");
			var keySlashEqual = ToTerm("/=", "coron_equal");
			var keyPercentEqual = ToTerm("%=", "coron_equal");
			var keyShiftLeftEqual = ToTerm("<<=", "coron_equal");
			var keyShiftRightEqual = ToTerm(">>=", "coron_equal");

			var keyQuestion = ToTerm("?", "question");
			var keyBang = ToTerm("!", "bang");

			var keyPlus2 = ToTerm("+", "plus2");
			var keyMinus2 = ToTerm("-", "minus2");

			var keyAmpersand = ToTerm("&", "ampersand");
			var keyPipe = ToTerm("|", "pipe");

			var keyAmpersand2 = ToTerm("&&", "ampersand2");
			var keyPipe2 = ToTerm("||", "pipe2");

			var keyHat = ToTerm("^", "hat");

			var keyParenLeft = ToTerm("(", "paren_left");
			var keyParenRight = ToTerm(")", "paren_right");

			var keySquareBracketLeft = ToTerm("[", "square_bracket_left");
			var keySquareBracketRight = ToTerm("]", "square_bracket_right");

			var keyCurlyBracketLeft = ToTerm("{", "curly_bracket_left");
			var keyCurlyBracketRight = ToTerm("}", "curly_bracket_right");

			var keyDot = ToTerm(".", "dot");
			var keyComma = ToTerm(",", "comma");
			var keySemicoron = ToTerm(";", "semicoron");
			var keyCoron = ToTerm(":", "coron");

			var singleComment = new CommentTerminal("single_comment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
			var multilineComment = new CommentTerminal("multiline_comment", "/*", "*/");
			NonGrammarTerminals.Add(singleComment);
			NonGrammarTerminals.Add(multilineComment);

			var numberLiteral = new NumberLiteral("number",
				NumberOptions.AllowSign |
				NumberOptions.AllowStartEndDot |
				NumberOptions.AllowUnderscore |
				NumberOptions.Binary |
				NumberOptions.Hex
			);

			var stringLiteral = new StringLiteral("string", "\"", StringOptions.AllowsAllEscapes);
			stringLiteral.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

			var identifier = new IdentifierTerminal("identifier");

			// Non Terminals
			var statements = new NonTerminal("statements");
			var statement = new NonTerminal("statement");
			var block = new NonTerminal("block");
			var expression = new NonTerminal("expression");

			var exprAndOr = new NonTerminal("exprAndOr");
			var exprComparision = new NonTerminal("exprComparision");
			var exprBit = new NonTerminal("exprBit");
			var exprAddSub = new NonTerminal("exprAddSub");
			var exprMulDiv = new NonTerminal("exprMulDiv");
			var exprParen = new NonTerminal("exprParen");
			var exprUnary = new NonTerminal("exprUnary");
			var exprPreIncDec = new NonTerminal("exprPreIncDec");
			var exprPostIncDec = new NonTerminal("exprPostIncDec");
			var exprValue = new NonTerminal("exprValue");
			var exprAssignImmutable = new NonTerminal("exprAssignImmutable");
			var exprAssignMutable = new NonTerminal("exprAssignMutable");

			var parameters = new NonTerminal("parameters");

			statements.Rule = MakeStarRule(statements, statement);
			statement.Rule =
				expression + keySemicoron |
				block;
			block.Rule = keyCurlyBracketLeft + statements + keyCurlyBracketRight;

			expression.Rule = exprAndOr;

			exprAndOr.Rule = exprComparision |
				exprAndOr + keyAmpersand2 + exprAndOr |
				exprAndOr + keyPipe2 + exprAndOr;

			exprComparision.Rule = exprBit |
				exprComparision + keyEqual2 + exprComparision |
				exprComparision + keyNotEqual + exprComparision |
				exprComparision + keyLesserThan + exprComparision |
				exprComparision + keyGreaterThan + exprComparision |
				exprComparision + keyLesserEqual + exprComparision |
				exprComparision + keyGreaterEqual + exprComparision;

			exprBit.Rule = exprAddSub |
				exprBit + keyAmpersand + exprBit |
				exprBit + keyPipe + exprBit |
				exprBit + keyHat + exprBit;

			exprAddSub.Rule = exprMulDiv |
				exprAddSub + keyPlus + exprAddSub |
				exprAddSub + keyMinus + exprAddSub;

			exprMulDiv.Rule = exprParen |
				exprMulDiv + keyAsterisk + exprMulDiv |
				exprMulDiv + keySlash + exprMulDiv |
				exprMulDiv + keyPercent + exprMulDiv;

			exprParen.Rule = exprUnary |
				keyParenLeft + expression + keyParenRight;

			exprUnary.Rule = exprPreIncDec |
				keyPlus + exprUnary |
				keyMinus + exprUnary;

			exprPreIncDec.Rule = exprPostIncDec |
				keyPlus2 + identifier |
				keyMinus2 + identifier;

			exprPostIncDec.Rule = exprValue |
				identifier + keyPlus2 |
				identifier + keyMinus2;

			exprValue.Rule = stringLiteral | numberLiteral | keyTrue | keyFalse | keyNull | identifier;

			parameters.Rule = MakeStarRule(parameters, keyComma, expression);


			Root = statements;
		}
	}
}