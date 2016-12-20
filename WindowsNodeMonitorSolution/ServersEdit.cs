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
    public partial class ServersEdit : Form
    {
        Servers currentServer;
       
        public ServersEdit(Servers server)
        {
            InitializeComponent();
            currentServer = server;
        
            BindData();
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindData()
        {
            //txt_email.Text = currentServer.email.Trim();
            txt_ip.Text = currentServer.serverIP!=null?currentServer.serverIP.Trim(): "0.0.0.0";
            txt_liskpassword.Text = currentServer.userPassword!=null?  currentServer.userPassword.Trim():"";
            txt_liskUser.Text = currentServer.userName!=null? currentServer.userName.Trim():"";
            txt_port.Text = currentServer.serverPort.ToString();
            //txt_rootPassword.Text = currentServer.rootPassword!=null? currentServer.rootPassword.Trim():"";
          //  txt_rootUser.Text = "root";
            txt_serverName.Text = currentServer.serverName!=null? currentServer.serverName:"";
            ck_enable.Checked = currentServer.isEnable;
            ck_EnableRebuild.Checked = currentServer.enableRebuild;
            ck_mainserver.Checked = currentServer.isMainServer;
            lb_blockdiff.Text = currentServer.blockDiff.ToString();
            lb_chainSynced.Text = currentServer.isChainSynced.ToString();

        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            currentServer.serverIP = txt_ip.Text.Trim();
              currentServer.userPassword= txt_liskpassword.Text.Trim();
            currentServer.userName = txt_liskUser.Text.Trim();
            currentServer.serverPort = Convert.ToInt16( txt_port.Text);
           // currentServer.rootPassword= txt_rootPassword.Text.Trim();
            //txt_rootUser.Text = "root";
             currentServer.serverName= txt_serverName.Text.Trim();
             currentServer.isEnable= ck_enable.Checked;
             currentServer.enableRebuild= ck_EnableRebuild.Checked;
            currentServer.isMainServer = ck_mainserver.Checked;


            this.Close();
        }
    }
}
