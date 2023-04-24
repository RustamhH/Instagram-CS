using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instagram.AdminNamespace;
using Instagram.NotificationNamespace;
using Instagram.PostNamespace;
using Instagram.UserNamespace;
namespace Instagram
{
    namespace DatabaseNamespace
    {
        internal static partial class Database
        {
            public static List<User> Users = new();

            public static List<Admin> Admins = new();
            public static partial Admin LoginAdmin();
            public static partial User LoginUser();
            public static partial void ShowEveryPostShort();
            public static partial void Like_View_Post(User user, string id);

            public static partial bool CheckUserExistance(User user);
            public static partial bool CheckAdminExistance(Admin admin);

        }






        internal static partial class Database
        {
            public static partial Admin LoginAdmin()
            {
                Console.Write("Enter E-Mail: ");
                string? email = Console.ReadLine();
                Console.Write("Enter Password: ");
                string? password = Console.ReadLine();
                if (email == null || password == null) throw new Exception("Argument Null");
                foreach (var item in Database.Admins)
                {
                    if (item.Email == email && item.Password == password) return item;
                }
                throw new Exception("Admin Not Found");
            }

            public static partial User LoginUser()
            {
                Console.Write("Enter E-Mail: ");
                string? email = Console.ReadLine();
                Console.Write("Enter Password: ");
                string? password = Console.ReadLine();
                if (email == null || password == null) throw new Exception("Argument Null");
                foreach (var item in Database.Users)
                {
                    if (item.Email == email && item.Password == password) return item;
                }
                throw new Exception("User Not Found");
            }

            public static partial void ShowEveryPostShort()
            {
                Console.Clear();
                foreach (var item in Admins)
                {
                    
                    Console.WriteLine($"Poster: {item.Username}");
                    item.ShowAllPostsShort();
                    Console.WriteLine("\n");
                }
            }




            public static partial void Like_View_Post(User user,string id)
            {
                foreach (var item in Admins)
                {
                    for (int i = 0; i < item.Posts.Count; i++)
                    {
                        if (item.Posts[i].Id == id)
                        {
                            Console.Clear();
                            Console.WriteLine(item.Posts[i]);
                            if (!item.Posts[i].IsViewed) {
                                item.Posts[i].ViewCount++;
                                item.Posts[i].IsViewed = true;
                                item.Notifications.Add(new Notification($"{user.Name} viewed your post", DateTime.Now, user));
                                item.UnreadNotifications++;
                            }
                            Console.Write("Enter 'L' to like the post: ");
                            if (Console.ReadKey(true).Key == ConsoleKey.L)
                            {
                                if (item.Posts[i].IsLiked) throw new Exception("You already liked this post");
                                item.Posts[i].LikeCount++;
                                item.Posts[i].IsLiked = true;
                                item.Notifications.Add(new Notification($"{user.Name} liked your post", DateTime.Now, user));
                                item.UnreadNotifications++;
                                return;
                            }
                            else return;
                        }
                    }
                }
                throw new Exception("Post not found");
            }

            public static partial bool CheckUserExistance(User user)
            {
                foreach (var item in Users)
                {
                    if (item == user) return true;
                }
                return false;
            }
            public static partial bool CheckAdminExistance(Admin admin)
            {
                foreach (var item in Admins)
                {
                    if (item == admin) return true;
                }
                return false;
            }

        }
    }
    
}
