using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace simpleUDP_Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            newSock.Bind(ipep);
            Console.WriteLine("Dang cho ket noi ...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(sender);

            recv = newSock.ReceiveFrom(data, ref remote);

            Console.WriteLine("Loi nhan nhan tu {0} :", remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string welcome = "Chao mung den voi Server.";
            data = Encoding.ASCII.GetBytes(welcome);
            newSock.SendTo(data, data.Length, SocketFlags.None, remote);

            

            // Gửi chương trình 
            string chuong_trinh = "Thuc hien tinh phuong trinh bac nhat: ax + b = 0;";
            data = Encoding.ASCII.GetBytes(chuong_trinh);
            newSock.SendTo(data, data.Length, SocketFlags.None, remote);

 
            while(true)
            {
                // Nhận số a và b
                int so_a, so_b;

                so_a = newSock.ReceiveFrom(data, ref remote);
                string stringData_a = Encoding.ASCII.GetString(data, 0, so_a);
                Console.WriteLine("Da nhan so a: " + stringData_a);

                so_b = newSock.ReceiveFrom(data, ref remote);
                string stringData_b = Encoding.ASCII.GetString(data, 0, so_b);
                Console.WriteLine("Da nhan so a: " + stringData_b);


                // Thực hiện phép toán
                int a = Convert.ToInt16(stringData_a);
                int b = Convert.ToInt16(stringData_b);
                decimal ketQua = decimal.Divide(-b, a);
                Console.WriteLine("Ket qua la: {0}", ketQua);

                // Gửi kết quả cho client.
                data = Encoding.ASCII.GetBytes(Convert.ToString(ketQua));
                newSock.SendTo(data, data.Length, SocketFlags.None, remote);

                recv = newSock.ReceiveFrom(data, ref remote);
                string stringData_ketThuc = Encoding.ASCII.GetString(data, 0, recv);
                if (stringData_ketThuc == "1")
                {
                    break;
                }
                
                 
            }




            Console.WriteLine("ckient da dung ket noi.");

            newSock.Close();

            Console.ReadLine();

        }
    }
}
