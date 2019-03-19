namespace Lithp.Functions
{
    public class EqualFunction : EqualityFunction
    {
        public override string Identifier { get; } = "=";

        public EqualFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(object[] args)
        {
            return Equals(args[0], args[1]);
        }
    }
}