using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestProject.DataBaseAccess;
using TestProject.DomainEntities;
using TestProject.InterfaceAdapters;
using TestProject.UseCaseInteractors;
using Unity;
using Unity.RegistrationByConvention;

namespace TestProject.ViewModels
{
    public class DependencyInjector : IDependencyInjector 
    {
        private static IDependencyInjector _instance;
        //private string _isSimulatorEnabled;
        //private string _defaultSerialChannel;
        //private string _defaultPumpVariantId;

        public static IDependencyInjector Instance
        {
            get
            {
                _instance = _instance ?? new DependencyInjector();
                return _instance;
            }
        }

        public object Container
        {
            get
            {
                return _container;
            }
        }

        internal IUnityContainer _container;

        private DependencyInjector()
        {
            _container = new UnityContainer();

            //_isSimulatorEnabled = ConfigurationManager.AppSettings[ResourceStrings.EnableEdwardsSimulator];
            //_defaultSerialChannel = ConfigurationManager.AppSettings[ResourceStrings.DefaultSerialChannel];
            //_defaultPumpVariantId = ConfigurationManager.AppSettings[ResourceStrings.DefaultPumpVariantID];
            RegisterInterfaces();
            RegisterInstances();
        }

        public void RegisterType<T1, T2>()
        {
        }

        public void RegisterInstance<T1, T2>()
        {
        }

        public void RegisterInterfaces()
        {
            _container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled, overwriteExistingMappings: true);

            _container.RegisterType<IPeopleDataBase, ListPerson>();
            _container.RegisterType<IPeopleDataStoreGateway, PeopleDataStoreGateway>();
            _container.RegisterType<IUpdatePeopleInteractor, UpdatePeopleInteractor>();
            _container.RegisterType<IUpdatePeopleAdapter, UpdatePeopleAdapter>();
                      
        }
    

        public void RegisterInstances()
        {
            _container.AddExtension(new Diagnostic());


            //var configFileReader = new ExternalConfigFileReader();
            //var settingsConfig = new AppSettingsConfig(configFileReader, new VersionInfoConfig(configFileReader),
            // new DeviceScanSettingsConfig(configFileReader), new DeviceProfilesConfig(configFileReader)
            // , new DeviceParameterConfig(configFileReader), new DisplayProfileConfig(configFileReader)
            // , new OperationSequenceConfig(configFileReader), new UserParameterConfig(configFileReader));

            //_container.RegisterInstance<IAppSettingsConfig>(settingsConfig);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }

        public IEnumerable<object> ResolveAll(Type t)
        {
            return _container.ResolveAll(t);
        }

        public Type BuildUp(object instance)
        {
            return _container.BuildUp(instance.GetType());
        }
    }
}
