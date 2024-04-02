namespace server
{
    partial class server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gb_consoleLog = new System.Windows.Forms.GroupBox();
            this.txt_consoleLog = new System.Windows.Forms.TextBox();
            this.gb_dsClient = new System.Windows.Forms.GroupBox();
            this.txt_dsClient = new System.Windows.Forms.TextBox();
            this.btn_batdauServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gb_consoleLog.SuspendLayout();
            this.gb_dsClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_consoleLog
            // 
            this.gb_consoleLog.Controls.Add(this.txt_consoleLog);
            this.gb_consoleLog.Location = new System.Drawing.Point(12, 65);
            this.gb_consoleLog.Name = "gb_consoleLog";
            this.gb_consoleLog.Size = new System.Drawing.Size(459, 237);
            this.gb_consoleLog.TabIndex = 0;
            this.gb_consoleLog.TabStop = false;
            this.gb_consoleLog.Text = "Console Log";
            // 
            // txt_consoleLog
            // 
            this.txt_consoleLog.Location = new System.Drawing.Point(6, 19);
            this.txt_consoleLog.Multiline = true;
            this.txt_consoleLog.Name = "txt_consoleLog";
            this.txt_consoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_consoleLog.Size = new System.Drawing.Size(447, 212);
            this.txt_consoleLog.TabIndex = 0;
            // 
            // gb_dsClient
            // 
            this.gb_dsClient.Controls.Add(this.txt_dsClient);
            this.gb_dsClient.Location = new System.Drawing.Point(477, 65);
            this.gb_dsClient.Name = "gb_dsClient";
            this.gb_dsClient.Size = new System.Drawing.Size(200, 237);
            this.gb_dsClient.TabIndex = 1;
            this.gb_dsClient.TabStop = false;
            this.gb_dsClient.Text = "Danh Sách Client";
            // 
            // txt_dsClient
            // 
            this.txt_dsClient.Location = new System.Drawing.Point(6, 19);
            this.txt_dsClient.Multiline = true;
            this.txt_dsClient.Name = "txt_dsClient";
            this.txt_dsClient.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_dsClient.Size = new System.Drawing.Size(188, 212);
            this.txt_dsClient.TabIndex = 1;
            // 
            // btn_batdauServer
            // 
            this.btn_batdauServer.Location = new System.Drawing.Point(12, 308);
            this.btn_batdauServer.Name = "btn_batdauServer";
            this.btn_batdauServer.Size = new System.Drawing.Size(75, 53);
            this.btn_batdauServer.TabIndex = 2;
            this.btn_batdauServer.Text = "Bắt Đầu Server";
            this.btn_batdauServer.UseVisualStyleBackColor = true;
            this.btn_batdauServer.Click += new System.EventHandler(this.btn_batdauServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(115, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(445, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "SERVER - TRÒ CHƠI ĐÁNH BÀI";
            // 
            // server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 377);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_batdauServer);
            this.Controls.Add(this.gb_dsClient);
            this.Controls.Add(this.gb_consoleLog);
            this.Name = "server";
            this.Text = "Server";
            this.gb_consoleLog.ResumeLayout(false);
            this.gb_consoleLog.PerformLayout();
            this.gb_dsClient.ResumeLayout(false);
            this.gb_dsClient.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_consoleLog;
        private System.Windows.Forms.GroupBox gb_dsClient;
        private System.Windows.Forms.Button btn_batdauServer;
        private System.Windows.Forms.TextBox txt_consoleLog;
        private System.Windows.Forms.TextBox txt_dsClient;
        private System.Windows.Forms.Label label1;
    }
}

