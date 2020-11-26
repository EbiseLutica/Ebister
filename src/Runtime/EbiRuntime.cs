using System;
using System.Collections.Generic;
using System.Linq;
using Ebister.Parsing.Node;
using Microsoft.VisualBasic.CompilerServices;

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
			get
			{
				return variables.ContainsKey(name) ? variables[name] : Parent?[name];
			}
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
			return InstallStd(new EbiScope());
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

	public class EbiRuntime
	{
		public EbiRuntime(RuntimeConfiguration? conf = null)
		{
			configuration = conf ?? new RuntimeConfiguration();
		}

		public object? Run(string source)
		{
			return Run(EbiParser.Parse(source));
		}

		public EbiValueBase Run(ProgramNode program)
		{
			EbiValueBase lastEvaluatedStatement = new EbiNull();
			foreach (var (node, i) in program.Nodes.Select((node, i) => (node, i)))
			{
				lastEvaluatedStatement = Evaluate(node);
			}

			return lastEvaluatedStatement;
		}

		private EbiValueBase Evaluate(StatementNode statement)
		{
			return statement switch
			{
				ExpressionStatementNode expr => Evaluate(expr.Expression),
				_ => new EbiNull(),
			};
		}

		private EbiValueBase Evaluate(ExpressionNode expr)
		{
			return expr switch
			{
				LiteralNode l => l.Value,
				UnaryExpressionNode un => Evaluate(un),
				BinaryExpressionNode bin => Evaluate(bin),
				_ => new EbiNull(),
			};
		}

		private EbiValueBase Evaluate(UnaryExpressionNode unary)
		{
			var term = Evaluate(unary.Terminal);

			if (term is not EbiDouble d) throw new RuntimeException();

			return unary.Operator switch
			{
				"+" => d,
				"-" => -d,
				_ => throw new RuntimeException(),
			};
		}

		private EbiValueBase Evaluate(BinaryExpressionNode binary)
		{
			var left = Evaluate(binary.TerminalLeft);
			var right = Evaluate(binary.TerminalRight);

			switch (binary.Operator)
			{
				case "+":
					{
						if (left is EbiString s)
							return s + right;
						else if (left is EbiDouble l && right is EbiDouble r)
							return l + r;
						else
							throw new RuntimeException($"Type mismatch({left.TypeName} + {right.TypeName})");
					}
				case "-":
					{
						if (left is EbiDouble l && right is EbiDouble r)
							return l - r;
						else
							throw new RuntimeException($"Type mismatch({left.TypeName} - {right.TypeName})");
					}
				case "*":
					{
						if (left is EbiDouble l && right is EbiDouble r)
							return l * r;
						else
							throw new RuntimeException($"Type mismatch({left.TypeName} * {right.TypeName})");
					}
				case "/":
					{
						if (left is EbiDouble l && right is EbiDouble r)
							return l / r;
						else
							throw new RuntimeException($"Type mismatch({left.TypeName} / {right.TypeName})");
					}
				case "%":
					{
						if (left is EbiDouble l && right is EbiDouble r)
							return l % r;
						else
							throw new RuntimeException($"Type mismatch({left.TypeName} % {right.TypeName})");
					}
				default:
					throw new RuntimeException();
			}
		}

		private RuntimeConfiguration configuration;
	}

	public delegate void EbiVoidNativeFunctionDelegate(EbiValueBase[] args);
}
