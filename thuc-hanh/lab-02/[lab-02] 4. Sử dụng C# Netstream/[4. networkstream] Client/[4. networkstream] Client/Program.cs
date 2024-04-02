using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace _4.networkstream__Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            int recv;

            // Giai đoạn: tạo socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("DAY LA CLIENT.");

            // Giai đoạn: accept() (kết nối đến Server)
            try
            {
                server.Connect(ipep);
            } catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den Server.");
                Console.WriteLine(e.ToString());
                return;
            }

            // Giai đoạn: trao đổi data giữa client và server
            NetworkStream ns = new NetworkStream(server);

            if (ns.CanRead)
            {
                recv = ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            else
            {
                Console.WriteLine("Khong the doc tu Socket nay.");
                ns.Close();
                server.Close();
                return;
            }

            // Gửi dữ liệu lặp cho Server
            while(true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }

                if (ns.CanWrite)
                {
                    ns.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
                    ns.Flush();
                }

                recv = ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);

            }

            Console.WriteLine("Ngat ket noi tu Server.");
            ns.Close();
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();



        }
    }
}
