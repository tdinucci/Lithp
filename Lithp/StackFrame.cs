using System;

namespace Lithp
{
    public class StackFrame : Scope
    {
        private readonly Scope _globalScope;

        public StackFrame(Scope globalScope)
        {
            _globalScope = globalScope ?? throw new ArgumentNullException(nameof(globalScope));
        }

        public override void Add(string name, object value)
        {
            if (_globalScope.Contains(name))
            {
                throw new InvalidOperationException(
                    $"A variable called '{name}' already exists in the global scope");
            }

            base.Add(name, value);
        }

        public override object Get(string name)
        {
            return _globalScope.Contains(name) ? _globalScope.Get(name) : base.Get(name);
        }
    }
}