using System;
using System.Linq;
using Ebister.Parsing.Node;
using Microsoft.VisualBasic.CompilerServices;

namespace Ebister
{
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
			if (!configuration.PersistContext)
				globalScope = EbiScope.CreateGlobalScope();
			EbiValueBase lastEvaluatedStatement = new EbiNull();

			var currentScope = globalScope;

			foreach (var (node, i) in program.Nodes.Select((node, i) => (node, i)))
			{
				lastEvaluatedStatement = Evaluate(currentScope, node);
			}

			return lastEvaluatedStatement;
		}

		private EbiValueBase Evaluate(EbiScope scope, StatementNode statement)
		{
			return statement switch
			{
				ExpressionStatementNode expr => Evaluate(scope, expr.Expression),
				_ => new EbiNull(),
			};
		}

		private EbiValueBase Evaluate(EbiScope scope, ExpressionNode expr)
		{
			return expr switch
			{
				LiteralNode l => l.Value,
				IdentifierNode id => scope[id.Name]?.Value ?? throw new RuntimeException($"No such identifier named '{id.Name}'"),
				CallExpressionNode call => Evaluate(scope, call),
				UnaryExpressionNode un => Evaluate(scope, un),
				BinaryExpressionNode bin => Evaluate(scope, bin),
				_ => new EbiNull(),
			};
		}

		private EbiValueBase Evaluate(EbiScope scope, CallExpressionNode call)
		{
			var callee = Evaluate(scope, call.Callee);
			var parameters = call.Parameters.Expressions.Select(p => Evaluate(scope, p)).ToArray();
			if (callee is EbiNativeFunction nativeFunc)
			{
				if (!EbiTypeHelper.IsAllValidTypes(parameters.Select(p => p.Type).ToArray(), nativeFunc.ArgumentTypes))
					throw new RuntimeException("Invalid arguments");
				return nativeFunc.Delegate.Invoke(parameters);
			}
			else if (callee is EbiFunction func)
			{
				throw new NotImplementedException("Unsupported yet");
			}
			else
			{
				throw new RuntimeException("Tried to call a non-callable expression");
			}
		}

		private EbiValueBase Evaluate(EbiScope scope, UnaryExpressionNode unary)
		{
			var term = Evaluate(scope, unary.Terminal);

			if (term is not EbiDouble d) throw new RuntimeException();

			return unary.Operator switch
			{
				"+" => d,
				"-" => -d,
				_ => throw new RuntimeException(),
			};
		}

		private EbiValueBase Evaluate(EbiScope scope, BinaryExpressionNode binary)
		{
			var left = Evaluate(scope, binary.TerminalLeft);
			var right = Evaluate(scope, binary.TerminalRight);

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

		private EbiScope globalScope = EbiScope.CreateGlobalScope();
	}

	public delegate void EbiVoidNativeFunctionDelegate(EbiValueBase[] args);
}
