using System;
using System.IO;
using Ebister.Parsing;

namespace Ebister
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

			var runtime = new Ebister();

			try
			{
				runtime.Evaluate(source);
			}
			catch (SyntaxErrorException err)
			{
				err.Log.ForEach(log =>
				{
					Console.Error.WriteLine($"{log.Level.ToString().ToUpperInvariant()} {log.Location}: {log.Message}");
				});
				return -1;
			}
			catch (RuntimeException err)
			{
				Console.WriteLine("Runtime error! " + err.Message);
				return -1;
			}

			return 0;
		}
	}
}
