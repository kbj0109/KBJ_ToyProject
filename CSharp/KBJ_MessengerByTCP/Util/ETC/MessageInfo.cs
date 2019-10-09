using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBJ_MessengerByTCP
{
    class MessageInfo
    {
        public string type;
        public string sender;
        public string content;
        public string time;
        public string userNumber;

        public MessageInfo(string type, string sender, string content, string time, string userNumber)
        {
            this.type = type;
            this.sender = sender;
            this.content = content;
            this.time = time;
            this.userNumber = userNumber;
        }

        ////이 부분이 필요하려나?
        //public string Type
        //{
        //    get { return type; }
        //    set { type = value; }
        //}
        //public string Sender
        //{
        //    get { return sender; }
        //    set { sender = value; }
        //}
        //public string Content
        //{
        //    get { return content; }
        //    set { content = value; }
        //}
        //public string Time
        //{
        //    get { return time; }
        //    set { time = value; }
        //}
        //public string UserNumber
        //{
        //    get { return userNumber; }
        //    set { userNumber = value; }
        //}
    }
}
