using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using TestProject.ViewModels;

namespace TestProject
{
    /// <summary>
    /// Boot strapper class for the application. Entry point for construction of elements of the application and GUI.
    /// </summary>
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        private InterfaceAdapters.IDependencyInjector _DependencyInjector;

        protected override void Configure()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "Views",
                DefaultSubNamespaceForViewModels = "ViewModels"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);

            _DependencyInjector = DependencyInjector.Instance;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            // Now, for example, you can't resolve dependency by key in SimpleInjector, so you have to
            // create the type out of the string (if the 'service' parameter is missing)
            var serviceType = service;
            if (serviceType == null)
            {
                var typeName = Assembly.GetExecutingAssembly().DefinedTypes.Where(x => x.Name == key).Select(x => x.FullName).FirstOrDefault();
                if (typeName == null)
                    throw new InvalidOperationException("No matching type found");

                serviceType = Type.GetType(typeName);
            }

            return _DependencyInjector.Resolve(serviceType);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _DependencyInjector.ResolveAll(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _DependencyInjector.BuildUp(instance);
        }
    }
}