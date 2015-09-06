namespace NRepository.EntityFramework.Events
{
    using System;
    using NRepository.Core.Events;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Utilities;

    public class LoadRepositoryQueryEvent<TEntity> : RepositoryQueryEvent where TEntity : class
    {
        public LoadRepositoryQueryEvent(IQueryRepository repository, TEntity entity, object navigationProperty, IQueryStrategy queryStrategy)
            : base(repository)
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            
            Entity = entity;
            QueryStrategy = queryStrategy;
            NavigationProperty = navigationProperty;
        }

        public TEntity Entity
        {
            get;
            private set;
        }

        public object NavigationProperty
        {
            get;
            private set;
        }

        public IQueryStrategy QueryStrategy
        {
            get;
            private set;
        }
    }
}