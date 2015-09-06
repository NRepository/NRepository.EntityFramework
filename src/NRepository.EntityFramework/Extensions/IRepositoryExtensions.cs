namespace NRepository.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using NRepository.Core.Command;
    using NRepository.Core.Query;
    using NRepository.Core;

    public interface IRepositoryExtensions
    {
        void UpdateEntityState<TEntity>(ICommandRepository repository, TEntity entity, EntityState entityState) 
            where TEntity : class;

        void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, TElement>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class;

        void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, ICollection<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class;

        void Load<TEntity, TElement>(IQueryRepository repository, TEntity entity, Expression<Func<TEntity, IList<TElement>>> navigationProperty, params IQueryStrategy[] strategies)
            where TEntity : class
            where TElement : class;

        void AddRange<TEntity>(ICommandRepository commandRepository, IEnumerable<TEntity> entities)
            where TEntity : class;

        int ExecuteStoredProcudure(ICommandRepository repository, string sql, params object[] sqlParams);

        IEnumerable<T> ExecuteSqlQuery<T>(IQueryRepository repository, string sql, params object[] sqlParams);
    }
}