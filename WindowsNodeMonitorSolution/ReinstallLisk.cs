using LiskLog.Objects;
using Renci.SshNet;
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
    public partial class ReinstallLisk : Form
    {
        List<DelegateServers> currentDelegateServers;
        string reinstallComand = string.Empty;
        string version = string.Empty;
        
        public ReinstallLisk(List<DelegateServers> _delegates,string liskFolder,string liskVersion)
        {

            InitializeComponent();
            currentDelegateServers = _delegates;
            version = liskVersion;
            txt_version.Text = liskVersion.Trim();
            lb_enviroement.Text = liskFolder.Trim();
            //lb_versionSite.Text = liskFolder.Trim();
            ChangeVersion();
            LoadCheck();
        }

        private void lb_versionSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lb_versionSite.Text);
        }

        private void ChangeVersion()
        {
          

            if(txt_version.Text!=string.Empty)
            {
                if (lb_enviroement.Text.Contains("main"))
                {
                    if(ck_resinstall.Checked)
                    {
                        string comand = System.IO.File.ReadAllText(@"comands\LiskMainInstall.txt");
                        //  txt_install_comand.Text = comand.Replace("#versionnumber#", txt_version.Text.Trim());
                        lb_versionSite.Text = "https://downloads.lisk.io/lisk/main";
                        txt_install_comand.Text = comand;
                    }
                    else
                    {
                        string comand = System.IO.File.ReadAllText(@"comands\LiskMainUpgrade.txt");
                        //  txt_install_comand.Text = comand.Replace("#versionnumber#", txt_version.Text.Trim());
                        lb_versionSite.Text = "https://downloads.lisk.io/lisk/main";
                        txt_install_comand.Text = comand;
                    }
                 

                }

                if (lb_enviroement.Text.Contains("test"))
                {

                    // string comand = System.IO.File.ReadAllText(@"comands\LiskTestInstall.txt");
                    //// txt_install_comand.Text = comand.Replace("#versionnumber#", txt_version.Text.Trim());
                    // lb_versionSite.Text = "https://downloads.lisk.io/lisk/test/";
                    if (ck_resinstall.Checked)
                    {
                        string comand = System.IO.File.ReadAllText(@"comands\LiskTestInstall.txt");
                        //  txt_install_comand.Text = comand.Replace("#versionnumber#", txt_version.Text.Trim());
                        lb_versionSite.Text = "https://downloads.lisk.io/lisk/main";
                        txt_install_comand.Text = comand;
                    }
                    else
                    {
                        string comand = System.IO.File.ReadAllText(@"comands\LiskTestUpgrade.txt");
                        //  txt_install_comand.Text = comand.Replace("#versionnumber#", txt_version.Text.Trim());
                        lb_versionSite.Text = "https://downloads.lisk.io/lisk/main";
                        txt_install_comand.Text = comand;
                    }

                }

              
            }
            else
            {
                MessageBox.Show("Enter Correct Version, validate version in " + lb_versionSite.Text.Trim());
            }

          

          
        }

        private void LoadCheck()
        {
            checkedListBox1.Items.Clear();
            foreach (DelegateServers server in currentDelegateServers)
            {
                checkedListBox1.Items.Add(server.account.username,false);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeVersion();
        }

        private void bt_execute_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txt_commandResponse.Text = "";
            List<Task<string>> tasks = new List<Task<string>>();

            foreach (var del in ck_list_servers.CheckedItems)
            {
                string ip= del.ToString().Split(':')[0];

               var srvers = currentDelegateServers.Select(s=>s.servers).ToList();

                foreach(List<Servers> srv in srvers.ToList())
                {
                    Servers server = srv.Where(s => s.serverIP == ip).FirstOrDefault();
                    if(server!=null)
                    {

                        lb_processing.Text+= "\r\n" + server.serverName + " " + server.serverIP + ":" + server.serverPort.ToString();
                        txt_commandResponse.Text += "\r\nStart Processing server " + server.serverName + " " + server.serverIP + ":" + server.serverPort.ToString() + "\r\n";
                        string error = "";
                        string response = "";
                        string comand = txt_install_comand.Text.Replace("\r","").Replace("#password#", server.userPassword.Trim());
                        
                        //response = ReinstallServer(server, comand, out error);

                        Task<string> t = Task.Factory.StartNew(() =>
                        {
                            string res = ReinstallServer(server, comand, out error);
                            //if (error != string.Empty)
                            //    return "ERROR:" + error + " RESULT:" + res;
                            //else
                                return res;
                            
                        });

                        tasks.Add(t);


                        txt_commandResponse.Text += response + "\r\n";
                    }
                   
                }
            }

            Task.WaitAll(tasks.ToArray());

            for (int i = 0; i < tasks.Count; i++)
            {
                //WriteLog(tasks[i].Result + "\r\n");
                txt_commandResponse.Text+= tasks[i].Result + "\r\n";
                // resTasks += ("\n\rStartDoMonitor END  " + listEnabled[i].account.username + "\n\r");
            }

            Cursor.Current = Cursors.Default;

            MessageBox.Show("Done...");


        }

        private string ReinstallServer(Servers server,string comand, out string error)
        {
             error = "";
            string response = ExecuteComandOverSSH(server, comand, out error); ;


            return response;
        }

        public string ExecuteComandOverSSH(Servers server, string comand, out string error)
        {
            string result = "";
            error = "";
            try
            {

                if (comand != string.Empty)
                {

                    // Execute (SHELL) Commands
                    using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
                    {

                        client.Connect();

                        // try DELETE OTHER VERSIONS IF EXIST
                        if(comand.Contains("lisk-main"))
                        {
                            client.RunCommand("cd lisk-test && bash lisk.sh stop && rm -rf ~/lisk-test");
                        }
                        else
                        {
                            client.RunCommand("cd lisk-main && bash lisk.sh stop && rm -rf ~/lisk-main");
                        }
                        var resp = client.RunCommand(comand);
                        result = resp.Result;
                        error = resp.Error;

                        client.RunCommand("exit");

                        client.Disconnect();
                    }

                    // File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + DateTime.Now.Ticks.ToString() + "_rebuild_" + server.serverName + ".txt", "Rebuild at " + DateTime.Now.ToString() + "\r\n" + respText);

                }
                else
                {
                    error += "\r\n### NO COMMAND ###";

                }



            }
            catch (Exception ex)
            {

                error += ("\r\nEXCEPTION ExecuteComandOverSSH " + server.serverName + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);

                //if (!error.Contains("Message type 80 is not valid"))
                //    SendEmail("ERROR ExecuteComandOverSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR ExecuteComand: " + ex.Message + " comand" + comand + " result:" + result);
            }
            finally
            {
                // StartNode(ip, user, pwd);
            }

            return  result;


        }

     
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ck_list_servers.Items.Clear();
            foreach (var del in checkedListBox1.CheckedItems)
            {
                string account = del.ToString().Trim();

                DelegateServers deleg = currentDelegateServers.Where(s => s.account.username == account).FirstOrDefault();

                foreach (Servers server in deleg.servers)
                {
                    ck_list_servers.Items.Add(server.serverIP + ":" + server.serverPort + " " + server.serverName);
                   // lb_servers.Text += "\r\nProcessing server " + server.serverName + " " + server.serverIP + ":" + server.serverPort.ToString(); ;

                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeVersion();
        }

        private void bt_rebuildlisknode_Click(object sender, EventArgs e)
        {
            if(currentDelegateServers[0].servers[0].serverPort==8000)
                txt_install_comand.Text = "cd ~ && cd lisk-main && bash lisk.sh rebuild -u https://snapshot.lisknode.io";
            else
                txt_install_comand.Text = "cd ~ && cd lisk-main && bash lisk.sh rebuild ";

        }

        private void bt_rebuild_Click(object sender, EventArgs e)
        {
            txt_install_comand.Text = "cd ~ && cd lisk-main && bash lisk.sh rebuild";
        }
    }
}
