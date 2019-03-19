namespace Lithp.Functions
{
    public class AddFunction : MathsFunction
    {
        public override string Identifier { get; } = "+";

        public AddFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }
        
        protected override double ApplyOperator(double running, double operand)
        {
            return running + operand;
        }
    }
}