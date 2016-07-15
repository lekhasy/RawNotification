namespace StartServiceHelper
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.btn_ServerCmm_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ServerCmm = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.lbl_ClientCmm = new System.Windows.Forms.Label();
            this.btn_ClientCmm_Start = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_LoginSvc = new System.Windows.Forms.Label();
            this.btn_Login_Start = new System.Windows.Forms.Button();
            this.btn_Login_Config = new System.Windows.Forms.Button();
            this.btn_ClientCmm_Config = new System.Windows.Forms.Button();
            this.btn_ServerCmm_Config = new System.Windows.Forms.Button();
            this.btn_Login_ViewLog = new System.Windows.Forms.Button();
            this.btn_ClientCmm_ViewLog = new System.Windows.Forms.Button();
            this.btn_ServerCmm_ViewLog = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ServerCmm_Start
            // 
            this.btn_ServerCmm_Start.Location = new System.Drawing.Point(219, 18);
            this.btn_ServerCmm_Start.Name = "btn_ServerCmm_Start";
            this.btn_ServerCmm_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_ServerCmm_Start.TabIndex = 0;
            this.btn_ServerCmm_Start.Text = "Start";
            this.btn_ServerCmm_Start.UseVisualStyleBackColor = true;
            this.btn_ServerCmm_Start.Click += new System.EventHandler(this.btn_ServerCmm_Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Communicator";
            // 
            // lbl_ServerCmm
            // 
            this.lbl_ServerCmm.AutoSize = true;
            this.lbl_ServerCmm.ForeColor = System.Drawing.Color.Teal;
            this.lbl_ServerCmm.Location = new System.Drawing.Point(145, 23);
            this.lbl_ServerCmm.Name = "lbl_ServerCmm";
            this.lbl_ServerCmm.Size = new System.Drawing.Size(54, 13);
            this.lbl_ServerCmm.TabIndex = 2;
            this.lbl_ServerCmm.Text = "Loading...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Client Communicator";
            // 
            // btn_Restart
            // 
            this.btn_Restart.Location = new System.Drawing.Point(357, 162);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(123, 23);
            this.btn_Restart.TabIndex = 4;
            this.btn_Restart.Text = "Restart all services";
            this.btn_Restart.UseVisualStyleBackColor = true;
            this.btn_Restart.Click += new System.EventHandler(this.btn_Restart_Click);
            // 
            // lbl_ClientCmm
            // 
            this.lbl_ClientCmm.AutoSize = true;
            this.lbl_ClientCmm.ForeColor = System.Drawing.Color.Teal;
            this.lbl_ClientCmm.Location = new System.Drawing.Point(145, 57);
            this.lbl_ClientCmm.Name = "lbl_ClientCmm";
            this.lbl_ClientCmm.Size = new System.Drawing.Size(54, 13);
            this.lbl_ClientCmm.TabIndex = 5;
            this.lbl_ClientCmm.Text = "Loading...";
            // 
            // btn_ClientCmm_Start
            // 
            this.btn_ClientCmm_Start.Location = new System.Drawing.Point(219, 52);
            this.btn_ClientCmm_Start.Name = "btn_ClientCmm_Start";
            this.btn_ClientCmm_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_ClientCmm_Start.TabIndex = 6;
            this.btn_ClientCmm_Start.Text = "Start";
            this.btn_ClientCmm_Start.UseVisualStyleBackColor = true;
            this.btn_ClientCmm_Start.Click += new System.EventHandler(this.btn_ClientCmm_Start_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Login Service";
            // 
            // lbl_LoginSvc
            // 
            this.lbl_LoginSvc.AutoSize = true;
            this.lbl_LoginSvc.ForeColor = System.Drawing.Color.Teal;
            this.lbl_LoginSvc.Location = new System.Drawing.Point(145, 92);
            this.lbl_LoginSvc.Name = "lbl_LoginSvc";
            this.lbl_LoginSvc.Size = new System.Drawing.Size(54, 13);
            this.lbl_LoginSvc.TabIndex = 8;
            this.lbl_LoginSvc.Text = "Loading...";
            // 
            // btn_Login_Start
            // 
            this.btn_Login_Start.Location = new System.Drawing.Point(219, 92);
            this.btn_Login_Start.Name = "btn_Login_Start";
            this.btn_Login_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Login_Start.TabIndex = 9;
            this.btn_Login_Start.Text = "Start";
            this.btn_Login_Start.UseVisualStyleBackColor = true;
            this.btn_Login_Start.Click += new System.EventHandler(this.btn_Login_Start_Click);
            // 
            // btn_Login_Config
            // 
            this.btn_Login_Config.Location = new System.Drawing.Point(312, 92);
            this.btn_Login_Config.Name = "btn_Login_Config";
            this.btn_Login_Config.Size = new System.Drawing.Size(75, 23);
            this.btn_Login_Config.TabIndex = 12;
            this.btn_Login_Config.Text = "Config";
            this.btn_Login_Config.UseVisualStyleBackColor = true;
            this.btn_Login_Config.Click += new System.EventHandler(this.btn_Login_Config_Click);
            // 
            // btn_ClientCmm_Config
            // 
            this.btn_ClientCmm_Config.Location = new System.Drawing.Point(312, 52);
            this.btn_ClientCmm_Config.Name = "btn_ClientCmm_Config";
            this.btn_ClientCmm_Config.Size = new System.Drawing.Size(75, 23);
            this.btn_ClientCmm_Config.TabIndex = 11;
            this.btn_ClientCmm_Config.Text = "Config";
            this.btn_ClientCmm_Config.UseVisualStyleBackColor = true;
            this.btn_ClientCmm_Config.Click += new System.EventHandler(this.btn_ClientCmm_Config_Click);
            // 
            // btn_ServerCmm_Config
            // 
            this.btn_ServerCmm_Config.Location = new System.Drawing.Point(312, 18);
            this.btn_ServerCmm_Config.Name = "btn_ServerCmm_Config";
            this.btn_ServerCmm_Config.Size = new System.Drawing.Size(75, 23);
            this.btn_ServerCmm_Config.TabIndex = 10;
            this.btn_ServerCmm_Config.Text = "Config";
            this.btn_ServerCmm_Config.UseVisualStyleBackColor = true;
            this.btn_ServerCmm_Config.Click += new System.EventHandler(this.btn_ServerCmm_Config_Click);
            // 
            // btn_Login_ViewLog
            // 
            this.btn_Login_ViewLog.Location = new System.Drawing.Point(405, 92);
            this.btn_Login_ViewLog.Name = "btn_Login_ViewLog";
            this.btn_Login_ViewLog.Size = new System.Drawing.Size(75, 23);
            this.btn_Login_ViewLog.TabIndex = 15;
            this.btn_Login_ViewLog.Text = "View Log";
            this.btn_Login_ViewLog.UseVisualStyleBackColor = true;
            this.btn_Login_ViewLog.Click += new System.EventHandler(this.btn_Login_ViewLog_Click);
            // 
            // btn_ClientCmm_ViewLog
            // 
            this.btn_ClientCmm_ViewLog.Location = new System.Drawing.Point(405, 52);
            this.btn_ClientCmm_ViewLog.Name = "btn_ClientCmm_ViewLog";
            this.btn_ClientCmm_ViewLog.Size = new System.Drawing.Size(75, 23);
            this.btn_ClientCmm_ViewLog.TabIndex = 14;
            this.btn_ClientCmm_ViewLog.Text = "View Log";
            this.btn_ClientCmm_ViewLog.UseVisualStyleBackColor = true;
            this.btn_ClientCmm_ViewLog.Click += new System.EventHandler(this.btn_ClientCmm_ViewLog_Click);
            // 
            // btn_ServerCmm_ViewLog
            // 
            this.btn_ServerCmm_ViewLog.Location = new System.Drawing.Point(405, 18);
            this.btn_ServerCmm_ViewLog.Name = "btn_ServerCmm_ViewLog";
            this.btn_ServerCmm_ViewLog.Size = new System.Drawing.Size(75, 23);
            this.btn_ServerCmm_ViewLog.TabIndex = 13;
            this.btn_ServerCmm_ViewLog.Text = "View Log";
            this.btn_ServerCmm_ViewLog.UseVisualStyleBackColor = true;
            this.btn_ServerCmm_ViewLog.Click += new System.EventHandler(this.btn_ServerCmm_ViewLog_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(219, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Close all services";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 188);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Login_ViewLog);
            this.Controls.Add(this.btn_ClientCmm_ViewLog);
            this.Controls.Add(this.btn_ServerCmm_ViewLog);
            this.Controls.Add(this.btn_Login_Config);
            this.Controls.Add(this.btn_ClientCmm_Config);
            this.Controls.Add(this.btn_ServerCmm_Config);
            this.Controls.Add(this.btn_Login_Start);
            this.Controls.Add(this.lbl_LoginSvc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_ClientCmm_Start);
            this.Controls.Add(this.lbl_ClientCmm);
            this.Controls.Add(this.btn_Restart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_ServerCmm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ServerCmm_Start);
            this.Name = "Form1";
            this.Text = "Service Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ServerCmm_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_ServerCmm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Restart;
        private System.Windows.Forms.Label lbl_ClientCmm;
        private System.Windows.Forms.Button btn_ClientCmm_Start;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_LoginSvc;
        private System.Windows.Forms.Button btn_Login_Start;
        private System.Windows.Forms.Button btn_Login_Config;
        private System.Windows.Forms.Button btn_ClientCmm_Config;
        private System.Windows.Forms.Button btn_ServerCmm_Config;
        private System.Windows.Forms.Button btn_Login_ViewLog;
        private System.Windows.Forms.Button btn_ClientCmm_ViewLog;
        private System.Windows.Forms.Button btn_ServerCmm_ViewLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

