using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace _5.stream__Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            string data;
            string input;

            //Giai đoạn: tạo socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("DAY LA CLIENT");

            // Giai đoạn: connect() (kết nối đến Server)
            try
            {
                // Kết nối đến Server
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den Server.");
                Console.WriteLine(e.ToString());
                return;
            }

            NetworkStream ns = new NetworkStream(server);
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            // nhận chuỗi "welcome" từ Server
            data = sr.ReadLine();
            Console.WriteLine(data);

            // Gửi chuỗi nhập từ bàn phím cho Server
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                else
                {
                    sw.WriteLine(input);
                    sw.Flush();

                    // nhận data từ server và in ra màn hình
                    data = sr.ReadLine();
                    Console.WriteLine(data);
                }
            }

            Console.WriteLine("Ngat ket noi tu Server.");
            sr.Close();
            sw.Close();
            ns.Close();

            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();
        }
    }
}
