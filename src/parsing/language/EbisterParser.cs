using System.Linq;
using Irony.Parsing;

namespace Ebister.Parsing
{
	public static class EbisterParser
	{
		public static ParseTree Parse(string sourceText)
		{
			var parser = new Parser(new EbisterGrammar());

			var tree = parser.Parse(sourceText);

			if (tree.HasErrors())
			{
				throw new SyntaxErrorException("Parser Error", tree.ParserMessages);
			}

			return tree;
		}
	}
}