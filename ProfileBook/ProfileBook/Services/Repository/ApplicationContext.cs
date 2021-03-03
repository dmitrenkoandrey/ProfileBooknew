using Microsoft.EntityFrameworkCore;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook
{
    public class ApplicationContext : DbContext
    {
        private string _databasePath;

        public DbSet<Person> Persons { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
