using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _01_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 01 - TẠO MỘT SERVER ĐƠN GIẢN (TCP)
            // Nội dung: Tạo một Server đơn giản nhận kết nối từ Client (qua Telnet) và hiển thị thông tin của Client (IP và Port)

            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("DAY LA MAY SERVER.");

            // Giai đoạn 01 - Tạo Socket 
            // Mở Port 5000 cho dịch vụ 
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5000);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Giai đoạn 02 - Tạo kết nối (bind và listen)
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi tu Client ...");
            Socket client = newSock.Accept();

            // Hiển thị thông tin kết nối của Client
            IPEndPoint clien_iep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Thong tin Client: IP Address: {0} va Port: {1}", clien_iep.Address, clien_iep.Port);

            // Giai đoạn 03 - Trao đổi dữ liệu 

            // Giai đoạn 04 - Đóng kết nối
            Console.WriteLine("Client dong ket noi.");
            client.Close();
            newSock.Close();

            Console.ReadLine();




        }
    }
}
