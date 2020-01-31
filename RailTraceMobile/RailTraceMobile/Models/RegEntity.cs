using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace RailTraceMobile.Models
{
    public class RegEntity
    {

       
        public string Username { get; set; }
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public RegEntity()
        {
          
        }

    }
      
}
