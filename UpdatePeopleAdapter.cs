using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DataBaseAccess;
using TestProject.DomainEntities;
using TestProject.UseCaseInteractors;
using static TestProject.UseCaseInteractors.UpdatePeopleInteractor;

namespace TestProject.InterfaceAdapters
{
    public class UpdatePeopleAdapter : IUpdatePeopleAdapter
    {
        public delegate void TransferPeople(object sender, PeopleAvailableEventArgs args);
        public event TransferPeople transferPeople;

        public delegate void PickPeople(object sender, PeopleRemovedEventArgs args);
        public event PickPeople pickPeople;

        IUpdatePeopleInteractor _people;

        public UpdatePeopleAdapter(IUpdatePeopleInteractor people)
        {
            _people = people;
        }

        public void GetPeople()
        {
            _people.RetrievePeople();

            _people.receivedPeople += new ReceivedPeople(this.ReceivedPeopleHandler); //subscriber
        }

        void ReceivedPeopleHandler(object sender, PeopleAvailableEventArgs args)
        {

            transferPeople?.Invoke(this, new PeopleAvailableEventArgs(args.PeopleCollection));
            //    //publisher     

        }

        public void RemovePeople()
        {
            _people.RemovePeople();

            _people.removedPeople += new RemovedPeople(this.RemovedPeopleHandler); //subscriber

        }


        void RemovedPeopleHandler(object sender, PeopleRemovedEventArgs args)
        {
  
                pickPeople?.Invoke(this, new PeopleRemovedEventArgs(args.PeopleCollection));
            //    //publisher        

        }

        //// method to get people from interactor and then convert people into an observable collection of person and then return that object
        //public ObservableCollection<Person> GetPeople()
        //{
        //    var peopleList = _people.RetrievePeople();
        //    var peopleCollection = new ObservableCollection<Person>();
        //    foreach(var item in peopleList)
        //    {
        //        peopleCollection.Add(item);
        //    }

        //    return peopleCollection;
        //}       

    }
}
