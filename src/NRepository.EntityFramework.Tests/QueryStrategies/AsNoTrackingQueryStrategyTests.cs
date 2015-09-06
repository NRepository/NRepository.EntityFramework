namespace NRepository.EntityFramework.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NRepository.Core.Query;
    using NUnit.Framework;
    using NRepository.EntityFramework.Query;
    using NRepository.EntityFramework.Tests._Utilities;
    
    public class AsNoTrackingQueryStrategyTests
    {
        [Test]
        public void CheckAllEntitiesReturned()
        {
            var simpleEntities = SimpleEntity.CreateSimpleEntities();
            var query = new AsNoTrackingQueryStrategy();
            simpleEntities.AddQueryStrategy(query).Count().ShouldEqual(simpleEntities.Count());
        }
    }
}
