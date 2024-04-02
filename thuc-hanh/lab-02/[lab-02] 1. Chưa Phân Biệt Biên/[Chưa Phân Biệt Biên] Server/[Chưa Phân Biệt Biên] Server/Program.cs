using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace _Chưa_Phân_Biệt_Biên__Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newSock.Bind(ipep);
            newSock.Listen(10);

            Console.WriteLine("Day la SERVER");

            Console.WriteLine("Dang cho ket noi ...\n");

            Socket client = newSock.Accept();
            string welcome = "Chao mung den Server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);

            IPEndPoint newClient = (IPEndPoint)client.RemoteEndPoint;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Da ket noi den {0} tai Port {1}", newClient.Address, newClient.Port);

            for (int i = 0; i < 5; i++)
			{
			    recv = client.Receive(data);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
			}

            Console.WriteLine("Da ngat ket noi tu {0}", newClient.Address);

            client.Close();
            newSock.Close();

            Console.ReadLine();



        }
    }
}
