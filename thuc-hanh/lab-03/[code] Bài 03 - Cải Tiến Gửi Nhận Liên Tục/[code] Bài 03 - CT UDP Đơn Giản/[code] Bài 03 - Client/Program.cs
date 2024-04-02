using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace _code__Bài_03___Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;

            Console.WriteLine("DAY LA MAY CLIENT.");

            // GIAI ĐOẠN: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

            // GIAI ĐOẠN: sendTo() (Kết nối với Server bằng gửi tin nhắn đầu tiên)
            string welcome = "Xin chao Server";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            // Nhận lời chào "welcome" từ Server
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Tin nhan nhan dc tu {0}", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            // Gửi các tin nhắn nhập từ bàn phím
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
                data = new byte[1024];

                // Nhận dữ liệu được gửi từ Server.
                recv = server.ReceiveFrom(data, ref Remote);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }

            // GIAI ĐOẠN: close()
            Console.WriteLine("Ngung ket noi den Server.");
            server.Close();

            Console.ReadLine();
        }
    }
}
