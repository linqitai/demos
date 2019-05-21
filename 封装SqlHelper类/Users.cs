using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 封装SqlHelper类
{
    public class Users
    {
        public Users() { }
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
}
