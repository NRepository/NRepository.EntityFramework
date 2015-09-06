namespace NRepository.EntityFramework.Events
{
    using System;
    using System.Data.Entity;
    using NRepository.Core.Command;
    using NRepository.Core.Events;
    using NRepository.EntityFramework.Utilities;

    public class EntityStateModifiedCommandEvent<TEntity> : EntityModifiedEvent
         where TEntity : class
    {
        public EntityStateModifiedCommandEvent(ICommandRepository commandRepository, TEntity entity, EntityState oldEntityState, EntityState newEntityState)
            : base(commandRepository, entity)
        {
            Check.NotNull(commandRepository, "commandRepostiory");
            Check.NotNull(entity, "entity");
 
            NewEntityState = newEntityState;
            OldEntityState = oldEntityState;
        }

        public EntityState OldEntityState
        {
            get;
            private set;
        }

        public EntityState NewEntityState
        {
            get;
            private set;
        }
    }
}