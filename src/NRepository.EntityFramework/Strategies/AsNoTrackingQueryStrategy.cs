namespace NRepository.EntityFramework.Query
{
    using System.Data.Entity;
    using System.Linq;
    using NRepository.Core.Query;

    public class AsNoTrackingQueryStrategy : QueryStrategy
    {
        public override IQueryable<T> GetQueryableEntities<T>(object additionalQueryData)
        {
            var query = this.QueryableRepository.GetQueryableEntities<T>(additionalQueryData).AsNoTracking();
            return query;
        }
    }
}
