using System.Collections.Generic;
using TestProject.DataBaseAccess;
using TestProject.Models;
using static TestProject.UseCaseInteractors.UpdatePeopleInteractor;

namespace TestProject.UseCaseInteractors
{
    public interface IUpdatePeopleInteractor
    {
        void RetrievePeople();
        event ReceivedPeople receivedPeople;
        event RemovedPeople removedPeople;
        void RemovePeople();
    }
}