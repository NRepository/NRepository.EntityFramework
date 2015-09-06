//namespace NRepository.EntityFramework.Tests
//{
//    using System.Linq;
//    using NRepository.Core.Query;
//    using NRepository.Core.Query.Specification;
//    using NUnit.Framework;

//    [TestFixture]
//    public class SmokeTests
//    {
//        [Test]
//        public void SmokeTestDefaultCalls()
//        {
//            using (var repository = new EntityFrameworkScopedQueryRepository(new FamilyDbContext()))
//            { 
//                var strategy = new DefaultQueryStrategy();
//                var specification = new ExpressionSpecificationQueryStrategy<Person>(p => p.Id == Names.AimmeOsborne);

//                //var Person = repository.GetEntity<Person>(p => p.Id == Names.AimmeOsborne,false);
//                //Person = repository.GetEntity<Person>(p => p.Id == Names.AimmeOsborne, strategy);
//                //Person = repository.GetEntity<Person>(p => p.Id == Names.AimmeOsborne, true);
//                //Person = repository.GetEntity<Person>(p => p.Id == Names.AimmeOsborne, strategy, true);
//                //Person = repository.GetEntity<Person>(specification);
//                //Person = repository.GetEntity<Person>(specification, strategy);
//                //Person = repository.GetEntity<Person>(specification, true);
//                //Person = repository.GetEntity<Person>(specification, strategy, true);

//                var person = repository.GetEntities<Parent>().Single();

//                repository.Load(
//                    person, 
//                    p => p.Partner.Children);

//                //      People = repository.GetEntities<Person>(strategy).ToList();
//                //People = repository.GetEntities<Person>(p => p.Id == Names.AimmeOsborne).ToList();
//                //People = repository.GetEntities<Person>(p => p.Id == Names.AimmeOsborne, strategy).ToList();
//                //People = repository.GetEntities<Person>().ToList();
//                //People = repository.GetEntities<Person>(specification).ToList();
//                //People = repository.GetEntities<Person>(specification, strategy).ToList();
//            }
//        }

//        [Test]
//        public void CheckQueryAndLoad()
//        {
//            var repository = new EntityFrameworkRepository(new FamilyDbContext());
//            var order = new Order
//            {
//                Name = "order1",
//                // Items = new List<Item>
//                // {
//                //     new Item{ Name = "small"},
//                ////     new Item{ Name = "large", IsLargeItem = true},
//                //     new Item{ Name = "small1"},
//                ////     new Item{ Name = "large2", IsLargeItem = true},
//                //     new Item{ Name = "small2"},
//                ////     new Item{ Name = "large3", IsLargeItem = true},
//                // }
//            };

//            repository.Add(order);
//            repository.Save();
//        }

//        [Test]
//        public void CheckEagerLoading()
//        {
//            using (var repository = new EntityFrameworkQueryRepository(new FamilyDbContext()))
//            {
//                var person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne);
//                var children = person.Children;
//            }

//            using (var repository = new EntityFrameworkQueryRepository(new FamilyDbContext()))
//            {
//                var person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne,
//                    new EagerLoadingQueryStrategy<Parent>()
//                        .Add(p => p.Children)
//                        .Add(p => p.FirstName));

//                var children = person.Children;
//            }
//        }

//        [Test]
//        public void CheckEagerLoading2()
//        {
//            using (var repository = new EntityFrameworkQueryRepository(new FamilyDbContext()))
//            {
//                var person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne);
//                var children = person.Children;
//            }

//            using (var repository = new EntityFrameworkQueryRepository(new FamilyDbContext()))
//            {
//                var person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne,
//                    new EagerLoadingQueryStrategy<Parent>(
//                        p => p.Children,
//                        p => p.FirstName,
//                        p => p.Children));

//                var children = person.Children;
//            }
//        }

//        [Test]
//        public void SmokeTestEagerLoadingCalls()
//        {
//            using (var repository = new EntityFrameworkQueryRepository(new FamilyDbContext()))
//            {
//                var strategy = new EagerLoadingQueryStrategy<Parent>(
//                    p => p.Children,
//                    p => p.FirstName);

//                var childSpec = new ExpressionSpecificationQueryStrategy<Child>(p => p.Id == Names.AimmeOsborne);
//                var parentSpec = new ExpressionSpecificationQueryStrategy<Parent>(p => p.Id == Names.IsabelleOsborne);

//                var person = repository.GetEntity<Person>(p => p.Id == Names.AimmeOsborne);
//                person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne);
//                person = repository.GetEntity<Person>(p => p.Id == Names.AimmeOsborne, true);
//                person = repository.GetEntity<Parent>(p => p.Id == Names.IsabelleOsborne, strategy, true);
//                person = repository.GetEntity(childSpec);
//                person = repository.GetEntity(parentSpec, strategy);
//                person = repository.GetEntity(parentSpec, true);
//                person = repository.GetEntity(parentSpec, strategy, true);

//                var peopleSpec = new ExpressionSpecificationQueryStrategy<Person>(p => p.IsFemale);
//                var people = repository.GetEntities<Person>().ToList();
//                var parent = repository.GetEntities<Parent>(strategy).ToList();
//                people = repository.GetEntities<Person>(p => p.IsFemale).ToList();
//                parent = repository.GetEntities<Parent>(p => p.IsFemale, strategy).ToList();
//                people = repository.GetEntities<Person>().ToList();
//                people = repository.GetEntities<Person>(peopleSpec).ToList();
//                parent = repository.GetEntities(new ExpressionSpecificationQueryStrategy<Parent>(p => p.IsFemale), strategy).ToList();
//            }
//        }

//        [Test]
//        public void ConditionalExtensionStringTest()
//        {
//            var eagerLoadingStrategy = new EagerLoadingQueryStrategy(
//                "1",
//                "2",
//                "3".OnCondition(false));

//            // Assert
//            eagerLoadingStrategy.Includes.Count().ShouldEqual(2);

//            eagerLoadingStrategy = new EagerLoadingQueryStrategy(
//                 "2".OnCondition(false));

//            // Assert
//            eagerLoadingStrategy.Includes.Count().ShouldEqual(0);
//        }
//    }
//}