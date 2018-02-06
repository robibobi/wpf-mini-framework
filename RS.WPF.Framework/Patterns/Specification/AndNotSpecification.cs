using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Patterns.Specification
{
    public class AndNotSpecification<T> : CompositeSpecification<T>
    {
        ISpecification<T> leftSpec;
        ISpecification<T> rightSpec;

        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpec = left;
            this.rightSpec = right;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
           return leftSpec.IsSatisfiedBy(candidate) &&
                rightSpec.IsSatisfiedBy(candidate) != true;
        }
    }
}
