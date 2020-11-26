using System;
using System.IO;
using Ebister;
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

var runtime = new EbiRuntime();

Console.WriteLine("Ebister REPL v1.0.0");
Console.WriteLine("READY");

object? result = null;

while (true)
{
	Console.Write(result != null ? "(" + result + ") > " : "> ");
	var s = Console.ReadLine();
	if (s == null) break;

	try
	{
		result = runtime.Run(s);
	}
	catch (ParserException e)
	{
		Console.Error.WriteLine(e.Message);
	}
	catch (RuntimeException e)
	{
		Console.Error.WriteLine(e.Message);
	}
}

return 0;
