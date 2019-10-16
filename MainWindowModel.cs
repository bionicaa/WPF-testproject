using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Models
{
    class MainWindowModel
    {
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Active { get; set; }

        ObservableCollection<Person> GetPeople { get; set; }
    }
}
