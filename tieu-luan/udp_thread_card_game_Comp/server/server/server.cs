using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace server
{
    public partial class server : Form
    {
        public List<EndPoint> client_ep_ban_dau = new List<EndPoint>();
        public List<string> ten_client = new List<string>();
        public List<string> con_bai = new List<string>();
        public List<string> conBai_clientEP = new List<string>();
        public List<EndPoint> clientEP_soSanh = new List<EndPoint>();

        public List<string> ketqua_clientEP = new List<string>();
        public List<EndPoint> ketqua_clientEP_sosanh = new List<EndPoint>();

        public List<string> yeu_cau_choi_lai = new List<string>();


        public Socket newSock;
        public IPEndPoint ipep;

        public List<string> diem_clientEP = new List<string>();

        public int so_bai_gui = 0;



        // Theo quy luật A<B<C<D
        public List<string> bo_bai = new List<string>() { 
        "1A","2A","3A","4A","5A","6A","7A","8A","9A","10A","JA","QA","KA",
        "1B","2B","3B","4B","5B","6B","7B","8B","9B","10B","JB","QB","KB",
        "1C","2C","3C","4C","5C","6C","7C","8C","9C","10C","JC","QC","KC",
        "1D","2D","3D","4D","5D","6D","7D","8D","9D","10D","JD","QD","KD",
        
        };
        


        public server()
        {
            InitializeComponent();
        }

        # region Quá trình client kết nối.
        public void bat_dau()
        {
            Thread main_thread = new Thread(new ThreadStart(ket_noi));
            main_thread.Start();


            
        }

        public void ket_noi()
        {
            ipep = new IPEndPoint(IPAddress.Any, 9050);
            newSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            newSock.Bind(ipep);
            this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
            {
                txt_consoleLog.AppendText("Dang cho ket noi ..." + Environment.NewLine);
            }));


            

            for (int i = 0; i < 2; i++)
            {
                Thread client_thread = new Thread(new ThreadStart(client_ket_noi));
                client_thread.Start();
            }


        }

        public void client_ket_noi()
        {
            int receive_data_length;
            string str;
            byte[] data = new byte[1024];

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)(sender);

            // Nhận tên đăng nhập của client.
            receive_data_length = newSock.ReceiveFrom(data, ref tmpRemote);
            client_ep_ban_dau.Add(tmpRemote);

            str = Encoding.ASCII.GetString(data, 0, receive_data_length);
            ten_client.Add(str);

            //Dang ky lai
            foreach (var item in ten_client)
            {
                if (ten_client.Count == 2)
                {
                    if (ten_client[1] == ten_client[0])
                    {

                        data = Encoding.ASCII.GetBytes("ten trung nhap lai ten");

                        newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[1]);

                        int index = ten_client.IndexOf(ten_client[1]);
                        ten_client.RemoveAt(index);
                        client_ep_ban_dau.Remove(client_ep_ban_dau[1]);

                        client_ket_noi();
                        break;
                    }

                 }
            }
            

            // Đẩy thông báo lên console.
            this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
            {
                txt_consoleLog.AppendText("Loi nhan tu : " + tmpRemote.ToString() + Environment.NewLine);
            }));

            this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
            {
                txt_consoleLog.AppendText("Loi nhan la : " + str + Environment.NewLine);

            }));

            // Gửi câu chào cho client
            if (client_ep_ban_dau.Count==2)
            {
                string welcome = "Welcome to my Server.";
                data = Encoding.ASCII.GetBytes(welcome);
                newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[0]);
                newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[1]);
            }
            
            
            if (ten_client.Count==2)
            {
                this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                {
                    txt_dsClient.Clear();
                    txt_dsClient.AppendText(ten_client[0].ToString() + "-" + client_ep_ban_dau[0] + Environment.NewLine);
                    txt_dsClient.AppendText(ten_client[1].ToString() + "-" + client_ep_ban_dau[1] + Environment.NewLine);

                }));
            }
            
            // Chia bài + so sánh + gửi kết quả (sau khi đủ client joint).

            if (client_ep_ban_dau.Count==2)
            {
                chia_bai_ngau_nhien();

                this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                {
                    txt_consoleLog.AppendText("Bat dau nhan bai." + Environment.NewLine);
                }));

                for (int i = 0; i < so_bai_gui; i++)
                {
                    nhan_bai_tu_client();
                }


                nhan_so_sanh_ketQua();
            }


            


            
        }
        # endregion

        # region Quá trình chia bài.
        public void chia_bai_ngau_nhien()
        {
            int receive_data_length;
            string str;
            byte[] data = new byte[1024];

            Random rnd = new Random();
            so_bai_gui = rnd.Next(3, 12);
            data = Encoding.ASCII.GetBytes(Convert.ToString(so_bai_gui));
            newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[0]);

            data = Encoding.ASCII.GetBytes(Convert.ToString(so_bai_gui));
            newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[1]);

            for (int i = 0; i < so_bai_gui; i++)
            {
                int vi_tri = rnd.Next(1,bo_bai.Count);
                data = Encoding.ASCII.GetBytes(bo_bai[vi_tri]);
                newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[0]);
                bo_bai.Remove(bo_bai[vi_tri]);
                
            }

            for (int i = 0; i < so_bai_gui; i++)
            {
                int vi_tri = rnd.Next(1, bo_bai.Count);
                data = Encoding.ASCII.GetBytes(bo_bai[vi_tri]);
                newSock.SendTo(data, data.Length, SocketFlags.None, client_ep_ban_dau[1]);
                bo_bai.Remove(bo_bai[vi_tri]);

            }

            this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
            {
                txt_consoleLog.AppendText("Da chia bai cho client : " + client_ep_ban_dau[0] + Environment.NewLine);
                txt_consoleLog.AppendText("Da chia bai cho client : " + client_ep_ban_dau[1] + Environment.NewLine);
                

            }));
        }
        # endregion


        public void nhan_bai_tu_client()
        {
            int receive_data_length;
            string str;
            byte[] data = new byte[1024];

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)(sender);

            // Nhận số từ Client
            while (true)
            {
                if (con_bai.Count == 2)
                {
                    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                    {
                        txt_consoleLog.AppendText("Da nhan du bai tu Client." + Environment.NewLine);
                    }));
                    break;
                }
                else
                {
                    receive_data_length = newSock.ReceiveFrom(data, ref tmpRemote);
                    str = Encoding.ASCII.GetString(data, 0, receive_data_length);

                    if (str.Length>3)
                    {
                        data = Encoding.ASCII.GetBytes("dadubai.");
                        newSock.SendTo(data, data.Length, SocketFlags.None, tmpRemote);
                        this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                        {
                            txt_consoleLog.AppendText("co client 03 dang nhap." + Environment.NewLine);
                        }));

                    }
                    else
                    {
                        con_bai.Add(str);
                        conBai_clientEP.Add(str);
                        clientEP_soSanh.Add(tmpRemote);



                        this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                        {
                            txt_consoleLog.AppendText("Nhan duoc bai : " + str + " tu - " + tmpRemote.ToString() + Environment.NewLine);
                        }));

                    }
                    
                }
            }


            this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
            {
                txt_consoleLog.AppendText(conBai_clientEP[0] + Environment.NewLine);
                txt_consoleLog.AppendText(conBai_clientEP[1] + Environment.NewLine);
            }));



            so_sanh_bai();

        }

        public bool kiemTraBaiGui(string str)
        {
            for (int i = 0; i < bo_bai.Count; i++)
            {
                if(str==bo_bai[i].ToString())
                {
                    
                    return true;
                    break;
                }
            }

            return false;
        }

        public void so_sanh_bai()
        {

            // Chuyển lá bài nhận được thành ASCII.
            List<int> bai_ascii_0 = new List<int>();
            List<int> bai_ascii_1 = new List<int>();


            foreach (var c in con_bai[0])
            {
                bai_ascii_0.Add((int)c);
            }

            foreach (var c in con_bai[1])
            {
                bai_ascii_1.Add((int)c);
            }

            string bai_ascii_0_joint = string.Join("", bai_ascii_0.ToArray());
            string bai_ascii_1_joint = string.Join("", bai_ascii_1.ToArray());



            this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
            {
                txt_consoleLog.AppendText(bai_ascii_0_joint + Environment.NewLine);
                txt_consoleLog.AppendText(bai_ascii_1_joint + Environment.NewLine);

            }));

            // So sánh lá bài (dạng số).
            byte[] data = new byte[1024];
            int int_bai_01 = Convert.ToInt32(bai_ascii_0_joint);
            int int_bai_02 = Convert.ToInt32(bai_ascii_1_joint);



            if (int_bai_01>int_bai_02)
            {
                data = Encoding.ASCII.GetBytes("Bai cua ban cao hon (+1 diem)");
                newSock.SendTo(data, data.Length, SocketFlags.None, clientEP_soSanh[0]);

                data = Encoding.ASCII.GetBytes("Bai cua ban nho hon (-1 diem)");
                newSock.SendTo(data, data.Length, SocketFlags.None, clientEP_soSanh[1]);

                diem_clientEP.Add(clientEP_soSanh[0] + "+1");
                diem_clientEP.Add(clientEP_soSanh[1] + "-1");

                foreach (var item in diem_clientEP)
                {
                    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                    {
                        txt_consoleLog.AppendText(item + Environment.NewLine);

                    }));
                }
                
            }

            if (int_bai_01 < int_bai_02)
            {
                data = Encoding.ASCII.GetBytes("Bai cua ban nho hon (-1 diem)");
                newSock.SendTo(data, data.Length, SocketFlags.None, clientEP_soSanh[0]);


                data = Encoding.ASCII.GetBytes("Bai cua ban cao hon (+1 diem)");
                newSock.SendTo(data, data.Length, SocketFlags.None, clientEP_soSanh[1]);

                diem_clientEP.Add(clientEP_soSanh[1] + " +1 diem");
                diem_clientEP.Add(clientEP_soSanh[0] + " -1 diem");

                foreach (var item in diem_clientEP)
                {
                    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                    {
                        txt_consoleLog.AppendText(item + Environment.NewLine);

                    }));
                }
            }

            if (int_bai_01 == int_bai_02)
            {
                data = Encoding.ASCII.GetBytes("Bai cua ban bang nhau.");
                newSock.SendTo(data, data.Length, SocketFlags.None, clientEP_soSanh[0]);

                data = Encoding.ASCII.GetBytes("Bai cua ban bang nhau.");
                newSock.SendTo(data, data.Length, SocketFlags.None, clientEP_soSanh[1]);

                foreach (var item in diem_clientEP)
                {
                    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                    {
                        txt_consoleLog.AppendText(item + Environment.NewLine);

                    }));
                }

            }

            con_bai.Clear();
            clientEP_soSanh.Clear();
            conBai_clientEP.Clear();



        }



        public void nhan_so_sanh_ketQua()
        {
            int receive_data_length;
            string str;
            byte[] data = new byte[1024];

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)(sender);

            while (true)
            {
                if (ketqua_clientEP.Count == 2)
                {
                    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                    {
                        txt_consoleLog.AppendText("Da nhan du ket qua tu 2 Client." + Environment.NewLine);
                    }));
                    break;
                }
                else
                {
                    receive_data_length = newSock.ReceiveFrom(data, ref tmpRemote);
                    str = Encoding.ASCII.GetString(data, 0, receive_data_length);
                    ketqua_clientEP.Add(str);
                    ketqua_clientEP_sosanh.Add(tmpRemote);



                    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
                    {
                        txt_consoleLog.AppendText("Tong diem cua " + tmpRemote.ToString() + " la: " + str + Environment.NewLine);
                    }));
                }
            }

            // Quá trình so sánh điểm của 2 client.
            if (Convert.ToInt16(ketqua_clientEP[0]) > Convert.ToInt16(ketqua_clientEP[1]))
            {
                data = Encoding.ASCII.GetBytes("Tong diem cua doi thu: " + ketqua_clientEP[1] + " point\n" + " Ban chien thang.");
                newSock.SendTo(data, data.Length, SocketFlags.None, ketqua_clientEP_sosanh[0]);

                data = Encoding.ASCII.GetBytes("Tong diem cua doi thu: " + ketqua_clientEP[0] + " point\n" + " Ban thua cuoc.");
                newSock.SendTo(data, data.Length, SocketFlags.None, ketqua_clientEP_sosanh[1]);
            }

            if (Convert.ToInt16(ketqua_clientEP[0]) < Convert.ToInt16(ketqua_clientEP[1]))
            {
                data = Encoding.ASCII.GetBytes("Tong diem cua doi thu: " + ketqua_clientEP[1] + " point\n" + " Ban thua cuoc.");
                newSock.SendTo(data, data.Length, SocketFlags.None, ketqua_clientEP_sosanh[0]);

                data = Encoding.ASCII.GetBytes("Tong diem cua doi thu: " + ketqua_clientEP[0] + " point\n" + " Ban chien thang.");
                newSock.SendTo(data, data.Length, SocketFlags.None, ketqua_clientEP_sosanh[1]);
            }

            if (Convert.ToInt16(ketqua_clientEP[0]) == Convert.ToInt16(ketqua_clientEP[1]))
            {
                data = Encoding.ASCII.GetBytes("Tong diem cua doi thu: " + ketqua_clientEP[1] + " point\n" + " Ban hoa nhau.");
                newSock.SendTo(data, data.Length, SocketFlags.None, ketqua_clientEP_sosanh[0]);

                data = Encoding.ASCII.GetBytes("Tong diem cua doi thu: " + ketqua_clientEP[1] + " point\n" + " Ban hoa nhau.");
                newSock.SendTo(data, data.Length, SocketFlags.None, ketqua_clientEP_sosanh[1]);
            }
            Console.ReadLine();
        }

        


        # region Các nút bấm (event)

        private void btn_batdauServer_Click(object sender, EventArgs e)
        {
            
            bat_dau();

           


           
            
        }

        # endregion

        //private void btn_chiaBai_Click(object sender, EventArgs e)
        //{
        //    chia_bai_ngau_nhien();

        //    this.txt_consoleLog.Invoke(new MethodInvoker(delegate()
        //    {
        //        txt_consoleLog.AppendText("Bat dau nhan bai." + Environment.NewLine);
        //    }));

        //    for (int i = 0; i < so_bai_gui; i++)
        //    {
        //        nhan_bai_tu_client();
        //    }

        //    nhan_so_sanh_ketQua();



        }

        

    }

