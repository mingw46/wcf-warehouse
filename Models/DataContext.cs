using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class DataContext : DbContext
    {
        public DataContext() : base("DataContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
