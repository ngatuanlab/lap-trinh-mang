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
using System.Media;
using System.Text.RegularExpressions;


namespace client
{
    public partial class client : Form
    {
        public List<string> la_bai = new List<string>();

        public IPEndPoint ipep;
        public Socket server;

        public string str;
        public byte[] data;
        public int receive_data;

        public int diem;

        PictureBox[] pictureBox = new PictureBox[14];
        Label[] label_card = new Label[14];

        public client()
        {
            InitializeComponent();

            pictureBox[0] = pictureBox1;
            pictureBox[1] = pictureBox2;
            pictureBox[2] = pictureBox3;
            pictureBox[3] = pictureBox4;
            pictureBox[4] = pictureBox5;
            pictureBox[5] = pictureBox6;
            pictureBox[6] = pictureBox7;
            pictureBox[7] = pictureBox8;
            pictureBox[8] = pictureBox9;
            pictureBox[9] = pictureBox10;
            pictureBox[10] = pictureBox11;
            pictureBox[11] = pictureBox12;

            label_card[0] = label1;
            label_card[1] = label2;
            label_card[2] = label3;
            label_card[3] = label4;
            label_card[4] = label5;
            label_card[5] = label7;
            label_card[6] = label8;
            label_card[7] = label9;
            label_card[8] = label10;
            label_card[9] = label11;
            label_card[10] = label12;
            label_card[11] = label13;

            txt_port.Text = "9050";
            txt_port.Enabled = false;
        }

        public void ket_noi_den_server()
        {
            try
            {
                

                data = new byte[1024];
                ipep = new IPEndPoint(IPAddress.Parse(txt_ip.Text), 9050);

                server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

                // Gửi tên client cho Server.
                string ten_client = txt_tenClient.Text;
                
                if (ten_client.Length>10)
                {
                    txt_console.AppendText("Ten khong qua 10 ki tu. Xin nhap lai." + Environment.NewLine);
                }
                else if (regexItem.IsMatch(ten_client.ToString())==false)
                {
                    txt_console.AppendText("Ten khong dc chua ki tu dac biet." + Environment.NewLine);
                }
                else
                {
                    data = Encoding.ASCII.GetBytes(ten_client+txt_ip.Text);
                    server.SendTo(data, data.Length, SocketFlags.None, ipep);
                    txt_console.AppendText("Da gui cho Server." + Environment.NewLine);
                    txt_console.AppendText("Xin doi trong giay lat." + Environment.NewLine);

                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint tmpRemote = (EndPoint)sender;
 

                    
                    // Nhận lời chào từ Server.
                    data = new byte[1024];
                    receive_data = server.ReceiveFrom(data, ref tmpRemote);
                    str = Encoding.ASCII.GetString(data, 0, receive_data);

                    if (str == "dadubai.")
                    {
                        txt_console.AppendText("Da du nguoi choi." + Environment.NewLine);
                        btn_ketNoi.Enabled = false;
                        btn_guiBai.Enabled = false;
                        btn_nhanBai.Enabled = false;

                    }
                    else
                    {
                        txt_console.AppendText("Loi nhan la : " + str + Environment.NewLine);
                        //btn_ketNoi.Enabled = false;
                    }

                    
                    

                }

                
            }
            catch (SocketException se)
            {
                MessageBox.Show("Không thể kết nối đến Server.");
            }
        }

        public void nhan_bai_tu_server()
        {

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)sender;
            data = new byte[1024];
            receive_data = server.ReceiveFrom(data, ref tmpRemote);
            int k = Convert.ToInt16(Encoding.ASCII.GetString(data, 0, receive_data));
            diem = k;
            txt_diem.Text = diem.ToString();

            txt_console.AppendText("Da nhan duoc so bai." + Environment.NewLine);
            // Nhận bài ngẫu nhiên từ Server.
            for (int i = 0; i < k; i++)
            {
                data = new byte[1024];
                receive_data = server.ReceiveFrom(data, ref tmpRemote);
                str = Encoding.ASCII.GetString(data, 0, receive_data);
                la_bai.Add(str);

                txt_console.AppendText("La bai nhan duoc : " + str + Environment.NewLine);

                // Đưa bài lên khung hình.
                
            }
            day_hinh_bai();


        }

        public void day_hinh_bai()
        {
            for (int i = 0; i < la_bai.Count; i++)
            {
                string str = la_bai[i];
                string line = @"..\..\image\" + str + ".bmp";
                pictureBox[i].Image = Image.FromFile(line);
                pictureBox[i].Image.Tag = str;

                label_card[i].Visible = true;
                label_card[i].Text = str.ToString();
                //txt_console.AppendText("Da dua hinh len mo ta." + Environment.NewLine);

            }
        }

        public void gui_bai_den_server()
        {
            
            data = new byte[1024];
            foreach (var bai in la_bai)
            {
                if (bai == txt_bai_gui.Text)
                {
                    data = Encoding.ASCII.GetBytes(txt_bai_gui.Text);
                    server.SendTo(data, data.Length, SocketFlags.None, ipep);
                    txt_console.AppendText("Da gui bai : " + txt_bai_gui.Text + Environment.NewLine);
                    break;
                
                }
                

            }

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)sender;

            // Nhận kết quả so sánh.
            data = new byte[1024];
            receive_data = server.ReceiveFrom(data, ref tmpRemote);
            str = Encoding.ASCII.GetString(data, 0, receive_data);

            this.txt_console.Invoke(new MethodInvoker(delegate()
            {
                txt_console.AppendText("-------------------------" + Environment.NewLine);
                txt_console.AppendText(str + Environment.NewLine);
                txt_console.AppendText("=========================" + Environment.NewLine);

            }));

            txt_diem.Clear();

            if (str == "Bai cua ban cao hon (+1 diem)")
            {
                diem = diem + 1;
            }
            else if (str == "Bai cua ban nho hon (-1 diem)")
            {
                diem = diem - 1;
            }
            else
            {
                diem = diem + 0;
            }

            txt_diem.Text = diem.ToString();
        }

        public void xoa_bai_sau_khi_gui()
        {
            int index = la_bai.IndexOf(txt_bai_gui.Text);
            la_bai.RemoveAt(index);

            foreach (var item in la_bai)
            {
                this.txt_console.Invoke(new MethodInvoker(delegate()
                {
                    txt_console.AppendText(item + Environment.NewLine);
                }));
            }

        }

        public void xoa_hinh_bai()
        {
            string line = @"..\..\image\" + "default" + ".bmp";

            for (int i = 0; i < pictureBox.Count(); i++)
            {
                try
                {
                    if (pictureBox[i].Image.Tag.ToString() == txt_bai_gui.Text)
                    {
                        pictureBox[i].Image = Image.FromFile(@"..\..\image\default.bmp");
                        pictureBox[i].Refresh();
                    }
                }
                catch
                {

                }
                
            }

            for (int i = 0; i < label_card.Count(); i++)
            {
                try
                {
                    if (label_card[i].Text == txt_bai_gui.Text)
                    {
                        label_card[i].Text = "";
                        label_card[i].Visible = false;
                        break;
                    }
                }
                catch
                {

                }
            }

            txt_bai_gui.Clear();
        }

        public void xem_ketQua()
        {
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(txt_diem.Text);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);

            // Nhận kết quả từ Server.
            data = new byte[1024];
            string str;
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)sender;

            receive_data = server.ReceiveFrom(data, ref tmpRemote);
            str = Encoding.ASCII.GetString(data, 0, receive_data);

            this.txt_console.Invoke(new MethodInvoker(delegate()
            {
                txt_console.AppendText("-------------------------" + Environment.NewLine);
                txt_console.AppendText(str + Environment.NewLine);
                txt_console.AppendText("=========================" + Environment.NewLine);

            }));

            lb_ketQua.Visible = true;
            lb_ketQua.Text = str.ToString();
        }



        # region Các nút bấm (event)

        private void btn_ketNoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_tenClient.Text))
                {
                    MessageBox.Show("Chưa nhập tên Client. Vui lòng nhập tên Client.");
                }
                else if (string.IsNullOrWhiteSpace(txt_ip.Text))
                {
                    MessageBox.Show("Chưa nhập IP. Vui lòng nhập IP.");
                }
                else if (string.IsNullOrWhiteSpace(txt_port.Text))
                {
                    MessageBox.Show("Chưa nhập Port. Vui lòng nhập Port.");
                }
                else
                {
                    ket_noi_den_server();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the ket noi den server.");
            }
            

            

        }

        private void btn_nhanBai_Click(object sender, EventArgs e)
        {
            try
            {
                nhan_bai_tu_server();
                btn_nhanBai.Enabled = false;
            }
            catch (Exception se)
            {
                MessageBox.Show("Loi.");
            }
            
        }

        # endregion

        private void btn_guiBai_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_bai_gui.Text))
                {
                    MessageBox.Show("Không có dữ liệu theo yêu cầu.");
                }
                else
                {
                    gui_bai_den_server();

                    xoa_bai_sau_khi_gui();

                    xoa_hinh_bai();

                    if (la_bai.Count == 0)
                    {
                        xem_ketQua();
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception se)
            {
                MessageBox.Show("Loi.");
            }
            

            
        }

        //private void btn_xemKetQua_Click(object sender, EventArgs e)
        //{
        //    xem_ketQua();
        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label1.Text;

            


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label2.Text;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label3.Text;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label4.Text;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label5.Text;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label9.Text;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label7.Text;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label8.Text;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label10.Text;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label11.Text;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label12.Text;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            txt_bai_gui.Clear();
            txt_bai_gui.Text = label13.Text;
        }

        //Am nhac
        //WMPLib.WindowsMediaPlayer sp = new WMPLib.WindowsMediaPlayer();
        //private void client_Load(object sender, EventArgs e)
        //{
        //    sp.URL = @"C:\Users\PC\DTDMnhombaocao18 Dropbox\Huy Hoang\PC\Desktop\lap trinh mang\Sang nho\udp_thread_card_game_Comp\client\client\kiepdoden.mp3";
        //    sp.controls.play();
        //}

        //private void sound(object sender, EventArgs e)
        //{
        //    sp.controls.stop();
        //}
    }
}
