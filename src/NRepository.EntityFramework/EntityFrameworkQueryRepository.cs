namespace NRepository.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Utilities;

    public class EntityFrameworkQueryRepository : QueryRepositoryBase
    {
        public EntityFrameworkQueryRepository(DbContext dbContext)
            : this(dbContext, new DefaultQueryEventHandlers(), new DefaultQueryInterceptor())
        {
        }

        public EntityFrameworkQueryRepository(DbContext dbContext, IQueryEventHandler queryEventHandlers)
            : this(dbContext, queryEventHandlers, new DefaultQueryInterceptor())
        {
        }

        public EntityFrameworkQueryRepository(DbContext dbContext, IQueryInterceptor queryInterceptor)
            : this(dbContext, new DefaultQueryEventHandlers(), queryInterceptor)
        {
        }

        public EntityFrameworkQueryRepository(DbContext dbContext, IQueryEventHandler queryEventHandlers, IQueryInterceptor queryInterceptor)
            : base(queryEventHandlers, queryInterceptor)
        {
            Check.NotNull(dbContext, "dbContext");

            ObjectContext = dbContext;
        }

        public override IQueryable<T> GetQueryableEntities<T>(object additionalQueryData)
        {
            var set = ((DbContext)ObjectContext).Set<T>();
            var retVal = QueryInterceptor.Query<T>(this, set.AsQueryable<T>(), additionalQueryData);
            return retVal;
        }
    }
}
