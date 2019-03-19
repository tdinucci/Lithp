using System;
using System.Collections.Generic;

namespace Lithp
{
    public class ScopeManager
    {
        private readonly Scope _globalScope;
        private readonly Stack<Scope> _scopeStack;

        public ScopeManager(Scope globalScope, Stack<Scope> scopeStack)
        {
            _globalScope = globalScope ?? throw new ArgumentNullException(nameof(globalScope));
            _scopeStack = scopeStack ?? throw new ArgumentNullException(nameof(scopeStack));
        }

        public void EnterScope()
        {
            _scopeStack.Push(new StackFrame(_globalScope));
        }

        public void LeaveScope()
        {
            _scopeStack.Pop();
        }

        public Scope GetCurrentScope()
        {
            return _scopeStack.Count == 0
                ? _globalScope
                : _scopeStack.Peek();
        }
    }
}