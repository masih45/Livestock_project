using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace task4a
{
    public class Database
    {
        private SQLiteConnection _connection;
        // Assuming you have a list to store the items in memory
        private List<Store> _storeItems;

        public Database()
        {
            var dbPath = App.DatabasePath;
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Cow>();
            _connection.CreateTable<Sheep>();
        }

        public List<Cow> GetCows()
        {
            return _connection.Table<Cow>().ToList();
        }

        public List<Sheep> GetSheep()
        {
            return _connection.Table<Sheep>().ToList();
        }

        // Method to read items from the database
        public List<Store> ReadItems()
        {
            // Replace this with actual database reading logic
            return _storeItems;
        }

        public Store GetItemById(int id)
        {
            var cow = _connection.Find<Cow>(id);
            if (cow != null)
                return cow;

            var sheep = _connection.Find<Sheep>(id);
            if (sheep != null)
                return sheep;

            return null;
        }

        public int InsertItem(Store item)
        {
            return _connection.Insert(item);
        }

        public int DeleteItem(Store item)
        {
            return _connection.Delete(item);
        }

        public int UpdateItem(Store item)
        {
            return _connection.Update(item);
        }
    }
}
