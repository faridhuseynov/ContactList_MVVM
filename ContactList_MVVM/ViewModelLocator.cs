﻿using Autofac;
using Autofac.Configuration;
using ContactList_MVVM.Services;
using ContactList_MVVM.ViewModels;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                appViewModel = Container.Resolve<AppViewModel>();
                contactListViewModel = Container.Resolve<ContactListViewModel>();
                addEditContactViewModel = Container.Resolve<AddEditContactViewModel>();

                navigationService.Register<ContactListViewModel>(contactListViewModel);
                navigationService.Register<AddEditContactViewModel>(addEditContactViewModel);

                navigationService.Navigate<ContactListViewModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public ViewModelBase GetAppViewModel()
        {
            return appViewModel;
        }
    }
}
