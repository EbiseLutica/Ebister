using System;
using System.IO;
using Ebister.Parsing;
using Ebister.Parsing.Node;
using Irony.Interpreter;
using Irony.Parsing;


// if (args.Length == 0)
// {
// 	Console.Error.WriteLine("Specify source file");
// 	return -1;
// }
// if (!File.Exists(args[0]))
// {
// 	Console.Error.WriteLine("No such file");
// 	return -2;
// }
// var source = File.ReadAllText(args[0]);

var lang = new LanguageData(new EbisterGrammar());
var app = new ScriptApp(lang);

Console.WriteLine("Ebister REPL v1.0.0");
Console.WriteLine("READY");

while (true)
{
	Console.Write("> ");
	var s = Console.ReadLine();
	if (s == null) break;

	try
	{
		var ast = app.Evaluate(s);

		if (ast is not ProgramNode prg) throw new Exception("not ProgramNode");

		Console.WriteLine(prg);
	}
	catch (ScriptException e)
	{
		Console.Error.WriteLine($"Syntax Error: {e.Location}");
	}
}

return 0;
