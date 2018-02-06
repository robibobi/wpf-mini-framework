using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Patterns.Specification
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> spec;

        public NotSpecification(ISpecification<T> s)
        {
            spec = s;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return !spec.IsSatisfiedBy(candidate);
        }
    }
}
