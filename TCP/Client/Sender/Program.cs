using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3030);

            Socket sender = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            sender.Connect(remoteEP);

            while (true)
            {
                Console.WriteLine("You: ");              
                var msg = Console.ReadLine();
                Console.WriteLine("\n");

                if (msg == "Tchau!" || msg == "[X]")
                    break;

                byte[] msgTobyte = Encoding.ASCII.GetBytes(msg);

                int bytesSent = sender.Send(msgTobyte);

                byte[] bytes = new byte[1024];
              
                int bytesRec = sender.Receive(bytes);

                Console.WriteLine("Fulano: ");
                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));
                Console.WriteLine("\n");
            }
            sender.Close();
        }
    }
}
