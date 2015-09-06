namespace NRepository.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using NRepository.Core;
    using NRepository.Core.Query;
    using NRepository.Core.Command;
    using NRepository.EntityFramework.Utilities;

    public static class EntityFrameworkRepositoryExtensions
    {
        private static IRepositoryExtensions _DefaultImplementation = new EntityFrameworkRepositoryExtensionImplementation();

        public static void SetDefaultImplementation(IRepositoryExtensions defaultImplementation)
        {
            Check.NotNull(defaultImplementation, "defaultImplementation");

            _DefaultImplementation = defaultImplementation;
        }

        public static void UpdateEntityState<TEntity>(this IRepository repository, TEntity entity, EntityState entityState) where TEntity : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");

            _DefaultImplementation.UpdateEntityState(repository, entity, entityState);
        }

        public static void AddRange<TEntity>(this ICommandRepository commandRepository, IEnumerable<TEntity> entities) where TEntity : class
        {
            Check.NotNull(commandRepository, "commandRepository");
            Check.NotNull(entities, "entities");

            _DefaultImplementation.AddRange(commandRepository, entities);
        }

        public static void AddRange<TEntity>(this IRepository repository, IEnumerable<TEntity> entities) where TEntity : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entities, "entities");

            _DefaultImplementation.AddRange(repository, entities);
        }

        public static void UpdateEntityState<TEntity>(this ICommandRepository repository, TEntity entity, EntityState entityState) where TEntity : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");

            _DefaultImplementation.UpdateEntityState(repository, entity, entityState);
        }

        public static void Load<TEntity, TElement>(this IQueryRepository repository, TEntity entity, Expression<Func<TEntity, TElement>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            _DefaultImplementation.Load<TEntity, TElement>(repository, entity, navigationProperty, strategies);
        }

        public static void Load<TEntity, TElement>(this IQueryRepository repository, TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            _DefaultImplementation.Load<TEntity, TElement>(repository, entity, navigationProperty, strategies);
        }

        public static void Load<TEntity, TElement>(this IQueryRepository repository, TEntity entity, Expression<Func<TEntity, IList<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            _DefaultImplementation.Load<TEntity, TElement>(repository, entity, navigationProperty, strategies);
        }

        public static void Load<TEntity, TElement>(this IRepository repository, TEntity entity, Expression<Func<TEntity, TElement>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            _DefaultImplementation.Load<TEntity, TElement>(repository, entity, navigationProperty, strategies);
        }

        public static void Load<TEntity, TElement>(this IRepository repository, TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            _DefaultImplementation.Load<TEntity, TElement>(repository, entity, navigationProperty, strategies);
        }

        public static void Load<TEntity, TElement>(this IRepository repository, TEntity entity, Expression<Func<TEntity, IList<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class
        {
            Check.NotNull(repository, "repository");
            Check.NotNull(entity, "entity");
            Check.NotNull(navigationProperty, "navigationProperty");
            Check.NotNull(strategies, "strategies");

            _DefaultImplementation.Load<TEntity, TElement>(repository, entity, navigationProperty, strategies);
        }

        public static int ExecuteStoredProcudure(this IRepository repository, string sql, params object[] args)
        {
            Check.NotNull(repository, "repository");
            Check.NotEmpty(sql, "sql");

            return _DefaultImplementation.ExecuteStoredProcudure(repository, sql, args);
        }

        public static int ExecuteStoredProcudure(this ICommandRepository repository, string sql, params object[] args)
        {
            Check.NotNull(repository, "repository");
            Check.NotEmpty(sql, "sql");

            return _DefaultImplementation.ExecuteStoredProcudure(repository, sql, args);
        }

        public static IEnumerable<T> ExecuteSqlQuery<T>(this IRepository repository, string sql, params object[] args)
        {
            Check.NotNull(repository, "repository");
            Check.NotEmpty(sql, "sql");

            return _DefaultImplementation.ExecuteSqlQuery<T>(repository, sql, args);
        }

        public static IEnumerable<T> ExecuteSqlQuery<T>(this IQueryRepository repository, string sql, params object[] args)
        {
            Check.NotNull(repository, "repository");
            Check.NotEmpty(sql, "sql");

            return _DefaultImplementation.ExecuteSqlQuery<T>(repository, sql, args);
        }
    }
}
