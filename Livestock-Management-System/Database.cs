
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livestock_Management_System
{
    public class Database
    {
        private SQLiteConnection _connection;
        public Database()
        {
            try
            {
                var dataDir = AppDomain.CurrentDomain.BaseDirectory;
                var dbPath = Path.Combine(dataDir, "FarmDataOriginal.db");
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }
                _connection = new SQLiteConnection(dbPath);
                _connection.CreateTables<Cow, Sheep>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization error: {ex.Message}");
                throw;

            }
        }
        public List<Animal> ReadItems()
        {
            var stores = new List<Animal>();
            var lst1 = _connection.Table<Cow>().ToList();
            stores.AddRange(lst1);
            var lst2 = _connection.Table<Sheep>().ToList();
            stores.AddRange(lst2);
            return stores;
        }
        public int InsertItem(Animal item)
        {
            return _connection.Insert(item);
        }
        public int DeleteItem(Animal item)
        {
            return _connection.Delete(item);
        }
        public int UpdateItem(Animal item)
        {
            return _connection.Update(item);
        }
    }

}
