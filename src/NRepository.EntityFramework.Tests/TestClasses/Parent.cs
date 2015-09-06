namespace NRepository.EntityFramework.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Parent : Person
    {
        public Parent()
        {
            Children = new List<Child>();
        }

        [ForeignKey("Id")]
        public virtual Parent Partner { get; set; }


        public Parent(Names id, string title, string firstName, string lastName, string sortValue, bool isFemale = false)
            : base(id, title, firstName, lastName, sortValue, isFemale)
        {
            Children = new List<Child>();
        }

        //public virtual ICollection<Child> Children { get; set; }
        public virtual IList<Child> Children { get; set; }

    }
}
