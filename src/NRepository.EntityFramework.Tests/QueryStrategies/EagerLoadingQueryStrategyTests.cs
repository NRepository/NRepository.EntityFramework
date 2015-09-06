namespace NRepository.EntityFramework.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using NRepository.EntityFramework.Tests._Utilities;

    [TestFixture]
    public class EagerLoadingQueryStrategyTests
    {
        [Test]
        public void CheckConstructors()
        {
            new EagerLoadingQueryStrategy().Includes.Count().ShouldEqual(0);

            var query = new EagerLoadingQueryStrategy("1");
            query.Includes.Count().ShouldEqual(1);
            query.Includes.First().ShouldEqual("1");

            query = new EagerLoadingQueryStrategy("1", "2", "3");
            query.Includes.Count().ShouldEqual(3);
            query.Includes.First().ShouldEqual("1");
            query.Includes.Last().ShouldEqual("3");

            query = new EagerLoadingQueryStrategy(new[] { "1", "2", "3" });
            query.Includes.Count().ShouldEqual(3);
            query.Includes.First().ShouldEqual("1");
            query.Includes.Last().ShouldEqual("3");

            query = new EagerLoadingQueryStrategy(new[] { "1", "2", "3", "", null });
            query.Includes.Count().ShouldEqual(3);
            query.Includes.First().ShouldEqual("1");
            query.Includes.Last().ShouldEqual("3");

            query = new EagerLoadingQueryStrategy(new[] { "1", "2", "3" }, new[] { "1", "2", "3" });
            query.Includes.Count().ShouldEqual(6);

            query = new EagerLoadingQueryStrategy(new[] { "1", "2", "3" }, new[] { "1", "2", "3" }, "1");
            query.Includes.Count().ShouldEqual(7);

            query = new EagerLoadingQueryStrategy(new[] { "1", "2", "3" }, "1", "2", "3");
            query.Includes.Count().ShouldEqual(6);
        }

        [Test]
        public void CheckAdd()
        {
            var query = new EagerLoadingQueryStrategy();
            query.Add("1");
            query.Includes.Count().ShouldEqual(1);
            query.Includes.Last().ShouldEqual("1");

            query.Add("");
            query.Includes.Count().ShouldEqual(1);

            query.Add(null);
            query.Includes.Count().ShouldEqual(1);

            query.Add("2", false);
            query.Includes.Count().ShouldEqual(1);

            query.Add("2");
            query.Includes.Count().ShouldEqual(2);
        }

        [Test]
        public void CheckAllEntitiesReturned()
        {
            var simpleEntities = SimpleEntity.CreateSimpleEntities();
            var query = new EagerLoadingQueryStrategy("Id");
            simpleEntities.AddQueryStrategy(query).Count().ShouldEqual(simpleEntities.Count());
        }
    }
}