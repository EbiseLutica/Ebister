
using System;
using Irony.Parsing;

namespace Citrine.Scripting
{
    [Language("CitrineScript", "1.0", "Citrine Script")]
    public class CitrineScriptGrammar : Grammar
    {
        public static readonly string NameSingleComment = "SingleComment";
        public static readonly string NameMultilineComment = "SingleMultilineComment";
        public static readonly string NameNumberLiteral = "Number";
        public static readonly string NameStringLiteral = "String";
        public static readonly string NameBooleanLiteral = "Boolean";
        public static readonly string NameIdentifierTerminal = "Identifier";

        public CitrineScriptGrammar()
        {
            // コメント
            var singleComment = new CommentTerminal(NameSingleComment, "//", "\r", "\n", "\u2085", "\u2028", "\u2029");
            var multilineComment = new CommentTerminal(NameMultilineComment, "/*", "*/");
            NonGrammarTerminals.Add(singleComment);
            NonGrammarTerminals.Add(multilineComment);

            // 数値リテラル
            var num = TerminalFactory.CreateCSharpNumber(NameNumberLiteral);
            var str = TerminalFactory.CreateCSharpString(NameStringLiteral);
            var booleanLiteral = new NonTerminal(NameBooleanLiteral);
            
            // 予約語
            var keyTrue = ToTerm("true", "true");
            var keyFalse = ToTerm("false", "false");
            var keyNull = ToTerm("null", "null");
            var keyPlus = ToTerm("+", "plus");
            var keyMinus = ToTerm("-", "minus");
            var keyAsterisk = ToTerm("*", "asterisk");
            var keySlash = ToTerm("/", "slash");
            var keyGt = ToTerm(">", "greater_than");
            var keyGe = ToTerm(">=", "greater_equal");
            var keyLt = ToTerm("<", "lesser_than");
            var keyLe = ToTerm("<=", "lesser_equal");
            var keyEqualEqual = ToTerm("==", "equal_equal");
            var keyNEqual = ToTerm("!=", "not_equal");
            var keyPercent = ToTerm("%", "percent");
            var keyEqual = ToTerm("=", "equal");
            var keyCoronEqual = ToTerm(":=", "coron_equal");
            // var key = ToTerm("", "");
            
            // 識別子
            var identifier = TerminalFactory.CreateCSharpIdentifier(NameIdentifierTerminal);
            
            booleanLiteral.Rule = ToTerm("true");
        }
    }
}