namespace Lithp.Functions
{
    public class DivideFunction : MathsFunction
    {
        public override string Identifier { get; } = "/";

        public DivideFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }
        
        protected override double ApplyOperator(double running, double operand)
        {
            return running / operand;
        }
    }
}