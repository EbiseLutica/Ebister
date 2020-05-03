using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Irony.Parsing;

namespace Ebister
{
	public static class Extension
	{
		public static ParseTreeNode? FindByTermName(this ParseTreeNode node, string termName) => node.ChildNodes.FirstOrDefault(n => n.Term.Name == termName);

		public static Dictionary<string, object?> ToDictionary(this object obj, BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
		{
			return obj.GetType().GetProperties(flags).ToDictionary(info => info.Name, info => info.GetValue(obj));
		}
	}
}
