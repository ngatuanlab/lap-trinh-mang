using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace _5.Stream__Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            string data;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            // Giai Đoạn: Tạo Socket()
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("DAY LA SERVER.");

            // Giai đoạn: bind() (chờ kết nối từ Client)
            newSock.Bind(ipep);
            newSock.Listen(10);
            Console.WriteLine("Dang cho ket noi ...");

            // Giai đoạn: accept() (chấp nhận kết nối từ Client)
            Socket client = newSock.Accept();

            IPEndPoint newClient = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("ket noi tu {0} tai Port {1}", newClient.Address, newClient.Port);

            // Giai đoạn: Trao đổi data giữa Server và Client
            NetworkStream ns = new NetworkStream(client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);

            // Gửi chuỗi welcome cho Client
            string welcome = "Chao mung den voi Server cua minh.";
            sw.WriteLine(welcome);
            sw.Flush();

            // Nhận chuỗi dữ liệu từ Client
            while (true)
            {
                try
                {
                    // nhận data từ client
                    data = sr.ReadLine();
                }
                catch (IOException)
                {
                    break;
                }

                Console.WriteLine(data);

                // Gửi data vừa nhận lại cho client
                sw.WriteLine(data);
                sw.Flush();

            }

            Console.WriteLine("Ngat ket noi tu {0}", newClient.Address);

            sw.Close();
            sr.Close();
            ns.Close();

            Console.ReadLine();
        }
    }
}
