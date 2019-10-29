using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace SynchronousSocketListener.src
{
    class Client
    {
        Socket clientSocket;

        string data;

        const short BYTE_BUFFER = 1024;

        byte[] bytes;

        public Client (Socket inClientSocket)
        {
            clientSocket = inClientSocket;
            bytes = new byte[BYTE_BUFFER];
            Console.WriteLine("User connected!");
            listen();
        }

        private void listen()
        {
            // An incoming connection needs to be processed.  
            data = null;
            while (true)
            {
                int bytesRec = clientSocket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("c") > -1)
                {
                    break;
                }
            }
            
            // Show the data on the console.  
            Console.WriteLine("Text received : {0}", data);

            // Echo the data back to the client.  
            byte[] msg = Encoding.ASCII.GetBytes(data);

            clientSocket.Send(msg);
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

    }
}
