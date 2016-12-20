namespace LiskLog
{
    partial class AccountEdit
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
            this.txt_username = new System.Windows.Forms.TextBox();
            this.bt_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ck_enableMonitor = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_passphrase = new System.Windows.Forms.TextBox();
            this.eee = new System.Windows.Forms.Label();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(107, 44);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(177, 20);
            this.txt_username.TabIndex = 0;
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(526, 129);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 1;
            this.bt_save.Text = "Save";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.bt_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Delegate Name";
            // 
            // ck_enableMonitor
            // 
            this.ck_enableMonitor.AutoSize = true;
            this.ck_enableMonitor.Location = new System.Drawing.Point(107, 12);
            this.ck_enableMonitor.Name = "ck_enableMonitor";
            this.ck_enableMonitor.Size = new System.Drawing.Size(15, 14);
            this.ck_enableMonitor.TabIndex = 3;
            this.ck_enableMonitor.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enable Monitor?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "PassPhrase";
            // 
            // txt_passphrase
            // 
            this.txt_passphrase.Location = new System.Drawing.Point(107, 81);
            this.txt_passphrase.Name = "txt_passphrase";
            this.txt_passphrase.PasswordChar = '*';
            this.txt_passphrase.Size = new System.Drawing.Size(494, 20);
            this.txt_passphrase.TabIndex = 6;
            // 
            // eee
            // 
            this.eee.AutoSize = true;
            this.eee.Location = new System.Drawing.Point(290, 47);
            this.eee.Name = "eee";
            this.eee.Size = new System.Drawing.Size(32, 13);
            this.eee.TabIndex = 7;
            this.eee.Text = "Email";
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(328, 44);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(273, 20);
            this.txt_email.TabIndex = 8;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(38, 129);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 9;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // AccountEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 166);
            this.ControlBox = false;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.eee);
            this.Controls.Add(this.txt_passphrase);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ck_enableMonitor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_save);
            this.Controls.Add(this.txt_username);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AccountEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ck_enableMonitor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_passphrase;
        private System.Windows.Forms.Label eee;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Button btn_close;
    }
}