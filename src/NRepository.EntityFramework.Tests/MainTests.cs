namespace NRepository.EntityFramework.Tests
{
    using NRepository.Core.Query;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NRepository.EntityFramework;
    using NRepository.TestKit;


    // Brings NRepository.EntityFramework into NCover
    [TestFixture]
    public class MainTests
    {
        [Test]
        public void CheckPagingQueryStrategy()
        {
            var repository = new EntityFrameworkQueryRepository(new FamilyDbContext());
            var c = repository.GetEntities<Animal>().ToList();

            var children = repository.GetEntities<Child>(
                new OrderByQueryStrategy<Child>(p => p.LastName));

            var pager = new FilterByPageQueryStrategy(2, 2, true);
            var results = children.AddQueryStrategy(pager);

            results.Count().ShouldEqual(2);
            pager.RowCount.ShouldEqual(5);
        }

        [Test]
        public void CheckMaterialiseQueryStrategy()
        {
            Func<string> GetName = () => "Marc";
            var repository = new EntityFrameworkQueryRepository(new FamilyDbContext());

            // Show it's not working due to running in the db
            Assert.Throws<NotSupportedException>(() =>
            {
                repository.GetEntity<Child>(
                    new ExpressionQueryStrategy<Child>(p => p.FirstName == GetName()))
                    .LastName.ShouldEqual("Burgess");
            });

            // Now show MaterialiseQueryStrategy does work
            repository.GetEntity<Child>(
                new MaterialiseQueryStrategy(),
                new ExpressionQueryStrategy<Child>(p => p.FirstName == GetName().ToString()))
                .LastName.ShouldEqual("Burgess");
        }

        [Test]
        public void ExecuteSqlQuery()
        {
            var repository = new EntityFrameworkRepository(new FamilyDbContext());
            var parents = repository.ExecuteSqlQuery<Parent>("select * from dbo.people").Count();
            parents.ShouldEqual(11);
        }

        [Test]
        public void CheckStoredProc()
        {
            var repository = new EntityFrameworkRepository(new FamilyDbContext());
            var x = repository.ExecuteStoredProcudure("GetCounter");
            x.ShouldEqual(-1);
        }

        [Test]
        public void AddRange()
        {
            var events = new RecordedRepositoryEvents();
            var repository = new EntityFrameworkRepository(new FamilyDbContext(), events);
            var count = repository.GetEntities<Animal>().Count();

            var newAnimals = new Animal[]
            {
                new Rabitt{Name = "rabitty"},
                new Cat{Name = "catty"}
            };

            repository.AddRange(newAnimals);
            repository.Save();

            events.AddedEvents.Count.ShouldEqual(2);
            events.SavedEvents.Count.ShouldEqual(1);

            var repository2 = new EntityFrameworkRepository(new FamilyDbContext());
            var count2 = repository2.GetEntities<Animal>().Count();
            count2.ShouldEqual(count + 2);
        }

        [Test]
        public void LoadTest()
        {
            var repository = new EntityFrameworkQueryRepository(new FamilyDbContext());
            var person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne);
            person.Children.Count.ShouldEqual(0);

            repository.Load(person, p => p.Children);

            person.Children.Count.ShouldEqual(2);
        }

        [Test]
        public void ConditionalTests()
        {
            var _queryRepository = new EntityFrameworkQueryRepository(new FamilyDbContext());

            var ss = new MultipleTextSearchSpecificationStrategy<Person>(
                    "a",
                    p => p.FirstName);

            var count_ = _queryRepository.GetEntities<Person>(ss).Count();

            var rowCountCallback = default(Func<int>);
            var results = _queryRepository.GetEntities<Person>(
                ss,
                new ConditionalQueryStrategy(true,
                    new OrderByQueryStrategy("FirstName"),
                    new PagingQueryStrategy(0, 2).OnCondition(true)));

            //            var rowCount2 = rowCountCallback();
            var rowCount = results.Count();
            var XX = results.ToArray();

        }
    }
}
