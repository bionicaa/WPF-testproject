using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestProject.DomainEntities
{
    public class People
    {
        public List<Person> PeopleData
        { get; set; }

        public People(List<Person> peopleData)
        {
            PeopleData = peopleData;         
            
        }

        public void Add(string FirstName, string LastName, int Age, bool Active)
        {
            
            Person person = new Person(FirstName, LastName, Age, Active);
           
            PeopleData?.Add(person); 

        }


        public People()
        {
            PeopleData = null;
        }
    }
}