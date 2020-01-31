using System;
using System.Collections.Generic;
using System.Text;

namespace RailTraceMobile.Models
{
    class Token
    {
        private int Id { get; set; }
        private string access_token { get; set; }
        private string error_description { get; set; }
        private DateTime expire_date { get; set; }

        public Token()
        {

        }

        public Token(int Id, string access_token, string error_description, DateTime expire_date)
        {
            this.Id = Id;
            this.access_token = access_token;
            this.error_description = error_description;
            this.expire_date = expire_date;
        }

        public int getId()
        {
            return Id;
        }

        public string getAccess()
        {
            return access_token;
        }

        public string getError()
        {
            return error_description;
        }

        public DateTime getExpireTime()
        {
            return expire_date;
        }

        public void setId(int Id)
        {
            this.Id = Id;
        }

        public void setAccess(string access_token)
        {
            this.access_token = access_token;
        }

        public void setError(string error_description)
        {
            this.error_description = error_description;
        }

        public void setExpireDate(DateTime expire_date)
        {
            this.expire_date = expire_date;
        }
    }

}
