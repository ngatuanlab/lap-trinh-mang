using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace retryUDPclient
{
    class retryUDPclient
    {
        public byte[] data = new byte[1024];
        public static IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        public static EndPoint Remote = (EndPoint)sender;

        public int sendRCvData(Socket s, byte[] message, EndPoint rmtDevice)
        {
            int recv;
            int retry = 0;

            while (true)
            {
                Console.WriteLine("Attempt #{0}", retry);

                try
                {
                    s.SendTo(message, message.Length, SocketFlags.None, rmtDevice);
                    data = new byte[1024];
                    recv = s.ReceiveFrom(data, ref Remote);
                }
                catch (SocketException)
                {
                    recv = 0;
                }

                if (recv > 0)
                {
                    return recv;
                }
                else
                {
                    retry++;
                    if (retry > 4)
                    {
                        return 0;
                    }
                }
            }
        }

        public retryUDPclient()
        {
            string input, stringData;
            int recv;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            int socketopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
            Console.WriteLine("Default timeout: {0}", socketopt);
            server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
            socketopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
            Console.WriteLine("New timeout: {0}", socketopt);
            

            string welcome = "Hello, are you there ?";
            data = Encoding.ASCII.GetBytes(welcome);

            recv = sendRCvData(server, data, ipep);
            if (recv > 0)
            {
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            else
            {
                Console.WriteLine("Unable to communicate with remote host.");
            }

            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                recv = sendRCvData(server, Encoding.ASCII.GetBytes(input), ipep);
                if (recv > 0)
                {
                    stringData = Encoding.ASCII.GetString(data,0, recv);
                    Console.WriteLine(stringData);
                }
                else
                {
                    Console.WriteLine("did not receive an answer.");

                }


            }

            Console.WriteLine("Stopping client.");
            server.Close();
        }


        public static void Main(string[] args)
        {
            retryUDPclient ruc = new retryUDPclient();

        }
    }
}
