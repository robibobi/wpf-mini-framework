using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Patterns.Specification
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> leftSpec;
        private readonly ISpecification<T> rightSpec;

        public OrSpecification(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            this.leftSpec = left;
            this.rightSpec = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return leftSpec.IsSatisfiedBy(candidate) ||
                rightSpec.IsSatisfiedBy(candidate);
        }
    }
}
