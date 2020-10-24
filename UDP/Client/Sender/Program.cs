using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Digite o primeiro digito");
                string input1 = Console.ReadLine();

                Console.WriteLine("Digite o operador");
                string input2 = Console.ReadLine();

                Console.WriteLine("Digite o segundo digito");
                string input3 = Console.ReadLine();

                string operation = input1 + ", " + input2 + ", " + input3;

                byte[] packetData = Encoding.ASCII.GetBytes(operation);

                UdpClient client = new UdpClient();

                IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12788);

                client.Connect(ep);

                client.Send(packetData, 7);

                byte[] data = client.Receive(ref ep);

                Console.WriteLine("O resultado da operação é: " + Encoding.ASCII.GetString(data));
                Console.WriteLine("\n");
            }
        }
    }
}
