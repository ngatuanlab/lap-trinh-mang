using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace exception_udp_client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            int recv;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            int socketopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);

            Console.WriteLine("Default timeout: {0}", socketopt);

            server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);

            socketopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);

            Console.WriteLine("New timeout: {0}", socketopt);

            string welcome = "Hello, Are you there ?";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            try
            {
                recv = server.ReceiveFrom(data, ref Remote);
                Console.WriteLine("Message received from : {0}", Remote.ToString());
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            }
            catch (SocketException)
            {
                Console.WriteLine("Problem communicating with remote server.");
            }

            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }

                server.SendTo(Encoding.ASCII.GetBytes(input), ipep);
                data = new byte[1024];

                try
                {
                    recv = server.ReceiveFrom(data, ref Remote);
                    stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);

                
                }
                catch(SocketException)
                {
                    Console.WriteLine("error receiving message.");
                }
            }





        }
    }
}
