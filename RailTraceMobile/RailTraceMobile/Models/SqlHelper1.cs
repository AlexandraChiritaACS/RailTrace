using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using PCLStorage;

namespace RailTraceMobile.Models
{
    public class SqlHelper1
    {
        static object locker = new object();
        SQLiteConnection database;

        public SqlHelper1()
        {
            database = GetConnection();
            // create the tables  
            database.CreateTable<RegEntity1>();
        }

        public IEnumerable<RegEntity1> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<RegEntity1>() select i).ToList();
            }
        }

        public RegEntity1 GetItem(string userName)
        {
            lock (locker)
            {
                return database.Table<RegEntity1>().FirstOrDefault(x => x.Username == userName);
            }
        }

        public RegEntity1 GetItem1(string pass)
        {
            lock (locker)
            {
                return database.Table<RegEntity1>().FirstOrDefault(x => x.Password == pass);
            }
        }

        public void AddItem (string username, string password)
        {
            RegEntity1 r1 = new RegEntity1
            {
                Username = username,
                Password = password
            };

            database.Insert(r1);
        }

        public SQLite.SQLiteConnection GetConnection()
        {
            SQLiteConnection sqlitConnection;
            var sqliteFilename = "Transaction.db3";
            IFolder folder = FileSystem.Current.LocalStorage;
            string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
            sqlitConnection = new SQLite.SQLiteConnection(path);
            return sqlitConnection;
        }
    }
}
