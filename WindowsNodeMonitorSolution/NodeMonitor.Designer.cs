namespace LiskLog
{
    partial class NodeMonitor
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
            this.bt_rebuild = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_peers = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_monitornode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_ticks = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_numbertickes = new System.Windows.Forms.Label();
            this.txt_logs = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lb_blocktorebuild = new System.Windows.Forms.Label();
            this.lb_laststart = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ck_allowrebuild = new System.Windows.Forms.CheckBox();
            this.ck_enableMonitor = new System.Windows.Forms.CheckBox();
            this.bt_accounts = new System.Windows.Forms.Button();
            this.bt_settings = new System.Windows.Forms.Button();
            this.bt_reisntall = new System.Windows.Forms.Button();
            this.lb_liskurl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_rebuild
            // 
            this.bt_rebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_rebuild.Location = new System.Drawing.Point(745, 100);
            this.bt_rebuild.Name = "bt_rebuild";
            this.bt_rebuild.Size = new System.Drawing.Size(185, 83);
            this.bt_rebuild.TabIndex = 0;
            this.bt_rebuild.Text = "Run Monitor";
            this.bt_rebuild.UseVisualStyleBackColor = true;
            this.bt_rebuild.Click += new System.EventHandler(this.bt_rebuild_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "peers";
            // 
            // lb_peers
            // 
            this.lb_peers.AutoSize = true;
            this.lb_peers.Location = new System.Drawing.Point(3, 27);
            this.lb_peers.Name = "lb_peers";
            this.lb_peers.Size = new System.Drawing.Size(33, 13);
            this.lb_peers.TabIndex = 2;
            this.lb_peers.Text = "peers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "monitor node";
            // 
            // lb_monitornode
            // 
            this.lb_monitornode.AutoSize = true;
            this.lb_monitornode.Location = new System.Drawing.Point(3, 147);
            this.lb_monitornode.Name = "lb_monitornode";
            this.lb_monitornode.Size = new System.Drawing.Size(41, 13);
            this.lb_monitornode.TabIndex = 4;
            this.lb_monitornode.Text = "servers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "pooling interval(min)";
            // 
            // lb_ticks
            // 
            this.lb_ticks.AutoSize = true;
            this.lb_ticks.Location = new System.Drawing.Point(3, 299);
            this.lb_ticks.Name = "lb_ticks";
            this.lb_ticks.Size = new System.Drawing.Size(43, 13);
            this.lb_ticks.TabIndex = 6;
            this.lb_ticks.Text = "lb_ticks";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "number pooling ticks made";
            // 
            // lb_numbertickes
            // 
            this.lb_numbertickes.AutoSize = true;
            this.lb_numbertickes.Location = new System.Drawing.Point(3, 349);
            this.lb_numbertickes.Name = "lb_numbertickes";
            this.lb_numbertickes.Size = new System.Drawing.Size(84, 13);
            this.lb_numbertickes.TabIndex = 8;
            this.lb_numbertickes.Text = "lb_numbertickes";
            // 
            // txt_logs
            // 
            this.txt_logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_logs.Location = new System.Drawing.Point(182, 9);
            this.txt_logs.Multiline = true;
            this.txt_logs.Name = "txt_logs";
            this.txt_logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_logs.Size = new System.Drawing.Size(555, 603);
            this.txt_logs.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(744, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_blocktorebuild
            // 
            this.lb_blocktorebuild.AutoSize = true;
            this.lb_blocktorebuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_blocktorebuild.Location = new System.Drawing.Point(3, 509);
            this.lb_blocktorebuild.Name = "lb_blocktorebuild";
            this.lb_blocktorebuild.Size = new System.Drawing.Size(104, 13);
            this.lb_blocktorebuild.TabIndex = 14;
            this.lb_blocktorebuild.Text = "lb_blocktorebuild";
            // 
            // lb_laststart
            // 
            this.lb_laststart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_laststart.AutoSize = true;
            this.lb_laststart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_laststart.ForeColor = System.Drawing.Color.Red;
            this.lb_laststart.Location = new System.Drawing.Point(743, 299);
            this.lb_laststart.Name = "lb_laststart";
            this.lb_laststart.Size = new System.Drawing.Size(69, 13);
            this.lb_laststart.TabIndex = 15;
            this.lb_laststart.Text = "lb_laststart";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(854, 49);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Open Logs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ck_allowrebuild
            // 
            this.ck_allowrebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ck_allowrebuild.AutoSize = true;
            this.ck_allowrebuild.Checked = true;
            this.ck_allowrebuild.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_allowrebuild.Location = new System.Drawing.Point(743, 72);
            this.ck_allowrebuild.Name = "ck_allowrebuild";
            this.ck_allowrebuild.Size = new System.Drawing.Size(85, 17);
            this.ck_allowrebuild.TabIndex = 18;
            this.ck_allowrebuild.Text = "Allow rebuild";
            this.ck_allowrebuild.UseVisualStyleBackColor = true;
            // 
            // ck_enableMonitor
            // 
            this.ck_enableMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ck_enableMonitor.AutoSize = true;
            this.ck_enableMonitor.Location = new System.Drawing.Point(743, 49);
            this.ck_enableMonitor.Name = "ck_enableMonitor";
            this.ck_enableMonitor.Size = new System.Drawing.Size(97, 17);
            this.ck_enableMonitor.TabIndex = 19;
            this.ck_enableMonitor.Text = "Enable Monitor";
            this.ck_enableMonitor.UseVisualStyleBackColor = true;
            // 
            // bt_accounts
            // 
            this.bt_accounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_accounts.Location = new System.Drawing.Point(746, 194);
            this.bt_accounts.Name = "bt_accounts";
            this.bt_accounts.Size = new System.Drawing.Size(185, 28);
            this.bt_accounts.TabIndex = 20;
            this.bt_accounts.Text = "Configure Accounts and Servers";
            this.bt_accounts.UseVisualStyleBackColor = true;
            this.bt_accounts.Click += new System.EventHandler(this.bt_accounts_Click);
            // 
            // bt_settings
            // 
            this.bt_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_settings.Location = new System.Drawing.Point(746, 250);
            this.bt_settings.Name = "bt_settings";
            this.bt_settings.Size = new System.Drawing.Size(185, 31);
            this.bt_settings.TabIndex = 21;
            this.bt_settings.Text = "Configure App Settings";
            this.bt_settings.UseVisualStyleBackColor = true;
            this.bt_settings.Click += new System.EventHandler(this.bt_settings_Click);
            // 
            // bt_reisntall
            // 
            this.bt_reisntall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_reisntall.Location = new System.Drawing.Point(745, 581);
            this.bt_reisntall.Name = "bt_reisntall";
            this.bt_reisntall.Size = new System.Drawing.Size(185, 31);
            this.bt_reisntall.TabIndex = 22;
            this.bt_reisntall.Text = "Reinstall Servers";
            this.bt_reisntall.UseVisualStyleBackColor = true;
            this.bt_reisntall.Click += new System.EventHandler(this.bt_reisntall_Click);
            // 
            // lb_liskurl
            // 
            this.lb_liskurl.AutoSize = true;
            this.lb_liskurl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_liskurl.Location = new System.Drawing.Point(3, 441);
            this.lb_liskurl.Name = "lb_liskurl";
            this.lb_liskurl.Size = new System.Drawing.Size(44, 13);
            this.lb_liskurl.TabIndex = 23;
            this.lb_liskurl.Text = "lisk url";
            // 
            // NodeMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 624);
            this.Controls.Add(this.lb_liskurl);
            this.Controls.Add(this.bt_reisntall);
            this.Controls.Add(this.bt_settings);
            this.Controls.Add(this.bt_accounts);
            this.Controls.Add(this.ck_enableMonitor);
            this.Controls.Add(this.ck_allowrebuild);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lb_laststart);
            this.Controls.Add(this.lb_blocktorebuild);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_logs);
            this.Controls.Add(this.lb_numbertickes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_ticks);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_monitornode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_peers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_rebuild);
            this.MaximizeBox = false;
            this.Name = "NodeMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NodeMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NodeMonitor_FormClosing);
            this.Load += new System.EventHandler(this.NodeMonitor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_rebuild;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_peers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_monitornode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_ticks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_numbertickes;
        private System.Windows.Forms.TextBox txt_logs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb_blocktorebuild;
        private System.Windows.Forms.Label lb_laststart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox ck_allowrebuild;
        private System.Windows.Forms.CheckBox ck_enableMonitor;
        private System.Windows.Forms.Button bt_accounts;
        private System.Windows.Forms.Button bt_settings;
        private System.Windows.Forms.Button bt_reisntall;
        private System.Windows.Forms.Label lb_liskurl;
    }
}