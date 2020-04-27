
using System;
using Irony.Interpreter.Ast;
using Irony.Parsing;

namespace Citrine.Scripting
{
	[Language("CitrineScript", "1.0", "CitrineScript")]
	public class CitrineScriptGrammar : Grammar
	{
		public CitrineScriptGrammar()
		{
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

			var statements = new NonTerminal("statements");
			var statementCanBeChild = new NonTerminal("statementCanBeChild");
			var statement = new NonTerminal("statement");
			var block = new NonTerminal("block");
			var expression = new NonTerminal("expression");

			var option = new NonTerminal("option");
			var options = new NonTerminal("options");

			var statementVar = new NonTerminal("statementVar");
			var statementConst = new NonTerminal("statementConst");
			var statementIf = new NonTerminal("statementIf");
			var statementFor = new NonTerminal("statementFor");
			var statementWhile = new NonTerminal("statementWhile");
			var statementDoWhile = new NonTerminal("statementDoWhile");
			var statementRepeat = new NonTerminal("statementRepeat");
			var statementGroup = new NonTerminal("statementGroup");
			var statementFunc = new NonTerminal("statementFunc");
			var statementReturn = new NonTerminal("statementReturn");
			var statementBreak = new NonTerminal("statementBreak");
			var statementContinue = new NonTerminal("statementContinue");

			var groupChildren = new NonTerminal("groupChildren");

			var exprAssignment = new NonTerminal("exprAssignment");
			var exprCondition = new NonTerminal("exprLambda");
			var exprOr2 = new NonTerminal("exprOr");
			var exprAnd2 = new NonTerminal("exprAnd");
			var exprXor = new NonTerminal("exprAnd");
			var exprOr = new NonTerminal("exprAnd");
			var exprAnd = new NonTerminal("exprAnd");
			var exprComparision = new NonTerminal("exprComparision");
			var exprShift = new NonTerminal("exprShift");
			var exprAddSub = new NonTerminal("exprAddSub");
			var exprMulDiv = new NonTerminal("exprMulDiv");
			var exprRange = new NonTerminal("exprRange");
			var exprPreUnary = new NonTerminal("exprUnary");
			var exprPostUnary = new NonTerminal("exprPreIncDec");
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
				exprParen |
				exprAssignment |
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
				(("(" + identifiers + ")") | identifier) + "=>" + (expression | block);

			exprAssignment.Rule = exprCondition |
				exprLambda |
				// x = y  x += y  x -= y  x *= y  x /= y  x %= y  x &= y  x |= y  x ^= y  x <<= y  x >>= y
				expression + (ToTerm("=") | "+=" | "-=" | "*=" | "/=" | "%=" | "&=" | "|=" | "^=" | "<<=" | ">>=") + expression;

			exprCondition.Rule = exprOr2 |
				// c ? t : f
				expression + "?" + expression + ":" + expression;

			exprOr2.Rule = exprAnd2 |
				// x || y
				expression + "||" + expression;

			exprAnd2.Rule = exprXor |
				// x && y
				expression + "&&" + expression;

			exprXor.Rule = exprOr |
				// x ^ y
				expression + "^" + expression;

			exprOr.Rule = exprAnd |
				// x | y
				expression + "|" + expression;

			exprAnd.Rule = exprComparision |
				// x & y
				expression + "&" + expression;

			exprComparision.Rule = exprShift |
				// x == y  x != y  x <= y  x >= y  x < y  x > y
				expression + (ToTerm("==") | "!=" | "<=" | ">=" | "<" | ">") + expression;

			exprShift.Rule = exprAddSub |
				// x << y  x >> y
				expression + (ToTerm("<<") | ">>") + expression;

			exprAddSub.Rule = exprMulDiv |
				// x + y  x - y
				expression + (ToTerm("+") | "-") + expression;

			exprMulDiv.Rule = exprRange |
				// x * y  x / y  x % y
				expression + (ToTerm("*") | "/" | "%") + expression;

			exprRange.Rule = exprPreUnary |
				// f -> t
				numberLiteral + "->" + numberLiteral |
				// f -> t @ s
				numberLiteral + "->" + numberLiteral + "@" + numberLiteral;

			exprPreUnary.Rule = exprPostUnary |
				// ++x --x +x -x !x
				(ToTerm("++") | "--" | "+" | "-" | "!") + expression;

			exprPostUnary.Rule =
				// x++ x--
				expression + (ToTerm("++") | ToTerm("--")) |
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
				"var" + identifier + "=" + expression + ";";

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
				MakeStarRule(groupChildren,
					statementVar | statementConst | statementFunc
				);

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
		}
	}
}