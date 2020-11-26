using System;
using Ebister.Parsing;
using Ebister.Parsing.Node;
using Irony.Interpreter;
using Irony.Parsing;

namespace Ebister
{
	public static class EbiParser
	{
		public static ProgramNode Parse(string source)
		{
			try
			{
				return (ProgramNode)app.Evaluate(source);
			}
			catch (ScriptException e)
			{
				throw new ParserException(e.Message, e);
			}
		}

		private static readonly ScriptApp app = new ScriptApp(new LanguageData(new EbisterGrammar()));
	}
}
