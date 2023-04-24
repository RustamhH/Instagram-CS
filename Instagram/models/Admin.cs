using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Instagram.models;
using Instagram.NetworkNamespace;
using Instagram.NotificationNamespace;
using Instagram.PostNamespace;



namespace Instagram
{
    namespace AdminNamespace
    {
        internal sealed partial class Admin : Person, ISendMail
        {
            private string _username;
            public List<Post> Posts { get; set; }
            public List<Notification> Notifications { get; set; }

            public uint UnreadNotifications { get; set; }
            public string Username
            {
                get => _username;
                set
                {
                    if (value.Length < 3) throw new Exception("Invalid Username");
                    _username = value;
                }
            }
            public Admin() : base()
            {
                Posts = new List<Post>();
                Notifications = new List<Notification>();
            }
            public Admin(string username, string email, string password) : base(email, password)
            {
                Posts = new List<Post>();
                Notifications = new List<Notification>();
                Username = username;
            }

            public partial void PrintNotifications();
            public partial void Post();
            public partial void ShowAllPostsShort();
            public partial void ShowAllPostsFull();
            public partial int SendVerificationCode(string toMail);

        }




        internal sealed partial class Admin:Person, ISendMail
        {
            public partial void PrintNotifications()
            {
                foreach (var item in Notifications)
                {
                    if (!item.IsViewed)
                    {
                        Console.WriteLine(item);
                        UnreadNotifications--;
                    }
                }
            }



            public partial void Post()
            {
                Console.Write("Enter Title: ");
                string? title = Console.ReadLine();
                Console.Write("Enter Content: ");
                string? content = Console.ReadLine();
                if (title != null && content != null)
                {
                    Posts.Add(new Post(content, title, DateTime.Now));
                    return;
                }
                throw new Exception("Invalid Argument");
            }


            public partial void ShowAllPostsShort()
            {
                foreach (var item in Posts)
                {
                    Console.WriteLine(item.GetShortInfo());
                }
            }

            public partial void ShowAllPostsFull()
            {
                foreach (var item in Posts)
                {
                    Console.WriteLine(item);
                }
            }

            public partial int SendVerificationCode(string toMail)
            {
                Random random = new Random();
                int randint = random.Next(100000, 1000000);
                MailMessage message = new MailMessage();
                message.From = new MailAddress(Email);
                message.To.Add(new MailAddress(toMail));
                message.Subject = "Your Verification Code";
                message.Body = $"<html><body> {randint} </body></html>";
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(Email, Password),
                    EnableSsl = true,
                };
                smtpClient.Send(message);
                return randint;
            }
        } 
    }
    

}
