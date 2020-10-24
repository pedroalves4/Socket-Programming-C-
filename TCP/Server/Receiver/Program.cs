using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        const int PORT_NO = 3030;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            listener.Start();

            Console.WriteLine("Aguardando alguém para conversar..");

            TcpClient client = listener.AcceptTcpClient();
            if(client.Connected)
            {
                Console.WriteLine("Alguém entrou no chat!");
            }
            
            while (true)
            {
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
              
                int bytesRead = nwStream.ReadAsync(buffer, 0, client.ReceiveBufferSize).Result;

                Console.WriteLine("Fulano: ");
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine(dataReceived);
                Console.WriteLine("\n");

                Console.WriteLine("You: ");

                var msg = Console.ReadLine();
                if (msg == "Tchau!" || msg == "[X]")
                    break;

                byte[] resp = Encoding.ASCII.GetBytes(msg);

                nwStream.WriteAsync(resp, 0, resp.Length);
                Console.WriteLine("\n");
            }
            client.Close();
            listener.Stop();
        }
    }
}
