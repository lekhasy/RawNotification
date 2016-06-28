namespace DemoServer
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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ServerPort = new System.Windows.Forms.NumericUpDown();
            this.ServerNotifyPort = new System.Windows.Forms.NumericUpDown();
            this.ClientPort = new System.Windows.Forms.NumericUpDown();
            this.btnStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PackageSID = new System.Windows.Forms.TextBox();
            this.SecretKey = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerNotifyPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClientPort)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 226);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Server Notify Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Client Port";
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(118, 12);
            this.ServerPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(120, 20);
            this.ServerPort.TabIndex = 7;
            this.ServerPort.Value = new decimal(new int[] {
            2694,
            0,
            0,
            0});
            // 
            // ServerNotifyPort
            // 
            this.ServerNotifyPort.Location = new System.Drawing.Point(118, 60);
            this.ServerNotifyPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ServerNotifyPort.Name = "ServerNotifyPort";
            this.ServerNotifyPort.Size = new System.Drawing.Size(120, 20);
            this.ServerNotifyPort.TabIndex = 8;
            this.ServerNotifyPort.Value = new decimal(new int[] {
            26941,
            0,
            0,
            0});
            // 
            // ClientPort
            // 
            this.ClientPort.Location = new System.Drawing.Point(118, 99);
            this.ClientPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ClientPort.Name = "ClientPort";
            this.ClientPort.Size = new System.Drawing.Size(120, 20);
            this.ClientPort.TabIndex = 9;
            this.ClientPort.Value = new decimal(new int[] {
            22112,
            0,
            0,
            0});
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(154, 226);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "PackageSID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Secret Key";
            // 
            // PackageSID
            // 
            this.PackageSID.Location = new System.Drawing.Point(118, 140);
            this.PackageSID.Name = "PackageSID";
            this.PackageSID.Size = new System.Drawing.Size(120, 20);
            this.PackageSID.TabIndex = 13;
            this.PackageSID.Text = "ms-app://s-1-15-2-2725197343-3336943269-1554551664-2344088653-1875215302-90021361" +
    "1-2750785773";
            // 
            // SecretKey
            // 
            this.SecretKey.Location = new System.Drawing.Point(118, 175);
            this.SecretKey.Name = "SecretKey";
            this.SecretKey.Size = new System.Drawing.Size(120, 20);
            this.SecretKey.TabIndex = 14;
            this.SecretKey.Text = "mNDa99DTqAqc0y+nNtXxNAF3vf6zVLZb";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 261);
            this.Controls.Add(this.SecretKey);
            this.Controls.Add(this.PackageSID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.ClientPort);
            this.Controls.Add(this.ServerNotifyPort);
            this.Controls.Add(this.ServerPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerNotifyPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClientPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ServerPort;
        private System.Windows.Forms.NumericUpDown ServerNotifyPort;
        private System.Windows.Forms.NumericUpDown ClientPort;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SecretKey;
        private System.Windows.Forms.TextBox PackageSID;
    }
}

