using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiskLog.Objects;
using RestSharp;
using RestSharp.Authenticators;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Renci.SshNet;
using LiskLog.Database;
using System.Net.Mail;

namespace LiskLog
{
    public class BLL
    {
        string liskBaseUrl = ""; 
        private string enviroment = "";
      

        private string saveToSqlServer = ConfigurationManager.AppSettings["saveToSqlServer"];
        private bool usesnapshot = true;
        private string commandrebuild = "";
        private string commandrebuildsnapshot = "";//ConfigurationManager.AppSettings["rebuild"].Replace("#", "&&");
        private string commandreload = ""; //ConfigurationManager.AppSettings["reload"].Replace("#", "&&");
        private string commandrestart = ""; //ConfigurationManager.AppSettings["restart"].Replace("#", "&&");
        private string commandstart = ""; //ConfigurationManager.AppSettings["start"].Replace("#", "&&");

        private string commanddeleteLogFilesAndRestart = "";//ConfigurationManager.AppSettings["deleteLogFilesAndRestart"].Replace("#", "&&");
   
        private string commanddeleteLogFiles = "";//ConfigurationManager.AppSettings["deleteLogFiles"].Replace("#", "&&");

        private string emailFrom = "";
        private string emailPassword = "";



        private string emailto = "";
        List<string> peers =new List<string>();
        AppConfig cfgCurrent;

        List<Conections> Conections = new List<Objects.Conections>();
       public BLL(AppConfig cfg)
       {
            cfgCurrent = cfg;
            peers =cfg.peers.Split(';').ToList();
            emailto = cfg.emailto;
            emailFrom = cfg.emailfrom;
            emailPassword = cfg.emailPassword;
            liskBaseUrl = cfg.liskBaseUrl;
            enviroment = cfg.enviroment;

            //change comands {liskfolder} in comands
            usesnapshot = ConfigurationManager.AppSettings["usesnapshot"]=="1"?true:false;
         commandrebuildsnapshot = ConfigurationManager.AppSettings["rebuildsnapshot"].Replace("#", "&&").Replace("{liskfolder}", cfg.liskFolderName);
          commandrebuild = ConfigurationManager.AppSettings["rebuild"].Replace("#", "&&").Replace("{liskfolder}", cfg.liskFolderName);
         commandreload = ConfigurationManager.AppSettings["reload"].Replace("#", "&&").Replace("{liskfolder}", cfg.liskFolderName);
         commandrestart = ConfigurationManager.AppSettings["restart"].Replace("#", "&&").Replace("{liskfolder}", cfg.liskFolderName);
         commandstart = ConfigurationManager.AppSettings["start"].Replace("#", "&&").Replace("{liskfolder}", cfg.liskFolderName);
         commanddeleteLogFilesAndRestart = ConfigurationManager.AppSettings["deleteLogFilesAndRestart"].Replace("#", "&&");
         commanddeleteLogFiles = ConfigurationManager.AppSettings["deleteLogFiles"].Replace("#", "&&").Replace("{liskfolder}", cfg.liskFolderName); ;

    }


        public void DestroyConections()
        {
            try
            {
                foreach (Conections con in Conections)
                {
                    if(con.SshClient!=null && con.SshClient.IsConnected)
                       con.SshClient.Disconnect();

                }
                
            }
            catch
            {

            }
            finally
            {
                Conections.Clear();
            }
           
        }
        public SshClient GetConection(Servers server)
        {
            SshClient client = null;

            try
            {
                Conections con = Conections.Where(s => s.servers.serverIP == server.serverIP).FirstOrDefault();
                if (con != null && con.SshClient != null)
                {
                    if (!con.SshClient.IsConnected)
                    {
                        con.SshClient.Connect();
                    }
                    client = con.SshClient;

                    return client;
                }
                else
                {
                    if (con == null)
                        con = new Objects.Conections();

                    con.servers = server;
                    con.SshClient = new SshClient(server.serverIP, server.userName, server.userPassword);
                    Conections.Add(con);

                    con.SshClient.Connect();
                    client = con.SshClient;

                    return client;

                }

            }
            catch (Exception ex)
            {
                this.SendEmail("ERROR GetConection " + server.serverName, cfgCurrent.emailto, ex.Message);
            }
         
            return client;
        }

        public void SendEmail(string subject, string emailTo, string body)
        {
            if(cfgCurrent.enableEmailNotifications)
            {
                Task t = Task.Factory.StartNew(() =>
                {
                    //go to gmail https://www.google.com/settings/security/lesssecureapps  turn on
                    MailMessage msg = new MailMessage();

                    msg.From = new MailAddress(emailFrom);
                    msg.To.Add(emailTo);
                    msg.Subject = subject;
                    msg.Body = body;
                    SmtpClient client = new SmtpClient();

                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailFrom, emailPassword);
                    client.Timeout = 10000;
                    try
                    {
                        client.Send(msg);
                        //return "Mail has been successfully sent!";
                    }
                    catch (Exception ex)
                    {
                        //return "Fail Has error" + ex.Message;
                    }
                    finally
                    {
                        msg.Dispose();
                    }
                });
            }

        

            ////go to gmail https://www.google.com/settings/security/lesssecureapps  turn on
            //MailMessage msg = new MailMessage();

            //msg.From = new MailAddress(emailFrom);
            //msg.To.Add(emailTo);
            //msg.Subject = subject;
            //msg.Body = body;
            //SmtpClient client = new SmtpClient();
          
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;
            //client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential(emailFrom, emailPassword);
            //client.Timeout = 10000;
            //try
            //{
            //    client.Send(msg);
            //    //return "Mail has been successfully sent!";
            //}
            //catch (Exception ex)
            //{
            //    //return "Fail Has error" + ex.Message;
            //}
            //finally
            //{
            //    msg.Dispose();
            //}
        }


        public bool ForgingActionAPI(bool enableForging, Servers server, string secret, out string error)
        {
            bool actionOk = false;
            /*
             * secret must add ip to forging section
             */
            error = "";
            string baseUrl = "http://" + server.serverIP + ":" + server.serverPort.ToString();

            string jsonResult = "";

            try
            {

                Request obj = new Request();
                HttpWebRequest request;
                obj.secret = secret.Trim();
                string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                if (enableForging)
                    request = WebRequest.CreateHttp(baseUrl + "/api/delegates/forging/enable");
                else
                    request = WebRequest.CreateHttp(baseUrl + "/api/delegates/forging/disable");

                request.Method = "POST";
                request.AllowWriteStreamBuffering = false;
                request.ContentType = "application/json";
                request.Accept = "Accept=application/json";
                request.SendChunked = false;
                request.ContentLength = serializedObject.Length;
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(serializedObject);
                }
                var response = request.GetResponse() as HttpWebResponse;

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    jsonResult = sr.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                actionOk = false;
                error = "ERROR : ForgingAction " + server.serverName + " see message : " + ex.Message;
                SendEmail("ERROR ForgingAction LiskApi SendTransaction", emailto, "ERROR LiskApi SendTransaction" + ex.Message + " jsonResult:" + jsonResult);

            }

            // MessageBox.Show(jsonResult);
            if (jsonResult.Contains("\"success\":true") || (enableForging == true && jsonResult.Contains("Forging is already enabled")) || (enableForging == false && jsonResult.Contains("Delegate not found")))
            {
                actionOk = true;
                server.isForging = enableForging;
            }
            else
            {
                actionOk = false;
                error += " ForgingAction :" + jsonResult;
            }



            return actionOk;


        }

        public bool ForgingActionSSH(bool enableForging, Servers server, string secret, out string error)
        {

            bool actionOk = false;
            /*
             * curl -k -H "Content-Type: application/json" \
                -X POST -d '{"secret":"mysecret"}' \
                http://localhost:8000/api/delegates/forging/enable
             */
            error = "";
           
            string jsonResult = "";

            try
            {

                string comand = "";
                if (enableForging)
                {
                     comand = System.IO.File.ReadAllText(@"comands\enableforging.txt");
                    comand = comand.Replace("#secret#", secret);
                    comand = comand.Replace("#port#", server.serverPort.ToString().Trim());
                }
                else
                {
                    comand = System.IO.File.ReadAllText(@"comands\disableforging.txt");
                    comand = comand.Replace("#secret#", secret);
                    comand = comand.Replace("#port#", server.serverPort.ToString().Trim());
                }
                  

                jsonResult = ExecuteComandOverSSH(server, comand,out error);

            }
            catch (Exception ex)
            {
                actionOk = false;
                error = "ERROR : ForgingActionSSH " + server.serverName + " see message : " + ex.Message;
                SendEmail("ERROR ForgingActionSSH LiskApi SendTransaction", emailto, "ERROR LiskApi SendTransaction" + ex.Message + " jsonResult:" + jsonResult);

            }

            // MessageBox.Show(jsonResult);
            if (jsonResult.Contains("\"success\":true") || (enableForging == true && jsonResult.Contains("Forging is already enabled")) || (enableForging == false && jsonResult.Contains("Delegate not found")))
            {
                actionOk = true;
                server.isForging = enableForging;
                server.isMainServer = enableForging;
            }
            else
            {
                actionOk = false;
                error += " ForgingAction :" + jsonResult;
            }



            return actionOk;


        }

        public string ReinstallActionSSH(Servers server, string secret,string comand)
        {

         
            string error = "";

            string jsonResult = "";

            try
            {

                jsonResult = ExecuteComandOverSSH(server, comand, out error);

                

            }
            catch (Exception ex)
            {
               
                error = "ERROR : ReinstallActionSSH " + server.serverName + " see message : " + ex.Message;
                SendEmail("ERROR ReinstallActionSSH ", emailto, "ERROR ReinstallActionSSH" + ex.Message + " jsonResult:" + jsonResult);

            }
            finally
            {
                jsonResult = error != string.Empty ? jsonResult + " error:" + error : jsonResult;
            }



            return jsonResult;


        }


        //public bool VerifyIfLiskIsRunning(Servers server, out string error_VerifyIfLiskIsRunning)
        //{
        //    error_VerifyIfLiskIsRunning = string.Empty;
        //    bool res = true;
        //    string commandIsRunning = "cd && cd " +cfgCurrent.liskFolderName.Trim() + " && bash lisk.sh status";
        //    string commandRestart = "cd && cd " + cfgCurrent.liskFolderName.Trim() + " && bash lisk.sh reload";

        //    using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
        //    {

        //        try
        //        {
        //            client.Connect();
        //            var resp_VerifyIfLiskIsRunning = client.RunCommand(commandIsRunning);
        //            string result_VerifyIfLiskIsRunning = resp_VerifyIfLiskIsRunning.Result;
        //            if (!result_VerifyIfLiskIsRunning.Contains("Lisk is running"))
        //            {
        //                // restart
        //                client.RunCommand(commandRestart);

        //                // validate restart
        //                resp_VerifyIfLiskIsRunning = client.RunCommand(commandIsRunning);
        //                result_VerifyIfLiskIsRunning = resp_VerifyIfLiskIsRunning.Result;
        //                if (!result_VerifyIfLiskIsRunning.Contains("Lisk is running"))
        //                {
        //                    res = false;
        //                    error_VerifyIfLiskIsRunning += " Lisk is not running on server " + server.serverName + " error:" + resp_VerifyIfLiskIsRunning.Error;
        //                    SendEmail(" Lisk is not running on server " + server.serverName, emailto, error_VerifyIfLiskIsRunning);
        //                }


        //                client.RunCommand("exit");

        //                client.Disconnect();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            error_VerifyIfLiskIsRunning += " Lisk is not running on server " + server.serverName + " exception:" + ex.Message;
        //            SendEmail(" Lisk is not running on server " + server.serverName, emailto, error_VerifyIfLiskIsRunning);
        //            return false;
        //        }
        //    }

        //    return res;

        //}

        public bool VerifyIfLiskIsRunning_V2(Servers server, out string error_VerifyIfLiskIsRunning, SshClient client)
        {
            error_VerifyIfLiskIsRunning = string.Empty;
            bool res = true;
            string commandIsRunning = "cd && cd " + cfgCurrent.liskFolderName.Trim() + " && bash lisk.sh status";
            string commandRestart = "cd && cd " + cfgCurrent.liskFolderName.Trim() + " && bash lisk.sh stop && bash lisk.sh start";

            //using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
            //{

                try
                {
                  
                    var resp_VerifyIfLiskIsRunning = client.RunCommand(commandIsRunning);
               // client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(5);
                string result_VerifyIfLiskIsRunning = resp_VerifyIfLiskIsRunning.Result;
                    if (!result_VerifyIfLiskIsRunning.Contains("Lisk is running"))
                    {
                        // restart
                        client.RunCommand(commandRestart);

                        // validate restart
                        resp_VerifyIfLiskIsRunning = client.RunCommand(commandIsRunning);
                        result_VerifyIfLiskIsRunning = resp_VerifyIfLiskIsRunning.Result;
                        if (!result_VerifyIfLiskIsRunning.Contains("Lisk is running"))
                        {
                            res = false;
                            error_VerifyIfLiskIsRunning += " Lisk is not running on server " + server.serverName + " error:" + resp_VerifyIfLiskIsRunning.Error;
                            SendEmail(" Lisk is not running on server " + server.serverName, emailto, error_VerifyIfLiskIsRunning);
                        }

                    }
                }
                catch (Exception ex)
                {
                    error_VerifyIfLiskIsRunning += " Lisk is not running on server " + server.serverName + " exception:" + ex.Message;
                    SendEmail(" Lisk is not running on server " + server.serverName, emailto, error_VerifyIfLiskIsRunning);
                    return false;
                }
          

            return res;

        }

        public LiskDelegates GetDelegates(int offSet)
        {
            string jsonResult;
            LiskDelegates result = new LiskDelegates();
            try
            {
                string baseUrl = liskBaseUrl;
                //HttpWebRequest http = (HttpWebRequest)WebRequest.Create(baseUrl + string.Format("/api/delegates/?&orderBy=rate&offset={0}", offSet));
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(baseUrl + string.Format("/api/delegates/?offset={0}", offSet));
                http.Method = "GET";
                WebResponse response = http.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    jsonResult = sr.ReadToEnd();
                }
                if (jsonResult != null || !jsonResult.Contains("502") || !jsonResult.Contains("Bad Gateway"))
                    result = JsonConvert.DeserializeObject<LiskDelegates>(jsonResult);
                else
                    result = null;
            }
            catch (Exception ex)
            {
                SendEmail("ERROR LiskApi GetDelegates " + cfgCurrent.enviroment, emailto, "ERROR LiskApi GetDelegates" + ex.Message);
            }
            if (result == null)
            {
                result = new LiskDelegates();
                result.delegates = new List<Contracts>();
            }
               

            return result;

        }

        public LiskDelegates GetDelegatesSSH(int offSet,Servers server)
        {
          
            LiskDelegates result = new LiskDelegates();
            try
            {
              

                string comand_GetBlockChainStatusSSH = "curl --connect-timeout 3 -k -X GET http://localhost:" + server.serverPort.ToString().Trim() + "/api/delegates/?offset=" + offSet;
                string  errorMessage= "";
                string jsonResult = ExecuteComandOverSSH(server, comand_GetBlockChainStatusSSH, out errorMessage);

                if (jsonResult != null || jsonResult!=string.Empty)
                    result = JsonConvert.DeserializeObject<LiskDelegates>(jsonResult);

            }
            catch (Exception ex)
            {
                SendEmail("ERROR GetDelegatesSSH  " + cfgCurrent.enviroment, emailto, "ERROR GetDelegatesSSH " + ex.Message);
            }
            if (result == null)
            {
                result = new LiskDelegates();
                result.delegates = new List<Contracts>();
            }


            return result;

        }


        public void RebootServer(Servers server)
        {
            try
            {
                SshCommand result = null;
                var client = this.GetConection(server);
                string command = string.Format("cd && cd {0} && bash lisk.sh stop && echo '{1}' | sudo -S reboot",cfgCurrent.liskFolderName, server.userPassword.Trim());
                server.lastReboot = DateTime.Now;
                result = client.RunCommand(command);
            }
            catch(Exception ex)
            {

            }
        }
        public string ReadTailLog_V2(Servers server, SshClient  client)
        {
            //cd lisk && tail  logs.log

            StringBuilder sms = new StringBuilder();
            SshCommand consensusResult=null;
            try
            {
                    var resp = client.RunCommand("cd " + cfgCurrent.liskFolderName.Trim() + " && cd logs && tail -10 lisk.log");
                    sms.Append("\r\nRESULT:" + resp.Result);
                    sms.Append("\r\nERROR:" + resp.Error);

                    sms.Append(resp.Result + " " + resp.Error);


            }
            catch (Exception ex)
            {
                sms.Append("\r\nReadTailLog  " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
                if (!sms.ToString().Contains("Message type 80 is not valid"))
                    SendEmail("ERROR nReadTailLog " +server.serverName + DateTime.Now.ToString(), emailto, "ERROR ReadTailLog: " + ex.Message + " sms:" + sms.ToString());
            }
            finally
            {
                this.ReadConsensusFromLog(server);
              
            }

            return sms.ToString();

        }

        public int  ReadConsensusFromLog(Servers server)
        {
            #region temorario
            string res = "";
            try
            {
               
                SshCommand consensusResult = null;
                if (server.isRebuilding == false)
                {
                    //grep Inadequate logs/lisk.log
                   var client= this.GetConection(server);
                    consensusResult = client.RunCommand("cd " + cfgCurrent.liskFolderName.Trim() + " && cd logs && grep 'consensus'  lisk.log| tail -2");
                     res = consensusResult.Result.ToString().Replace("\nBinary file lisk.log matches", " ").Trim();
                    if (res.Contains("Inadequate"))
                    {
                        server.consensus = 0;

                    }

                    string[] consensus = res.Split('\n');

                    int j = 0;
                    decimal consensusAvg = 0;
                    for (int i = 0; i < consensus.Length; i++)
                    {
                        if (!consensus[0].Contains("Inadequate"))
                        {
                            j = j + 1;
                            string consensusString = consensus[0].Replace("Broadhash consensus now", "").Replace("%", "").Split('|')[1].Trim();
                            decimal cons = 0;
                            decimal.TryParse(consensusString, out cons);
                            consensusAvg += cons;

                        }

                        if(i== consensus.Length-1)
                        {
                            string cons = "";
                            if (consensus[i] == string.Empty)
                                cons = consensus[i - 1];
                            else
                                cons = consensus[i];

                            server.consensusStr = cons;
                            if (!cons.Contains("Inadequate"))
                            {
                                int newConsensus = 0;
                                int.TryParse(cons.Replace("Broadhash consensus now", "").Replace("%", "").Split('|')[1].Trim(), out newConsensus);
                                server.consensusFromLog = newConsensus;
                            }
                           
                        }
                    }

                    if (j > 0)
                        server.consensusAvg = consensusAvg / j;

                }
            }
            catch(Exception ex)
            {
                server.consensusFromLog = server.consensus;
                this.SendEmail("ERROR ReadConsensusFromLog " + server.serverName, cfgCurrent.emailto, ex.Message + res);
            }


            return server.consensusFromLog;
            #endregion
        }

        public string GetAccountString(DelegateServers account)
        {
            StringBuilder stringRes = new StringBuilder();
            Servers activeServer = account.servers.Where(s => s.isForging).FirstOrDefault();

            // string accountString = JsonConvert.SerializeObject(account);
            stringRes.AppendLine(" DELEGATE: " + account.account.username.ToString());
          
            stringRes.AppendLine(" FORGING SERVER: " + (activeServer != null ? activeServer.serverName.ToUpper() : " ### ERROR NO FORGING SERVER ###").ToString());
            stringRes.AppendLine(" IS FORGING: " + (activeServer != null ? activeServer.isForging.ToString().ToUpper() : " ### ERROR NO FORGING SERVER ###").ToString());
            stringRes.AppendLine(" RATE: " + account.account.rate.ToString());
            stringRes.AppendLine(" MISSED BLOCKS: " + account.account.missedblocks.ToString());
            stringRes.AppendLine(" PRODUCED BLOCKS: " + account.account.producedblocks.ToString());
            stringRes.AppendLine(" PRODUCTIVITY: " + account.account.productivity.ToString());

            if (activeServer != null)
            {
                stringRes.AppendLine(" serverName: " + activeServer.serverName.ToUpper());
                stringRes.AppendLine(" isMainServer: " + activeServer.isMainServer.ToString().ToUpper());
                stringRes.AppendLine(" Last Block forged: " + activeServer.LastBlockMinutsPassedSince.ToString() + " min ago");
                stringRes.AppendLine("Consensus: " + activeServer.consensus.ToString() + " ConsensusFromLog: " + activeServer.consensusFromLog.ToString());
                stringRes.AppendLine("ConsensusStr: " + activeServer.consensusStr.Trim());
                stringRes.AppendLine("ConsensusAVG: " + activeServer.consensusAvg.ToString());
                stringRes.AppendLine(" LastBlockForgedHeight: " + activeServer.LastBlockForgedHeight.ToString());
                
                stringRes.AppendLine(" blockChainHeight: " + activeServer.blockChainHeight.ToString());
                stringRes.AppendLine(" blockDiff: " + activeServer.blockDiff.ToString());
               
               
                stringRes.AppendLine(" isChainSynced: " + activeServer.isChainSynced.ToString());
               

                int numberServsers = account.servers.Where(s => s.isChainSynced == true && s.isRebuilding == false && s.isEnable == true).Count();
                if (numberServsers > 0)
                    stringRes.AppendLine(" NUMBER SERVERS AVAILABLE: " + numberServsers.ToString());
            }
            else
            {
                stringRes.AppendLine(" ### ATENTION NO FORGING SERVER TRY RESYNC ####");
            }



            return stringRes.ToString();
        }

        public int GetBlockChainHeightAPI(string baseUrl, out string errorMessage)
        {

            string jsonResult;

            errorMessage = "";
            BlockChainHeight result = new BlockChainHeight();
            if (!baseUrl.EndsWith("/"))
                baseUrl = baseUrl.Trim() + "/";

            try
            {
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(baseUrl + "api/blocks/getHeight");
                http.Timeout = 3000;
                http.Method = "GET";
                WebResponse response = http.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    jsonResult = sr.ReadToEnd();
                }
                result = JsonConvert.DeserializeObject<BlockChainHeight>(jsonResult);
            }
            catch (Exception ex)
            {
                errorMessage = "\r\nEXCEPTION Monitor GetBlockChainHeight " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;
                // bll.SendEmail("ERROR GetBlockChainHeight " + DateTime.Now.ToString() ,emailto, "ERROR GetBlockChainHeight:" + baseUrl + " EX:" + ex.Message);
                if (result == null)
                {
                    result = new BlockChainHeight();
                    result.height = 0;
                }

            }

            return result.height;
        }

        public int GetMaxPeerBlock(out string error, int port)
        {
            List<int> listBlocks = new List<int>();
            error = "";
            foreach (string ip in peers)
            {
                string url = string.Format("http://{0}:{1}", ip.Trim(), port.ToString().Trim());
                int cuurBlock = GetBlockChainHeightAPI(url, out error);

                listBlocks.Add(cuurBlock);

            }

            if(listBlocks.Max()==0)
            {
                int cuurBlock = GetBlockChainHeightAPI(this.liskBaseUrl, out error);
                return cuurBlock;
            }


            return listBlocks.Max();
        }
        
        //public int GetMaxPeerBlock_V2(out string error_GetMaxPeerBlockV2, Servers server)
        //{
        //    int maxHeight = 0;
        //    int ?heightLocalhost = 0;
        //    int? heightLiskurl = 0;
        //    string errorMessage_GetMaxPeerBlockV2_localhost = "";
        //    string errorMessage_GetMaxPeerBlockV2_lisk = "";


        //    string comandLiskUrl = "curl -k -X GET " + liskBaseUrl + "/api/peers?state=2&height>0";
        //    string jsonResultLiskUrl = ExecuteComandOverSSH(server, comandLiskUrl, out errorMessage_GetMaxPeerBlockV2_lisk);

        //    string comandLocalhost = "curl -k -X GET http://localhost:" + server.serverPort.ToString().Trim() + "/api/peers?state=2&height>0";
        //    string jsonResultLocalhost = ExecuteComandOverSSH(server, comandLocalhost, out errorMessage_GetMaxPeerBlockV2_localhost);
            

        //    PeersResponse resultLocalhost = new PeersResponse();
        //    PeersResponse resultLiskurl = new PeersResponse();

        //    try
        //    {
        //        errorMessage_GetMaxPeerBlockV2_localhost = "";
        //         errorMessage_GetMaxPeerBlockV2_lisk = "";
        //        #region localHost
        //        if (jsonResultLocalhost != string.Empty)
        //        {
        //            resultLocalhost = JsonConvert.DeserializeObject<PeersResponse>(jsonResultLocalhost);

                   
        //            if (!resultLocalhost.success)
        //            {
        //                errorMessage_GetMaxPeerBlockV2_localhost += "ERROR GetMaxPeerBlockV2: result.success IS FALSE TO " + server.userName;
        //            }

        //            heightLocalhost = resultLocalhost.peers.Max(s => s.height).Value;
        //        }
        //        else
        //        {
        //            errorMessage_GetMaxPeerBlockV2_localhost += "ERROR GetMaxPeerBlockV2: jsonResult is empty ";
        //        }
        //        #endregion

        //        #region liskurl
        //        heightLiskurl = 0;
        //        if (jsonResultLiskUrl != string.Empty && server.blockChainHeight< heightLocalhost)
        //        {
        //            resultLiskurl = JsonConvert.DeserializeObject<PeersResponse>(jsonResultLiskUrl);

                 
        //            if (!resultLiskurl.success)
        //            {
        //                errorMessage_GetMaxPeerBlockV2_lisk += "ERROR GetMaxPeerBlockV2: result.success IS FALSE TO " + server.userName;
        //            }

        //            heightLiskurl = resultLiskurl.peers.Max(s => s.height).Value;
        //        }
        //        else
        //        {
        //            errorMessage_GetMaxPeerBlockV2_localhost += "ERROR GetMaxPeerBlockV2: jsonResult is empty ";
        //        }
        //        #endregion

        //         maxHeight = heightLocalhost.Value > heightLiskurl.Value ? heightLocalhost.Value : heightLiskurl.Value;


        //    }
        //    catch (Exception ex)
        //    {
        //        errorMessage_GetMaxPeerBlockV2_localhost = "\r\nEXCEPTION Monitor BlockChainStatus " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;

        //        maxHeight = 0;

        //    }

        //    error_GetMaxPeerBlockV2 = errorMessage_GetMaxPeerBlockV2_localhost  + errorMessage_GetMaxPeerBlockV2_lisk;


           

        //    return maxHeight;


        //}

        public int GetMaxPeerBlock_V2(out string error_GetMaxPeerBlockV2, Servers server,SshClient client)
        {
            int maxHeight = 0;
            int? heightLocalhost = 0;
            int? heightLiskurl = 0;
            string errorMessage_GetMaxPeerBlockV2_localhost = "";
            //string errorMessage_GetMaxPeerBlockV2_lisk = "";


            //string comandLiskUrl = "curl -k -X GET " + liskBaseUrl + "/api/peers?state=2&height>0";
            //string jsonResultLiskUrl = ExecuteComandOverSSH_V2(server, comandLiskUrl, out errorMessage_GetMaxPeerBlockV2_lisk, client);

            string comandLocalhost = "curl --connect-timeout 3 -k -X GET http://localhost:" + server.serverPort.ToString().Trim() + "/api/peers?state=2&height>0";
            string jsonResultLocalhost = ExecuteComandOverSSH_V2(server, comandLocalhost, out errorMessage_GetMaxPeerBlockV2_localhost, client);


            PeersResponse resultLocalhost = new PeersResponse();
         //   PeersResponse resultLiskurl = new PeersResponse();

            try
            {
                errorMessage_GetMaxPeerBlockV2_localhost = "";
               // errorMessage_GetMaxPeerBlockV2_lisk = "";
                #region localHost
                if (jsonResultLocalhost != string.Empty)
                {
                    resultLocalhost = JsonConvert.DeserializeObject<PeersResponse>(jsonResultLocalhost);


                    if (!resultLocalhost.success)
                    {
                        errorMessage_GetMaxPeerBlockV2_localhost += "ERROR GetMaxPeerBlockV2: result.success IS FALSE TO " + server.userName;
                    }

                    heightLocalhost = resultLocalhost.peers.Max(s => s.height).Value;
                }
                else
                {
                    errorMessage_GetMaxPeerBlockV2_localhost += "ERROR GetMaxPeerBlockV2: jsonResult is empty ";
                }
                #endregion

                //#region liskurl
                //heightLiskurl = 0;
                //if (jsonResultLiskUrl != string.Empty)
                //{
                //    resultLiskurl = JsonConvert.DeserializeObject<PeersResponse>(jsonResultLiskUrl);


                //    if (!resultLiskurl.success)
                //    {
                //        errorMessage_GetMaxPeerBlockV2_lisk += "ERROR GetMaxPeerBlockV2: result.success IS FALSE TO " + server.userName;
                //    }

                //    heightLiskurl = resultLiskurl.peers.Max(s => s.height).Value;
                //}
                //else
                //{
                //    errorMessage_GetMaxPeerBlockV2_localhost += "ERROR GetMaxPeerBlockV2: jsonResult is empty ";
                //}
                //#endregion

                maxHeight = heightLocalhost.Value;//> heightLiskurl.Value ? heightLocalhost.Value : heightLiskurl.Value;


            }
            catch (Exception ex)
            {
                errorMessage_GetMaxPeerBlockV2_localhost = "\r\nEXCEPTION Monitor BlockChainStatus " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;

               

            }
            finally
            {
                maxHeight = heightLocalhost.Value;//> heightLiskurl.Value ? heightLocalhost.Value : heightLiskurl.Value;
                if (maxHeight > 0)
                {

                    error_GetMaxPeerBlockV2 = "";

                }
                else
                {
                    error_GetMaxPeerBlockV2 = errorMessage_GetMaxPeerBlockV2_localhost;// + errorMessage_GetMaxPeerBlockV2_lisk;
                }
            }

           




            return maxHeight;


        }


        public BlockChainStatus GetBlockChainStatusAPI(Servers server, out string errorMessage)
        {

            string baseUrl = "http://" + server.serverIP.Trim() + ":" + server.serverPort.ToString().Trim();
            string jsonResult;

            errorMessage = "";
            BlockChainStatus result = new BlockChainStatus();
            if (!baseUrl.EndsWith("/"))
                baseUrl = baseUrl.Trim() + "/";

            try
            {
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(baseUrl + "api/loader/status/sync");
                http.Method = "GET";
                WebResponse response = http.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    jsonResult = sr.ReadToEnd();
                }
                result = JsonConvert.DeserializeObject<BlockChainStatus>(jsonResult);

                if (!result.success)
                {
                    errorMessage += "ERROR GetBlockChainStatus: result.success IS FALSE TO " + server.userName;
                }


            }
            catch (Exception ex)
            {
                errorMessage = "\r\nEXCEPTION Monitor BlockChainStatus " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;
                if (result == null)
                {
                    result = new BlockChainStatus();
                    result.height = 0;
                    result.success = false;
                }

            }

            return result;
        }

        //public BlockChainStatus GetBlockChainStatusSSH(Servers server, out string errorMessage_GetBlockChainStatusSSH)
        //{
        //    //curl -k -X GET http://localhost:8000/api/loader/status/sync
        //   // string baseUrl = http + "://localhost:" + server.serverPort.ToString().Trim();


        //    string comand_GetBlockChainStatusSSH = "curl --connect-timeout 3 -k -X GET http://localhost:" + server.serverPort.ToString().Trim() + "/api/loader/status/sync";
        //    errorMessage_GetBlockChainStatusSSH = "";
        //    string jsonResult=ExecuteComandOverSSH(server,comand_GetBlockChainStatusSSH,out errorMessage_GetBlockChainStatusSSH);

           
        //    BlockChainStatus result_GetBlockChainStatusSSH = new BlockChainStatus();
          
        //    try
        //    {
        //      if(jsonResult!=string.Empty)
        //        {
        //            result_GetBlockChainStatusSSH = JsonConvert.DeserializeObject<BlockChainStatus>(jsonResult);
        //            errorMessage_GetBlockChainStatusSSH = string.Empty;
        //            if (!result_GetBlockChainStatusSSH.success)
        //            {
        //                errorMessage_GetBlockChainStatusSSH += "ERROR GetBlockChainStatus: result.success IS FALSE TO " + server.userName;
        //            }
        //        }
        //       else
        //        {
        //            errorMessage_GetBlockChainStatusSSH += "ERROR GetBlockChainStatus: jsonResult is empty ";
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        errorMessage_GetBlockChainStatusSSH = "\r\nEXCEPTION Monitor BlockChainStatus " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;
        //        if (result_GetBlockChainStatusSSH == null)
        //        {
        //            result_GetBlockChainStatusSSH = new BlockChainStatus();
        //            result_GetBlockChainStatusSSH.height = 0;
        //            result_GetBlockChainStatusSSH.success = false;
        //        }

        //    }

        //    return result_GetBlockChainStatusSSH;
        //}

        public BlockChainStatus GetBlockChainStatusSSH_V2(Servers server, out string errorMessage_GetBlockChainStatusSSH,SshClient client)
        {
            //curl -k -X GET http://localhost:8000/api/loader/status/sync
            // string baseUrl = http + "://localhost:" + server.serverPort.ToString().Trim();


            string comand_GetBlockChainStatusSSH = "curl --connect-timeout 3 -k -X GET http://localhost:" + server.serverPort.ToString().Trim() + "/api/loader/status/sync";
            errorMessage_GetBlockChainStatusSSH = "";
            string jsonResult = ExecuteComandOverSSH_V2(server, comand_GetBlockChainStatusSSH, out errorMessage_GetBlockChainStatusSSH, client);


            BlockChainStatus result_GetBlockChainStatusSSH = new BlockChainStatus();

            try
            {
                if (jsonResult != string.Empty)
                {
                    result_GetBlockChainStatusSSH = JsonConvert.DeserializeObject<BlockChainStatus>(jsonResult);
                    errorMessage_GetBlockChainStatusSSH = string.Empty;
                    if (!result_GetBlockChainStatusSSH.success)
                    {
                        errorMessage_GetBlockChainStatusSSH += "ERROR GetBlockChainStatus: result.success IS FALSE TO " + server.userName;
                    }
                }
                else
                {
                    errorMessage_GetBlockChainStatusSSH += "ERROR GetBlockChainStatus: jsonResult is empty ";
                }


            }
            catch (Exception ex)
            {
                errorMessage_GetBlockChainStatusSSH = "\r\nEXCEPTION Monitor BlockChainStatus " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message;
                if (result_GetBlockChainStatusSSH == null)
                {
                    result_GetBlockChainStatusSSH = new BlockChainStatus();
                    result_GetBlockChainStatusSSH.height = 0;
                    result_GetBlockChainStatusSSH.success = false;
                }

            }

            return result_GetBlockChainStatusSSH;
        }

      

        public Block GetLatestBlockSSH(Servers server, string generatorPublicKey, out string error_GetLatestBlockSSH)
        {
            //time passed seconds beteween blocks load, in blockchain
            int blockInterval = cfgCurrent.blocksInterval;
           
            
            // curl -k -X GET "http://localhost:#port#/api/blocks?generatorPublicKey=#publickey#&orderBy=height:desc&limit=1"

            error_GetLatestBlockSSH = "";


           string comand = System.IO.File.ReadAllText(@"comands\getlatestblock.txt");
            comand = comand.Replace("#publickey#", generatorPublicKey.Trim());
            comand = comand.Replace("#port#", server.serverPort.ToString().Trim());
            string jsonResult = ExecuteComandOverSSH(server, comand, out error_GetLatestBlockSSH);

            BlockResponse result_GetLatestBlockSSH = new BlockResponse();
            Block resp_GetLatestBlockSSH = new Block();
          
            try
            {
                if (jsonResult != string.Empty)
                {
                    result_GetLatestBlockSSH = JsonConvert.DeserializeObject<BlockResponse>(jsonResult);
                    error_GetLatestBlockSSH = string.Empty;
                    if (result_GetLatestBlockSSH.blocks.Count > 0)
                        resp_GetLatestBlockSSH = result_GetLatestBlockSSH.blocks[0];


                    resp_GetLatestBlockSSH.minutsPassedSinceLastBlock = Math.Ceiling(Convert.ToDouble((server.blockChainHeight - resp_GetLatestBlockSSH.height) * blockInterval / 60));
                }
                else
                {
                    error_GetLatestBlockSSH += "ERROR GetLatestBlockSSH " + server.serverName +  " jsonResult is empty ";
                }
                  

            }
            catch (Exception ex)
            {
                // WriteLog("EXCEPTION Monitor " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
                error_GetLatestBlockSSH += "ERROR GetLatestBlockSSH " + server.serverName + " ex:" + ex.Message + " jsonResult:" + jsonResult;
                SendEmail("ERROR GetLatestBlockSSH " + server.serverName +  DateTime.Now.ToString(), emailto, "ERROR GetLatestBlock:"  + " ex:" + ex.Message + " result:" + jsonResult);
                if (result_GetLatestBlockSSH == null)
                {
                    result_GetLatestBlockSSH = new BlockResponse();
                    resp_GetLatestBlockSSH = new Block();
                    resp_GetLatestBlockSSH.timestamp = 0;
                    resp_GetLatestBlockSSH.minutsPassedSinceLastBlock = 0;

                }

            }

            return resp_GetLatestBlockSSH;
        }

        public Block GetLatestBlockSSH_V2(Servers server, string generatorPublicKey, out string error_GetLatestBlockSSH,SshClient client)
        {
            //time passed seconds beteween blocks load, in blockchain
            int blockInterval = cfgCurrent.blocksInterval;


            // curl -k -X GET "http://localhost:#port#/api/blocks?generatorPublicKey=#publickey#&orderBy=height:desc&limit=1"

            error_GetLatestBlockSSH = "";


            string comand = System.IO.File.ReadAllText(@"comands\getlatestblock.txt");
            comand = comand.Replace("#publickey#", generatorPublicKey.Trim());
            comand = comand.Replace("#port#", server.serverPort.ToString().Trim());
            string jsonResult = ExecuteComandOverSSH_V2(server, comand, out error_GetLatestBlockSSH, client);

            BlockResponse result_GetLatestBlockSSH = new BlockResponse();
            Block resp_GetLatestBlockSSH = new Block();

            try
            {
                if (jsonResult != string.Empty)
                {
                    result_GetLatestBlockSSH = JsonConvert.DeserializeObject<BlockResponse>(jsonResult);
                    error_GetLatestBlockSSH = string.Empty;
                    if (result_GetLatestBlockSSH.blocks.Count > 0)
                        resp_GetLatestBlockSSH = result_GetLatestBlockSSH.blocks[0];


                    //resp_GetLatestBlockSSH.minutsPassedSinceLastBlock = Math.Ceiling(Convert.ToDouble(((server.blockChainHeight - resp_GetLatestBlockSSH.height) * blockInterval) / 60));
                    resp_GetLatestBlockSSH.minutsPassedSinceLastBlock = Math.Floor( Convert.ToDouble(((server.blockChainHeight - resp_GetLatestBlockSSH.height) * blockInterval) / 60));
                }
                else
                {
                    error_GetLatestBlockSSH += "ERROR GetLatestBlockSSH " + server.serverName + " jsonResult is empty ";
                }


            }
            catch (Exception ex)
            {
                // WriteLog("EXCEPTION Monitor " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
                error_GetLatestBlockSSH += "ERROR GetLatestBlockSSH " + server.serverName + " ex:" + ex.Message + " jsonResult:" + jsonResult;
                SendEmail("ERROR GetLatestBlockSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR GetLatestBlock:" + " ex:" + ex.Message + " result:" + jsonResult);
                if (result_GetLatestBlockSSH == null)
                {
                    result_GetLatestBlockSSH = new BlockResponse();
                    resp_GetLatestBlockSSH = new Block();
                    resp_GetLatestBlockSSH.timestamp = 0;
                    resp_GetLatestBlockSSH.minutsPassedSinceLastBlock = 0;

                }

            }

            return resp_GetLatestBlockSSH;
        }

        public int GetForgingPosition(Servers server, string generatorPublicKey, SshClient client)
        {
            int position =-1;
            string error = "";
            GetNextForgersResponse result = new GetNextForgersResponse();
            if (server.isRebuilding==false && server.isMainServer==true)
            {

                try
                {
                    string comandLiskUrl = "curl --connect-timeout 3 -k -X GET http://" + server.serverIP + ":" + server.serverPort + "/api/delegates/getNextForgers";
                    string jsonResult = ExecuteComandOverSSH_V2(server, comandLiskUrl, out error, client);

                    if (jsonResult != string.Empty)
                    {
                        result = JsonConvert.DeserializeObject<GetNextForgersResponse>(jsonResult);

                        if (result != null && result.delegates != null && result.delegates.Count() > 0)
                        {
                            position = Array.FindIndex(result.delegates, row => row.Contains(generatorPublicKey));
                        }


                    }
                }
                catch
                {

                }
              


         }

            return position;

        }

        //public Block _GetLatestBlockAPI_(Servers server, string generatorPublicKey, out string error)
        //{
        //    error = "";
          

        //    string baseUrl = string.Format("http://{0}:{1}", server.serverIP.Trim(), server.serverPort.ToString().Trim());
        //    string jsonResult;

        //    BlockResponse result = new BlockResponse();
        //    Block resp = new Block();
        //    if (!baseUrl.EndsWith("/"))
        //        baseUrl = baseUrl.Trim() + "/";

        //    try
        //    {
        //        //string url = string.Format(baseUrl + "api/blocks?&generatorPublicKey={0}&limit=1&orderBy=timestamp", generatorPublicKey);
        //        string url = string.Format(baseUrl + "api/blocks?&generatorPublicKey={0}&limit=1&orderBy=timestamp", generatorPublicKey.Trim());

        //        HttpWebRequest http = (HttpWebRequest)WebRequest.Create(url);
        //        http.Method = "GET";
        //        WebResponse response = http.GetResponse();

        //        using (Stream stream = response.GetResponseStream())
        //        {
        //            StreamReader sr = new StreamReader(stream);
        //            jsonResult = sr.ReadToEnd();
        //        }
        //        result = JsonConvert.DeserializeObject<BlockResponse>(jsonResult);

        //        if (result.blocks.Count > 0)
        //            resp = result.blocks[0];
                

        //        resp.minutsPassedSinceLastBlock = Math.Ceiling(Convert.ToDouble((server.blockChainHeight - resp.height) * 12 / 60));

        //    }
        //    catch (Exception ex)
        //    {
        //        // WriteLog("EXCEPTION Monitor " + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
        //        error = "ERROR GetLatestBlock " + baseUrl + " ex:" + ex.Message;
        //        SendEmail("ERROR GetLatestBlock " + DateTime.Now.ToString(), emailto, "ERROR GetLatestBlock:" + baseUrl + " ex:" + ex.Message);
        //        if (result == null)
        //        {
        //            result = new BlockResponse();
        //            resp = new Block();
        //            resp.timestamp = 0;
        //            resp.minutsPassedSinceLastBlock = 1000;

        //        }

        //    }

        //    return resp;
        //}

        // public string _RebuildNodeSSH_(Servers server,bool allow)
        //{
        //    StringBuilder sms = new StringBuilder();
        //    try
        //    {
        //        if (allow)
        //        {

        //            commandrebuild = commandrebuild.Replace("#", "&&");
        //            commanddeleteLogFiles = commanddeleteLogFiles.Replace("#", "&&");

        //            string respText = "";

        //            if (commanddeleteLogFiles != string.Empty)
        //            {
                       
        //                using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
        //                {
        //                    client.Connect();
        //                    var resp = client.RunCommand(commanddeleteLogFiles);
        //                    sms.Append("\r\nRESULT:" + resp.Result);
        //                    sms.Append("\r\nERROR:" + resp.Error);

        //                    respText = resp.Result + " " + resp.Error;
        //                    client.RunCommand("exit");
        //                    client.Disconnect();
        //                }
        //            }

        //            if (commandrebuild != string.Empty)
        //            {

        //                // Execute (SHELL) Commands
        //                using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
        //                {
        //                    client.Connect();
        //                    var resp = client.RunCommand(commandrebuild);
        //                    sms.Append("\r\nRESULT:" + resp.Result);
        //                    sms.Append("\r\nERROR:" + resp.Error);

        //                    respText = resp.Result + " " + resp.Error; ;

        //                    client.RunCommand("exit");
        //                    client.Disconnect();
        //                }

        //                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + "REBUILD_START_" + server.serverName + "_"+ DateTime.Now.Ticks.ToString() +  ".txt", "Rebuild at " + DateTime.Now.ToString() + "\r\n" + respText + " \r\n Server Data:" + JsonConvert.SerializeObject(server));

        //            }
        //            else
        //            {
        //                sms.Append("\r\n### NO COMMAND ###");
        //            }

        //        }
        //        else
        //        {
        //            sms.Append("\r\nREBUILD LISK NOT ENABLED!!!!");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        sms.Append("\r\nEXCEPTION Monitor " + server.serverName + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
        //        SendEmail("ERROR RebuildNodeSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR RebuildNodeSSH: " + ex.Message);
        //    }
        //    finally
        //    {
        //        // StartNode(ip, user, pwd);
        //    }

        //    return sms.ToString();


        //}

        public string RebuildNodeClosePortSSH(Servers server, bool allow)
        {
            StringBuilder sms = new StringBuilder();
            // var client = new SshClient(server.serverIP, server.userName, server.userPassword);
            var client = this.GetConection(server);
            try
            {
               
               // client.Connect();
                if (allow)
                {
   
                    string rebuild = "";
                    server.lastRebuild = DateTime.Now;
                    if(server.serverPort==7000)
                    {
                        rebuild = commandrebuild.Replace("#", "&&");
                        commanddeleteLogFiles = commanddeleteLogFiles.Replace("#", "&&");
                    }
                    else
                    {
                        if(usesnapshot)
                        {
                            rebuild=commandrebuildsnapshot.Replace("#", "&&");
                        }
                        else
                        {
                            rebuild = commandrebuild.Replace("#", "&&");
                        }
                        
                        commanddeleteLogFiles = commanddeleteLogFiles.Replace("#", "&&");
                    }
                   
                   
                    string respText = "";

                    if (commanddeleteLogFiles != string.Empty)
                    {
                       
                           
                            var resp = client.RunCommand(commanddeleteLogFiles);
                            sms.Append("\r\nRESULT deleteLogFiles :" + resp.Result);
                            sms.Append("\r\nERROR deleteLogFiles :" + resp.Error);

                           
                            respText += resp.Result + " " + resp.Error;
                    
                    }

                    if (rebuild != string.Empty)
                    {

                      
                            var resp = client.RunCommand(rebuild);

                            sms.Append("\r\nRESULT rebuild :" + resp.Result);
                            sms.Append("\r\nERROR rebuild :" + resp.Error);

                            respText += resp.Result + " " + resp.Error; ;

                     

                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + DateTime.Now.Ticks.ToString() + "_rebuild_" + server.serverName + ".txt", "Rebuild at " + DateTime.Now.ToString() + "\r\n" + respText);


                    
                            var ufwEnable = client.RunCommand("echo '#password#' | sudo -S ufw status".Replace("#password#",server.userPassword.Trim()));
                            sms.Append("\r\nRESULT ufw status :" + ufwEnable.Result);
                            sms.Append("\r\nERROR ufw status :" + ufwEnable.Error);

                        if (ufwEnable != null && !ufwEnable.Result.Contains(": active"))
                        {
                            respText += "\r\nERROR: UFW DISABLE IN " + server.serverIP;
                            SendEmail("ERROR: UFW DISABLE IN " + server.serverIP, emailto, respText);
                        }
                        else
                        {
                            //echo '#password#' | sudo -S ufw deny 8000 && echo '#password#' | sudo -S ufw reload 
                            string comandClosePort = "echo '#password#' | sudo -S ufw deny " + server.serverPort.ToString().Trim() + " && echo '#password#' | sudo -S ufw reload";
                            //string comandClosePort = "sudo ufw deny " + server.serverPort.ToString().Trim() + " && sudo ufw reload";

                            comandClosePort = comandClosePort.Replace("#password#", server.userPassword.Trim());
                            var resp2 = client.RunCommand(comandClosePort);

                            sms.Append("\r\nRESULT ClosePort :" + resp2.Result);
                            sms.Append("\r\nERROR ClosePort :" + resp2.Error);


                            respText += resp2.Result + " " + resp2.Error;
                        }
                          
                         
                     

                    }
                    else
                    {
                        sms.Append("\r\n### NO COMMAND ###");
                    }

                }
                else
                {
                    sms.Append("\r\nREBUILD LISK NOT ENABLED!!!!");
                }

            }
            catch (Exception ex)
            {
                sms.Append("\r\nEXCEPTION Monitor " + server.serverName + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
                SendEmail("ERROR RebuildNodeClosePortSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR RebuildNodeClosePortSSH: " + ex.Message + " respText: " + sms.ToString());
            }
            finally
            {
                //client.RunCommand("exit");
                //client.Disconnect();
            }

            return sms.ToString();


        }

        public string OpenPortSSH(Servers server)
        {
            StringBuilder sms = new StringBuilder();
            string respText = string.Empty;
            try
            {

                //open port
                var client = this.GetConection(server);
                //using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
                //{
                //    client.Connect();

                    // teste if is enable if not notify admin to enable on ip this command  sudo ufw enable 
                    // var ufwEnable = client.RunCommand("sudo ufw status");
                    var ufwEnable = client.RunCommand("echo '#password#' | sudo -S ufw status".Replace("#password#", server.userPassword.Trim()));

                    sms.Append("\r\nRESULT ufw status :" + ufwEnable.Result);
                    sms.Append("\r\nERROR ufw status :" + ufwEnable.Error);

                    if (ufwEnable != null && !ufwEnable.Result.Contains(": active"))
                    {
                        respText += "\r\nWARNING: UFW DISABLE IN " + server.serverIP;
                        sms.AppendLine(respText);
                        //SendEmail("ERROR: UFW DISABLE IN " + server.serverIP, emailto, respText);
                    }
                    else
                {
                    //string comandOpenPort = "sudo ufw allow " + server.serverPort.ToString().Trim() + " && " + " sudo ufw reload";
                    //echo '#password#' | sudo -S ufw allow 8000 && echo '#password#' | sudo -S ufw reload
                    string comandOpenPort = "echo '#password#' | sudo -S ufw allow " + server.serverPort.ToString().Trim() + " && " + " echo '#password#' | sudo -S ufw reload";

                    comandOpenPort = comandOpenPort.Replace("#password#", server.userPassword.Trim());

                    var resp = client.RunCommand(comandOpenPort);
                    sms.Append("\r\nRESULT OpenPort :" + resp.Result);
                    sms.Append("\r\nERROR OpenPort :" + resp.Error);

                    // client.RunCommand("exit");
                    respText += resp.Result + " " + resp.Error;
                }

                  
                 
                //    client.Disconnect();
                //}


            }
            catch (Exception ex)
            {
                sms.Append("\r\nEXCEPTION OpenPortSSH " + server.serverName + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);

                if (!sms.ToString().Contains("Message type 80 is not valid"))
                    SendEmail("ERROR OpenPortSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR OpenPortSSH: " + ex.Message);
            }
            finally
            {
                // StartNode(ip, user, pwd);
            }

            return sms.ToString();


        }

        public string ExecuteComandOverSSH(Servers server, string comand,out string error)
        {
            string result = "";
             error = "";
            try
            {
               var client= this.GetConection(server);
                    
                    if (comand != string.Empty)
                    {

                        // Execute (SHELL) Commands
                        //using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
                        //{
                        
                           // client.Connect();
                            var resp = client.RunCommand(comand);
                            result=resp.Result;
                             error= resp.Error;

                             //client.RunCommand("exit");

                             //client.Disconnect();
                       // }

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

                if (!error.Contains("Message type 80 is not valid"))
                    SendEmail("ERROR ExecuteComandOverSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR ExecuteComand: " + ex.Message + " comand" + comand + " result:" + result);
            }
            finally
            {
                // StartNode(ip, user, pwd);
            }

            return result;


        }

        public string ExecuteComandOverSSH_V2(Servers server, string comand, out string error, SshClient client)
        {
            string result = "";
            error = "";
            try
            {

                if (comand != string.Empty)
                {

                    // Execute (SHELL) Commands
                    //using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
                    //{

                    //    client.Connect();
                        var resp = client.RunCommand(comand);
                        result = resp.Result;
                        error = resp.Error;
                    //}

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

                if (!error.Contains("Message type 80 is not valid"))
                    SendEmail("ERROR ExecuteComandOverSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR ExecuteComand: " + ex.Message + " comand" + comand + " result:" + result);
            }
            finally
            {
                // StartNode(ip, user, pwd);
            }

            return result;


        }


        public string StopAndStartNodeSSH(Servers server)
        {
            StringBuilder sms = new StringBuilder();
            try
            {
                    commandrestart = commandrestart.Replace("#", "&&");

                    string respText = "";

                    if (commandreload != string.Empty)
                    {

                    var client = this.GetConection(server);
                   
                            var resp = client.RunCommand(commandrestart);
                            sms.Append("\r\nRESULT:" + resp.Result);
                            sms.Append("\r\nERROR:" + resp.Error);

                            respText = resp.Result + " " + resp.Error; ;
                    

                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + DateTime.Now.Ticks.ToString() + "_reload_" + server.serverName + ".txt", "Stop Start at " + DateTime.Now.ToString() + "\r\n" + respText);

                    }
                    else
                    {
                        sms.Append("\r\n### NO COMMAND ###");
                    }

              

            }
            catch (Exception ex)
            {
                sms.Append("\r\nEXCEPTION ReLoadNodeSSH " + server.serverName + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
                SendEmail("ERROR ReLoadNodeSSH " + server.serverName + DateTime.Now.ToString(), emailto, "ERROR ReLoadNodeSSH: " + ex.Message);
            }
            finally
            {
               
            }

            return sms.ToString();


        }

        //public string ReStartNodeSSH(Servers server)
        //{
        //    StringBuilder sms = new StringBuilder();
           
        //    string restart = commandrestart;
        //    try
        //    {
        //        if (restart != string.Empty)
        //        {
        //            var respText = "";
        //            // Execute (SHELL) Commands
        //            using (var client = new SshClient(server.serverIP, server.userName, server.userPassword))
        //            {
        //                client.Connect();
        //                var resp = client.RunCommand(restart);
        //                sms.Append("\r\nRESULT:" + resp.Result);
        //                sms.Append("\r\nERROR:" + resp.Error);

        //                respText = resp.Result + " " + resp.Error;
        //                client.RunCommand("exit");
        //                client.Disconnect();
        //            }

        //            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\logs\\" + DateTime.Now.Ticks.ToString() + "_restart_" + server.serverName + ".txt", "Restart at " + DateTime.Now.ToString() + "\r\n" + respText);

        //            //lastRebuild = DateTime.Now;
        //        }
        //        else
        //        {
        //            sms.Append("\r\n### NO COMMAND ###");
        //        }




        //    }
        //    catch (Exception ex)
        //    {
        //        sms.Append("\r\nEXCEPTION Monitor ReStartNodeSSH" + server.serverName + DateTime.Now.ToString() + " EXCEPTION:" + ex.Message);
        //        SendEmail("ERROR StartNode ReStartNodeSSH" + server.serverName + DateTime.Now.ToString(), emailto, "ERROR StartNode: " + ex.Message);
        //    }

        //    return sms.ToString();


        //}

        public void SaveToDatabase(List<DelegateServers> data,string enviroment)
        {
            try
            {
                if (!string.IsNullOrEmpty(saveToSqlServer) && saveToSqlServer == "1")
                {
                    using (DatabaseDataContext bd = new DatabaseDataContext())
                    {
                        foreach (DelegateServers account in data)
                        {
                            var existingAccount = bd.Accounts.Where(s => s.Enviroment == enviroment && s.Username == account.account.username).FirstOrDefault();
                            if (existingAccount == null)
                            {
                                #region new account
                                existingAccount = new LiskLog.Database.Account();
                                existingAccount.Address = account.account.address;
                                existingAccount.Balance = account.account.balance;
                                existingAccount.Email = account.account.email;
                                existingAccount.EnableMonitor = account.account.enableMonitor;
                                existingAccount.Enviroment = enviroment;
                                existingAccount.LastUpdate = DateTime.Now;
                                existingAccount.Missedblocks = account.account.missedblocks;
                                existingAccount.Producedblocks = account.account.producedblocks;
                                existingAccount.Productivity = account.account.productivity;
                                existingAccount.Rate = account.account.rate;
                                existingAccount.Username = account.account.username;
                                existingAccount.Servers = new System.Data.Linq.EntitySet<Server>();

                                foreach (Servers server in account.servers)
                                {
                                    LiskLog.Database.Server s = new LiskLog.Database.Server();
                                    s.BlockDiff = (int)server.blockDiff;
                                    s.Error = server.error;
                                    s.IsChainSynced = server.isChainSynced;
                                    s.IsChainSyncing = server.isChainSyncing;
                                    s.IsEnable = server.isEnable;
                                    s.isForging = server.isForging;
                                    s.IsRebuilding = server.isRebuilding;
                                    s.LastBlockForgedHeight = server.LastBlockForgedHeight;
                                    s.LastBlockMinutsPassedSince = (decimal)server.LastBlockMinutsPassedSince;
                                    s.LastRebuild = server.lastRebuild;
                                    s.ServerIP = server.serverIP;
                                    s.ServerName = server.serverName;
                                    s.ServerPort = server.serverPort;
                                    s.LastUpdateDate = DateTime.Now;
                                    s.BlockChainHeight = (int)server.blockChainHeight;
                                    s.isMainServer = server.isMainServer;
                                    s.BlockChainHeightPeer = (int)server.maxPeerBlock;


                                    existingAccount.Servers.Add(s);
                                }

                                bd.Accounts.InsertOnSubmit(existingAccount);
                                #endregion
                            }
                            else //exist account
                            {
                                existingAccount.Address = account.account.address;
                                existingAccount.Balance = account.account.balance;
                                existingAccount.Email = account.account.email;
                                existingAccount.EnableMonitor = account.account.enableMonitor;
                                existingAccount.Enviroment = enviroment;
                                existingAccount.LastUpdate = DateTime.Now;
                                existingAccount.Missedblocks = account.account.missedblocks;
                                existingAccount.Producedblocks = account.account.producedblocks;
                                existingAccount.Productivity = account.account.productivity;
                                existingAccount.Rate = account.account.rate;
                                existingAccount.Username = account.account.username;


                                bd.Servers.DeleteAllOnSubmit(existingAccount.Servers);

                                existingAccount.Servers = new System.Data.Linq.EntitySet<Server>();
                                foreach (Servers server in account.servers)
                                {
                                    LiskLog.Database.Server s = new LiskLog.Database.Server();
                                    s.BlockDiff = (int)server.blockDiff;
                                    s.Error = server.error;
                                    s.IsChainSynced = server.isChainSynced;
                                    s.IsChainSyncing = server.isChainSyncing;
                                    s.IsEnable = server.isEnable;
                                    s.isForging = server.isForging;
                                    s.IsRebuilding = server.isRebuilding;
                                    s.LastBlockForgedHeight = server.LastBlockForgedHeight;
                                    s.LastBlockMinutsPassedSince = (decimal)server.LastBlockMinutsPassedSince;
                                    s.LastRebuild = server.lastRebuild;
                                    s.ServerIP = server.serverIP;
                                    s.ServerName = server.serverName;
                                    s.ServerPort = server.serverPort;
                                    s.LastUpdateDate = DateTime.Now;
                                    s.BlockChainHeight = (int)server.blockChainHeight;
                                    s.isMainServer = server.isMainServer;
                                    s.BlockChainHeightPeer = (int)server.maxPeerBlock;
                                    existingAccount.Servers.Add(s);
                                }

                            }

                        }

                        bd.SubmitChanges();


                    }
                }
            }
            catch
            {

            }
            


        }
        
        private string RequestInfo(string ip, string user, string pwd, string[] v)
        {
            throw new NotImplementedException();
        }
    }


 



}
