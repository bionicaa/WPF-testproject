using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DomainEntities
{
    public class Person 
    {
        private string firstName;
        private string lastName;
        private int age;
        private bool active;

        public Person(string first, string last, int years, bool enable)
        {
            firstName = first;
            lastName = last;
            age = years;
            active = enable;
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                if (active != value)
                {
                    active = value;

                }
            }

        }

        public Person()
        {

        }       

    }
}
