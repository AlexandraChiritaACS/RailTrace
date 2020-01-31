using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;

namespace RailTraceMobile.Models
{
    public class SqlHelper
    {
        static object locker = new object();
        SQLiteConnection database;

        public SqlHelper()
        {
            database = GetConnection();
            // create the tables  
            database.CreateTable<RegEntity>();
        }

        public IEnumerable<RegEntity> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<RegEntity>() select i).ToList();
            }
        }

        public RegEntity GetItem(string userName)
        {
            lock (locker)
            {
                return database.Table<RegEntity>().FirstOrDefault(x => x.Username == userName);
            }
        }

        public RegEntity GetItem1(string pass)
        {
            lock (locker)
            {
                return database.Table<RegEntity>().FirstOrDefault(x => x.Password == pass);
            }
        }

        public void AddItem (string username, string password)
        {

        }

        public SQLite.SQLiteConnection GetConnection()
        {
            SQLiteConnection sqlitConnection;
            var sqliteFilename = "Employee.db3";
            IFolder folder = FileSystem.Current.LocalStorage;
            string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
            sqlitConnection = new SQLite.SQLiteConnection(path);
            return sqlitConnection;
        }
    }
}
