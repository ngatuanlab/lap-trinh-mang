using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace simpleUDP_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData, so_a, so_b;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            string welcome = "xin chao, ban co o do khong ?";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)sender;

            try
            {
                server.Connect(ipep);
                Console.WriteLine("Da nhan ket noi tu Server.");

                data = new byte[1024];
                int recv = server.ReceiveFrom(data, ref remote);

                Console.WriteLine("Tin nhan duoc nhan tu {0} :", remote.ToString());
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));



                // Nhận chương trình 
                recv = server.ReceiveFrom(data, ref remote);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);

                while (true)
                {
                    Console.WriteLine("========================================================");


                    // Gửi số a và b
                    while (true)
                    {
                        int number_a;

                        Console.Write("Nhap so a : ");
                        so_a = Console.ReadLine();
                        if (so_a == string.Empty)
                        {
                            Console.WriteLine("So nhap khong duoc de trong.");
                        }
                        else if (int.TryParse(so_a, out number_a) == false)
                        {
                            Console.WriteLine("Khong duoc nhap chu.");
                        }


                        else
                        {
                            server.SendTo(Encoding.ASCII.GetBytes(so_a), remote);
                            break;
                        }
                    }


                    while (true)
                    {
                        int number_b;

                        Console.Write("Nhap so b : ");
                        so_b = Console.ReadLine();
                        if (so_b == string.Empty)
                        {
                            Console.WriteLine("So nhap khong duoc de trong.");
                        }
                        else if (int.TryParse(so_b, out number_b) == false)
                        {
                            Console.WriteLine("Khong duoc nhap chu.");
                        }


                        else
                        {
                            server.SendTo(Encoding.ASCII.GetBytes(so_b), remote);
                            break;
                        }
                    }

                    Console.WriteLine("Phuong trinh vua nhap can tinh: {0}x+{1}=0", so_a, so_b);

                    // Nhận kết quả:
                    int ketQua;
                    ketQua = server.ReceiveFrom(data, ref remote);
                    string stringData_ketQua = Encoding.ASCII.GetString(data, 0, ketQua);
                    Console.WriteLine("Ket qua la: " + stringData_ketQua);

                    Console.Write("Chon phuong an: " + "\n" + "1.KetThuc" + "\n" + "!=1.TiepTuc" + "\n");

                    int flag_ketThuc = 0;
                    int flag_while_big = 0;

                    while (flag_ketThuc == 0)
                    {
                        Console.Write("Ban chon la: ");
                        input = Console.ReadLine();

                        int number;

                        if (int.TryParse(input, out number))
                        {
                            server.SendTo(Encoding.ASCII.GetBytes(input), remote);

                            flag_ketThuc = 1;
                        }
                        else
                        {
                            Console.WriteLine("Khong duoc nhap chu.");
                        }

                        if (input == "1")
                        {
                            flag_while_big = 1;
                            break;
                        }

                        
                    }

                    if (flag_while_big == 1)
                    {
                        break;
                    }

                    


                    

                    
                    






                }

            }
            catch
            {
                Console.WriteLine("Khong the ket noi den server.");
            }


            



            Console.WriteLine("Dung ket noi ...");
            server.Close();

            Console.ReadLine();

        }
    }
}
