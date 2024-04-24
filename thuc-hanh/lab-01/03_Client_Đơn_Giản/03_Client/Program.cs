using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _03_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 03 - XÂY DỰNG CLIENT ĐƠN GIẢN

            byte[] data = new byte[1024];
            string input, inputData;

            Console.WriteLine("BAI 03 - XAY DUNG CLIENT DON GIAN.");
            Console.WriteLine("DAY LA MAY CLIENT.");

            // Giai đoạn 01 - Tạo socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Giai đoạn 02 - Kết nối đến Server.
            // sẽ xảy ra lỗi nếu như không bật Server trước (có thể khắc phục bằng try-catch tron bài 04)
            server.Connect(ipep);

            // Giai đoạn 03 - Trao đổi dữ liệu giữa Client và Server
            // Nhận câu chào từ Server
            int recv = server.Receive(data);
            inputData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(inputData);

            // Giai đoạn 04 - Đóng kết nối
            Console.WriteLine("Client dong ket noi.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();



        }
    }
}
