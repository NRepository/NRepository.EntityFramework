namespace NRepository.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using NRepository.Core;
    using NRepository.Core.Command;
    using NRepository.Core.Events;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Events;
    using NRepository.EntityFramework.Utilities;

    public class EntityFrameworkRepositoryExtensionImplementation : IRepositoryExtensions
    {
        public void AddRange<TEntity>(ICommandRepository commandRepository, IEnumerable<TEntity> entities) where TEntity : class
        {
            Check.NotNull(commandRepository, "commandRepository");
            Check.NotNull(entities, "entities");

            var dbContext = commandRepository.ObjectContext as DbContext;
            if (dbContext == null)
                throw new RepositoryException("AddRange can only be used with a DbContext");

            dbContext.Set<TEntity>().AddRange(entities);

            foreach (var entity in entities)
            {
                var entityAddedEvent = new EntityAddedEvent(commandRepository, entity);
                commandRepository.RaiseEvent(entityAddedEvent);
            }
        }

        public void UpdateEntityState<TEntity>(ICommandRepository commandRepository, TEntity entity, EntityState entityState) where TEntity : class
        {
            Check.NotNull(commandRepository, "commandRepository");
            Check.NotNull(entity, "entity");

            var dbContext = commandRepository.ObjectContext as DbContext;
            if (dbContext == null)
                throw new RepositoryException("ModifyEntityStateCommandInterceptor can only be used with a DbContext");

            var entry = dbContext.Entry(entity);
            var oldEntityState = entry.State;

            // Set the state
            entry.State = entityState;

            var stateModifiedEvent = new EntityStateModifiedCommandEvent<TEntity>(commandRepository, entity, oldEntityState, entityState);
            commandRepository.RaiseEvent(stateModifiedEvent);
        }

        public void Load<TEntity, TElement>(
               IQueryRepository repository,
               TEntity entity,
               Expression<Func<TEntity, TElement>> navigationProperty,
               params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            var dbContext = repository.ObjectContext as DbContext;
            if (dbContext == null)
                throw new RepositoryException("Load can only be used with a DbContext");

            var query = dbContext.Entry(entity).Reference(navigationProperty).Query();
            query = query.AddQueryStrategy(strategies);
            query.Load();

            var loadEvent = new LoadRepositoryQueryEvent<TEntity>(repository, entity, navigationProperty, new AggregateQueryStrategy(strategies));
            repository.RaiseEvent(loadEvent);
        }

        public void Load<TEntity, TElement>(
            IQueryRepository repository,
            TEntity entity,
            Expression<Func<TEntity, ICollection<TElement>>> navigationProperty,
            params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            var dbContext = repository.ObjectContext as DbContext;
            if (dbContext == null)
                throw new RepositoryException("Load can only be used with a DbContext");

            var query = dbContext.Entry(entity).Collection(navigationProperty).Query();
            query = query.AddQueryStrategy(strategies);
            query.Load();

            var loadEvent = new LoadRepositoryQueryEvent<TEntity>(repository, entity, navigationProperty, new AggregateQueryStrategy(strategies));
            repository.RaiseEvent(loadEvent);
        }

        public void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, IList<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            var dbContext = repository.ObjectContext as DbContext;
            if (dbContext == null)
                throw new RepositoryException("Load can only be used with a DbContext");

            var property = PropertyInfo<TEntity>.GetMemberName(navigationProperty);
            var query = (IQueryable<TElement>)dbContext.Entry(entity).Collection(property).Query();
            query = query.AddQueryStrategy(strategies);
            query.Load();

            var loadEvent = new LoadRepositoryQueryEvent<TEntity>(repository, entity, navigationProperty, new AggregateQueryStrategy(strategies));
            repository.RaiseEvent(loadEvent);
        }

        public int ExecuteStoredProcudure(ICommandRepository commandRepository, string sql, params object[] args)
        {
            Check.NotNull(commandRepository, "commandRepository");
            Check.NotEmpty(sql, "sql");

            if (!(commandRepository.ObjectContext is DbContext))
                throw new RepositoryException("ExecuteStoredProcudure can only be used with a DbContext");

            var database = ((DbContext)commandRepository.ObjectContext).Database;
            var retVal = database.ExecuteSqlCommand(sql, args);

            var stateModifiedEvent = new ExecuteStoredProcedureCommandEvent(commandRepository, sql, args);
            commandRepository.RaiseEvent(stateModifiedEvent);

            return retVal;
        }

        public IEnumerable<T> ExecuteSqlQuery<T>(IQueryRepository queryRepository, string sql, params object[] args)
        {
            Check.NotNull(queryRepository, "queryRepository");
            Check.NotEmpty(sql, "sql");

            if (!(queryRepository.ObjectContext is DbContext))
                throw new RepositoryException("ExecuteStoredProcudure can only be used with a DbContext");

            var database = ((DbContext)queryRepository.ObjectContext).Database;
            IEnumerable<T> data = database.SqlQuery<T>(sql, args);

            var stateModifiedEvent = new ExecuteSqlQueryEvent(queryRepository, sql, args);
            queryRepository.RaiseEvent(stateModifiedEvent);

            return data;
        }
    }
}
