using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DomainEntities
{
    public class PeopleAvailableEventArgs : EventArgs
    {/*
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Active { get; set; }*/

        public List<Person> PeopleCollection { get; set; }


        public PeopleAvailableEventArgs(List<Person> peopleCollection)
        {
            PeopleCollection = peopleCollection;
           
        }

        public void Add(List<Person> people)
        {
            foreach(var item in people)
            {
                PeopleCollection.Add(item);
            }            
        }

    }
}
