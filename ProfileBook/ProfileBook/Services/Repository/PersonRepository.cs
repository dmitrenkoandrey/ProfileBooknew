using System;
using System.Collections.Generic;
using System.Text;
using ProfileBook.Models;
using SQLite;
namespace ProfileBook
{
  public  class PersonRepository
    {
        SQLiteConnection database;
        public PersonRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Person>();
        }
        public IEnumerable<Person> GetItems()
        {
            return database.Table<Person>().ToList();
        }
        public Person GetItem(int id)
        {
            return database.Get<Person>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Person>(id);
        }
        public int SaveItem(Person item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
