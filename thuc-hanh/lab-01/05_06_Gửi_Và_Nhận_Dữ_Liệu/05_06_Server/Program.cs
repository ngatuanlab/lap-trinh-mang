using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _05_06_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 05 VÀ 06: XÂY DỰNG CLIENT VÀ SERVER GỬI DỮ LIỆU NHẬP TỪ BÀN PHÍM CHO NHAU
            // Nội dung: 
            // - Client gửi dữ liệu nhập từ bàn phím cho Server.
            // - Server nhận và hiện thị ra màn hình.
            // - Server gửi lại dữ liệu ấy cho Client và Client in ra màn hình.

            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("BAI 05 VA 06 - XAY DUNG CLIENT VA SERVER DUI DU LIEU TU BAN PHIM.");
            Console.WriteLine("DAY LA MAY SERVER.");

            // giai đoạn 01 - tạo socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5000);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // giai đoạn 02 - kết nối
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi tu Client ...");
            Socket client = newSock.Accept();

            // in thông tin của client ra màn hình 
            IPEndPoint client_ep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Client ket noi: IP-{0} va Port-{1}", client_ep.Address, client_ep.Port);

            // giai đoạn 03 - gửi và nhận dữ liệu giữa Server và Client
            // gửi câu chào cho Client
            string welcome = "Chao mung den Server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            // gửi câu chào cho client (dùng send)
            client.Send(data, data.Length, SocketFlags.None);

            // Quá trình nhận và gửi lại dữ liệu cho Client
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                if (recv == 0)
                {
                    break;
                }
                // in nội dung ra màn hình
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                // gửi lại dữ liệu ấy cho Client
                client.Send(data, recv, SocketFlags.None);
            }

            // giai đoạn 04 - ngắt kết nối
            Console.WriteLine("Server dong ket noi.");
            client.Close();
            newSock.Close();

            Console.ReadLine();
            




        }
    }
}
