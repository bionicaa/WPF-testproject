using System.Collections.Generic;
using TestProject.DomainEntities;
using static TestProject.DataBaseAccess.ListPerson;

namespace TestProject.DataBaseAccess
{
    public interface IPeopleDataBase
    {
        ICollection<Person> GetPeople();
        void GetPeopleAsync();
        event ReadData PeopleAvailable;
        void RemovePeopleAsync();
        event WriteData PeopleGone;
    }
}