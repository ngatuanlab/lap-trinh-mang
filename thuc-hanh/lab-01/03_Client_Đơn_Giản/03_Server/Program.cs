using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _03_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 03 - XÂY DỰNG CHƯƠNG TRÌNH CLIENT ĐƠN GIẢN.
            // Nội dung: Xây dựng một chương trình Client đơn giản nhận câu chào từ Server và hiện thị lên dòng lệnh.

            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("BAI 03 - XAY DUNG CHUONG TRINH CLIENT DON GIAN.");
            Console.WriteLine("DAY LA MAY SERVER.");

            // Giai đoạn 01 - Tạo Socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5000);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Giai đoạn 02 - kết nối bind() và accept()
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi tu Client ...");
            Socket client = newSock.Accept();

            // hiển thị thông tin của Client khi kết nối.
            IPEndPoint client_ep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Thong tin Client: IP-{0} va Port-{1}", client_ep.Address, client_ep.Port);

            // Giai đoạn 03 - Gửi và nhận dữ liệu giữa Server và Client
            // gửi câu chào cho Client

            string welcome = "Chao mung den Server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);

            // Giai đoạn 04 - Đóng kết nối close()
            Console.WriteLine("Server dong ket noi.");
            client.Close();
            newSock.Close();

            Console.ReadLine();







        }
    }
}
