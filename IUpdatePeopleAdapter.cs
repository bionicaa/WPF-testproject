using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using static TestProject.InterfaceAdapters.UpdatePeopleAdapter;

namespace TestProject.InterfaceAdapters
{
    public interface IUpdatePeopleAdapter
    {
        void GetPeople();
        event TransferPeople transferPeople;

        event PickPeople pickPeople;
        void RemovePeople();
    }
}
