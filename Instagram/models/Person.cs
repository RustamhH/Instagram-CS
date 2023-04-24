using Instagram.NotificationNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.models
{


    internal abstract class Person
    {
        private string _email;
        private string _password;
        public readonly string Id;



        public string Email
        {
            get { return _email; }
            set
            {
                if (!value.EndsWith(".com") || !value.Contains('@')) throw new Exception("Invalid mail name");
                _email = value;
            }
        }



        public string Password
        {
            get { return _password; }
            set
            {
                if (value.Length < 8) throw new Exception("Invalid password");
                _password = value;
            }
        }



        public Person() { Id = Guid.NewGuid().ToString().Substring(0, 5); }



        public Person(string email, string password) : this()
        {
            Email = email;
            Password = password;
        }


        public override string ToString()
        {
            return $"{Id}\n{Email}\n{Password}";
        }


        public static bool operator ==(Person u1, Person u2) => u1.Email == u2.Email || u1.Password == u2.Password;
        public static bool operator !=(Person u1, Person u2) => u1.Email != u2.Email || u1.Password != u2.Password;


    }


}
