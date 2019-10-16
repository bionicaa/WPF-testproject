using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using Caliburn.Micro;
using TestProject.Models;
using TestProject.InterfaceAdapters;
using TestProject.UseCaseInteractors;
using System.IO;
using TestProject.DomainEntities;
using static TestProject.InterfaceAdapters.UpdatePeopleAdapter;

namespace TestProject.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        IUpdatePeopleAdapter _adapter;

        private static object _lock = new object();

        public MainWindowViewModel(IUpdatePeopleAdapter adapter)
        {
            _adapter = adapter;
            BindingOperations.EnableCollectionSynchronization(people, _lock); //allows for synchronisation of threads
        }

        private string populate = "Populate";
        public string Populate
        {
            get { return populate; }
        }

        public void Append()
        {

            _adapter.GetPeople();
            _adapter.transferPeople += new TransferPeople(this.TransferPeopleHandler); //subscriber 


            // call method on adapter to get Collection of Person objects
            // Adapter can return ObservableCollection of Person objects
        }

        private void TransferPeopleHandler(object sender, PeopleAvailableEventArgs args)
        {

            if (args == null || args.PeopleCollection == null)
            {
                return;
            }

            foreach (var item in args.PeopleCollection.ToList())
            {
                people.Add(new Models.Person() { FirstName = item.FirstName, LastName = item.LastName, Age = item.Age, Active = item.Active });
            }

        }



        private ObservableCollection<Models.Person> people = new ObservableCollection<Models.Person>();
        public ObservableCollection<Models.Person> GetPeople
        {
            get
            {
                people.Add(new Models.Person() { FirstName = "Bill", LastName = "Gates", Age = 63, Active = true });

                return people;
            }
            set
            {
                people = value;
            }

        }

        public void Add(Models.Person person)
        {
            people.Add(new Models.Person());

        }
        public void Add(string FirstName, string LastName, int Age, bool Active)
        {
            people.Add(new Models.Person(FirstName, LastName, Age, Active));

        }

        public void AddPersonToPeople(ObservableCollection<Models.Person> people, Models.Person person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "FirstName");
            }
            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                throw new ArgumentException("You passed in an invalid parameter", "LastName");
            }
            people.Add(person);
        }


        private string member = "Member?";
        public string Member
        {
            get { return member; }
        }

        public void Remove()
        {

            _adapter.RemovePeople();

            _adapter.pickPeople += new PickPeople(this.PickPeopleHandler); //subscriber

        }

        void PickPeopleHandler(object sender, PeopleRemovedEventArgs args)
        {

            foreach (Models.Person person in people.ToList())
            {              
                if (person.Active == false)
                {
                    people.Remove(person);             
                }

                args.PeopleCollection.Add(new DomainEntities.Person()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    Active = person.Active
                });
            }

        }

    }
}



