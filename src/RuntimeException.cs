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
