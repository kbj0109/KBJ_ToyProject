using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KBJ_MessengerByTCP.Server
{
    class ClientInfo
    {
        public Socket clientSocket;
        public bool clientSocketAlive;
        public int userNumber;
        public string userName = "";

        public Socket ClientSocket
        {
            get { return clientSocket; }
            set { clientSocket = value; }
        }
        
        public bool ClientSocketAlive
        {
            get { return clientSocketAlive; }
            set { clientSocketAlive = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public int UserNumber
        {
            get { return userNumber; }
            set { userNumber = value; }
        }
    }
}
