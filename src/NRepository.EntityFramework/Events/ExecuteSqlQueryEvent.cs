namespace NRepository.EntityFramework.Events
{
    using System;
    using System.Collections.Generic;
    using NRepository.Core.Events;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Utilities;

    public class ExecuteSqlQueryEvent : RepositoryQueryEvent
    {
        public ExecuteSqlQueryEvent(IQueryRepository queryRepository, string sql, IEnumerable<object> args)
            : base(queryRepository)
        {
            Check.NotNull(queryRepository, "queryRepository");
            Check.NotEmpty(sql, "sql");

            Args = args;
            Sql = sql;
        }

        public string Sql
        {
            get;
            private set;
        }

        public IEnumerable<object> Args
        {
            get;
            private set;
        }
    }
}