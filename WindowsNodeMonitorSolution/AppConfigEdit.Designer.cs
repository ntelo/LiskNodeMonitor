namespace LiskLog
{
    partial class AppConfigEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_info_test_peers = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_info_test_foldername = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.bt_copy_test = new System.Windows.Forms.Button();
            this.txt_info_test_url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_info_main_foldername = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.bt_copy_main = new System.Windows.Forms.Button();
            this.txt_info_main_url = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_info_main_peers = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_enviroment = new System.Windows.Forms.TextBox();
            this.txt_peers = new System.Windows.Forms.TextBox();
            this.txt_liskurl = new System.Windows.Forms.TextBox();
            this.txt_blocksinterval = new System.Windows.Forms.TextBox();
            this.txt_timertickms = new System.Windows.Forms.TextBox();
            this.txt_blockdiftorebuild = new System.Windows.Forms.TextBox();
            this.txt_emailto = new System.Windows.Forms.TextBox();
            this.txt_emailfrom = new System.Windows.Forms.TextBox();
            this.txt_emailpassword = new System.Windows.Forms.TextBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ck_emailnotifications = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_version_Number = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_foldername = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "peers :";
            // 
            // txt_info_test_peers
            // 
            this.txt_info_test_peers.Enabled = false;
            this.txt_info_test_peers.Location = new System.Drawing.Point(105, 28);
            this.txt_info_test_peers.Name = "txt_info_test_peers";
            this.txt_info_test_peers.Size = new System.Drawing.Size(351, 20);
            this.txt_info_test_peers.TabIndex = 1;
            this.txt_info_test_peers.Text = "13.69.159.242;40.68.34.176;52.165.40.188;13.82.31.30;13.91.61.2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_info_test_foldername);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.bt_copy_test);
            this.groupBox1.Controls.Add(this.txt_info_test_url);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_info_test_peers);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(554, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 124);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Testnet Information";
            // 
            // txt_info_test_foldername
            // 
            this.txt_info_test_foldername.Enabled = false;
            this.txt_info_test_foldername.Location = new System.Drawing.Point(353, 63);
            this.txt_info_test_foldername.Name = "txt_info_test_foldername";
            this.txt_info_test_foldername.Size = new System.Drawing.Size(103, 20);
            this.txt_info_test_foldername.TabIndex = 6;
            this.txt_info_test_foldername.Text = "lisk-test";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(265, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "liskFolderName:";
            // 
            // bt_copy_test
            // 
            this.bt_copy_test.Location = new System.Drawing.Point(352, 91);
            this.bt_copy_test.Name = "bt_copy_test";
            this.bt_copy_test.Size = new System.Drawing.Size(104, 23);
            this.bt_copy_test.TabIndex = 4;
            this.bt_copy_test.Text = "Copy Test Net";
            this.bt_copy_test.UseVisualStyleBackColor = true;
            this.bt_copy_test.Click += new System.EventHandler(this.bt_copy_test_Click);
            // 
            // txt_info_test_url
            // 
            this.txt_info_test_url.Enabled = false;
            this.txt_info_test_url.Location = new System.Drawing.Point(105, 63);
            this.txt_info_test_url.Name = "txt_info_test_url";
            this.txt_info_test_url.Size = new System.Drawing.Size(145, 20);
            this.txt_info_test_url.TabIndex = 3;
            this.txt_info_test_url.Text = "https://testnet.lisk.io";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = " url :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_info_main_foldername);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.bt_copy_main);
            this.groupBox2.Controls.Add(this.txt_info_main_url);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_info_main_peers);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(554, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 119);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mainnet Information";
            // 
            // txt_info_main_foldername
            // 
            this.txt_info_main_foldername.Enabled = false;
            this.txt_info_main_foldername.Location = new System.Drawing.Point(353, 63);
            this.txt_info_main_foldername.Name = "txt_info_main_foldername";
            this.txt_info_main_foldername.Size = new System.Drawing.Size(103, 20);
            this.txt_info_main_foldername.TabIndex = 8;
            this.txt_info_main_foldername.Text = "lisk-main";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(265, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "liskFolderName:";
            // 
            // bt_copy_main
            // 
            this.bt_copy_main.Location = new System.Drawing.Point(352, 90);
            this.bt_copy_main.Name = "bt_copy_main";
            this.bt_copy_main.Size = new System.Drawing.Size(104, 23);
            this.bt_copy_main.TabIndex = 5;
            this.bt_copy_main.Text = "Copy Main Net";
            this.bt_copy_main.UseVisualStyleBackColor = true;
            this.bt_copy_main.Click += new System.EventHandler(this.bt_copy_main_Click);
            // 
            // txt_info_main_url
            // 
            this.txt_info_main_url.Enabled = false;
            this.txt_info_main_url.Location = new System.Drawing.Point(105, 63);
            this.txt_info_main_url.Name = "txt_info_main_url";
            this.txt_info_main_url.Size = new System.Drawing.Size(145, 20);
            this.txt_info_main_url.TabIndex = 3;
            this.txt_info_main_url.Text = "https://login.lisk.io";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "url :";
            // 
            // txt_info_main_peers
            // 
            this.txt_info_main_peers.Enabled = false;
            this.txt_info_main_peers.Location = new System.Drawing.Point(105, 28);
            this.txt_info_main_peers.Name = "txt_info_main_peers";
            this.txt_info_main_peers.Size = new System.Drawing.Size(351, 20);
            this.txt_info_main_peers.TabIndex = 1;
            this.txt_info_main_peers.Text = "40.68.214.86;13.70.207.248;13.89.42.130;52.160.98.183;40.121.84.254";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = " peers :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "peers:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "liskBaseUrl:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Blocks Interval(sec):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Enviroment:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Sync Interval(minutes):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Block diff to rebuild:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "email To:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "email From:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "email Password:";
            // 
            // txt_enviroment
            // 
            this.txt_enviroment.Location = new System.Drawing.Point(106, 24);
            this.txt_enviroment.Name = "txt_enviroment";
            this.txt_enviroment.Size = new System.Drawing.Size(200, 20);
            this.txt_enviroment.TabIndex = 13;
            // 
            // txt_peers
            // 
            this.txt_peers.Location = new System.Drawing.Point(106, 61);
            this.txt_peers.Name = "txt_peers";
            this.txt_peers.Size = new System.Drawing.Size(406, 20);
            this.txt_peers.TabIndex = 14;
            // 
            // txt_liskurl
            // 
            this.txt_liskurl.Location = new System.Drawing.Point(106, 96);
            this.txt_liskurl.Name = "txt_liskurl";
            this.txt_liskurl.Size = new System.Drawing.Size(200, 20);
            this.txt_liskurl.TabIndex = 15;
            // 
            // txt_blocksinterval
            // 
            this.txt_blocksinterval.Location = new System.Drawing.Point(129, 25);
            this.txt_blocksinterval.Name = "txt_blocksinterval";
            this.txt_blocksinterval.Size = new System.Drawing.Size(73, 20);
            this.txt_blocksinterval.TabIndex = 16;
            this.txt_blocksinterval.Text = "10";
            // 
            // txt_timertickms
            // 
            this.txt_timertickms.Location = new System.Drawing.Point(131, 60);
            this.txt_timertickms.Name = "txt_timertickms";
            this.txt_timertickms.Size = new System.Drawing.Size(73, 20);
            this.txt_timertickms.TabIndex = 17;
            this.txt_timertickms.Text = "1";
            // 
            // txt_blockdiftorebuild
            // 
            this.txt_blockdiftorebuild.Location = new System.Drawing.Point(131, 95);
            this.txt_blockdiftorebuild.Name = "txt_blockdiftorebuild";
            this.txt_blockdiftorebuild.Size = new System.Drawing.Size(73, 20);
            this.txt_blockdiftorebuild.TabIndex = 18;
            this.txt_blockdiftorebuild.Text = "12";
            // 
            // txt_emailto
            // 
            this.txt_emailto.Location = new System.Drawing.Point(95, 23);
            this.txt_emailto.Name = "txt_emailto";
            this.txt_emailto.Size = new System.Drawing.Size(181, 20);
            this.txt_emailto.TabIndex = 19;
            // 
            // txt_emailfrom
            // 
            this.txt_emailfrom.Location = new System.Drawing.Point(95, 58);
            this.txt_emailfrom.Name = "txt_emailfrom";
            this.txt_emailfrom.Size = new System.Drawing.Size(181, 20);
            this.txt_emailfrom.TabIndex = 20;
            // 
            // txt_emailpassword
            // 
            this.txt_emailpassword.Location = new System.Drawing.Point(97, 90);
            this.txt_emailpassword.Name = "txt_emailpassword";
            this.txt_emailpassword.PasswordChar = '*';
            this.txt_emailpassword.Size = new System.Drawing.Size(179, 20);
            this.txt_emailpassword.TabIndex = 21;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(962, 316);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 22;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(12, 316);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(75, 23);
            this.bt_close.TabIndex = 23;
            this.bt_close.Text = "Close";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ck_emailnotifications);
            this.groupBox3.Controls.Add(this.txt_emailto);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txt_emailpassword);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txt_emailfrom);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(248, 162);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(291, 143);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gmail Configuration";
            // 
            // ck_emailnotifications
            // 
            this.ck_emailnotifications.AutoSize = true;
            this.ck_emailnotifications.Location = new System.Drawing.Point(97, 120);
            this.ck_emailnotifications.Name = "ck_emailnotifications";
            this.ck_emailnotifications.Size = new System.Drawing.Size(144, 17);
            this.ck_emailnotifications.TabIndex = 22;
            this.ck_emailnotifications.Text = "enable email notifications";
            this.ck_emailnotifications.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_version_Number);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.txt_foldername);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txt_enviroment);
            this.groupBox4.Controls.Add(this.txt_peers);
            this.groupBox4.Controls.Add(this.txt_liskurl);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(527, 139);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Enviroment (test or main)";
            // 
            // txt_version_Number
            // 
            this.txt_version_Number.Location = new System.Drawing.Point(409, 98);
            this.txt_version_Number.Name = "txt_version_Number";
            this.txt_version_Number.Size = new System.Drawing.Size(103, 20);
            this.txt_version_Number.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(321, 101);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "liskVersion:";
            // 
            // txt_foldername
            // 
            this.txt_foldername.Location = new System.Drawing.Point(409, 24);
            this.txt_foldername.Name = "txt_foldername";
            this.txt_foldername.Size = new System.Drawing.Size(103, 20);
            this.txt_foldername.TabIndex = 17;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(321, 27);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "liskFolderName:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txt_blocksinterval);
            this.groupBox5.Controls.Add(this.txt_blockdiftorebuild);
            this.groupBox5.Controls.Add(this.txt_timertickms);
            this.groupBox5.Location = new System.Drawing.Point(12, 162);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(224, 143);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Monitor Parameters";
            // 
            // AppConfigEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 351);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.bt_close);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "AppConfigEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppConfig";
            this.Load += new System.EventHandler(this.AppConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_info_test_peers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_info_test_url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_info_main_url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_info_main_peers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_enviroment;
        private System.Windows.Forms.TextBox txt_peers;
        private System.Windows.Forms.TextBox txt_liskurl;
        private System.Windows.Forms.TextBox txt_blocksinterval;
        private System.Windows.Forms.TextBox txt_timertickms;
        private System.Windows.Forms.TextBox txt_blockdiftorebuild;
        private System.Windows.Forms.TextBox txt_emailto;
        private System.Windows.Forms.TextBox txt_emailfrom;
        private System.Windows.Forms.TextBox txt_emailpassword;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button bt_copy_test;
        private System.Windows.Forms.Button bt_copy_main;
        private System.Windows.Forms.CheckBox ck_emailnotifications;
        private System.Windows.Forms.TextBox txt_info_test_foldername;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_info_main_foldername;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_foldername;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_version_Number;
        private System.Windows.Forms.Label label17;
    }
}