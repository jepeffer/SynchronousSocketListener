using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SynchronousSocketListener.src
{
    class ClientManager
    {

        List<Client> clientList;

        public ClientManager()
        {
            clientList = new List<Client>();
        }

        public void addClient(Socket inClientSocket)
        {
            Client client = null;
            Thread t1 = new Thread(delegate ()
            {
              client = new Client(inClientSocket);
            });
                clientList.Add(client);

            t1.Start();

        }
    }
}
