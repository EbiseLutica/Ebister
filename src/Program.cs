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

			var tree = parser.Parse(source);

			if (tree.HasErrors())
			{
				Console.Error.WriteLine("Interpreter Error!");
				tree.ParserMessages.ForEach(log =>
				{
					Console.Error.WriteLine($"{log.Level.ToString().ToUpperInvariant()} {log.Location}: {log.Message}");
				});
				return -1;
			}
			Console.WriteLine(tree.ToXml());
			return 0;
		}
	}

	public class CitrineScript
	{

	}
}
