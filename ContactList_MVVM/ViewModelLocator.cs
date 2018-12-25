using Autofac;
using Autofac.Configuration;
using ContactList_MVVM.Services;
using ContactList_MVVM.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList_MVVM
{
    class ViewModelLocator
    {
        private AppViewModel appViewModel;
        private ContactListViewModel contactListViewModel;
        private AddEditContactViewModel addEditContactViewModel;

        private INavigationService navigationService;
        public static IContainer Container;

        public ViewModelLocator()
        {
            try
            {
                var config = new ConfigurationBuilder();
                config.AddJsonFile("autofac.json");
                var module = new ConfigurationModule(config.Build());
                var builder = new ContainerBuilder();
                builder.RegisterModule(module);
                Container = builder.Build();


                navigationService = Container.Resolve<INavigationService>();
                contactListViewModel = Container.Resolve<ContactListViewModel>();
                addEditContactViewModel = Container.Resolve<AddEditContactViewModel>();

                navigationService.Register<ContactListViewModel>(contactListViewModel);
                navigationService.Register<AddEditContactViewModel>(addEditContactViewModel);

                navigationService.Navigate<ContactListViewModel>();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
