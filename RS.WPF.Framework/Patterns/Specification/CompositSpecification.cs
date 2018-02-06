using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS.WPF.Framework.Patterns.Specification
{
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T candidate);
        public ISpecification<T> And(ISpecification<T> other) => new AndSpecification<T>(this, other);
        public ISpecification<T> AndNot(ISpecification<T> other) => new AndNotSpecification<T>(this, other);
        public ISpecification<T> Or(ISpecification<T> other) => new OrSpecification<T>(this, other);
        public ISpecification<T> OrNot(ISpecification<T> other) => new OrNotSpecification<T>(this, other);
        public ISpecification<T> Not() => new NotSpecification<T>(this);
    }
}
