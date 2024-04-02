using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _Phân_Biệt_02__Server
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
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("DAY LA SERVER.");

            newSock.Bind(ipep);
            newSock.Listen(10);

            Console.WriteLine("Dang cho ket noi ...");

            Socket client = newSock.Accept();
            IPEndPoint newClient = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine("Ket noi tai {0} voi Port {1}", newClient.Address, newClient.Port);

            string welcome = "Chao mung";
            data = Encoding.ASCII.GetBytes(welcome);
            int sent = sendData(client, data);

            for (int i = 0; i < 5; i++)
            {
                data = receiveData(client);
                Console.WriteLine(Encoding.ASCII.GetString(data));
            }

            Console.WriteLine("Da ngat ket noi tu {0}", newClient.Address);

            client.Close();
            newSock.Close();

            Console.ReadLine();


        }
    }
}
