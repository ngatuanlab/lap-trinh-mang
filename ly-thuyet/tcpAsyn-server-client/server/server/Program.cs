using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace server
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "tcp server.";
            var listener = new TcpListener(IPAddress.Any, 9050);
            listener.Start();
            listener.BeginAcceptSocket(AcceptCallBack, listener);
            // Dừng màn hình.
            Console.ReadLine();


        }

        public static readonly int _size = 1024;
        public static readonly byte[] _buffer = new byte[_size];
        public static void AcceptCallBack(IAsyncResult ar)
        {
            var listener = ar.AsyncState as TcpListener;
            listener.BeginAcceptSocket(AcceptCallBack, listener);
            var socket = listener.EndAcceptSocket(ar);
            socket.BeginReceive(_buffer, 0, _size, SocketFlags.None, ReceiveCallBack, socket);

        }

        public static void ReceiveCallBack(IAsyncResult ar)
        {
            var socket = ar.AsyncState as Socket;
            int count = socket.EndReceive(ar);
            var request = Encoding.ASCII.GetString(_buffer, 0, count);
            Console.WriteLine("Received: {0}", request);
            var response = request.ToUpper();
            var buffer = Encoding.ASCII.GetBytes(response);
            socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, socket);

        }

        public static void SendCallBack(IAsyncResult ar)
        {
            var socket = ar.AsyncState as Socket;
            int count = socket.EndSend(ar);
            Console.WriteLine("{0} bytes have byte sent to client.", count);
        }

    }
}
