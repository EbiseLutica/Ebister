using System;

namespace Ebister
{
	public class EbiNativeFunction : EbiValueBase
	{
		public override string TypeName => "native function";
		public override EbiType Type => EbiType.NativeFunction;

		public string? Name { get; }
		public EbiType ReturnType { get; }
		public EbiType[] ArgumentTypes { get; }
		public EbiNativeFunctionDelegate Delegate { get; }

		public EbiNativeFunction(string? name, EbiNativeFunctionDelegate d) : this(name, d, Array.Empty<EbiType>()) { }

		public EbiNativeFunction(EbiNativeFunctionDelegate d) : this(null, d, Array.Empty<EbiType>()) { }

		public EbiNativeFunction(string? name, EbiNativeFunctionDelegate d, params EbiType[] argTypes) : this(name, d, EbiType.Void, argTypes) { }

		public EbiNativeFunction(EbiNativeFunctionDelegate d, params EbiType[] argTypes) : this(null, d, EbiType.Void, argTypes) { }

		public EbiNativeFunction(EbiNativeFunctionDelegate d, EbiType returnType, params EbiType[] argTypes) : this(null, d, returnType, argTypes) { }

		public EbiNativeFunction(string? name, EbiNativeFunctionDelegate d, EbiType returnType, params EbiType[] argTypes)
		{
			Name = name;
			ReturnType = returnType;
			ArgumentTypes = argTypes;
			Delegate = d;
		}

		public override string ToString() => $"[NativeFunction {Name}]";
	}

	public delegate EbiValueBase EbiNativeFunctionDelegate(EbiValueBase[] args);
}
