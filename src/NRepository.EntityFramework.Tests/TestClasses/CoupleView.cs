namespace NRepository.EntityFramework.Tests
{
    using System.Collections.Generic;
    
    public class CoupleView
    {
        public string WifeFullName { get; set; }

        public string HusbandFullName { get; set; }

        public List<Person> CombinedChilren { get; set; }
    }
}
