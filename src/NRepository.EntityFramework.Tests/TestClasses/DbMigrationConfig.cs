namespace NRepository.EntityFramework.Tests
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class DbMigrationConfig : DbMigrationsConfiguration<FamilyDbContext>
    {
        public DbMigrationConfig()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FamilyDbContext context)
        {
            var repository = new EntityFrameworkRepository(context);
            repository.GetEntities<Person>().ToList().ForEach(repository.Delete);
            repository.Save();

            var family = new Person[]
            {
                new Parent(Names.JohnKelly, "Mr", "John", "Kelly", "Z"),
                new Child(Names.MarcBurgess, "Mr", "Marc", "Burgess", "A"),
                new Parent(Names.PaulCox, "Mr", "Paul", "Cox", "B"),
                new Parent(Names.NigelBurgess, "Mr", "Nigel", "Burgess", "C"),
                new Child(Names.TomCox, "Mr", "Tom", "Cox", "D"),
                new Parent(Names.SueCox, "Mrs", "Sue", "Cox", "E", true),
                new Parent(Names.JeanetteBurgess, "Mrs", "Jeanette", "Burgess", "F",true),
                new Parent(Names.IsabelleOsborne, "Ms", "Isabelle", "Osborne", "G",true),
                new Child(Names.EllieOsborne, "Miss", "Ellie", "Osborne", "H",true),
                new Child(Names.AimmeOsborne, "Miss", "Aimee", "Osborne", "I",true),
                new Child(Names.ToBeDecided,"To", "Be", "Decided", "J",true),
            };

            family.ToList().ForEach(repository.Add);
            repository.Save();

            Func<Names, Parent> GetPerson = name => repository.GetEntities<Parent>().Single(p => p.Id == (Names)name);
            Func<Names, Child> GetChild = name => repository.GetEntities<Child>().Single(p => p.Id == (Names)name);

            GetPerson(Names.IsabelleOsborne).Children.Add(GetChild(Names.EllieOsborne));
            GetPerson(Names.IsabelleOsborne).Children.Add(GetChild(Names.AimmeOsborne));

            repository.Modify(family.Single(p => p.Id == Names.IsabelleOsborne));
            repository.Save();
        }
    }
}
