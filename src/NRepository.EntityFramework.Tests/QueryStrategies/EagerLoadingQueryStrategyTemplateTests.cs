namespace NRepository.EntityFramework.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NRepository.Core.Query;
    using NUnit.Framework;
    using NRepository.EntityFramework.Query;
    using NRepository.EntityFramework.Tests._Utilities;

    [TestFixture]
    public class EagerLoadingQueryStrategyTemplateTests
    {
        [Test]
        public void CheckConstructors()
        {
            new EagerLoadingQueryStrategy<SimpleEntity>().Includes.Count().ShouldEqual(0);

            var query = new EagerLoadingQueryStrategy<SimpleEntity>(p => p.Id);
            query.Includes.Count().ShouldEqual(1);
            query.Includes.First().ShouldEqual("Id");

            query = new EagerLoadingQueryStrategy<SimpleEntity>(p => p.Id, p => p.GroupId);
            query.Includes.Count().ShouldEqual(2);
            query.Includes.First().ShouldEqual("Id");
            query.Includes.Last().ShouldEqual("GroupId");
        }

        [Test]
        public void CheckAddUsingString()
        {
            var query = new EagerLoadingQueryStrategy<SimpleEntity>();
            query.Add("1");
            query.Includes.Count().ShouldEqual(1);
            query.Includes.Last().ShouldEqual("1");

            query.Add("");
            query.Includes.Count().ShouldEqual(1);

            query.Add(default(string));
            query.Includes.Count().ShouldEqual(1);

            query.Add("2", false);
            query.Includes.Count().ShouldEqual(1);

            query.Add("2");
            query.Includes.Count().ShouldEqual(2);
        }


        [Test]
        public void CheckAddUsingExpression()
        {
            var query = new EagerLoadingQueryStrategy<SimpleEntity>();
            query.Add(p => p.Id);
            query.Includes.Count().ShouldEqual(1);
            query.Includes.Last().ShouldEqual("Id");

            query.Add(p => p.Id, false);
            query.Includes.Count().ShouldEqual(1);

            query.Add(p => p.Id, true);
            query.Includes.Count().ShouldEqual(2);
        }

        [Test]
        public void CheckAllEntitiesReturned()
        {
            var simpleEntities = SimpleEntity.CreateSimpleEntities();
            var query = new EagerLoadingQueryStrategy<SimpleEntity>(p => p.Id);
            simpleEntities.AddQueryStrategy(query).Count().ShouldEqual(simpleEntities.Count());
        }
    }
}
