﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Bài_02___Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            Console.WriteLine("DAY LA MAY SERVER.");

            // Giai đoạn: socket()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Giai đoạn: bind()
            newsock.Bind(ipep);
            Console.WriteLine("Dang cho ket noi cua Client ...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);

            // Nhận câu chào kết nối từ Client
            recv = newsock.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Nhan dc tin nhan tu {0}", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            // Gửi câu chào đến với Client.
            string welcome = "chao mung den voi Server cua minh.";
            data = Encoding.ASCII.GetBytes(welcome);
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);

            // Nhận nội dung Client nhập vào và gửi cho Server.
            while (true)
            {
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref Remote);

                string stringData = Encoding.ASCII.GetString(data, 0, recv);

                if (stringData == "exit all")
                {

                    Console.WriteLine("Dong ket noi Server.");
                    newsock.Close();
                    break;
                }
                else
                {
                    Console.WriteLine(stringData);
                }
            }


            

            Console.ReadLine();

        }
    }
}
