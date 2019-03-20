using System;
using System.Collections.Generic;
using System.Linq;

namespace Lithp
{
    public class CustomFunction
    {
        public string Name { get; }
        public string[] Parameters { get; }
        public Func<object> Body { get; }

        public CustomFunction(string name, string[] parameters, Func<object> body)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

            Name = name;
            Parameters = parameters ?? new string[0];
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public object Execute()
        {
            var result = Body();
            if (result is IEnumerable<object> enumResult)
                return enumResult.Last();

            return result;
        }
    }

    public class CustomFunctionTable
    {
        private readonly Dictionary<string, CustomFunction> _functions = new Dictionary<string, CustomFunction>();

        public bool Contains(string name)
        {
            return _functions.ContainsKey(name);
        }

        public virtual void Add(CustomFunction function)
        {
            if (_functions.ContainsKey(function.Name))
            {
                throw new InvalidOperationException(
                    $"A function called '{function.Name}' has already been declared in this scope");
            }

            _functions.Add(function.Name, function);
        }

        public virtual CustomFunction GetCustomFunction(string name)
        {
            if (!_functions.TryGetValue(name, out var func))
                throw new InvalidOperationException($"No function called '{name}' in this scope");

            return func;
        }
    }
}