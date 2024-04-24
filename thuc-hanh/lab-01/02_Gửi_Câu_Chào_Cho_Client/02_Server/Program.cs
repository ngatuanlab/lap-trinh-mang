using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _02_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 02 - TẠO SERVER ĐƠN GIẢN VÀ GỬI CÂU CHÀO CHO CLIENT
            // Nội dung: Tạo một Server (dạng TCP) đơn giản và gửi câu chào cho Client khi kết nối (thông qua Telnet)

            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("DAY LA MAY SERVER.");

            // Giai đoạn 01 - Tạo Socket
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5000);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Giai đoạn 02 - Kết nối Connect()
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho Client ket noi ...");
            Socket client = newSock.Accept();

            // hiển thị thông tin kết nối của Client (IP và Port)
            IPEndPoint client_ep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Client ket noi: IP-{0} va Port-{1}", client_ep.Address, client_ep.Port);

            // Giai đoạn 03 - gửi và nhận dữ liệu giữa Server và Client
            // gửi câu chào cho Client
            string welcome = "Chao mung den Server cua minh";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);

            // Giai đoạn 04 - Đóng kết nối
            Console.WriteLine("Dong ket noi.");
            client.Close();
            newSock.Close();

            Console.ReadLine();



        }
    }
}
