using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bài_04___Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;

            Console.WriteLine("DAY LA MAY CLIENT.");

            // GIAI ĐOẠN: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);

            // GIAI ĐOẠN: sendto() (dùng hàm connect())
            server.Connect(ipep);
            string welcome = "Xin chao Server.";
            data = Encoding.ASCII.GetBytes(welcome);
            server.Send(data);

            // GIAI ĐOẠN: receiveFrom() 
            data = new byte[1024];
            int recv = server.Receive(data);
            Console.WriteLine("Nhan dc thong diep tu {0}", ipep.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            // GIAI ĐOẠN: sendTo()
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }

                server.Send(Encoding.ASCII.GetBytes(input));
                data = new byte[1024];

                recv = server.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }

            // GIAI ĐOẠN: close()
            Console.WriteLine("Ngat ket noi den Server.");
            server.Close();


        }
    }
}
