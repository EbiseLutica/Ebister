using System;
using System.Collections.Generic;

namespace Ebister
{
	public class EbiScope
	{
		public EbiScope? Parent { get; }

		public EbiScope(EbiScope? parent = null)
		{
			Parent = parent;
		}

		public EbiConstant? this[string name]
		{
			get => variables.ContainsKey(name) ? variables[name] : Parent?[name];
			set
			{
				if (value == null)
					variables.Remove(name);
				else
					variables[name] = value;
			}
		}

		public static EbiScope CreateGlobalScope()
		{
			var scope = new EbiScope();
			InstallStd(scope);

			return scope;
		}

		private static EbiScope InstallStd(EbiScope scope)
		{
			scope["print"] = DefineFunction(args =>
			{
				Console.Write(args[0].ToEbiString().ToDotNetObject());
			}, EbiType.Any);

			scope["printLine"] = DefineFunction(args =>
			{
				Console.WriteLine(args[0].ToEbiString().ToDotNetObject());
			}, EbiType.Any);

			scope["inputLine"] = DefineFunction(args =>
			{
				return EbiValueBase.ToEbiObject(Console.ReadLine());
			}, EbiType.String);
			return scope;
		}

		private static EbiConstant DefineFunction(EbiNativeFunctionDelegate del, EbiType returnType = EbiType.Void, params EbiType[] paramTypes)
		{
			return new EbiConstant(new EbiNativeFunction(del, returnType, paramTypes));
		}

		private static EbiConstant DefineFunction(EbiVoidNativeFunctionDelegate del, params EbiType[] paramTypes)
		{
			return new EbiConstant(new EbiNativeFunction((args) =>
			{
				del(args);
				return new EbiVoid();
			}, EbiType.Void, paramTypes));
		}

		private readonly Dictionary<string, EbiConstant> variables = new Dictionary<string, EbiConstant>();
	}
}
