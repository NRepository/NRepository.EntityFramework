namespace NRepository.EntityFramework.Query
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using NRepository.Core;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Utilities;

    public class EagerLoadingQueryStrategy<T> : QueryStrategy where T : class
    {
        private List<string> _Includes = new List<string>();

        public EagerLoadingQueryStrategy()
        {
        }

        public EagerLoadingQueryStrategy(params Expression<Func<T, object>>[] properties)
        {
            Check.NotNull(properties, "properties");
            
            properties.ToList().ForEach(p => _Includes.Add(PropertyInfo<T>.GetMemberName(p)));
        }

        public IEnumerable<string> Includes
        {
            get { return _Includes; }
        }

        public EagerLoadingQueryStrategy<T> Add(Expression<Func<T, object>> expression)
        {
            Check.NotNull(expression, "expression");

            return Add(expression, true);
        }

        public EagerLoadingQueryStrategy<T> Add(Expression<Func<T, object>> expression, bool onCondition)
        {
            Check.NotNull(expression, "expression");

            if (onCondition)
                _Includes.Add(PropertyInfo<T>.GetMemberName(expression));

            return this;
        }

        public EagerLoadingQueryStrategy<T> Add(string include)
        {
            return Add(include, true);
        }

        public EagerLoadingQueryStrategy<T> Add(string include, bool onCondition)
        {
            if (onCondition && !string.IsNullOrWhiteSpace(include))
                _Includes.Add(include);

            return this;
        }

        public override IQueryable<TEntity> GetQueryableEntities<TEntity>(object additionalQueryData)
        {
            var query = QueryableRepository.GetQueryableEntities<TEntity>(additionalQueryData);
            foreach (var include in _Includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
