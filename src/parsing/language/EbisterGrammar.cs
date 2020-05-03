
using System;
using Irony.Parsing;

namespace Ebister.Parsing
{
	[Language("Ebister", "1.0", "Ebister")]
	public class EbisterGrammar : Grammar
	{
		public EbisterGrammar(bool generateAst = false)
		{
			var singleComment = new CommentTerminal("single_comment", "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
			var multilineComment = new CommentTerminal("multiline_comment", "/*", "*/");
			NonGrammarTerminals.Add(singleComment);
			NonGrammarTerminals.Add(multilineComment);

			var numberLiteral = new NumberLiteral("number",
				NumberOptions.AllowSign |
				NumberOptions.AllowStartEndDot |
				NumberOptions.AllowUnderscore
			);
			numberLiteral.AddPrefix("0x", NumberOptions.Hex);
			numberLiteral.AddPrefix("0b", NumberOptions.Binary);

			var stringLiteral = new StringLiteral("string", "\"", StringOptions.AllowsAllEscapes);
			stringLiteral.AddPrefix("@", StringOptions.NoEscapes | StringOptions.AllowsLineBreak | StringOptions.AllowsDoubledQuote);

			var identifier = new IdentifierTerminal("identifier");

			var statements = new NonTerminal("statements");
			var statementCanBeChild = new NonTerminal("statementCanBeChild");
			var statement = new NonTerminal("statement");
			var block = new NonTerminal("block");
			var expression = new NonTerminal("expression");

			var option = new NonTerminal("option");
			var options = new NonTerminal("options");

			var statementVar = new NonTerminal("var");
			var statementConst = new NonTerminal("const");
			var statementIf = new NonTerminal("if");
			var statementFor = new NonTerminal("for");
			var statementWhile = new NonTerminal("while");
			var statementDoWhile = new NonTerminal("do-while");
			var statementRepeat = new NonTerminal("repeat");
			var statementGroup = new NonTerminal("group");
			var statementFunc = new NonTerminal("func");
			var statementReturn = new NonTerminal("return");
			var statementBreak = new NonTerminal("break");
			var statementContinue = new NonTerminal("continue");

			var groupChildren = new NonTerminal("groupChildren");
			var groupChild = new NonTerminal("groupChild");

			var preUnaryOps = new NonTerminal("preUnaryOps")
			{
				Rule = ToTerm("++") | "--" | "+" | "-" | "!"
			};
			var postUnaryOps = new NonTerminal("postUnaryOps")
			{
				Rule = ToTerm("++") | "--"
			};
			var binOps = new NonTerminal("binOps")
			{
				Rule = ToTerm("||") | "&&" | "^" | "|" | "&" | "==" | "!=" | "<=" | ">=" | "<" | ">" | "<<" | ">>" | "+" | "-" | "*" | "/" | "%"
			};
			var assignmentOps = new NonTerminal("assignmentOps")
			{
				Rule = ToTerm("=") | "+=" | "-=" | "*=" | "/=" | "%=" | "&=" | "|=" | "^=" | "<<=" | ">>="
			};

			var exprAssignment = new NonTerminal("exprAssignment");
			var exprCondition = new NonTerminal("exprCondition");
			var exprBin = new NonTerminal("exprBin");
			var exprRange = new NonTerminal("exprRange");
			var exprPreUnary = new NonTerminal("exprPreUnary");
			var exprPostUnary = new NonTerminal("exprPostUnary");
			var exprParen = new NonTerminal("exprParen");
			var exprLambda = new NonTerminal("exprLambda");

			var parameters = new NonTerminal("parameters");
			var identifiers = new NonTerminal("identifiers");
			var keyValuePairs = new NonTerminal("keyValuePairs");
			var keyValuePair = new NonTerminal("keyValuePair");
			var @object = new NonTerminal("object");
			var array = new NonTerminal("array");


			keyValuePair.Rule =
				(identifier | stringLiteral) + ":" + expression;

			keyValuePairs.Rule =
				MakeStarRule(keyValuePairs, ToTerm(","), keyValuePair);

			parameters.Rule =
				MakeStarRule(parameters, ToTerm(","), expression);

			identifiers.Rule =
				MakeStarRule(identifiers, ToTerm(","), identifier);

			@object.Rule =
				"{" + keyValuePairs + "}";

			array.Rule =
				"[" + parameters + "]";

			statementCanBeChild.Rule =
				statementIf |
				statementFor |
				statementWhile |
				statementDoWhile |
				statementRepeat |
				statementReturn |
				statementBreak |
				statementContinue |
				expression + ";" |
				";" |
				block;

			statement.Rule =
				statementCanBeChild |
				statementVar |
				statementConst |
				statementGroup |
				statementFunc;

			statements.Rule =
				MakeStarRule(statements, statement);

			block.Rule =
				"{" + statements + "}";

			expression.Rule =
				exprAssignment |
				exprLambda |
				numberLiteral |
				stringLiteral |
				identifier |
				@object |
				array |
				"null" | "true" | "false";

			exprParen.Rule =
				// (expr)
				"(" + expression + ")";

			exprLambda.Rule =
				// (params) => <expr | block>;
				"(" + identifiers + ")" + "=>" + (expression | block);

			exprAssignment.Rule = exprCondition |
				// x = y  x += y  x -= y  x *= y  x /= y  x %= y  x &= y  x |= y  x ^= y  x <<= y  x >>= y
				expression + assignmentOps + expression;

			exprCondition.Rule = exprBin |
				// c ? t : f
				expression + "?" + expression + ":" + expression;

			exprBin.Rule = exprRange |
				expression + binOps + expression;

			exprRange.Rule = exprPreUnary |
				// f -> t
				numberLiteral + "->" + numberLiteral |
				// f -> t @ s
				numberLiteral + "->" + numberLiteral + "@" + numberLiteral;

			exprPreUnary.Rule = exprPostUnary |
				// ++x --x +x -x !x
				preUnaryOps + expression;

			exprPostUnary.Rule = exprParen |
				// x++ x--
				expression + postUnaryOps |
				// x.y
				expression + "." + identifier |
				// x[y]
				expression + "[" + expression + "]" |
				// f(x)
				expression + "(" + parameters + ")";

			option.Rule =
				ToTerm("option") + "strict" + ";";

			options.Rule =
				MakeStarRule(options, option);

			statementVar.Rule =
				// const id = expr;
				"var" + identifier + "=" + expression + ";" |
				"var" + identifier + ";";

			statementConst.Rule =
				// const id = expr;
				"const" + identifier + "=" + expression + ";";

			statementIf.Rule =
				// if (expr) statement
				ToTerm("if") + "(" + expression + ")" + statementCanBeChild |
				// if (expr) statement else statement
				ToTerm("if") + "(" + expression + ")" + statementCanBeChild + "else" + statementCanBeChild;

			statementFor.Rule =
				// for (id in expr) statement
				ToTerm("for") + "(" + identifier + "in" + expression + ")" + statementCanBeChild;

			statementWhile.Rule =
				// while (expr) statement
				ToTerm("while") + "(" + expression + ")" + statementCanBeChild;

			statementDoWhile.Rule =
				// do statement while (expr);
				"do" + statementCanBeChild + "while" + "(" + expression + ")" + ";";

			statementRepeat.Rule =
				// repeat statement
				"repeat" + statementCanBeChild;

			statementGroup.Rule =
				// group id { groupChildren }
				"group" + identifier + "{" + groupChildren + "}";

			groupChildren.Rule =
				MakeStarRule(groupChildren, groupChild);

			groupChild.Rule =
				statementVar | statementConst | statementFunc;

			statementFunc.Rule =
				// func id(identifiers) { statements }
				"func" + identifier + "(" + identifiers + ")" + block;

			statementReturn.Rule =
				// return;  return expr;
				ToTerm("return") + ";" |
				"return" + expression + ";";

			statementBreak.Rule =
				// break;
				ToTerm("break") + ";";

			statementContinue.Rule =
				// break;
				ToTerm("continue") + ";";

			Root = new NonTerminal("program")
			{
				Rule = options + statements
			};

			RegisterOperators(1, Associativity.Right, "=", "+=", "-=", "*=", "/=", "&=", "|=", "^=", "<<=", ">>=");
			RegisterOperators(2, "||");
			RegisterOperators(3, "&&");
			RegisterOperators(4, "^");
			RegisterOperators(5, "|");
			RegisterOperators(6, "&");
			RegisterOperators(7, "==", "!=", "<=", ">=", "<", ">");
			RegisterOperators(8, "<<", ">>");
			RegisterOperators(9, "+", "-");
			RegisterOperators(10, "*", "/", "%");

			MarkReservedWords("var", "const", "if", "else", "func", "group", "for", "in", "while", "do", "repeat", "break", "continue", "return", "option", "strict");
			MarkPunctuation("{", "}", ",", ";", ":", "[", "]", "(", ")", "var", "const", "if", "else", "func", "group", "for", "in", "while", "do", "repeat", "break", "continue", "return", "option");
			RegisterBracePair("(", ")");
			RegisterBracePair("[", "]");
			RegisterBracePair("{", "}");
			MarkTransient(
				statementCanBeChild,
				groupChild,
				expression,
				statement,
				assignmentOps,
				binOps,
				preUnaryOps,
				postUnaryOps,
				option
			);

			LanguageFlags = LanguageFlags.NewLineBeforeEOF;
			if (generateAst)
				LanguageFlags |= LanguageFlags.CreateAst;
		}
	}
}