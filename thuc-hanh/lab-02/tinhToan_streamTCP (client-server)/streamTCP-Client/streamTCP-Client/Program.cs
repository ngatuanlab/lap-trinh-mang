using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace streamTCP_Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            string data;
            string input;

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
                Console.WriteLine("Khong ket noi server.");
            }
            catch (SocketException e)
            {
                Console.WriteLine("Khong the ket noi den may chu.");
                //Console.WriteLine(e.ToString());
            }

            try
            {
                NetworkStream ns = new NetworkStream(server);
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);

                data = sr.ReadLine();
                Console.WriteLine(data);
                
                data = sr.ReadLine();
                Console.WriteLine(data);

                // Nhập phép toán (a,b,c,d)
                
                int flag_chon = 0;

                bool stop = false;
                while (!stop)
                {
                    Console.Write("Nhap phep toan (chon a,b,c,d) : ");
                    input = Console.ReadLine();
                    if (input == "a" || input == "b" || input == "c" || input == "d")
                    {
                        sw.WriteLine(input);
                        sw.Flush();
                        stop = true;
                    }
                    else
                    {
                        Console.WriteLine("Phuong thuc khong ton tai.");
                        

                      
                    }
                }

                // Nhập (gửi) số a và b
                int flag_a = 0;

                while (flag_a == 0)
                {
                    Console.Write("Nhap so a = ");
                    input = Console.ReadLine();

                    int number;

                    if (int.TryParse(input, out number))
                    {
                        sw.WriteLine(input);
                        sw.Flush();
                        flag_a = 1;
                    }
                    else
                    {
                        Console.WriteLine("Nhap lai.");
                    }
                }

                int flag_b = 0;

                while (flag_b == 0)
                {
                    Console.Write("Nhap so b = ");
                    input = Console.ReadLine();

                    int number;

                    if (int.TryParse(input, out number))
                    {
                        sw.WriteLine(input);
                        sw.Flush();
                        flag_b = 1;
                    }
                    else
                    {
                        Console.WriteLine("Nhap lai.");
                    }
                }
                





                


                //Nhận kết quả và hiển thị
                data = sr.ReadLine();
                Console.WriteLine("Ket qua la : " + data);

                Console.WriteLine("Dong ket noi tu Server.");

                sr.Close();
                sw.Close();
                ns.Close();
            }
            catch
            {

            }
            



            //server.Shutdown(SocketShutdown.Both);
            server.Close();

            Console.ReadLine();

        }
    }
}
