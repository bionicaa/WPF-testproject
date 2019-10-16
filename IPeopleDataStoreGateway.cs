using System.Collections.Generic;
using TestProject.DataBaseAccess;
using TestProject.DomainEntities;
using static TestProject.InterfaceAdapters.PeopleDataStoreGateway;

namespace TestProject.InterfaceAdapters
{
    public interface IPeopleDataStoreGateway
    {

        void RetrievePeople();
        void RemovePeople();
        event GotPeople gotPeople;
        event TakenPeople takenPeople;
    }
}