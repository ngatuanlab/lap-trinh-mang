using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _05_06_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 05 VÀ 06 - XÂY DỰNG CLIET VÀ SERVER GỬI DỮ LIỆU NHẬP TỪ BÀN PHÍM

            byte[] data = new byte[1024];
            string input, inputData;

            Console.WriteLine("BAI 05 VA 06 - XAY DUNG SERVER VA CLIENT GUI DU LIEU TU BAN PHIM.");
            Console.WriteLine("DAY LA MAY CLIENT.");

            // Giai đoạn 01 - Tạo Socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Giai đoạn 02 - kết nối
            try
            {
                server.Connect(ipep);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Khong the ket noi den Server.");
                Console.WriteLine(se.ToString());
                Console.ReadLine();
                return;
            }

            // giai đoạn 03 - Gửi và nhận dữ liệu nhập từ bàn phím
            // nhận câu chào từ Server
            int recv = server.Receive(data);
            inputData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(inputData);

            // gửi và nhận dữ liệu từ bàn phím
            while (true)
            {
                // nhập dữ liệu từ bàn phím
                input = Console.ReadLine();
                // nhập exit thì thoát hay đóng kết nối bên Client (cho bài tập vn 02)
                if (input == "exit")
                {
                    break;
                }
                // gửi dữ liệu nhập cho Server
                server.Send(Encoding.ASCII.GetBytes(input));
                
                // nhận dữ liệu gửi từ Server
                data = new byte[1024];
                recv = server.Receive(data);
                inputData = Encoding.ASCII.GetString(data, 0, recv);
                // in ra màn hình
                Console.WriteLine(inputData);
            }

            // giai đoạn 04 - đóng kết nối
            Console.WriteLine("Client dong ket noi.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();

        }
    }
}
