using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartServiceHelper
{
    public partial class Form1 : Form
    {
        RNSStatusModel model = new RNSStatusModel();
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_ServerCmm_Start_Click(object sender, EventArgs e)
        {
            model.ServerCommunicator.StartService();
        }

        private void btn_ServerCmm_Config_Click(object sender, EventArgs e)
        {
            model.ServerCommunicator.OpenConfig();
        }

        private void btn_ServerCmm_ViewLog_Click(object sender, EventArgs e)
        {
            model.ServerCommunicator.ViewLog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var status = model.ServerCommunicator.GetStatus();
            lbl_ServerCmm.Text = status.ToString();
            lbl_ServerCmm.ForeColor = status.GetColor();

            status = model.ClientCommunicator.GetStatus();
            lbl_ClientCmm.Text = status.ToString();
            lbl_ClientCmm.ForeColor = status.GetColor();

            status = model.LoginService.GetStatus();
            lbl_LoginSvc.Text = status.ToString();
            lbl_LoginSvc.ForeColor = status.GetColor();
        }

        private void btn_ClientCmm_Start_Click(object sender, EventArgs e)
        {
            model.ClientCommunicator.StartService();
        }

        private void btn_ClientCmm_Config_Click(object sender, EventArgs e)
        {
            model.ClientCommunicator.OpenConfig();
        }

        private void btn_ClientCmm_ViewLog_Click(object sender, EventArgs e)
        {
            model.ClientCommunicator.ViewLog();
        }

        private void btn_Login_Start_Click(object sender, EventArgs e)
        {
            model.LoginService.StartService();
        }

        private void btn_Login_Config_Click(object sender, EventArgs e)
        {
            model.LoginService.OpenConfig();
        }

        private void btn_Login_ViewLog_Click(object sender, EventArgs e)
        {
            model.LoginService.ViewLog();
        }

        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            model.CloseAll();
        }

        private void btn_Restart_Click(object sender, EventArgs e)
        {
            model.StartAll();
        }
    }
}
