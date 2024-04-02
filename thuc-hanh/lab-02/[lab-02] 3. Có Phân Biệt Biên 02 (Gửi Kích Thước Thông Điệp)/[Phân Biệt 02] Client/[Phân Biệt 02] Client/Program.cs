using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _Phân_Biệt_02__Client
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

            byte[] dataSize = new byte[4];
            dataSize = BitConverter.GetBytes(size);
            sent = s.Send(dataSize);

            while (total < size)
            {
                sent = s.Send(data, total, dataLeft, SocketFlags.None);
                total += sent;
                dataLeft -= sent;
            }

            return total;
        }

        // Hàm nhận dữ liệu thông điệp
        private static byte[] receiveData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] dataSize = new byte[4];

            recv = s.Receive(dataSize, 0, 4, 0);
            int size = BitConverter.ToInt32(dataSize, 0);
            int dataLeft = size;
            byte[] data = new byte[size];

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

            Console.WriteLine("DAY LA CLIENT.");

            try
            {
                server.Connect(ipep);
                Console.WriteLine("Ket noi thanh cong den Server.");
            }
            catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den Server.");
                Console.WriteLine(e.ToString());
                return;
                
            }

            data = receiveData(server);

            string stringData = Encoding.ASCII.GetString(data);
            Console.WriteLine(stringData);

            string message1 = "Thong diep dau tien";
            string message2 = "Thong diep ngan";
            string message3 = "Day la thong diep dai hon";
            string message4 = "Xin chao, day la thong diep dai nhat";
            string message5 = "Ngan nhat";

            sent = sendData(server, Encoding.ASCII.GetBytes(message1));
            sent = sendData(server, Encoding.ASCII.GetBytes(message2));
            sent = sendData(server, Encoding.ASCII.GetBytes(message3));
            sent = sendData(server, Encoding.ASCII.GetBytes(message4));
            sent = sendData(server, Encoding.ASCII.GetBytes(message5));

            Console.WriteLine("Ngat ket noi tu Server.");
            server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();
        }
    }
}
