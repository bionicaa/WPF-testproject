using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DataBaseAccess;
using TestProject.InterfaceAdapters;
using static TestProject.InterfaceAdapters.PeopleDataStoreGateway;
using TestProject.DomainEntities;

namespace TestProject.UseCaseInteractors
{
    public class UpdatePeopleInteractor : IUpdatePeopleInteractor 
    {
        public delegate void ReceivedPeople(object sender, PeopleAvailableEventArgs args);
        public event ReceivedPeople receivedPeople;

        public delegate void RemovedPeople(object sender, PeopleRemovedEventArgs args);
        public event RemovedPeople removedPeople;

        IPeopleDataStoreGateway _people;

        public UpdatePeopleInteractor(IPeopleDataStoreGateway people)
        {
            _people = people;
        }

        public void RetrievePeople()
        {
                      
            _people.RetrievePeople();

            _people.gotPeople += new GotPeople(this.GotPeopleHandler); //subscriber
            
        }
               

        void GotPeopleHandler(object sender, PeopleAvailableEventArgs args)
        {

            receivedPeople?.Invoke(this, new PeopleAvailableEventArgs(args.PeopleCollection));
            //    //publisher        

        }

        public void RemovePeople()
        {
            _people.RemovePeople();

            _people.takenPeople += new TakenPeople(this.TakenPeopleHandler); //subscriber

        }


        void TakenPeopleHandler(object sender, PeopleRemovedEventArgs args)
        {
            
            removedPeople?.Invoke(this, new PeopleRemovedEventArgs(args.PeopleCollection));
            //    //publisher        

        }
    }


    
}
