using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpServer = new UdpClient(12788);

            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12788);
                byte[] data = udpServer.Receive(ref remoteEP);
                string[] result = Encoding.ASCII.GetString(data).Split(", ");

                double digit1 = Convert.ToDouble(result[0]);
                string operador = result[1];
                double digit2 = Convert.ToDouble(result[2]);
                double finalResult = 0;

                switch (operador)
                {
                    case "+":
                        finalResult = digit1 + digit2;
                        break;
                    case "-":
                        finalResult = digit1 - digit2;
                        break;
                    case ".":
                        finalResult = digit1 * digit2;
                        break;
                    case "/":
                        finalResult = digit1 / digit2;
                        break;
                    default:
                        operador = null;
                        Console.WriteLine("\n");
                        Console.WriteLine("*************** OPERADOR INVÁLIDO ***************");
                        break;
                }

                if (operador == null)
                    break;

                byte[] doubleTobyte = Encoding.ASCII.GetBytes(finalResult.ToString());

                Console.Write("Retornando o número " + finalResult.ToString() + " para o client");
                         
                udpServer.Send(doubleTobyte, doubleTobyte.Length, remoteEP);
            }
        }
    }
}
