using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiskLog.Objects
{
    #region Contracts

    public class LiskDelegates
    {
        public bool success { get; set; }
        public List<Contracts> delegates { get; set; }

    }

    public class Conections
    {
        public Servers servers { get; set; }
        public SshClient SshClient { get; set; }

    }
    public class Contracts
    {
        public string username { get; set; }
        public string address { get; set; }
        public string publicKey { get; set; }
        public string vote { get; set; }
        public int producedblocks { get; set; }
        public int missedblocks { get; set; }
        public string virgin { get; set; }
        public int rate { get; set; }
        public decimal productivity { get; set; }

        public int NumberVotesMade { get; set; }

        public int NumberVotesReceived { get; set; }

        public decimal Forged { get; set; }

        public int NumberVotesReceivedWithoutTop200 { get; set; }
        public bool VotedOnMe { get; set; }

        decimal _balance;
        public decimal balance
        {
            get { return this._balance > 0 ? this._balance : 0; }
            set
            {
                this._balance = value;
            }
        }




    }

    public class BlockChainStatus
    {
        public bool success { get; set; }
        public bool syncing { get; set; }
        public int blocks { get; set; }
        public int height { get; set; }

        public string broadhash { get; set; }

        public string consensus { get; set; }


    }
    public class DelegateServers
    {
        public DateTime lastServerSwitch { get; set; }
        public Accounts account { get; set; }

        public List<Servers> servers { get; set; }


    }

    public class Accounts
    {
        public string username { get; set; }
        public bool enableMonitor { get; set; }
      
        public string address { get; set; }
        public int rate { get; set; }
        public string generatorPublicKey { get; set; }
        public string email { get; set; }

        public decimal missedblocks { get; set; }

        public decimal producedblocks { get; set; }
        public decimal balance { get; set; }
        public decimal productivity { get; set; }
        public string passPhrase { get; set; }

      


    }
    public class Servers : ICloneable
    {
        //isMainServer
        public bool isMainServer { get; set; }
        public string serverName { get; set; }
        public bool isEnable { get; set; }
        public string serverIP { get; set; }
        public int serverPort { get; set; }
        public string userName { get; set; }
    
        ///api/loaders/status/sync
     
        
        public int consensus { get; set; }
        public double LastBlockMinutsPassedSince { get; set; }
        public int consensusFromLog { get; set; }
        public decimal consensusAvg { get; set; }

        public string consensusStr { get; set; }

       

        public string broadhash { get; set; }
        public decimal LastBlockForgedHeight { get; set; }
        
        public DateTime LastBlockLastNofication { get; set; }

        //REBUILD
        public bool enableRebuild { get; set; }
        public string email { get; set; }
        public DateTime lastRebuild { get; set; }

        public DateTime lastRebbot { get; set; }
        public bool isRebuilding { get; set; }
        public bool isForging { get; set; }

        //BlockChainHeight
        public decimal blockChainHeight { get; set; }
        //maxPeerBlock
        public decimal maxPeerBlock { get; set; }
        //"blockDiff":0
        public decimal blockDiff { get; set; }

   
        public bool isChainSynced { get; set; }

        public bool isChainSyncing { get; set; }

        public string error { get; set; }

        public string tailLog { get; set; }
        public int forgingPositionCurrenSlot { get; set; }

        public string userPassword { get; set; }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }


    public class BlockChainHeight
    {
        public bool success { get; set; } // bigint

        public int height { get; set; } // bigint
    }

    public class BlockResponse
    {
        public bool success { get; set; } // bigint

        public List<Block> blocks { get; set; } // bigint
    }

    public class Request
    {
        public string secret { get; set; }
    }

    public class Block
    {
        public decimal id { get; set; }
        public decimal version { get; set; }
        public long timestamp { get; set; }
        public decimal height { get; set; }
        public string previousBlock { get; set; }
        public decimal numberOfTransactions { get; set; }
        public decimal totalAmount { get; set; }
        public decimal totalFee { get; set; }
        public decimal reward { get; set; }
        public decimal payloadLength { get; set; }
        public string payloadHash { get; set; }
        public string generatorPublicKey { get; set; }
        public string generatorId { get; set; }
        public string blockSignature { get; set; }
        public decimal confirmations { get; set; }
        public decimal totalForged { get; set; }

        public DateTime blockDate { get; set; }
        public double minutsPassedSinceLastBlock { get; set; }

    }

    public class PeersResponse
    {
        public bool success { get; set; } // bigint

        public List<Peers> peers { get; set; }
    }

    public class GetNextForgersResponse
    {
        public bool success { get; set; } // bigint

        public double currentBlock { get; set; }

        public double currentSlot { get; set; }

        public string[] delegates { get; set; }
    }
    public class Peers
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string state { get; set; }
        public string os { get; set; }
        public string version { get; set; }
        public string broadhash { get; set; }

        private int? _height;
        public int? height {
            get
            {
                return _height;
            }
            set
            {
                if (value == null)
                    _height = 0;
                else
                    _height = value;
               
            }
        }
    }
    public class AppConfig
    {
       
        public string peers { get; set; }
        public string liskBaseUrl { get; set; }
        public int blocksInterval { get; set; }

        public string liskFolderName { get; set; }

        public string enviroment { get; set; }

        public int timerticksms { get; set; }
        public int blockdiftorebuild { get; set; }

        public string emailto { get; set; }
        public string emailfrom { get; set; }
        public string emailPassword { get; set; }

        public bool enableEmailNotifications { get; set; }

        public string liskVersionNumber { get; set; }


    }


    #endregion
}
