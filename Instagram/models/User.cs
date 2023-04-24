using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instagram.DatabaseNamespace;
using Instagram.models;

namespace Instagram
{
    namespace UserNamespace
    {
        internal sealed class User : Person
        {
            private ushort _age;

            public string Name { get; set; }
            public string Surname { get; set; }
            public ushort Age
            {
                get => _age;
                set
                {
                    if (value < 13) throw new Exception("You can't have a profile until you're 13");
                    _age = value;
                }
            }


            public User() : base() { }

            public User(string name, string surname, ushort age, string email, string password) : base(email, password)
            {
                Name = name;
                Surname = surname;
                Age = age;
            }

            public void Register()
            {
            start:
                try
                {
                    Console.Write("Enter Name: ");
                    Name = Console.ReadLine();
                    Console.Write("Enter Surname: ");
                    Surname = Console.ReadLine();
                    Console.Write("Enter Age: ");
                    Age = Convert.ToUInt16(Console.ReadLine());
                    Console.Write("Enter E-Mail: ");
                    Email = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    Password = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto start;
                }
                if (!Database.CheckUserExistance(this)) Database.Users.Add(this);
                else throw new Exception("This user already exists , try Login");


            }

            
            
        }
    }
  
}
