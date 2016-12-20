using LiskLog.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiskLog
{
    public partial class AccountEdit : Form
    {
        Accounts currentAccount;
        public AccountEdit(Accounts account )
        {
            InitializeComponent();
            currentAccount = account;
            BindData();
        }

        private void BindData()
        {
            txt_username.Text = currentAccount.username!=null? currentAccount.username.Trim(): currentAccount.username="";
            txt_email.Text = currentAccount.email!=null?currentAccount.email.Trim():"";
            txt_passphrase.Text = currentAccount.passPhrase!=null? currentAccount.passPhrase.Trim():"";
            ck_enableMonitor.Checked = currentAccount.enableMonitor;
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            currentAccount.username = txt_username.Text.Trim();
            currentAccount.email = txt_email.Text.Trim();
            currentAccount.passPhrase = txt_passphrase.Text.Trim();
            currentAccount.enableMonitor = ck_enableMonitor.Checked;
            this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
