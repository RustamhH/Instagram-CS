using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Instagram.UserNamespace;

namespace Instagram
{
    namespace NotificationNamespace
    {
        internal sealed class Notification
        {
            public bool IsViewed { get; set; }

            public readonly string Id;
            public string Text { get; set; }

            public readonly DateTime TimeReceived;

            public User FromUser { get; set; }

            public Notification() { Id = Guid.NewGuid().ToString().Substring(0, 5); }


            public Notification(string text, DateTime timeReceived, User fromUser) : this()
            {
                Text = text;
                TimeReceived = timeReceived;
                FromUser = fromUser;
            }

            public override string ToString()
            {
                return $"Text: {Text}\nTime: {TimeReceived}";
            }
        }
    }
    
}
