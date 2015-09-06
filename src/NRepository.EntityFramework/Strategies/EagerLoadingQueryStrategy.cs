namespace NRepository.EntityFramework.Query
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Utilities;

    public class EagerLoadingQueryStrategy : QueryStrategy
    {
        private readonly List<string> _Includes;

        public EagerLoadingQueryStrategy(params string[] includes)
        {
            Check.NotNull(includes, "includes");

            _Includes = new List<string>();
            _Includes.AddRange(includes);
            _Includes = _Includes.Where(p => !string.IsNullOrEmpty(p)).ToList();
        }

        public EagerLoadingQueryStrategy(IEnumerable<string> groupIncludes, params string[] includes)
        {
            Check.NotNull(groupIncludes, "groupIncludes");
            Check.NotNull(includes, "includes");

            _Includes = new List<string>();
            _Includes.AddRange(includes);
            _Includes.AddRange(groupIncludes);
            _Includes = _Includes.Where(p => !string.IsNullOrEmpty(p)).ToList();
        }

        public EagerLoadingQueryStrategy(IEnumerable<string> groupIncludes, IEnumerable<string> groupIncludes2, params string[] includes)
        {
            Check.NotNull(groupIncludes, "groupIncludes");
            Check.NotNull(groupIncludes, "groupIncludes2");
            Check.NotNull(groupIncludes, "includes");
            
            _Includes = new List<string>();
            _Includes.AddRange(includes);
            _Includes.AddRange(groupIncludes);
            _Includes.AddRange(groupIncludes2);
            _Includes = _Includes.Where(p => !string.IsNullOrEmpty(p)).ToList();
        }

        public IEnumerable<string> Includes
        {
            get { return _Includes; }
        }

        public EagerLoadingQueryStrategy Add(string include)
        {
            return Add(include, true);
        }

        public EagerLoadingQueryStrategy Add(string include, bool onCondition)
        {
            if (onCondition && !string.IsNullOrWhiteSpace(include))
                _Includes.Add(include);

            return this;
        }

        public override IQueryable<T> GetQueryableEntities<T>(object additionalQueryData)
        {
            var query = QueryableRepository.GetQueryableEntities<T>(additionalQueryData);
            if (_Includes != null)
            {
                foreach (var include in _Includes)
                {
                    if (include != null)
                        query = query.Include(include);
                }
            }

            return query;
        }
    }
}