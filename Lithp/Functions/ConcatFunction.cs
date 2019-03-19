using System.Linq;

namespace Lithp.Functions
{
    public class ConcatFunction : Function
    {
        public override string Identifier { get; } = "concat";
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; }

        public ConcatFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(object[] args)
        {
            var result = args
                .Select(a => a == null ? string.Empty : a.ToString())
                .Aggregate(string.Empty, (c, n) => c + n);

            return result;
        }
    }
}