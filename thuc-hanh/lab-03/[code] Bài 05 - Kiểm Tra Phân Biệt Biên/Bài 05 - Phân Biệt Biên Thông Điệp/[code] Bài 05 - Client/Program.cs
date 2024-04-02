using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _code__Bài_05___Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];

            Console.WriteLine("DAY LA MAY CLIENT.");

            // Giai đoạn: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

            // Giai đoạn: sendTo() (gửi "xin chào" đến Server - kết nối với Server)
            string welcome = "xin chao Server, minh la Client.";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            // Giai đoạn: receiveFrom() (nhận "xin chào" từ Server)
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)sender;
            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref tmpRemote);

            Console.WriteLine("Nhan dc tin nhan tu {0}", tmpRemote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            // Gửi 5 thông điệp cho Server
            server.SendTo(Encoding.ASCII.GetBytes("Thong Diep 01"), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Thong Diep 02"), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Thong Diep 03"), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Thong Diep 04"), tmpRemote);
            server.SendTo(Encoding.ASCII.GetBytes("Thong Diep 05"), tmpRemote);

            // Giai đoạn: close()
            Console.WriteLine("Client ngat ket noi den Server.");
            server.Close();

            Console.ReadLine();
        }
    }
}
