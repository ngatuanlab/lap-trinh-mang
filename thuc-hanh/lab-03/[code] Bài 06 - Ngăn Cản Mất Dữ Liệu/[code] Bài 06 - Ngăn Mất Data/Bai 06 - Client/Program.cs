using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bai_06___Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[30];
            string input, stringData;

            Console.WriteLine("DAY LA MAY CLIENT.");

            // Giai đoạn: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

            // Giai đoạn: sendTo()
            string welcome = "Xin chao Server, mình la Client.";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            // Giai đoạn: receiveFrom()
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)sender;

            data = new byte[30];
            int recv = server.ReceiveFrom(data, ref tmpRemote);

            Console.WriteLine("Nhan dc tin nhan tu {0}", tmpRemote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0,recv));

            // Gửi thông điệp cho máy Server
            int i = 30;
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                server.SendTo(Encoding.ASCII.GetBytes(input), tmpRemote);
                data = new byte[i];
                try
                {
                    recv = server.ReceiveFrom(data, ref tmpRemote);
                    stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Canh bao: Du lieu bi mat, dang thu lai.");
                    i += 10;
                }
            }

            Console.WriteLine("Dong ket noi cua Client.");
            server.Close();

            Console.ReadLine();

        }
    }
}
