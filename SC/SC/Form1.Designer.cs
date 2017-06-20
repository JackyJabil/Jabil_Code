namespace SC
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
            this.p = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.X = new System.Windows.Forms.TextBox();
            this.Y = new System.Windows.Forms.TextBox();
            this.W = new System.Windows.Forms.TextBox();
            this.H = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.TimerCapture = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.panColor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.p)).BeginInit();
            this.SuspendLayout();
            // 
            // p
            // 
            this.p.Location = new System.Drawing.Point(-1, 85);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(628, 281);
            this.p.TabIndex = 0;
            this.p.TabStop = false;
            this.p.MouseDown += new System.Windows.Forms.MouseEventHandler(this.p_MouseDown);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(376, 4);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(88, 31);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "W";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "H";
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(31, 16);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(56, 20);
            this.X.TabIndex = 6;
            this.X.Text = "680";
            // 
            // Y
            // 
            this.Y.Location = new System.Drawing.Point(127, 15);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(56, 20);
            this.Y.TabIndex = 7;
            this.Y.Text = "88";
            // 
            // W
            // 
            this.W.Location = new System.Drawing.Point(31, 48);
            this.W.Name = "W";
            this.W.Size = new System.Drawing.Size(56, 20);
            this.W.TabIndex = 8;
            this.W.Text = "440";
            // 
            // H
            // 
            this.H.Location = new System.Drawing.Point(127, 48);
            this.H.Name = "H";
            this.H.Size = new System.Drawing.Size(56, 20);
            this.H.TabIndex = 9;
            this.H.Text = "200";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 31);
            this.button1.TabIndex = 10;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TimerCapture
            // 
            this.TimerCapture.Interval = 5000;
            this.TimerCapture.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(552, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 31);
            this.button2.TabIndex = 11;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panColor
            // 
            this.panColor.Location = new System.Drawing.Point(285, 15);
            this.panColor.Name = "panColor";
            this.panColor.Size = new System.Drawing.Size(62, 53);
            this.panColor.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 370);
            this.Controls.Add(this.panColor);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.H);
            this.Controls.Add(this.W);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.p);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.p)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox p;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox X;
        private System.Windows.Forms.TextBox Y;
        private System.Windows.Forms.TextBox W;
        private System.Windows.Forms.TextBox H;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer TimerCapture;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panColor;
    }
}

