namespace Ebister
{
	using System;

	[Serializable]
	public class RuntimeException : Exception
	{
		public RuntimeException() { }
		public RuntimeException(string message) : base(message) { }
		public RuntimeException(string message, System.Exception inner) : base(message, inner) { }
		protected RuntimeException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}

	[System.Serializable]
	public class BreakException : System.Exception
	{
		public BreakException() { }
		public BreakException(string message) : base(message) { }
		public BreakException(string message, System.Exception inner) : base(message, inner) { }
		protected BreakException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}

	[System.Serializable]
	public class ContinueException : System.Exception
	{
		public ContinueException() { }
		public ContinueException(string message) : base(message) { }
		public ContinueException(string message, System.Exception inner) : base(message, inner) { }
		protected ContinueException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}

	[System.Serializable]
	public class ParserException : Exception
	{
		public ParserException() { }
		public ParserException(string message) : base(message) { }
		public ParserException(string message, System.Exception inner) : base(message, inner) { }
		protected ParserException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
