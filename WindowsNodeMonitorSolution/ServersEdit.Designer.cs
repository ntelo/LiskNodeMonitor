namespace LiskLog
{
    partial class ServersEdit
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
            this.ck_enable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_serverName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_liskUser = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_liskpassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ck_EnableRebuild = new System.Windows.Forms.CheckBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ck_mainserver = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lb_blockdiff = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lb_chainSynced = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ck_enable
            // 
            this.ck_enable.AutoSize = true;
            this.ck_enable.Checked = true;
            this.ck_enable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_enable.Location = new System.Drawing.Point(922, 57);
            this.ck_enable.Name = "ck_enable";
            this.ck_enable.Size = new System.Drawing.Size(15, 14);
            this.ck_enable.TabIndex = 0;
            this.ck_enable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(834, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Monitor Server?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server Name";
            // 
            // txt_serverName
            // 
            this.txt_serverName.Location = new System.Drawing.Point(81, 22);
            this.txt_serverName.Name = "txt_serverName";
            this.txt_serverName.Size = new System.Drawing.Size(181, 20);
            this.txt_serverName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(276, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Server IP";
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(347, 23);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(181, 20);
            this.txt_ip.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(542, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Server Port";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(607, 24);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(138, 20);
            this.txt_port.TabIndex = 7;
            this.txt_port.Text = "8000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Lisk User";
            // 
            // txt_liskUser
            // 
            this.txt_liskUser.Location = new System.Drawing.Point(81, 58);
            this.txt_liskUser.Name = "txt_liskUser";
            this.txt_liskUser.Size = new System.Drawing.Size(181, 20);
            this.txt_liskUser.TabIndex = 9;
            this.txt_liskUser.Text = "lisk";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Lisk Password";
            // 
            // txt_liskpassword
            // 
            this.txt_liskpassword.Location = new System.Drawing.Point(347, 61);
            this.txt_liskpassword.Name = "txt_liskpassword";
            this.txt_liskpassword.PasswordChar = '*';
            this.txt_liskpassword.Size = new System.Drawing.Size(181, 20);
            this.txt_liskpassword.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(834, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Enable rebuild?";
            // 
            // ck_EnableRebuild
            // 
            this.ck_EnableRebuild.AutoSize = true;
            this.ck_EnableRebuild.Checked = true;
            this.ck_EnableRebuild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_EnableRebuild.Location = new System.Drawing.Point(922, 96);
            this.ck_EnableRebuild.Name = "ck_EnableRebuild";
            this.ck_EnableRebuild.Size = new System.Drawing.Size(15, 14);
            this.ck_EnableRebuild.TabIndex = 16;
            this.ck_EnableRebuild.UseVisualStyleBackColor = true;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(850, 173);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 20;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(13, 173);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(75, 23);
            this.bt_close.TabIndex = 21;
            this.bt_close.Text = "Close";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(834, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Main Server?";
            // 
            // ck_mainserver
            // 
            this.ck_mainserver.AutoSize = true;
            this.ck_mainserver.Location = new System.Drawing.Point(922, 22);
            this.ck_mainserver.Name = "ck_mainserver";
            this.ck_mainserver.Size = new System.Drawing.Size(15, 14);
            this.ck_mainserver.TabIndex = 23;
            this.ck_mainserver.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(100, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Block Diff From Peers";
            // 
            // lb_blockdiff
            // 
            this.lb_blockdiff.AutoSize = true;
            this.lb_blockdiff.ForeColor = System.Drawing.Color.Red;
            this.lb_blockdiff.Location = new System.Drawing.Point(215, 103);
            this.lb_blockdiff.Name = "lb_blockdiff";
            this.lb_blockdiff.Size = new System.Drawing.Size(13, 13);
            this.lb_blockdiff.TabIndex = 25;
            this.lb_blockdiff.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(344, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Is Chain Synced?";
            // 
            // lb_chainSynced
            // 
            this.lb_chainSynced.AutoSize = true;
            this.lb_chainSynced.ForeColor = System.Drawing.Color.Red;
            this.lb_chainSynced.Location = new System.Drawing.Point(458, 103);
            this.lb_chainSynced.Name = "lb_chainSynced";
            this.lb_chainSynced.Size = new System.Drawing.Size(13, 13);
            this.lb_chainSynced.TabIndex = 27;
            this.lb_chainSynced.Text = "0";
            // 
            // ServersEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 210);
            this.ControlBox = false;
            this.Controls.Add(this.lb_chainSynced);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lb_blockdiff);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ck_mainserver);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bt_close);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ck_EnableRebuild);
            this.Controls.Add(this.txt_liskpassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_liskUser);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_serverName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ck_enable);
            this.Name = "ServersEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServersEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ck_enable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_serverName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_liskUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_liskpassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ck_EnableRebuild;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ck_mainserver;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lb_blockdiff;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lb_chainSynced;
    }
}