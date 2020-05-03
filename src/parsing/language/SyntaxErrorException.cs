using System;
using Irony;

namespace Ebister.Parsing
{
	[Serializable]
	public class SyntaxErrorException : Exception
	{
		public LogMessageList Log { get; }
		public SyntaxErrorException(string message, LogMessageList log) : base(message) { Log = log; }
	}
}
