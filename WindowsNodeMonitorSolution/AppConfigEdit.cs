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
    public partial class AppConfigEdit : Form
    {
        AppConfig currentCfg;
        public AppConfigEdit(AppConfig cfg)
        {
            currentCfg = cfg;
            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            // tooltips
            System.Windows.Forms.ToolTip tt_txt_blocksinterval = new System.Windows.Forms.ToolTip();
            tt_txt_blocksinterval.SetToolTip(this.txt_blocksinterval, "Block chain, block interval in seconds, should be greater than 10 sec.");

            System.Windows.Forms.ToolTip tt_txt_blockdiftorebuild = new System.Windows.Forms.ToolTip();
            tt_txt_blockdiftorebuild.SetToolTip(this.txt_blockdiftorebuild, "Blocks diference from main chain, in order to ativate rebuild.");

            txt_foldername.Text = currentCfg.liskFolderName.Trim();
            txt_blockdiftorebuild.Text = currentCfg.blockdiftorebuild.ToString();
            txt_blocksinterval.Text = currentCfg.blocksInterval.ToString();
            txt_emailfrom.Text = currentCfg.emailfrom.Trim();
            txt_emailpassword.Text = currentCfg.emailPassword.Trim();
            txt_emailto.Text = currentCfg.emailto.Trim();
            txt_enviroment.Text = currentCfg.enviroment.Trim();
            txt_liskurl.Text = currentCfg.liskBaseUrl.Trim();
            txt_peers.Text = currentCfg.peers.Trim();
            txt_timertickms.Text = (currentCfg.timerticksms/ 60000).ToString();
            ck_emailnotifications.Checked = currentCfg.enableEmailNotifications;
            txt_version_Number.Text = currentCfg.liskVersionNumber.Trim();

        }
        private void AppConfig_Load(object sender, EventArgs e)
        {

        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            try
            {
                currentCfg.blockdiftorebuild = Convert.ToInt16(txt_blockdiftorebuild.Text);
                currentCfg.blocksInterval = Convert.ToInt16(txt_blocksinterval.Text);
                currentCfg.emailfrom = txt_emailfrom.Text.Trim();
                currentCfg.emailPassword = txt_emailpassword.Text.Trim();
                currentCfg.emailto = txt_emailto.Text.Trim();
                currentCfg.enviroment = txt_enviroment.Text.Trim();
                currentCfg.liskBaseUrl = txt_liskurl.Text.Trim();
                currentCfg.peers = txt_peers.Text.Trim();
                currentCfg.timerticksms = Convert.ToInt16(txt_timertickms.Text)*60000;
                currentCfg.liskFolderName = txt_foldername.Text.Trim();
                currentCfg.enableEmailNotifications = ck_emailnotifications.Checked;
                currentCfg.liskVersionNumber = txt_version_Number.Text.Trim();

                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: Please validate configuration values!!!");
            }
          
        }

        private void bt_copy_test_Click(object sender, EventArgs e)
        {
            MessageBox.Show("will copy TESTNET peers and url information for your configuration, need to configure servers to use port 7000.");
            txt_enviroment.Text = "TEST NET";
            txt_peers.Text = txt_info_test_peers.Text;
            txt_liskurl.Text = txt_info_test_url.Text.Trim();
            txt_foldername.Text = txt_info_test_foldername.Text.Trim();
        }

        private void bt_copy_main_Click(object sender, EventArgs e)
        {
            MessageBox.Show("will copy MAINNET peers and url information for your configuration, need to configure servers to use port 8000.");
            txt_enviroment.Text = "MAIN NET";
            txt_peers.Text = txt_info_main_peers.Text;
            txt_liskurl.Text = txt_info_main_url.Text.Trim();
            txt_foldername.Text = txt_info_main_foldername.Text.Trim();
        }
    }
}
