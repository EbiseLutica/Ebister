using System;
using System.Collections.Generic;
using System.Linq;

namespace Ebister.Runtime
{
	public class EbiObject
	{
		public EbiValue this[string key]
		{
			get => members.ContainsKey(key) ? members[key].value : EbiValue.Null;
			set
			{
				if (members[key].isReadonly)
					throw new ArgumentException("readonly");
				members[key] = (value, false);
			}
		}

		public EbiObject() { }
		public EbiObject(object dotnetObject)
		{
			dotnetObject
				.ToDictionary()
				.ToList()
				.ForEach(kv => this[kv.Key] = new EbiValue(kv.Value));
		}


		public bool IsReadonly(string key) => members.ContainsKey(key) && members[key].isReadonly;

		public bool IsDefined(string key) => members.ContainsKey(key);

		public void DefineReadonly(string key, EbiValue value)
		{
			if (IsDefined(key))
			{
				throw new ArgumentException("already defined");
			}
			members[key] = (value, true);
		}

		private readonly Dictionary<string, (EbiValue value, bool isReadonly)> members = new Dictionary<string, (EbiValue, bool)>();
	}
}
