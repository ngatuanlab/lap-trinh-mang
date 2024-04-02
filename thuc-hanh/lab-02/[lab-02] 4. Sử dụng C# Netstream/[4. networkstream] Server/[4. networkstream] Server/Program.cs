using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace _4.networkstream__Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            int recv;

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


            // Giai đoạn: trao đổi data giữa client và server
            NetworkStream ns = new NetworkStream(client);

            // Gửi chuỗi welcome cho Client
            string welcome = "Chao mung den voi Server cua minh.";
            ns.Write(Encoding.ASCII.GetBytes(welcome), 0, welcome.Length);
            ns.Flush();

            while (true)
            {
                if (ns.CanRead)
                {
                    recv = ns.Read(data, 0, data.Length);
                    stringData = Encoding.ASCII.GetString(data, 0, recv);
                    Console.WriteLine(stringData);

                    if (stringData == "")
                    {
                        break;
                    }

                }
                else
                {
                    break;

                }


                ns.Write(Encoding.ASCII.GetBytes(stringData), 0, stringData.Length);
                ns.Flush();



            }

            Console.WriteLine("Ngat ket noi tu client {0}", newClient.Address);
            ns.Close();

            client.Close();
            newSock.Close();


            Console.ReadLine();


        }
    }
}
