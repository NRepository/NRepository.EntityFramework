namespace NRepository.EntityFramework.Query
{
    using NRepository.Core;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Utilities;
    using System.Linq;

    public class ExecuteStoreProcedureQueryStrategy<TEntity> : QueryStrategy
    {
        public ExecuteStoreProcedureQueryStrategy(IQueryRepository queryRepository, string sql, params object[] args)
        {
            Check.NotNull(queryRepository, "queryRepository");
            Check.NotEmpty(sql, "sql");

            QueryRepository = queryRepository;
            Sql = sql;
            Args = args ?? new object[0];
        }

        public IQueryRepository QueryRepository
        {
            get;
            private set;
        }

        public string Sql
        {
            get;
            protected set;
        }

        public object[] Args
        {
            get;
            protected set;
        }

        public override IQueryable<T> GetQueryableEntities<T>(object additionalQueryData)
        {
            if (typeof(TEntity) != typeof(T))
                throw new RepositoryException(string.Format("Unexpected type in QueryStrategy. Expected: {0}, was: {1}", typeof(TEntity).Name, typeof(T).Name));

            var entities = QueryRepository.ExecuteSqlQuery<T>(Sql, Args);
            return (IQueryable<T>)entities.AsQueryable();
        }
    }
}
