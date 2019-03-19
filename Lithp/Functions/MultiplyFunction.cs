namespace Lithp.Functions
{
    public class MultiplyFunction : MathsFunction
    {
        public override string Identifier { get; } = "*";

        public MultiplyFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }
        
        protected override double ApplyOperator(double running, double operand)
        {
            return running * operand;
        }
    }
}