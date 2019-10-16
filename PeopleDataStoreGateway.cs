using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DataBaseAccess;
using TestProject.DomainEntities;


namespace TestProject.InterfaceAdapters
{
    public class PeopleDataStoreGateway : IPeopleDataStoreGateway
    {
        public delegate void GotPeople(object sender, PeopleAvailableEventArgs args);
        public event GotPeople gotPeople;

        public delegate void TakenPeople(object sender, PeopleRemovedEventArgs args);
        public event TakenPeople takenPeople;


        IPeopleDataBase _people;

        public PeopleDataStoreGateway(IPeopleDataBase people)
        {
            _people = people;
        }


        public void RetrievePeople()
        {
             _people.GetPeopleAsync();                     

            _people.PeopleAvailable += new ListPerson.ReadData(this.PeopleAvailableHandler); //subscribe         
            
        }

        void PeopleAvailableHandler(object sender, PeopleAvailableEventArgs args)
        {
          
            gotPeople?.Invoke(this, new PeopleAvailableEventArgs(args.PeopleCollection));
            //     //publisher
           
        }

        public void RemovePeople()
        {
            _people.RemovePeopleAsync();

            _people.PeopleGone += new ListPerson.WriteData(this.PeopleGoneHandler); //subscribe         

        }

        void PeopleGoneHandler(object sender, PeopleRemovedEventArgs args)
        {

            takenPeople?.Invoke(this, new PeopleRemovedEventArgs(args.PeopleCollection));
            //     //publisher
          
        }

        /*
        public List<Models.Person> RetrievePeople()
        {           
            var peopleCollection = _people.GetPeople();
            List<Models.Person> peopleList = new List<Models.Person>();         
            
            foreach(var item in peopleCollection)
            {
                peopleList.Add(new Models.Person(item.FirstName, item.LastName, item.Age, item.Active));                               
            }

            return peopleList; 
        }*/


    }
}
