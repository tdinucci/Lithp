using System;

namespace Lithp.Functions
{
    public abstract class Function
    {
        public virtual bool DeferChildExecution { get; } = false;

        public abstract string Identifier { get; }
        public abstract int? MinArgCount { get; }
        public abstract int? MaxArgCount { get; }

        protected ScopeManager ScopeManager { get; }

        protected Function(ScopeManager scopeManager)
        {
            ScopeManager = scopeManager ?? throw new ArgumentNullException(nameof(scopeManager));
        }

        public abstract object Execute(object[] args);
    }
}