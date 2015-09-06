namespace NRepository.EntityFramework.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using NRepository.Core.Query;

    public class FamilyQueryRepository : QueryRepositoryBase
    {
        private List<object> Objects = new List<object>();

        public FamilyQueryRepository()
            : this(new DefaultQueryEventHandlers(), new DefaultQueryInterceptor())
        {
        }

        public FamilyQueryRepository(IQueryInterceptor queryInterceptor)
            : this(new DefaultQueryEventHandlers(), queryInterceptor)
        {
        }

        public FamilyQueryRepository(IQueryEventHandler queryEventHandlers)
            : this(queryEventHandlers, new DefaultQueryInterceptor())
        {
        }

        public FamilyQueryRepository(IQueryEventHandler queryEventHandlers, IQueryInterceptor queryInterceptor)
            : base(queryEventHandlers, queryInterceptor)
        {
            ObjectContext = Objects;
        }

        public override IQueryable<T> GetQueryableEntities<T>(object additionalData)
        {
            var query = Objects.OfType<T>().AsQueryable<T>();
            var retVal = QueryInterceptor.Query<T>(this, query, additionalData);
            return retVal;
        }
    }
}
