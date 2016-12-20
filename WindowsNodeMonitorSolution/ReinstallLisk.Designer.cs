namespace LiskLog
{
    partial class ReinstallLisk
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
            this.lb_versionSite = new System.Windows.Forms.LinkLabel();
            this.txt_install_comand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_enviroement = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_version = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_commandResponse = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.bt_execute = new System.Windows.Forms.Button();
            this.ck_list_servers = new System.Windows.Forms.CheckedListBox();
            this.lb_processing = new System.Windows.Forms.Label();
            this.ck_resinstall = new System.Windows.Forms.CheckBox();
            this.bt_rebuildlisknode = new System.Windows.Forms.Button();
            this.bt_rebuild = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_versionSite
            // 
            this.lb_versionSite.AutoSize = true;
            this.lb_versionSite.Location = new System.Drawing.Point(539, 22);
            this.lb_versionSite.Name = "lb_versionSite";
            this.lb_versionSite.Size = new System.Drawing.Size(55, 13);
            this.lb_versionSite.TabIndex = 0;
            this.lb_versionSite.TabStop = true;
            this.lb_versionSite.Text = "linkLabel1";
            this.lb_versionSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lb_versionSite_LinkClicked);
            // 
            // txt_install_comand
            // 
            this.txt_install_comand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_install_comand.Location = new System.Drawing.Point(12, 65);
            this.txt_install_comand.Multiline = true;
            this.txt_install_comand.Name = "txt_install_comand";
            this.txt_install_comand.Size = new System.Drawing.Size(377, 335);
            this.txt_install_comand.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Comand To Execute";
            // 
            // lb_enviroement
            // 
            this.lb_enviroement.AutoSize = true;
            this.lb_enviroement.Location = new System.Drawing.Point(12, 20);
            this.lb_enviroement.Name = "lb_enviroement";
            this.lb_enviroement.Size = new System.Drawing.Size(79, 13);
            this.lb_enviroement.TabIndex = 3;
            this.lb_enviroement.Text = "lb_enviroement";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Version Number";
            // 
            // txt_version
            // 
            this.txt_version.Location = new System.Drawing.Point(201, 17);
            this.txt_version.Name = "txt_version";
            this.txt_version.Size = new System.Drawing.Size(70, 20);
            this.txt_version.TabIndex = 5;
            this.txt_version.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(277, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Change Comand Version";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Validate Version on:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Comand Execution Response";
            // 
            // txt_commandResponse
            // 
            this.txt_commandResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_commandResponse.Location = new System.Drawing.Point(404, 65);
            this.txt_commandResponse.Multiline = true;
            this.txt_commandResponse.Name = "txt_commandResponse";
            this.txt_commandResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_commandResponse.Size = new System.Drawing.Size(386, 335);
            this.txt_commandResponse.TabIndex = 9;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(796, 65);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(293, 124);
            this.checkedListBox1.TabIndex = 10;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // bt_execute
            // 
            this.bt_execute.Location = new System.Drawing.Point(796, 402);
            this.bt_execute.Name = "bt_execute";
            this.bt_execute.Size = new System.Drawing.Size(293, 110);
            this.bt_execute.TabIndex = 11;
            this.bt_execute.Text = "Execute Script";
            this.bt_execute.UseVisualStyleBackColor = true;
            this.bt_execute.Click += new System.EventHandler(this.bt_execute_Click);
            // 
            // ck_list_servers
            // 
            this.ck_list_servers.CheckOnClick = true;
            this.ck_list_servers.FormattingEnabled = true;
            this.ck_list_servers.Location = new System.Drawing.Point(796, 195);
            this.ck_list_servers.Name = "ck_list_servers";
            this.ck_list_servers.Size = new System.Drawing.Size(293, 199);
            this.ck_list_servers.TabIndex = 12;
            // 
            // lb_processing
            // 
            this.lb_processing.AutoSize = true;
            this.lb_processing.Location = new System.Drawing.Point(410, 407);
            this.lb_processing.Name = "lb_processing";
            this.lb_processing.Size = new System.Drawing.Size(0, 13);
            this.lb_processing.TabIndex = 13;
            // 
            // ck_resinstall
            // 
            this.ck_resinstall.AutoSize = true;
            this.ck_resinstall.Location = new System.Drawing.Point(624, 421);
            this.ck_resinstall.Name = "ck_resinstall";
            this.ck_resinstall.Size = new System.Drawing.Size(154, 17);
            this.ck_resinstall.TabIndex = 14;
            this.ck_resinstall.Text = "if true reinstall else upgrade";
            this.ck_resinstall.UseVisualStyleBackColor = true;
            this.ck_resinstall.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // bt_rebuildlisknode
            // 
            this.bt_rebuildlisknode.Location = new System.Drawing.Point(16, 415);
            this.bt_rebuildlisknode.Name = "bt_rebuildlisknode";
            this.bt_rebuildlisknode.Size = new System.Drawing.Size(140, 23);
            this.bt_rebuildlisknode.TabIndex = 15;
            this.bt_rebuildlisknode.Text = "rebuild from lisknode";
            this.bt_rebuildlisknode.UseVisualStyleBackColor = true;
            this.bt_rebuildlisknode.Click += new System.EventHandler(this.bt_rebuildlisknode_Click);
            // 
            // bt_rebuild
            // 
            this.bt_rebuild.Location = new System.Drawing.Point(249, 415);
            this.bt_rebuild.Name = "bt_rebuild";
            this.bt_rebuild.Size = new System.Drawing.Size(140, 23);
            this.bt_rebuild.TabIndex = 16;
            this.bt_rebuild.Text = "rebuild from lisk";
            this.bt_rebuild.UseVisualStyleBackColor = true;
            this.bt_rebuild.Click += new System.EventHandler(this.bt_rebuild_Click);
            // 
            // ReinstallLisk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 524);
            this.Controls.Add(this.bt_rebuild);
            this.Controls.Add(this.bt_rebuildlisknode);
            this.Controls.Add(this.ck_resinstall);
            this.Controls.Add(this.lb_processing);
            this.Controls.Add(this.ck_list_servers);
            this.Controls.Add(this.bt_execute);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.txt_commandResponse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_version);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_enviroement);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_install_comand);
            this.Controls.Add(this.lb_versionSite);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReinstallLisk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReinstallLisk";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lb_versionSite;
        private System.Windows.Forms.TextBox txt_install_comand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_enviroement;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_version;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_commandResponse;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button bt_execute;
        private System.Windows.Forms.CheckedListBox ck_list_servers;
        private System.Windows.Forms.Label lb_processing;
        private System.Windows.Forms.CheckBox ck_resinstall;
        private System.Windows.Forms.Button bt_rebuildlisknode;
        private System.Windows.Forms.Button bt_rebuild;
    }
}