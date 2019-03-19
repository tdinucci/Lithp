using System;
using System.Collections.Generic;

namespace Lithp
{
    public class CustomFunctionTable
    {
        private readonly Dictionary<string, LithpList[]> _functions = new Dictionary<string, LithpList[]>();
 
        public bool Contains(string name)
        {
            return _functions.ContainsKey(name);
        }

        public virtual void Add(string name, LithpList[] funcLists)
        {
            if (_functions.ContainsKey(name))
            {
                throw new InvalidOperationException(
                    $"A function called '{name}' has already been declared in this scope");
            }

            _functions.Add(name, funcLists);
        }

        public virtual LithpList[] Get(string name)
        {
            if (!_functions.TryGetValue(name, out var funcLists))
                throw new InvalidOperationException($"No function called '{name}' in this scope");

            return funcLists;
        }
    }
}