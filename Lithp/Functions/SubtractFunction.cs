namespace Lithp.Functions
{
    public class SubtractFunction : MathsFunction
    {
        public override string Identifier { get; } = "-";

        public SubtractFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }
        
        protected override double ApplyOperator(double running, double operand)
        {
            return running - operand;
        }
    }
}