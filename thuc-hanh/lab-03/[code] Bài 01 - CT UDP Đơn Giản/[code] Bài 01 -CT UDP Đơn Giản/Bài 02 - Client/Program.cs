using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bài_02___Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;

            Console.WriteLine("DAY LA MAY CLIENT.");

            // Giai đoạn: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

            // Giai đoạn: sendTo()
            // Gửi câu chào đến Server để kết nối với Server
            string welcome = "xin chao Server";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            // Giai đoạn: receiveFrom()
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            // Nhận câu chào từ Server
            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Nhan tin nhan tu {0}", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));


            // Giai đoạn: close()
            Console.WriteLine("Ngat ket noi den Server.");
            server.Close();

            Console.ReadLine();
        }
    }
}
