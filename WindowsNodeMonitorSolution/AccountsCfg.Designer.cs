namespace LiskLog
{
    partial class AccountsCfg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_account_edit = new System.Windows.Forms.Button();
            this.bt_account_delete = new System.Windows.Forms.Button();
            this.bt_account_add = new System.Windows.Forms.Button();
            this.dataGrid_accounts = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_delete_server = new System.Windows.Forms.Button();
            this.bt_add_server = new System.Windows.Forms.Button();
            this.bt_serverEdit = new System.Windows.Forms.Button();
            this.dataGrid_servers = new System.Windows.Forms.DataGridView();
            this.bt_close = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_accounts)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_servers)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_account_edit);
            this.groupBox1.Controls.Add(this.bt_account_delete);
            this.groupBox1.Controls.Add(this.bt_account_add);
            this.groupBox1.Controls.Add(this.dataGrid_accounts);
            this.groupBox1.Location = new System.Drawing.Point(23, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 164);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accounts";
            // 
            // bt_account_edit
            // 
            this.bt_account_edit.Location = new System.Drawing.Point(803, 19);
            this.bt_account_edit.Name = "bt_account_edit";
            this.bt_account_edit.Size = new System.Drawing.Size(57, 43);
            this.bt_account_edit.TabIndex = 3;
            this.bt_account_edit.Text = "edit";
            this.bt_account_edit.UseVisualStyleBackColor = true;
            this.bt_account_edit.Click += new System.EventHandler(this.bt_account_edit_Click);
            // 
            // bt_account_delete
            // 
            this.bt_account_delete.Location = new System.Drawing.Point(803, 135);
            this.bt_account_delete.Name = "bt_account_delete";
            this.bt_account_delete.Size = new System.Drawing.Size(57, 23);
            this.bt_account_delete.TabIndex = 2;
            this.bt_account_delete.Text = "delete";
            this.bt_account_delete.UseVisualStyleBackColor = true;
            this.bt_account_delete.Click += new System.EventHandler(this.bt_account_delete_Click);
            // 
            // bt_account_add
            // 
            this.bt_account_add.Location = new System.Drawing.Point(803, 81);
            this.bt_account_add.Name = "bt_account_add";
            this.bt_account_add.Size = new System.Drawing.Size(57, 23);
            this.bt_account_add.TabIndex = 1;
            this.bt_account_add.Text = "add";
            this.bt_account_add.UseVisualStyleBackColor = true;
            this.bt_account_add.Click += new System.EventHandler(this.bt_account_add_Click);
            // 
            // dataGrid_accounts
            // 
            this.dataGrid_accounts.AllowUserToAddRows = false;
            this.dataGrid_accounts.AllowUserToDeleteRows = false;
            this.dataGrid_accounts.AllowUserToOrderColumns = true;
            this.dataGrid_accounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_accounts.Location = new System.Drawing.Point(21, 19);
            this.dataGrid_accounts.Name = "dataGrid_accounts";
            this.dataGrid_accounts.ReadOnly = true;
            this.dataGrid_accounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_accounts.Size = new System.Drawing.Size(776, 139);
            this.dataGrid_accounts.TabIndex = 0;
            this.dataGrid_accounts.SelectionChanged += new System.EventHandler(this.dataGrid_accounts_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_delete_server);
            this.groupBox2.Controls.Add(this.bt_add_server);
            this.groupBox2.Controls.Add(this.bt_serverEdit);
            this.groupBox2.Controls.Add(this.dataGrid_servers);
            this.groupBox2.Location = new System.Drawing.Point(23, 201);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(872, 358);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Servers";
            // 
            // bt_delete_server
            // 
            this.bt_delete_server.Location = new System.Drawing.Point(803, 329);
            this.bt_delete_server.Name = "bt_delete_server";
            this.bt_delete_server.Size = new System.Drawing.Size(57, 23);
            this.bt_delete_server.TabIndex = 6;
            this.bt_delete_server.Text = "delete";
            this.bt_delete_server.UseVisualStyleBackColor = true;
            this.bt_delete_server.Click += new System.EventHandler(this.bt_delete_server_Click);
            // 
            // bt_add_server
            // 
            this.bt_add_server.Location = new System.Drawing.Point(803, 82);
            this.bt_add_server.Name = "bt_add_server";
            this.bt_add_server.Size = new System.Drawing.Size(57, 23);
            this.bt_add_server.TabIndex = 5;
            this.bt_add_server.Text = "add";
            this.bt_add_server.UseVisualStyleBackColor = true;
            this.bt_add_server.Click += new System.EventHandler(this.bt_add_server_Click);
            // 
            // bt_serverEdit
            // 
            this.bt_serverEdit.Location = new System.Drawing.Point(803, 19);
            this.bt_serverEdit.Name = "bt_serverEdit";
            this.bt_serverEdit.Size = new System.Drawing.Size(57, 39);
            this.bt_serverEdit.TabIndex = 4;
            this.bt_serverEdit.Text = "edit";
            this.bt_serverEdit.UseVisualStyleBackColor = true;
            this.bt_serverEdit.Click += new System.EventHandler(this.bt_serverEdit_Click);
            // 
            // dataGrid_servers
            // 
            this.dataGrid_servers.AllowUserToAddRows = false;
            this.dataGrid_servers.AllowUserToDeleteRows = false;
            this.dataGrid_servers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_servers.Location = new System.Drawing.Point(21, 19);
            this.dataGrid_servers.Name = "dataGrid_servers";
            this.dataGrid_servers.ReadOnly = true;
            this.dataGrid_servers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_servers.Size = new System.Drawing.Size(776, 333);
            this.dataGrid_servers.TabIndex = 0;
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(644, 564);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(239, 33);
            this.bt_close.TabIndex = 2;
            this.bt_close.Text = "Close";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // AccountsCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 599);
            this.ControlBox = false;
            this.Controls.Add(this.bt_close);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AccountsCfg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accounts";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_accounts)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_servers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGrid_accounts;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGrid_servers;
        private System.Windows.Forms.Button bt_account_edit;
        private System.Windows.Forms.Button bt_account_delete;
        private System.Windows.Forms.Button bt_account_add;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Button bt_serverEdit;
        private System.Windows.Forms.Button bt_delete_server;
        private System.Windows.Forms.Button bt_add_server;
    }
}