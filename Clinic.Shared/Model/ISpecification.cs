namespace Clinic.Shared.Model
{
    public interface ISpecfication<in T>
    {
        bool IsSatisfiedBy(T entity);
    }

    public class OrSpecification<T> : ISpecfication<T>
    {
        private readonly IList<ISpecfication<T>> _specifications;

        public OrSpecification(params ISpecfication<T>[] specifications)
        {
            _specifications = [.. specifications];
        }
        public bool IsSatisfiedBy(T entity)
        {
            var isSatisfied = false;
            foreach (var specification in _specifications)
            {
                isSatisfied = isSatisfied || specification.IsSatisfiedBy(entity);
            }

            return isSatisfied;
        }
    }

    public class AndSpecification<T> : ISpecfication<T>
    {
        private readonly IList<ISpecfication<T>> _specifications;

        public AndSpecification(params ISpecfication<T>[] specifications)
        {
            _specifications = [.. specifications];
        }
        public bool IsSatisfiedBy(T entity)
        {
            var isSatisfied = true;
            foreach (var specification in _specifications)
            {
                isSatisfied = isSatisfied && specification.IsSatisfiedBy(entity);
            }

            return isSatisfied;
        }
    }
}
