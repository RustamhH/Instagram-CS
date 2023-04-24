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

        // (https://myaccount.google.com/lesssecureapps)

        // eger mailinizde 2-Step Verification varsa ,  (https://myaccount.google.com/apppasswords)
        // burdan size verilen 16 simvollu parolu qeydiyatdan kecende password kimi qeyd edirsiz.
        // https://myaccount.google.com/apppasswords?rapt=AEjHL4OZhaix4ijnxcmiP3zDGWYH5E8DZbz_be6gU8LMAwEaplrK_xZEO96w9SWJghJLdaVE_c64HgHOcBYEBBP-ZVfyQ9T1TA


        static void Main(string[] args)
        {

            try
            {

                Admin admin = new Admin("RustamH", "rustamh2006@gmail.com", "plznmscfulyymljp");
                Database.Admins.Add(admin);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
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