namespace TestInterface
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
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_ServerIP = new System.Windows.Forms.TextBox();
            this.num_Serverport = new System.Windows.Forms.NumericUpDown();
            this.num_NotifyPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_line1 = new System.Windows.Forms.TextBox();
            this.txt_Line2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_Serverport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NotifyPort)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Notification";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(161, 143);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 49);
            this.button3.TabIndex = 2;
            this.button3.Text = "Send All Notification";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_ServerIP
            // 
            this.txt_ServerIP.Location = new System.Drawing.Point(118, 33);
            this.txt_ServerIP.Name = "txt_ServerIP";
            this.txt_ServerIP.Size = new System.Drawing.Size(120, 20);
            this.txt_ServerIP.TabIndex = 3;
            this.txt_ServerIP.Text = "127.0.0.1";
            // 
            // num_Serverport
            // 
            this.num_Serverport.Location = new System.Drawing.Point(118, 59);
            this.num_Serverport.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_Serverport.Name = "num_Serverport";
            this.num_Serverport.Size = new System.Drawing.Size(120, 20);
            this.num_Serverport.TabIndex = 4;
            this.num_Serverport.Value = new decimal(new int[] {
            2694,
            0,
            0,
            0});
            // 
            // num_NotifyPort
            // 
            this.num_NotifyPort.Location = new System.Drawing.Point(118, 85);
            this.num_NotifyPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_NotifyPort.Name = "num_NotifyPort";
            this.num_NotifyPort.Size = new System.Drawing.Size(120, 20);
            this.num_NotifyPort.TabIndex = 5;
            this.num_NotifyPort.Value = new decimal(new int[] {
            26941,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Server IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Server Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Server Notify Port";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 143);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 49);
            this.button4.TabIndex = 9;
            this.button4.Text = "Connect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Line 1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Line 2";
            // 
            // txt_line1
            // 
            this.txt_line1.Location = new System.Drawing.Point(307, 36);
            this.txt_line1.Name = "txt_line1";
            this.txt_line1.Size = new System.Drawing.Size(120, 20);
            this.txt_line1.TabIndex = 12;
            this.txt_line1.Text = "Đại học Bình Dương";
            // 
            // txt_Line2
            // 
            this.txt_Line2.Location = new System.Drawing.Point(307, 66);
            this.txt_Line2.Name = "txt_Line2";
            this.txt_Line2.Size = new System.Drawing.Size(120, 20);
            this.txt_Line2.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 204);
            this.Controls.Add(this.txt_Line2);
            this.Controls.Add(this.txt_line1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.num_NotifyPort);
            this.Controls.Add(this.num_Serverport);
            this.Controls.Add(this.txt_ServerIP);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.num_Serverport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NotifyPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txt_ServerIP;
        private System.Windows.Forms.NumericUpDown num_Serverport;
        private System.Windows.Forms.NumericUpDown num_NotifyPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_line1;
        private System.Windows.Forms.TextBox txt_Line2;
    }
}

