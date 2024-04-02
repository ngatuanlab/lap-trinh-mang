using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _Phân_Biệt_01__Client
{
    class Program
    {
        // Hàm gửi dữ liệu thông điệp 
        private static int sendData(Socket s, byte[] data)
        {
            int total = 0;
            int size = data.Length;
            int dataLeft = size;
            int sent;

            while (total < size)
            {
                sent = s.Send(data, total, dataLeft, SocketFlags.None);
                total += sent;
                dataLeft -= sent;
            }

            return total;
        }

        // Hàm nhận dữ liệu thông điệp
        private static byte[] receiveData(Socket s, int size)
        {
            int total = 0;
            int dataLeft = size;
            byte[] data = new byte[size];
            int recv;

            while (total < size)
            {
                recv = s.Receive(data, total, dataLeft, 0);
                if (recv == 0)
                {
                    data = Encoding.ASCII.GetBytes("exit");
                    break;
                }

                total += recv;
                dataLeft -= recv;
            }

            return data;
        }


        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            int sent;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den Server.");
                Console.WriteLine(e.ToString());
                
            }

            int recv = server.Receive(data);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);

            sent = sendData(server, Encoding.ASCII.GetBytes("Thong Diep 01"));
            sent = sendData(server, Encoding.ASCII.GetBytes("Thong Diep 02"));
            sent = sendData(server, Encoding.ASCII.GetBytes("Thong Diep 03"));
            sent = sendData(server, Encoding.ASCII.GetBytes("Thong Diep 04"));
            sent = sendData(server, Encoding.ASCII.GetBytes("Thong Diep 05"));

            Console.WriteLine("Da ngat ket noi tu Server.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();


            Console.ReadLine();
        }
    }
}
