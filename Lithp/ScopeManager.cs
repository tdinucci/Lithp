using System;
using System.Collections.Generic;

namespace Lithp
{
    public class ScopeManager
    {
        private readonly Scope _globalScope;
        private readonly Stack<Scope> _scopeStack = new Stack<Scope>();

        public ScopeManager(Scope globalScope)
        {
            _globalScope = globalScope ?? throw new ArgumentNullException(nameof(globalScope));
        }

        public void EnterScope()
        {
            _scopeStack.Push(new Scope());
        }

        public void LeaveScope()
        {
            _scopeStack.Pop();
        }

        public void Add(string varName, object varValue)
        {
            if (Contains(varName))
            {
                throw new InvalidOperationException(
                    $"A variable called '{varName}' already exists in the current scope");
            }

            if (_scopeStack.Count == 0)
                _globalScope.Add(varName, varValue);
            else
                _scopeStack.Peek().Add(varName, varValue);
        }

        public bool Contains(string varName)
        {
            return _globalScope.Contains(varName) || (_scopeStack.Count > 0 && _scopeStack.Peek().Contains(varName));
        }

        public object Get(string varName)
        {
            return _globalScope.Contains(varName) | _scopeStack.Count == 0
                ? _globalScope.Get(varName)
                : _scopeStack.Peek().Get(varName);
        }
    }
}