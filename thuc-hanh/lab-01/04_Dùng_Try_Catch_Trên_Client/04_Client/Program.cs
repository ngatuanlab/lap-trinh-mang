using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _04_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 04 - SỬ DỤNG TRY-CATCH TRONG CLIENT ĐƠN GIẢN

            byte[] data = new byte[1024];
            string input, inputData;

            Console.WriteLine("DAY LA MAY CLIENT.");

            // giai đoạn 01 - tạo socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // giai đoạn 02 - kết nối đến Server
            // sử dụng try-catch để hạn chế thông báo lỗi và dừng chương trình 
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

            // giai đoạn 03 - Trao đổi dữ liệu 
            // nhận câu chào từ Server
            int recv = server.Receive(data);
            inputData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(inputData);

            // giai đoạn 04 - đóng kết nối
            Console.WriteLine("Client dong ket noi.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();
            

        }
    }
}
