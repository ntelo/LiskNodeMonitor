using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using Newtonsoft.Json.Linq;
using LiskLog.Objects;
using LiskLog;

namespace LiskLog
{
    public partial class NodeMonitor : Form
    {
    
        // Flag that indcates if a process is running
        bool isRunning = false;

        private string enviroment = "";//ConfigurationManager.AppSettings["enviroment"];

        BLL bll =null;

        List<string> peers = new List<string>();//ConfigurationManager.AppSettings["peers"].Split(';').ToList();

        private string emailto = "";//ConfigurationManager.AppSettings["emailto"];
        private string nodename = "";
        private DateTime lastRebuild = DateTime.Now.AddMinutes(-80);
        private DateTime lastStart = DateTime.Now.AddMinutes(-80);

       // private int port = 0;
        private int timerticksms = 0;
       
        private int blockDiffToRebuild =0;
 
        int numberTickesMade = 0;

        private DateTime heartBeatNotification;
      
        System.Windows.Forms.Timer _timer;

        List<DelegateServers> servers = new List<DelegateServers>();

        List<Contracts> delegates = new List<Contracts>();
        private DateTime LastDelegatesSync = DateTime.Now.AddMinutes(-5);

        AppConfig currentConfig = new AppConfig();
        public NodeMonitor()
        {
            InitializeComponent();

            try
            {
                #region MyRegion

                //enablerebuild
                if (ConfigurationManager.AppSettings["enablerebuild"].Trim() == "1")
                    ck_allowrebuild.Checked = true;

                string text_servers = System.IO.File.ReadAllText(@"monitordata.txt");
                servers = JsonConvert.DeserializeObject<List<DelegateServers>>(text_servers);

                LoadConfigs();
              

                lb_monitornode.Text = "";


                var server = servers[0].servers.Where(s => s.isMainServer == true).FirstOrDefault();

               bool res= LoadDelegates(false, server);
                if (res == false)
                    MessageBox.Show("Unable load delegates.. will retry!!");

                ValidateAccounts();
              
                lb_ticks.Text = (timerticksms / 60000).ToString();
                lb_numbertickes.Text = numberTickesMade.ToString();

                lb_blocktorebuild.Text = "Blocks diff to rebuild:" + blockDiffToRebuild.ToString();

                lb_laststart.Text = "Start " + lastStart.ToString();



                Start();

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Monitor", "Some configurations are invalid, please review configurations files!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            


        }



        private void LoadConfigs()
        {
            string text_appconfig = System.IO.File.ReadAllText(@"monitorconfig.txt");
            currentConfig = JsonConvert.DeserializeObject<AppConfig>(text_appconfig);

            lb_liskurl.Text = currentConfig.liskBaseUrl;
            bll = new BLL(currentConfig);
            peers = currentConfig.peers.Split(';').ToList();
            enviroment = currentConfig.enviroment;
            emailto = currentConfig.emailto;
            timerticksms = currentConfig.timerticksms;
            blockDiffToRebuild = currentConfig.blockdiftorebuild;

            this.Text = " Enviroment - " + enviroment + " Lisk Folder - " + currentConfig.liskFolderName;


            lb_peers.Text = "";
            foreach (string s in peers)
            {
                lb_peers.Text += s + "\n\r";
            }
        }
        private void ValidateAccounts()
        {
            lb_monitornode.Text = string.Empty;
            foreach (DelegateServers s in servers)
            {
                
                s.lastServerSwitch = DateTime.Now.AddMinutes(2);
                
                Contracts acc = delegates.Where(z => z.username == s.account.username).FirstOrDefault();
                if (acc != null)
                {
                    int countMainservers = s.servers.Where(s1 => s1.isMainServer == true).Count();
                    if (countMainservers != 1)
                    {
                        MessageBox.Show("account " + s.account.username + " must have only one mainserver, configure - isMainServer in monitordata.txt for this account!!!",
                            "Main Server Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;

                    }
                    lb_monitornode.Text += s.account.username + " enable-" + s.account.enableMonitor + " Nº servers " + s.servers.Count.ToString() + "\n\r";
                }


            }
        }
        private void NodeMonitor_Load(object sender, EventArgs e)
        {


        }

        private bool LoadDelegates(bool retry,Servers server)
        {
            bool res = false;

            if (delegates != null && delegates.Count > 0)
                res = true;

          

            if (LastDelegatesSync<=DateTime.Now )
            {
                List<Contracts> list_0 = bll.GetDelegatesSSH(0,server).delegates;

                if (list_0 != null && list_0.Count > 0)
                {
                    delegates.Clear();
                    try
                    {
                        delegates.AddRange(list_0);
                        delegates.AddRange(bll.GetDelegatesSSH(101,server).delegates);
                        delegates.AddRange(bll.GetDelegatesSSH(201,server).delegates);

                        res = true;

                    }
                    catch
                    {
                        res = false;
                    }


                    LastDelegatesSync = DateTime.Now.AddMinutes(30);
                }
                else
                {
                    
                    
                    list_0 = bll.GetDelegatesSSH(0, servers[0].servers[0]).delegates;
                    if (list_0 != null && list_0.Count > 0)
                    {
                        delegates.AddRange(list_0);
                        delegates.AddRange(bll.GetDelegatesSSH(1, servers[0].servers[0]).delegates);
                        delegates.AddRange(bll.GetDelegatesSSH(2, servers[0].servers[0]).delegates);
                        res = true;
                        LastDelegatesSync = DateTime.Now.AddMinutes(30);
                    }
                       
                }

            }

         
          

                return res;
           

        }
        void Start()
        {

            _timer = new System.Windows.Forms.Timer(); // Set up the timer for 3 seconds
            _timer.Interval = timerticksms;                           //

            _timer.Tick += _timer_Tick;
            _timer.Enabled = true; // Enable it

            //WriteLog("Start WATCH load " + DateTime.Now.ToString());

            if (ck_enableMonitor.Checked == true)
            {
                StartDoMonitor();
            }
            else
            {
                txt_logs.Text = "\n\r #######  Monitor is disable, please enable on check box!!! ###### ";
            }
        }
        private void _timer_Tick(object sender, EventArgs e)
        {
           
           if(ck_enableMonitor.Checked==true && isRunning==false)
            {

                isRunning = true;
                 StartDoMonitor();

                //numberTickesMade += 1;
                lb_numbertickes.Text = numberTickesMade.ToString();


                isRunning = false;


            }
           else
            {
                txt_logs.Text = "\n\r #######  Enable Monitor is disable, please enable on check box!!! ###### ";
            }
          
           

        }

        
        private void StartDoMonitor()
        {

            Cursor.Current = Cursors.WaitCursor;
            var ser = servers[0].servers.Where(s => s.isMainServer == true).FirstOrDefault();
            bool resLoadDelegates = LoadDelegates(false, ser);

            if(resLoadDelegates == false)
            {
                txt_logs.Text = DateTime.Now.ToString() +  " ERROR Unable to load delegates!!! LoadDelegates()";
                return;
            }

            lb_laststart.Text = string.Empty;
            lb_laststart.Text = "Start : " + DateTime.Now.ToString();
            Stopwatch stopwatch = Stopwatch.StartNew();

            #region async code
            //txt_logs.Text = "   #####   NODE MONITOR STARTED...WAIT PLEASE, WE ARE GETTING NODE DATA!... ######";
            List<Task<string>> tasks = new List<Task<string>>();

            List<DelegateServers> listEnabled = servers.Where(s => s.account.enableMonitor == true).ToList();
            foreach (DelegateServers s in listEnabled)
            {
               

                Contracts acc = delegates.Where(z => z.username== s.account.username).FirstOrDefault();
                if (acc != null)
                {
                    int countMainservers = s.servers.Where(s1 => s1.isMainServer == true).Count();
                    if (countMainservers != 1)
                    {
                        MessageBox.Show("account " + s.account.username + " must have only one mainserver, configure - isMainServer in monitordata.txt for this account!!!",
                              "Main Server Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                       
                    }

                    //WriteLog("\n\r StartDoMonitor servers account  " + s.account.username + " Rank:" + s.account.rate + "\n\r");

                    s.account.generatorPublicKey = acc.publicKey;
                    s.account.address = acc.address;
                    s.account.missedblocks = acc.missedblocks;
                    s.account.producedblocks = acc.producedblocks;
                    s.account.productivity = acc.productivity;
                    s.account.rate = acc.rate;
                    s.account.balance = acc.balance;

                    Task<string> t = Task.Factory.StartNew(() =>
                    {

                        string res = PerformMonitor(s);
                      
                        return res;
                    });
                   
                    tasks.Add(t);
                }


              

            }
           

            Task.WaitAll(tasks.ToArray());

            numberTickesMade += 1;


           
            string resTasks =string.Empty;
            for (int i = 0; i < tasks.Count; i++)
            {
                //WriteLog(tasks[i].Result + "\r\n");
                resTasks += tasks[i].Result + "\r\n";
               // resTasks += ("\n\rStartDoMonitor END  " + listEnabled[i].account.username + "\n\r");
            }

            txt_logs.Text = string.Empty;
            WriteLog(resTasks);
            //numberTickesMade += 1;
            string server = JsonConvert.SerializeObject(servers);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\monitordata.txt", server);
            #endregion

            bll.SaveToDatabase(listEnabled, enviroment);
            SendHeartBeatNotification();

            stopwatch.Stop();
            long executionTime = stopwatch.ElapsedMilliseconds / 1000;

            lb_laststart.Text += "\r\nEnd   : " + DateTime.Now.ToString();
            lb_laststart.Text += "\r\nExec(sec): " + executionTime.ToString();


            Cursor.Current = Cursors.Default;
        }

        private void SendHeartBeatNotification()
        {
            try
            {
                if (numberTickesMade>1 && heartBeatNotification < DateTime.Now)
                {
                    StringBuilder message = new StringBuilder();
                    List<DelegateServers> listEnabled = servers.Where(s => s.account.enableMonitor == true).ToList();
                    foreach (DelegateServers s in listEnabled)
                    {
                        message.AppendLine(bll.GetAccountString(s));

                    }

                    bll.SendEmail(" Nodes HeartBeat ", emailto, message.ToString());

                    heartBeatNotification = DateTime.Now.AddHours(1);
                }
            }
            catch(Exception ex)
            {

            }
           
          
        }
        private string PerformMonitor(DelegateServers account)
        {

            StringBuilder message = new StringBuilder(); ;
            string takeactions_result = "";
            try
            {
                List<Task> tasks = new List<Task>();
                foreach (Servers s in account.servers.Where(s => s.isEnable == true).OrderByDescending(s => s.isForging))
                {
                    if (s.lastRebuild == null || s.lastRebuild == DateTime.Now)
                        s.lastRebuild = DateTime.Now;

                    // GetServerMonitorData_V2(s, account);
                    Task t = Task.Factory.StartNew(() =>
                    {

                        GetServerMonitorData_V2(s, account);
                    });

                    tasks.Add(t);

                }

                Task.WaitAll(tasks.ToArray());
                
                if (numberTickesMade > 0)
                {
                     takeactions_result = TakeActions(account);
                }

               // WriteLog("\n\r StartDoMonitor servers account  " + s.account.username + " Rank:" + s.account.rate + "\n\r");
                //print output
                #region PRINT OUT PUT
                message.Append("\r\n##############  PROCESSING ACCOUNT " + account.account.username.ToUpper() + " #####################\r\n");

                string serverResult = bll.GetAccountString(account);//GetServerJsonToNotify(s);//JsonConvert.SerializeObject(s);
                message.Append("\r\nACCOUNT INFO:\r\n" + serverResult);

                message.Append("\r\n");

                foreach (Servers s in account.servers.Where(s => s.isEnable == true).OrderByDescending(s => s.isForging).OrderByDescending(s=>s.consensus))
                {
                    message.Append("\r\nSERVER INFO: " + s.serverName.ToUpper() + " " + s.serverIP + " " + s.serverPort + " IS MAINSERVER:" + s.isMainServer + " IS FORGING:" + s.isForging + " IS REBUILDING:" + s.isRebuilding + "\r\n## BLOCK DIFF: " + s.blockDiff + " ##\r\n");
                  
                    message.Append("Consensus: " + s.consensus.ToString() + " ConsensusFromLog: " + s.consensusFromLog.ToString() +   "\r\n");
                    message.Append("ConsensusStr: " + s.consensusStr.Trim() + "\r\n");
                    message.Append("ConsensusAVG: " + s.consensusAvg.ToString() + "\r\n");
                    message.Append("isChainSyncing: " + s.isChainSyncing.ToString() + "\r\n");
                    message.Append("Forging Position Curren Slot: " + s.forgingPositionCurrenSlot.ToString() + "\r\n");
                    message.Append("Estimated Time(sec) To Forge : " + (s.forgingPositionCurrenSlot==-1?">100 seg" : (s.forgingPositionCurrenSlot*10).ToString() + " seg") + "\r\n");
                    if (s.isRebuilding)
                    {
                        message.AppendLine(s.serverName.ToUpper() + "\r\n ..IS REBUILDING....\r\n");
                    }

                    //string serverResult = bll.GetAccountString(account);//GetServerJsonToNotify(s);//JsonConvert.SerializeObject(s);
                    //message.Append("\r\nACCOUNT INFO:\r\n" + serverResult);
                    if (s.error != string.Empty)
                    {
                        message.Append("\r\nERROR - " + s.error);
                    }
                    message.Append("\r\n");
                }

                message.Append("\r\n" + takeactions_result); 
                #endregion



            }
            catch (Exception ex)
            {
                string error = "EXCEPTION Monitor " + account.account.username + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;
                if (ex.InnerException != null)
                    error += ex.InnerException + ex.StackTrace;

                message.Append("EXCEPTION Monitor " + account.account.username + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
                bll.SendEmail("ERROR DoMonitor " + account.account.username + DateTime.Now.ToString(), emailto, error);
            }
          

            return message.ToString(); ;


        }
        
        private Servers GetServerMonitorData_V2(Servers server, DelegateServers account)
        {
            string name = server.serverName;
            string error = "";
            server.tailLog = string.Empty;
            server.error = string.Empty;

            var client = bll.GetConection(server);//new SshClient(server.serverIP, server.userName, server.userPassword);

            try
            {
                
                bool isLiskRunning = bll.VerifyIfLiskIsRunning_V2(server, out error, client);

                decimal missedBlockPrevous = account.account.missedblocks;
                Servers serverPreviousData = (Servers)server.Clone();

                string url = "http://" + server.serverIP + ":" + server.serverPort;

                BlockChainStatus status = bll.GetBlockChainStatusSSH_V2(server, out error, client);
                server.error += error;
                if (status.success && isLiskRunning)
                {

                    server.blockChainHeight = status.height;
                    server.blockDiff = status.blocks;
                    server.isChainSyncing = status.syncing;
                    server.broadhash = status.broadhash;
                    server.consensus = string.IsNullOrEmpty(status.consensus) ? 0 : Convert.ToInt16(status.consensus) ;
                   // server.BroadhashConsensus100=status
                    if (server.isChainSyncing)
                    {
                        server.isChainSynced = false;
                        server.isRebuilding = true;
                        //server.lastRebuild = DateTime.Now;

                        if (serverPreviousData.isForging)
                            server.isForging = false;

                    }
                    else
                    {
                        server.isChainSynced = true;
                        server.isRebuilding = false;
                    }
                }


                //GetMaxPeerBlock
                string erro2 = "";
                //server.maxPeerBlock= bll.GetMaxPeerBlock(out erro2,server.serverPort);
                server.maxPeerBlock = bll.GetMaxPeerBlock_V2(out erro2, server, client);
                server.error += erro2;

                //blockdiff
                server.blockDiff = server.maxPeerBlock - server.blockChainHeight;
                if (server.blockDiff < 0)
                    server.blockDiff = server.blockDiff * -1;


                if (server.blockDiff > blockDiffToRebuild)
                {
                    server.isChainSynced = false;
                    if (serverPreviousData.isRebuilding == true && server.isRebuilding == false /*changed  because GetBlockChainStatusSSH above has issue*/)
                    {
                        server.isChainSynced = false;
                        server.isRebuilding = true;
                    }
                }
                else
                {
                    server.isChainSynced = true;
                    server.isRebuilding = false;
                }


                //latestBlockForged
                string error3 = "";
                if (server.isChainSynced == true && server.isRebuilding == false)
                {
                    Block block = bll.GetLatestBlockSSH_V2(server, account.account.generatorPublicKey, out error3,client);
                    server.LastBlockMinutsPassedSince = block.minutsPassedSinceLastBlock;
                    server.LastBlockForgedHeight = block.height;

                    // getnextforgers
                  int position=  bll.GetForgingPosition(server, account.account.generatorPublicKey, client);

                    server.forgingPositionCurrenSlot = position==-1?-1:position+1;
                }
                else
                {
                    Block block = new Block();
                    server.LastBlockMinutsPassedSince = 9999;
                    server.LastBlockForgedHeight = 0;
                    server.forgingPositionCurrenSlot = -1;

                }


                server.error += error3;

                server.tailLog = bll.ReadTailLog_V2(server,client);


                ////notify END rebuild
                if (serverPreviousData.isChainSynced == false && serverPreviousData.isRebuilding && server.isRebuilding == false)
                {
                    string serverResult = bll.GetAccountString(account);//GetServerJsonToNotify(server);

                    if (serverPreviousData.isRebuilding == true && server.isRebuilding == false)
                    {
                        //server.lastRebuild = DateTime.Now.AddMinutes(10);
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + "REBUILD_END_" + server.serverName + "_" + DateTime.Now.Ticks.ToString() + ".txt", "Rebuild End at " + DateTime.Now.ToString() + " \r\n Server Data:" + serverResult);

                        bll.SendEmail("SERVER END REBUILD " + server.serverName + " " + DateTime.Now.ToString(), account.account.email, "SERVER END REBUILD " + server.serverName + " " + DateTime.Now.ToString() + " server data: " + serverResult);
                    }
                }

                //notify LastBlockForgedHeight
                if (server.isForging && server.isChainSynced && numberTickesMade > 1)
                {
                    if (server.LastBlockLastNofication < DateTime.Now)
                    {
                        if (account.account.rate <= 101 && serverPreviousData.LastBlockForgedHeight < server.LastBlockForgedHeight && server.LastBlockForgedHeight > 0)
                        {
                            //string accountString = JsonConvert.SerializeObject(account);
                            string accountString = bll.GetAccountString(account);

                            string sms = account.account.username + " Rank: " + account.account.rate.ToString() + " Last Block: " + DateTime.Now.ToString() + " - " + server.serverName + " LastBlockForgedHeight:" + server.LastBlockForgedHeight.ToString() + " LastBlockMinutsPassed: " + server.LastBlockMinutsPassedSince + " this notification is sent every 5 hours, see inside account data.!!!";
                            bll.SendEmail(sms, account.account.email, sms + " Account: " + accountString);

                        }
                        else
                        {
                            if (account.account.rate > 101)
                            {
                                //string accountString = JsonConvert.SerializeObject(account);
                                string accountString = bll.GetAccountString(account);

                                string sms = account.account.username + " Rank: " + account.account.rate.ToString() + " this notification is sent every 5 hours, see inside account data.!!!";
                                bll.SendEmail(sms, account.account.email, sms + " Account:" + accountString);

                            }
                        }
                        server.LastBlockLastNofication = DateTime.Now.AddHours(5);
                    }

                }

                //notify missed block
                if (missedBlockPrevous < account.account.missedblocks)
                {
                    string accountString = bll.GetAccountString(account); //JsonConvert.SerializeObject(account);

                    string sms = "Missed Block Notification  " + account.account.username + " Rank:" + account.account.rate.ToString() + " " + DateTime.Now.ToString() + " - Server: " + server.serverName + " Missed Blocks:" + account.account.missedblocks;
                    bll.SendEmail(sms, account.account.email, sms + " Account:" + accountString);
                }

            }
            catch
            {

            }
            finally
            {
                //client.Disconnect();
                //client.Dispose();
            }
           



            return server;
        }

        private string TakeActions(DelegateServers account)
        {
            string tracing = "";

            List<Servers> activeServers = account.servers.Where(s => s.isEnable == true).ToList();

           int numberServersAvailable= activeServers.Where(s => s.blockDiff < blockDiffToRebuild).Count();

            foreach (Servers servers in activeServers)
            {
               
                string fork = ("Fork - {'delegate':'" + account.account.generatorPublicKey.Trim() +  "'").Replace("'", "\"");

                List<string> forkLogs = servers.tailLog.Split('\n').ToList();
                //"cause":5
                bool rebuildNowForkIssue = false;
                foreach(string logLine in forkLogs)
                {
                    if(logLine.Contains(fork))
                    {
                        if(logLine.Contains("5"))
                        {
                            rebuildNowForkIssue = true;

                            bll.SendEmail(servers.serverName + " Fork 5 detected rebuild will be done", account.account.email, "tailLog:"+ servers.tailLog);
                            break;
                        }
                    }
                }
                
                #region rebuild
                if ((servers != null && servers.isChainSynced == false && servers.isRebuilding == false && servers.blockDiff > blockDiffToRebuild)
                  || (servers != null && servers.isRebuilding == false
                  && servers.isForging == true && servers.LastBlockMinutsPassedSince > 120 && account.account.rate <= 101 && numberServersAvailable > 1)
                  || (servers != null && servers.tailLog.Contains("SIGKILL"))
                  || (servers != null && servers.tailLog.Contains("FATAL ERROR"))
                   || (servers != null && servers.tailLog.Contains("SIGABRT"))
                    || (servers != null && rebuildNowForkIssue)
                     || (servers != null && servers.tailLog.Contains("Rebuilding blockchain, current block height: 1"))
                    || servers.isRebuilding==true && servers.lastRebuild.AddMinutes(12)<DateTime.Now
                   
                   //Rebuilding blockchain, current block height: 1
                   )
                {
                    servers.consensus = 0;
                    
                    //if is main server switch emidialty
                    if(servers.isMainServer && servers.isForging)
                    {
                        string main = servers.serverName;
                        string log = servers.tailLog;
                        var newMain=activeServers.Where(s => s.isMainServer == false && s.blockDiff < 2).OrderByDescending(s => s.consensus).FirstOrDefault();
                        if(newMain!=null)
                        {
                          
                            string err = "";
                            //disable forging on main
                            bll.ForgingActionSSH(false, servers, account.account.passPhrase, out err);
                            servers.isForging = false;
                            servers.isMainServer = false;

                            //enable forgin onback
                            bll.ForgingActionSSH(true, newMain, account.account.passPhrase, out err);
                            newMain.isForging = true;
                            newMain.isMainServer = true;
                            bll.SendEmail("Priority Swith Server to " + newMain.serverName + " Main will rebuild:" + main, account.account.email,servers.serverName + ": TailLog:"+ log);

                        }
                      

                    }


                    servers.lastRebuild = DateTime.Now;
                    numberServersAvailable = numberServersAvailable - 1;

                    tracing += "Rebuild SERVER " + servers.serverName + ": \r\n";

                    string rebuild = bll.RebuildNodeClosePortSSH(servers, ck_allowrebuild.Checked);
                    if (rebuild.Contains("Failed"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        //retry one more time
                        rebuild += " Rebuild Failed, retry rebuild :" + bll.RebuildNodeClosePortSSH(servers, ck_allowrebuild.Checked);

                    }
                    tracing += rebuild;
                    servers.isRebuilding = true;
                    servers.isChainSyncing = true;
                    servers.isChainSynced = false;
                    servers.lastRebuild = DateTime.Now;
                    servers.isForging = false;


                    string serverResult = bll.GetAccountString(account);//GetServerJsonToNotify(servers);

                    bll.SendEmail("SERVER START REBUILD " + servers.serverName, account.account.email, "SERVER START REBUILD " + servers.serverName + " " + DateTime.Now.ToString() + "\nservers.blockDiff:" + servers.blockDiff + " server data: " + serverResult + " Tracing: " + tracing + " TailLog:" + servers.tailLog );
                } 
                #endregion

                //open port 
                if(servers.isRebuilding==false && servers.blockDiff<blockDiffToRebuild)
                {
                    bll.OpenPortSSH(servers);
                }
            }

            if (activeServers.Count()>0)// can only have one server forging
            {
                string error = "";
                tracing = "";
                bool resBestServerToForge = ChooseBestServerToForge(account, out error, out tracing);
                if (!resBestServerToForge)
                {
                    tracing += "\r\nMessage: " + error;
                }
            }

            return tracing;

        }
        

        private bool ChooseBestServerToForge(DelegateServers account, out string error,out string tracing)
        {
            bool res = false;
             error="";
            tracing = "ChooseBestServerToForge : Actions Taken: \r\n";

            List<Servers> activeServers = account.servers.Where(s => s.isEnable == true).ToList();

            Servers mainServer = activeServers.Where(s => s.isMainServer == true).FirstOrDefault();
            List<Servers> backupServers = activeServers.Where(s => s.isMainServer == false 
            && s.isRebuilding==false &&s.blockDiff<5).OrderByDescending(s=>s.consensus).ToList();


            //When not synching, consensus is usually high, so you need to add the -Z flag.
            //This way LiskAK will ignore consensus on nodes showing syncing = false.

            // switch main server to another one if lastblock > 20
            if (account.lastServerSwitch == null || account.lastServerSwitch == new DateTime())
                account.lastServerSwitch = DateTime.Now.AddMinutes(-60);

            double minPassedSinceLastSwitch =( DateTime.Now- account.lastServerSwitch).TotalMinutes;
            int comsemsusShift =51;
            var bestServersBroadhashConsensus = backupServers.Where( s=>s.consensus>= comsemsusShift && s.blockDiff<4  && s.isRebuilding==false).OrderByDescending(s=>s.consensusAvg).OrderByDescending(s => s.consensus).ToList();

            bool isMainServerConsensusSmallerThanBackUp = true;
            if(bestServersBroadhashConsensus.Count()>1)
            isMainServerConsensusSmallerThanBackUp= (mainServer.consensus < bestServersBroadhashConsensus.Max(s => s.consensusFromLog))?true:false;

            if (( mainServer.consensus == 0 && bestServersBroadhashConsensus.Count > 0) 
                || (mainServer.forgingPositionCurrenSlot == -1&& mainServer.blockDiff>=5 && bestServersBroadhashConsensus.Count>0)
                || ((mainServer.forgingPositionCurrenSlot==-1 &&  mainServer.consensus< comsemsusShift 
                && minPassedSinceLastSwitch >=1 && bestServersBroadhashConsensus.Count > 0 
                && isMainServerConsensusSmallerThanBackUp)//2
                //&& ((mainServer.LastBlockMinutsPassedSince>6 &&  mainServer.LastBlockMinutsPassedSince <17) || mainServer.LastBlockMinutsPassedSince>20)) 
                && (mainServer.LastBlockMinutsPassedSince>6) )
                )     
            {

                //new if
                int consensusFromLog = bll.ReadConsensusFromLog(mainServer);
                if(consensusFromLog< comsemsusShift || mainServer.consensus< comsemsusShift)
                {
                    #region switch server
                    account.lastServerSwitch = DateTime.Now;

                    tracing += "ChooseBestServerToForge :switch main server : \r\n" + mainServer.serverName + " BlockDiff:" +  mainServer.blockDiff +  " TO " + bestServersBroadhashConsensus[0].serverName;

                    string previousMain = mainServer.serverName;
                    string oldConsensus = mainServer.consensus.ToString();
                    mainServer.isMainServer = false;
                    //choose best backupserver

                    string sms = "Now Forging:" + bestServersBroadhashConsensus[0].serverName + " - " + " Swicth forged server from: " + previousMain + " With Consensus:" + oldConsensus + " to: "
                        + bestServersBroadhashConsensus[0].serverName
                        + " With Consensus:" + bestServersBroadhashConsensus[0].consensus.ToString() + " Average Consensus:" + bestServersBroadhashConsensus[0].consensusAvg.ToString();

                    //disable forging main
                    string err = "";
                    bll.ForgingActionSSH(false, mainServer, account.account.passPhrase, out err);
                    mainServer.isMainServer = false;

                    //enabe forging backup
                    string err2 = "";
                    bll.ForgingActionSSH(true, bestServersBroadhashConsensus[0], account.account.passPhrase, out err2);
                    bestServersBroadhashConsensus[0].isMainServer = true;
                    //RELOAD COLECTIONS
                    mainServer = activeServers.Where(s => s.isMainServer == true).FirstOrDefault();
                    backupServers = activeServers.Where(s => s.isMainServer == false && s.isChainSynced == true && s.isRebuilding == false).OrderBy(s => s.blockDiff).ToList();

                    string tailsLogs = "\n" + mainServer.serverName + "\nTail Log:" + mainServer.tailLog + "\n";
                      

                    bll.SendEmail(sms, account.account.email, bll.GetAccountString(account) + tailsLogs+ "\nERROR:" + err + " " + err2);

                    //RELOAD PREVIOUS MAIN 2
                   var serverToReload= activeServers.Where(s => s.serverName == previousMain).FirstOrDefault();
                    if(serverToReload.isMainServer==false && serverToReload.blockDiff<3)
                    {
                        bll.StopAndStartNodeSSH(serverToReload);
                        System.Threading.Thread.Sleep(1000);
                    }
                        
                    #endregion


                }

            }
        
            if (mainServer!=null && mainServer.isRebuilding)
            {
                error += "Warning: MainServer is rebuilding...\r\n";
            }

            foreach(Servers server in backupServers)
            if (server.isRebuilding)
            {
                error += "Warning: BackupServer is rebuilding...\r\n";
            }

            //if theres a backup 
            if (mainServer!=null && backupServers!=null && backupServers.Count>0)
            {
                tracing += " There's MainServer and BackUpServer \r\n";
                //mainServer.isChainSynced try activate mainserver
                if (mainServer.isChainSynced)
                {
                    //disable forging in backup
                    foreach(Servers server in backupServers)
                    {
                        string error1 = "";
                        bool resDisable = bll.ForgingActionSSH(false, server, account.account.passPhrase, out error1);
                        error += error1;
                    }
                  
                    if (true)//enable forging main
                    {
                        string error2 = "";
                        bool resEnable = bll.ForgingActionSSH(true, mainServer, account.account.passPhrase, out error2);
                        error += error2;
                        res = resEnable;

                        tracing += " MainServer Acivate Forging, disable in BackUpServer \r\n";
                    }


                } //mainServer.isChainSynced==false try activate best backupserver
                else if(mainServer.isChainSynced==false && backupServers != null && backupServers.Count>0)
                {
                    //disable forging in main
                    string error1 = "";
                    bool resDisable = bll.ForgingActionSSH(false, mainServer, account.account.passPhrase, out error1);
                    error += error1;

                    //verify forging
                    List<Servers> backServerForging = backupServers.Where(s => s.isChainSynced==true)
                        .OrderByDescending(s=>s.consensusAvg).OrderByDescending(s=>s.consensus).ToList();
                    //if theres only one forging
                    if(backServerForging!=null && backServerForging.Count==1)
                    {
                       
                        string error2 = "";
                        bool resEnable = bll.ForgingActionSSH(true, backServerForging[0], account.account.passPhrase, out error2);
                        error += error2;
                        res = resEnable;
                        if (res)
                            tracing += " BackUpServer: " + backServerForging[0].serverName + " Activate Forging, MainServer is not synced \r\n";
                      
                    }

                    //if theres more than one forging correct fork 
                    if (backServerForging != null && backServerForging.Count > 1)
                    {
                        Servers serverToForge = backupServers.Where(s => s.isChainSynced).FirstOrDefault();

                        //disable all minus elected
                        foreach(Servers s in backServerForging)
                        {
                            if(s.serverIP!=serverToForge.serverIP)
                            {
                                string error3 = "";
                                 bll.ForgingActionSSH(false, s, account.account.passPhrase, out error3);//###
                                error += error3;
                            }
                        }
                        //enable best serverToForge
                        string error2 = "";
                        bool resEnable = bll.ForgingActionSSH(true, serverToForge, account.account.passPhrase, out error2);
                        error += error2;
                        res = resEnable;
                        if (res)
                            tracing += " BackUpServer: " + serverToForge + " Activate Forging, MainServer is not synced \r\n";

                    }




                }

            }
            //if theres only one
            if (mainServer.isChainSynced && (backupServers == null || backupServers.Count==0))
            {
                tracing += " There's Only  MainServer \r\n";
                string error2 = "";
                bool resEnable = bll.ForgingActionSSH(true, mainServer, account.account.passPhrase, out error2);
                error += error2;
                res = resEnable;

                if (res)
                    tracing += " mainServer Activate Forging \r\n";

            }
        

            return res;

        }

        private void bt_rebuild_Click(object sender, EventArgs e)
        {
           
            StartDoMonitor();
           
            lb_numbertickes.Text = numberTickesMade.ToString();
        }
        
        private void WriteLog(string sms)
        {
            //if (numberTickesMade == 10)
            //    txt_logs.Text = string.Empty;

            //if (txt_logs.Text.Split('\r').ToArray().Length > 250)
            //    txt_logs.Text = string.Empty;
          
               
            txt_logs.Text += sms + "\r\n";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            bll.SendEmail("MONITOR CLOSED IN " + nodename + " AT " + DateTime.Now.ToString(), emailto, "MONITOR CLOSED IN " + nodename + " AT " + DateTime.Now.ToString());
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //txt_logs.Text = string.Empty;
            System.Diagnostics.Process.Start("explorer.exe", Path.Combine(Environment.CurrentDirectory, "logs"));
        }

        private void bt_accounts_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Monitor will be disable for configurations, must manualy enable it after configurations!!!");
            ck_enableMonitor.Checked = false;

            AccountsCfg frm = new AccountsCfg(servers);
            frm.ShowDialog();

            string server = JsonConvert.SerializeObject(servers);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\monitordata.txt", server);

            //reload configs
            ValidateAccounts();
        }

        private void bt_settings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Monitor will be disable for configurations, must manualy enable it, after configurations!!!");
            ck_enableMonitor.Checked = false;

            AppConfigEdit cfg = new AppConfigEdit(currentConfig);
            cfg.ShowDialog();

            string server = JsonConvert.SerializeObject(currentConfig);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\monitorconfig.txt", server);

            LoadConfigs();
        }

        private void bt_reisntall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Monitor will be disable for configurations, must manualy enable it, after configurations!!!");
            ck_enableMonitor.Checked = false;

            ReinstallLisk install = new ReinstallLisk(servers,currentConfig.liskFolderName,currentConfig.liskVersionNumber);
            install.ShowDialog();
        }

        private void NodeMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isRunning = false;
                bll.DestroyConections();
            }
            catch
            {

            }
          
          
        }

        //private async void button3_Click(object sender, EventArgs e)
        //{
        //    progressBar1.Visible = true;
        //    await Task.Run(() =>
        //    {
        //        Thread.Sleep(5000);
        //    });
        //    progressBar1.Visible = false;
        //    MessageBox.Show("Hi from the UI thread!");
        //}
    }
}
