using ContactList_MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList_MVVM.Messages
{
    class ContactListChangedMessage
    {
        public Contact Item { get; set; }
    }
}
