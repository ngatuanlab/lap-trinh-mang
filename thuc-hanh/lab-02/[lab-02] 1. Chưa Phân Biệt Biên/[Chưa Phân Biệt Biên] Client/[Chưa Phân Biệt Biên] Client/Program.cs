using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _Chưa_Phân_Biệt_Biên__Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string stringData;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Day la CLIENT\n");

            try
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den may chu.");
                Console.WriteLine(e.ToString());

                return;
            }

            int recv = server.Receive(data);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);

            server.Send(Encoding.ASCII.GetBytes("Thong diep 01."));
            server.Send(Encoding.ASCII.GetBytes("Thong diep 02."));
            server.Send(Encoding.ASCII.GetBytes("Thong diep 03."));
            server.Send(Encoding.ASCII.GetBytes("Thong diep 04."));
            server.Send(Encoding.ASCII.GetBytes("Thong diep 05."));

            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Ngat ket noi tu may chu.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();
        }
    }
}
