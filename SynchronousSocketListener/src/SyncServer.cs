using SynchronousSocketListener.src;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SyncServer
{
    internal ClientManager clientManager;

    public SyncServer()
    {
        clientManager = new ClientManager();
        StartListening();
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public void StartListening()
    {
 
        // Establish the local endpoint for the socket.  
        // Dns.GetHostName returns the name of the   
        // host running the application.  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        Console.WriteLine(ipAddress.ToString());
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5556);

        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and   
        // listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();
                clientManager.addClient(handler);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }



    }
}