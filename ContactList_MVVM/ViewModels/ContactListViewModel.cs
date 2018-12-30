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
    class ContactListViewModel:ViewModelBase
    {
        private ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts { get => contacts; set =>Set(ref contacts, value); }

        private readonly INavigationService navigationService;
        private readonly AppDbContext db;
        private readonly IMessageService messageService;

        public ContactListViewModel(INavigationService navigationService, 
            AppDbContext db,
            IMessageService messageService)
        {
            this.navigationService = navigationService;
            this.db = db;
            this.messageService = messageService;
            Contacts = new ObservableCollection<Contact>(db.Contacts);
            Messenger.Default.Register<ContactListChangedMessage>(this, msg =>
             {
                 Contacts.Add(msg.Item);
             });
        }

        private RelayCommand addContactCommand;
        public RelayCommand AddContactCommand
        {
            get => addContactCommand ?? (addContactCommand = new RelayCommand(
                () =>
                {
                    navigationService.Navigate<AddEditContactViewModel>();
                }
            ));
        }

        private RelayCommand<Contact> openInfoCommand;
        public RelayCommand<Contact> OpenInfoCommand
        {
            get => openInfoCommand ?? (openInfoCommand = new RelayCommand<Contact>(
                param =>
                {
                    var info = $@"Name: {param.Name}
Phone: {param.Phone}
Email: {param.Email ?? "Empty"}
Category: {param.Category.Name}";
                    messageService.ShowInfo(info);
                }
            ));
        }

        private RelayCommand<Contact> deleteContactCommand;
        public RelayCommand<Contact> DeleteContactCommand
        {
            get => deleteContactCommand ?? (deleteContactCommand = new RelayCommand<Contact>(
                param =>
                {
                    if (messageService.ShowYesNo("Are you sure?"))
                    {
                        db.Contacts.Remove(param);
                        db.SaveChanges();
                        Contacts.Remove(param);
                    }
                }
            ));
        }

    }
}
