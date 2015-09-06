namespace NRepository.EntityFramework
{
    using System.Data.Entity;
    using NRepository.Core;
    using NRepository.EntityFramework.Utilities;

    public class EntityFrameworkRepository : RepositoryBase
    {
        public EntityFrameworkRepository(DbContext context)
            : this(context, new DefaultRepositoryEventsHandlers(), new DefaultRepositoryInterceptors())
        {
        }

        public EntityFrameworkRepository(DbContext context, IRepositoryEventsHandlers queryEventHandlers)
            : this(context, queryEventHandlers, new DefaultRepositoryInterceptors())
        {
        }

        public EntityFrameworkRepository(DbContext context, IRepositoryInterceptors repositoryInterceptors)
            : this(context, new DefaultRepositoryEventsHandlers(), repositoryInterceptors)
        {
        }

        public EntityFrameworkRepository(
            DbContext context,
            IRepositoryEventsHandlers eventHandlers,
            IRepositoryInterceptors repositoryInterceptors)
            : base(
               new EntityFrameworkQueryRepository(context, eventHandlers, repositoryInterceptors.QueryInterceptor),
               new EntityFrameworkCommandRepository(context, eventHandlers, repositoryInterceptors))
        {
            Check.NotNull(context, "context");

            ObjectContext = context;
        }
    }
}