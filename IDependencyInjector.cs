using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.InterfaceAdapters
{
    public interface IDependencyInjector 
    {
        object Container { get; }
        Type BuildUp(object instance);
        void RegisterInterfaces();
        void RegisterType<T1, T2>();
        T Resolve<T>();
        object Resolve(Type t);
        IEnumerable<object> ResolveAll(Type t);
    }
}
