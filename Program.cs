using System;
using System.IO;
using Irony.Parsing;

namespace Citrine.Scripting
{
	class Program
	{
		static int Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.Error.WriteLine("Specify source file");
				return -1;
			}
			if (!File.Exists(args[0]))
			{
				Console.Error.WriteLine("No such file");
				return -2;
			}
			var source = File.ReadAllText(args[0]);
			var parser = new Parser(new CitrineScriptGrammar());
			var ast = parser.Parse(source);

			ast.ParserMessages.ForEach(log =>
			{
				Console.WriteLine($"{log.Level.ToString().ToUpperInvariant()} {log.Location}: {log.Message}");
			});
			Console.WriteLine(ast.ToXml());
			return 0;
		}
	}
}
