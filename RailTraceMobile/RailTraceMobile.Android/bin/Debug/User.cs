using System;
using System.Collections.Generic;
using System.Text;

namespace RailTraceMobile.Models
{
    class User
    {
        private int Id { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }

        public User()
        {

        }

        public User(string UserName, string PassWord)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        public User(int Id, string UserName, string PassWord)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.Password = Password;
        }

        public int getId()
        {
            return Id;
        }

        public string getUserName()
        {
            return UserName;
        }

        public string getPassword()
        {
            return Password;
        }

        public void setId(int Id)
        {
            this.Id = Id;
        }    
        
        public void setUserName(string UserName)
        {
            this.UserName = UserName;
        }

        public void setPassword(string Password)
        {
            this.Password = Password;
        }

        public bool checkInformation()
        {
            //if (!this.UserName.Equals("") && !this.Password.Equals(""))
            //{
                return true;
            //}

            //return false;
        }
    }
}
