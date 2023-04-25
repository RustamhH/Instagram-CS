using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Xml;
using Instagram.NetworkNamespace;
using Instagram.AdminNamespace;
using Instagram.DatabaseNamespace;
using Instagram.UserNamespace;
using Instagram.NotificationNamespace;

namespace Instagram
{

    

    
    



    internal class Program
    {

        static public int Print(List<string> arr)
        {
            int index = 0;
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (i == index) Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(50, i + 10);
                    Console.WriteLine(arr[i]);
                }
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index == 0) index = arr.Count - 1;
                    else index--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index == arr.Count - 1) index = 0;
                    else index++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    return index;
                }
            }
        }

        // Admin-in emailinden userlere mesaj gede bilmesi ucun "App password" elde etmeli ve onu register olarken email passwordu kimi qeyd etmelisiniz
        // (eger ozunuz admin yaradib yoxlamaq isteyirsinizse)

        // Her ehtimala qarsi menim yaratdigim adminlerle islemeyiniz yaxsi olar.

        // Emaillerin gonderilmesi run-time in yavas getmesine sebeb ola biler , kodda problem yoxdur

        // https://www.fakemail.net/

        // https://myaccount.google.com/apppasswords

        static void Main(string[] args)
        {

                
            Admin admin2 = new Admin("Admin", "mya8min@gmail.com", "adminadmin123", "brcldnhkuwhcdqrr");
            if(!Database.CheckAdminExistance(admin2)) Database.Admins.Add(admin2);

            User currentUser = new();
            Admin currentAdmin = new();
            
            

            

            bool AdminIsLogined = false;
            

            while(true)
            {
                int choice;
                if(!AdminIsLogined) choice = Print(new List<string> {"Admin"});
                else choice = Print(new List<string> { "Admin", "User" });

                if (choice == 0)
                {
                    Print(new List<string> { "Login" });
                    try
                    {
                        currentAdmin = Database.LoginAdmin();
                        Console.Clear();
                        Console.WriteLine($"Welcome , {currentAdmin.Username}");
                        Console.ReadKey(true);
                        int adminChoice = Print(new List<string> { "Post",$"Notifications({currentAdmin.UnreadNotifications})"});
                        AdminIsLogined = true;
                        Console.Clear();
                        if(adminChoice==0)
                        {
                            currentAdmin.Post();
                            Console.WriteLine("Post Uploaded Successfully");
                            Console.ReadKey(true);
;                       }
                        else
                        {
                            currentAdmin.PrintNotifications();
                            Console.ReadKey(true);
                        }

                    }
                    catch (Exception ex) { 
                        Console.WriteLine(ex.Message); 
                        Console.ReadKey(true);
                    }
                }
                else
                {

                    int userChoice = Print(new List<string> { "Login","Register" });
                    if(userChoice==0)
                    {
                        try
                        {
                            currentUser = Database.LoginUser();
                            Console.Clear();
                            Console.WriteLine($"Welcome , {currentUser.Name}");
                            currentAdmin.Notifications.Add(new Notification($"New Login by {currentUser.Name}", DateTime.Now, currentUser));
                            currentAdmin.UnreadNotifications++;
                            Console.ReadKey(true);
                            Database.ShowEveryPostShort();
                            Console.Write("Enter Id: ");
                            string? id = Console.ReadLine();
                            if(id!=null)
                            {
                                Database.Like_View_Post(currentUser, id);
                            }

                        }
                        catch (Exception ex) { 
                            Console.WriteLine(ex.Message);
                            Console.ReadKey(true);
                        }
                    }
                    else
                    {
                        try
                        {
                            currentUser.Register();
                        }
                        catch (Exception ex) { 
                            Console.WriteLine(ex.Message);
                            Console.ReadKey(true);
                            continue;
                        }
                        
                        int VerificationCode = currentAdmin.SendVerificationCode(currentUser.Email);
                        Console.WriteLine("Enter your 6 digit verification code: ");
                        if(int.TryParse(Console.ReadLine(),out int vcode))
                        {
                            if (vcode == VerificationCode)
                            {
                                Console.WriteLine("Registration succesfull , welcome!!!");
                                currentAdmin.Notifications.Add(new Notification($"New Registration by {currentUser.Name}", DateTime.Now, currentUser));
                                currentAdmin.UnreadNotifications++;
                                Console.ReadKey(true);
                            }
                            else {
                                Console.WriteLine("Verification code isn't correct , your registration has failed");
                                Console.ReadKey(true);
                            } 
                        }
                        else
                        {
                            Console.WriteLine("Invalid Format");
                            Console.ReadKey(true);
                        } 
                    }
                }
            }
        }
    }
}