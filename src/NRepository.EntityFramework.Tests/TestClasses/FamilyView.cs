namespace NRepository.EntityFramework.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    public class FamilyView
    {
        public Names Wife { get; set; }

        public string WifeFullName { get; set; }

        public Names Husband { get; set; }

        public string HusbandFullName { get; set; }

        public IEnumerable<Person> CombinedChildren { get; set; }
    }
}
