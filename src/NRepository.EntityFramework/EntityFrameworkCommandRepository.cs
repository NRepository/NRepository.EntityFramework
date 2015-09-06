namespace NRepository.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using NRepository.Core;
    using NRepository.Core.Command;
    using NRepository.Core.Events;
    using NRepository.EntityFramework.Utilities;

    public class EntityFrameworkCommandRepository : CommandRepositoryBase
    {
        private readonly ICommandInterceptors _CommandInterceptors;
        
        public EntityFrameworkCommandRepository(
            DbContext dbContext,
            ICommandInterceptors commandInterceptor)
            : this(dbContext, new DefaultCommandEventsHandlers(), commandInterceptor)
        {
        }

        public EntityFrameworkCommandRepository(
            DbContext dbContext,
            ICommandEventHandlers commandEvents)
            : this(dbContext, commandEvents, new CommandInterceptors())
        {
        }

        public EntityFrameworkCommandRepository(
            DbContext dbContext,
            ICommandEventHandlers commandEvents,
            ICommandInterceptors commandInterceptor)
            : base(commandEvents)
        {
            Check.NotNull(dbContext, "dbContext");
            Check.NotNull(commandEvents, "commandEvents");
            Check.NotNull(commandInterceptor, "commandInterceptor");

            ObjectContext = DbContext = dbContext;
            _CommandInterceptors = commandInterceptor;
        }

        public DbContext DbContext
        {
            get;
            private set;
        }

        public override void Add<T>(T entity)
        {
            Check.NotNull(entity, "entity");

            _CommandInterceptors.AddCommandInterceptor.Add(
                this,
                new Action<T>(p => DbContext.Entry(p).State = EntityState.Added),
                entity);

            EventHandlers.EntityAddedEventHandler.Handle(new EntityAddedEvent(this, entity));
        }

        public override void Delete<T>(T entity)
        {
            Check.NotNull(entity, "entity");

            _CommandInterceptors.DeleteCommandInterceptor.Delete(
              this,
              new Action<T>(p => DbContext.Entry(p).State = EntityState.Deleted),
              entity);

            EventHandlers.EntityDeletedEventHandler.Handle(new EntityDeletedEvent(this, entity));
        }

        public override void Modify<T>(T entity)
        {
            Check.NotNull(entity, "entity");

            _CommandInterceptors.ModifyCommandInterceptor.Modify(
               this,
               new Action<T>(p => DbContext.Entry(p).State = EntityState.Modified),
               entity);

            EventHandlers.EntityModifiedEventHandler.Handle(new EntityModifiedEvent(this, entity));
        }

        public override int Save()
        {
            try
            {   
                var writtenObjectsCount =  _CommandInterceptors.SaveCommandInterceptor.Save(
                    this,
                    new Func<int>(() => { return DbContext.SaveChanges(); }));

                EventHandlers.RepositorySavedEventHandler.Handle(new RepositorySavedEvent(this));

                return writtenObjectsCount;
            }
            catch (DbEntityValidationException validationException)
            {
                if (_CommandInterceptors.SaveCommandInterceptor.ThrowOriginalException)
                    throw;

                var entityErrors = CreateDbEntityValidationErrorDictionary(validationException);
                var evEx = new EntityValidationRepositoryException(entityErrors, validationException);
                throw evEx;
            }
            catch (Exception ex)
            {
                if (_CommandInterceptors.SaveCommandInterceptor.ThrowOriginalException)
                    throw;

                throw new EntityRepositoryException("Failed to save changes. See inner exception for details.", ex);
            }
        }

        private static Dictionary<string, string> CreateDbEntityValidationErrorDictionary(DbEntityValidationException dbException)
        {
            DebugCheck.NotNull(dbException);

            var retVal = new Dictionary<string, string>();

            dbException.EntityValidationErrors.ToList()
                .ForEach(p => p.ValidationErrors.ToList()
                    .ForEach(p1 =>
                    {
                        if (!retVal.ContainsKey(p1.PropertyName))
                            retVal.Add(p1.PropertyName, p1.ErrorMessage);
                    }));

            return retVal;
        }
    }
}
