namespace Lithp.Functions
{
    public abstract class EqualityFunction : Function
    {
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; } = 2;

        protected EqualityFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }
    }
}