using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Bài_06___Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("DAY LA MAY SERVER.");

            // GIAI ĐOẠN: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // GIAI ĐOẠN: bind()
            newSock.Bind(ipep);
            Console.WriteLine("Dang cho ket noi tu Client ...");

            // GIAI ĐOẠN: receiveFrom()
            // Nhận dữ liệu đầu tiên từ Client để thiết lập kết nối.
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);

            recv = newSock.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Nhan thong diep tu {0}", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            // GIAI ĐOẠN: sendTo()
            // Gửi câu chào đến Client
            string welcome = "Chao mung den Server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            newSock.SendTo(data, data.Length, SocketFlags.None, Remote);


            // Nhận dữ liệu liên tục từ Client và in ra màn hình.
            while (true)
            {
                // Nhận dữ liệu từ Client 
                data = new byte[1024];
                recv = newSock.ReceiveFrom(data, ref Remote);

                // In ra màn hình
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                // Gửi lại dữ liệu ấy cho Client
                newSock.SendTo(data, recv, SocketFlags.None, Remote);
            }


            Console.ReadLine();

        }
    }
}
