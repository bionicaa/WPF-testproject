using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DomainEntities;

namespace TestProject.DomainEntities
{
    public class PeopleRemovedEventArgs : EventArgs 
    {
        public List<Person> PeopleCollection { get; set; }


        public PeopleRemovedEventArgs(List<Person> peopleCollection)
        {
            PeopleCollection = peopleCollection;

        }

        public void Add(List<Person> people)
        {
            foreach (var item in people)
            {
                PeopleCollection.Add(item);
            }
        }
    }
}
