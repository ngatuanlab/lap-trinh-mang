using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace client
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "TCP client";
            var ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            var size = 1024;
            while (true)
            {
                Console.Write("Text >>> ");
                var text = Console.ReadLine();
                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipep);

                var sendBuffer = Encoding.ASCII.GetBytes(text);
                socket.Send(sendBuffer);
                socket.Shutdown(SocketShutdown.Send);

                var receiveBuffer = new byte[1024];
                var length = socket.Receive(receiveBuffer);
                var result = Encoding.ASCII.GetString(receiveBuffer, 0, length);
                socket.Close();
                Console.WriteLine(">>> {0}", result);
            }


        }
    }
}
