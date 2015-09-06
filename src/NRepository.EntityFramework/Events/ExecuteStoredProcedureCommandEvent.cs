namespace NRepository.EntityFramework.Events
{
    using System;
    using System.Collections.Generic;
    using NRepository.Core.Command;
    using NRepository.Core.Events;
    using NRepository.EntityFramework.Utilities;

    public class ExecuteStoredProcedureCommandEvent : EntityModifiedEvent
    {
        public ExecuteStoredProcedureCommandEvent(ICommandRepository commandRepository, string sql, IEnumerable<object> args)
            : base(commandRepository, sql)
        {
            Check.NotNull(commandRepository, "commandRepository");
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
