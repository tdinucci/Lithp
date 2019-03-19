using System.Linq;

namespace Lithp.Functions
{
    public class ListFunction : Function
    {
        public override string Identifier { get; } = "list";
        public override int? MinArgCount { get; }
        public override int? MaxArgCount { get; }

        public ListFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(object[] args)
        {
            return args;
        }
    }
}