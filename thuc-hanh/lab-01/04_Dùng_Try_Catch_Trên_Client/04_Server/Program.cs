using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _04_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            // BÀI 04 - DÙNG TRY-CATCH TRÊN CLIENT ĐƠN GIẢN
            // Nội dung: Với bài trước thì khi mở Client trước sẽ báo lỗi vì không kết nối được với Server. Để khắc phục thì áp dụng try-catch vào phần kết nối. Nếu báo lỗi thì sẽ thoát chương trình và hiển thị thông báo lỗi (nếu thiết lập).

            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("BAI 04 - DUNG TRY-CATCH TREN CLIENT DON GIAN.");
            Console.WriteLine("DAY LA MAY SERVER.");

            // giai đoạn 01 - tạo socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5000);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // giai đoạn 02 - lắng nghe và kết nối (bind và accept)
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi ...");
            Socket client = newSock.Accept();

            // hiển thị thông tin của Client khi kết nối (IP và Port)
            IPEndPoint client_ep = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Client ket noi: IP-{0} va Port-{1}", client_ep.Address, client_ep.Port);

            // giai đoạn 03 - gửi và nhận dữ liệu giữa Client và Server
            // gửi câu chào cho Client
            string welcome = "Chao mung den server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);

            // giai đoạn 04 - đóng kết nối
            Console.WriteLine("Server dong ket noi.");
            client.Close();
            newSock.Close();

            Console.ReadLine();


        }
    }
}
