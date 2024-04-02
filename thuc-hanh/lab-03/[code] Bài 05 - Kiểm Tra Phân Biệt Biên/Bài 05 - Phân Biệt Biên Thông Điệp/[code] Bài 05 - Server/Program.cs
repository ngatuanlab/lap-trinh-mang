using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _code__Bài_05___Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("DAY LA MAY SERVER.");

            // Giai đoạn: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Giai đoạn: bind()
            newsock.Bind(ipep);
            Console.WriteLine("Dang cho ket noi tu Client.");

            // Giai đoạn: receiveFrom() (nhận thông báo "xin chao" tu Client")
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)(sender);

            recv = newsock.ReceiveFrom(data, ref tmpRemote);

            Console.WriteLine("Nhan dc tin nhan tu {0}", tmpRemote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            // Giai đoạn: sendTo() (gửi thông báo "xin chao" cho Client)
            string welcome = "chao mung den voi Server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            newsock.SendTo(data, data.Length, SocketFlags.None, tmpRemote);

            // Nhận 5 thông điệp được gửi từ Server 
            for (int i = 0; i < 5; i++)
            {
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref tmpRemote);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            }

            // Giai đoạn: close() (phía Server)
            Console.WriteLine("Server ngat ket noi.");
            newsock.Close();

            Console.ReadLine();

        }
    }
}
