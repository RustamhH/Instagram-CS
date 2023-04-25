using Instagram.NotificationNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram
{
    namespace NetworkNamespace
    {
        internal interface ISendMail
        {
            public int SendVerificationCode(string toMail);
            public void SendPostInfo(Notification n); 
        }

    }
}
