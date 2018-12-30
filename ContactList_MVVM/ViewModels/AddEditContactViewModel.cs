using ContactList_MVVM.Messages;
using ContactList_MVVM.Models;
using ContactList_MVVM.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList_MVVM.ViewModels
{
    class AddEditContactViewModel:ViewModelBase
    {
        private ObservableCollection<Category> categories;
        public ObservableCollection<Category> Categories { get => categories; set => Set(ref categories, value); }

        private Contact contact = new Contact();
        public Contact Contact { get => contact; set => Set(ref contact, value); }

        private Category selectedCategory;
        public Category SelectedCategory { get => selectedCategory; set => Set(ref selectedCategory, value); }

        private readonly INavigationService navigationService;
        private readonly AppDbContext db;

        public AddEditContactViewModel(INavigationService navigationService, AppDbContext db)
        {
            this.navigationService = navigationService;
            this.db = db;

            Categories = new ObservableCollection<Category>(db.Categories);
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get => cancelCommand ?? (cancelCommand = new RelayCommand(
                () =>
                {
                    navigationService.Navigate<ContactListViewModel>();
                }
            ));
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get => addCommand ?? (addCommand = new RelayCommand(
                () =>
                {
                    //Contact.Category = SelectedCategory;
                    db.Contacts.Add(Contact);
                    db.SaveChanges();
                    Messenger.Default.Send(new ContactListChangedMessage { Item = Contact });
                    navigationService.Navigate<ContactListViewModel>();
                }
            ));
        }
    }
}
