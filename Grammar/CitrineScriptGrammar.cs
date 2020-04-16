
using System;
using Irony.Parsing;

namespace Citrine.Scripting
{
	[Language("CitrineScript", "1.0", "Citrine Script")]
	public class CitrineScriptGrammar : Grammar
	{
		public CitrineScriptGrammar()
		{
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

			var keyEqualEqual = ToTerm("==", "equal_equal");
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

			var keyAmpersand = ToTerm("&", "ampersand");
			var keyPipe = ToTerm("|", "pipe");

			var keyAmpersand2 = ToTerm("&&", "ampersand2");
			var keyPipe2 = ToTerm("||", "pipe2");

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

			var num = new NumberLiteral("number",
				NumberOptions.AllowSign |
				NumberOptions.AllowStartEndDot |
				NumberOptions.AllowUnderscore |
				NumberOptions.Binary |
				NumberOptions.Hex
			);

			var str = new StringLiteral("string", "\"", StringOptions.AllowsAllEscapes);
			str.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

			var identifier = new IdentifierTerminal("identifier");

		}
	}
}