using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Patterns.Specification
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        private ISpecification<T> leftSpec;
        private ISpecification<T> rightSpec;

        public AndSpecification(
            ISpecification<T> left,
            ISpecification<T> right)
        {
            leftSpec = left;
            rightSpec = right;
        }


        public override bool IsSatisfiedBy(T candidate)
        {
            return leftSpec.IsSatisfiedBy(candidate) &&
                rightSpec.IsSatisfiedBy(candidate);
        }


    }
}
