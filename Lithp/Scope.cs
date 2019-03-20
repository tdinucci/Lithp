using System;
using System.Collections.Generic;

namespace Lithp
{
    public class Scope
    {
        private static int _no;

        private readonly Dictionary<string, object> _scopeVars = new Dictionary<string, object>();

        public int No { get; } = _no++;
        
        public bool Contains(string name)
        {
            return _scopeVars.ContainsKey(name);
        }

        public virtual void Add(string name, object value)
        {
            if (_scopeVars.ContainsKey(name))
            {
                throw new InvalidOperationException(
                    $"A variable called '{name}' has already been declared in this scope");
            }

            _scopeVars.Add(name, value);
        }

        public virtual object Get(string name)
        {
            if (!_scopeVars.TryGetValue(name, out var value))
                throw new InvalidOperationException($"No variable called '{name}' in this scope");

            return value;
        }
    }
}