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
    public partial class AccountsCfg : Form
    {
        public List<DelegateServers> delegateServers=new List<DelegateServers>();
        public List<Accounts> accounts = new List<Accounts>();
        public List<Servers> servers = new List<Servers>();
        public AccountsCfg(List<DelegateServers> delegateServersPara)
        {
            InitializeComponent();
            this.delegateServers = delegateServersPara;
            BindAccounts();
        }

        private void BindAccounts()
        {
            accounts = new List<Accounts>() ;
            foreach (DelegateServers del in delegateServers)
            {
                accounts.Add(del.account);
            }

            dataGrid_accounts.DataSource = accounts;
            dataGrid_accounts.Refresh();
        
        }

        private void dataGrid_accounts_SelectionChanged(object sender, EventArgs e)
        {

            BindServers();



        }

        private void BindServers()
        {
            if (dataGrid_accounts.SelectedRows.Count == 1)
            {
                dataGrid_servers.DataSource = null;
               var sel = dataGrid_accounts.SelectedRows[0].DataBoundItem as Accounts;

                dataGrid_servers.DataSource = delegateServers.Where(s => s.account.username == sel.username).FirstOrDefault().servers;
                dataGrid_servers.Refresh();
            }
        }

        #region ACCOUNTS
        private void bt_account_edit_Click(object sender, EventArgs e)
        {
            if (dataGrid_accounts.SelectedRows.Count == 1)
            {


                var sel = dataGrid_accounts.SelectedRows[0].DataBoundItem as Accounts;
                AccountEdit edit = new AccountEdit(sel);
                edit.ShowDialog();
                dataGrid_accounts.Refresh();


            }
            else
            {
                MessageBox.Show("must select account!");
            }
        }

        private void bt_account_delete_Click(object sender, EventArgs e)
        {
            if (dataGrid_accounts.SelectedRows.Count == 1)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this account, and related servers ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var sel = dataGrid_accounts.SelectedRows[0].DataBoundItem as Accounts;
                    foreach (DelegateServers s in delegateServers)
                    {
                        if (s.account.username == sel.username)
                        {
                            delegateServers.Remove(s);
                            break;
                            
                        }
                    }
                    BindAccounts(); 
                }

            }
            else
            {
                MessageBox.Show("must select account!");
            }
        }

        private void bt_account_add_Click(object sender, EventArgs e)
        {
            DelegateServers delegateAccount = new DelegateServers();

            delegateAccount.account = new Accounts();
            delegateAccount.account.enableMonitor = false;

            delegateServers.Add(delegateAccount);

            BindAccounts();

            AccountEdit edit = new AccountEdit(delegateAccount.account);
            edit.ShowDialog();

            dataGrid_accounts.Refresh();


        } 
        #endregion

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();

          
        }
        #region servers

        private void bt_serverEdit_Click(object sender, EventArgs e)
        {
            if (dataGrid_servers.SelectedRows.Count == 1)
            {
                var currentServser = dataGrid_servers.SelectedRows[0].DataBoundItem as Servers;

            
                ServersEdit edit = new ServersEdit(currentServser);
                edit.ShowDialog();
                dataGrid_servers.Refresh();



            }
            else
            {
                MessageBox.Show("must select server!");
            }
        }

        private void bt_add_server_Click(object sender, EventArgs e)
        {
            if (dataGrid_accounts.SelectedRows.Count == 1)
            {
                var sel = dataGrid_accounts.SelectedRows[0].DataBoundItem as Accounts;
                var currentDelegate = new DelegateServers();
                foreach (DelegateServers s in delegateServers)
                {
                    if (s.account.username == sel.username)
                    {
                        currentDelegate = s;
                        break;

                    }
                }

                if (currentDelegate.servers == null)
                    currentDelegate.servers = new List<Servers>();

                Servers newServer = new Servers();
                newServer.serverName = "new server";
                currentDelegate.servers.Add(newServer);

                BindAccounts();
                BindServers();

                ServersEdit edit = new ServersEdit(newServer);
                edit.ShowDialog();
                dataGrid_servers.Refresh();


            }
            else
            {
                MessageBox.Show("must select account!");
            }
        }

        private void bt_delete_server_Click(object sender, EventArgs e)
        {
            if (dataGrid_accounts.SelectedRows.Count == 1 && dataGrid_servers.SelectedRows.Count==1)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this server ??",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    var sel = dataGrid_accounts.SelectedRows[0].DataBoundItem as Accounts;
                    foreach (DelegateServers s in delegateServers)
                    {
                        if (s.account.username == sel.username)
                        {
                            var currentServser = dataGrid_servers.SelectedRows[0].DataBoundItem as Servers;
                            if (currentServser.isMainServer != true)
                            {
                                s.servers.Remove(currentServser);
                            }
                            else
                                MessageBox.Show("Can not delete a main server!!!!!");

                            break;

                        }
                    }
                    BindAccounts();
                    BindServers();
                }

            }
            else
            {
                MessageBox.Show("must select account and server !");
            }
        }

        #endregion


    }
}
