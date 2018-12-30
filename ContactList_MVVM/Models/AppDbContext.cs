using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList_MVVM.Models
{
    class AppDbContext:DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
            if (!Categories.Any())
            {
                Categories.Add(new Category { Name = "Family" });
                Categories.Add(new Category { Name = "Friends" });
                Categories.Add(new Category { Name = "Co-workers" });
                SaveChanges();
            }
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
