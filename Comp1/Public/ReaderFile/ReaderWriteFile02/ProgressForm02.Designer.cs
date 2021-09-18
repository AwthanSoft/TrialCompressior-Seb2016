namespace Comp1.Public.ReaderWriteFile02
{
    partial class ProgressForm02
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NowTime = new System.Windows.Forms.Label();
            this.StartTime = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SizeDone = new System.Windows.Forms.Label();
            this.ReaderProgress = new System.Windows.Forms.Label();
            this.SaveSize = new System.Windows.Forms.Label();
            this.RestSize = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveExtension = new System.Windows.Forms.Label();
            this.Extension = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.Label();
            this.OrignalSize = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox7
            // 
            this.groupBox7.AutoSize = true;
            this.groupBox7.Controls.Add(this.progressBar1);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(3, 390);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(1797, 190);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(42, 54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1656, 97);
            this.progressBar1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox6.Location = new System.Drawing.Point(3, 596);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1797, 214);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(378, 71);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(183, 65);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1194, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(347, 99);
            this.button1.TabIndex = 0;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox2.Size = new System.Drawing.Size(1797, 354);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FileInfo";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.NowTime);
            this.groupBox5.Controls.Add(this.StartTime);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(535, 36);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(749, 315);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Time";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label16.Location = new System.Drawing.Point(159, 249);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 33);
            this.label16.TabIndex = 3;
            this.label16.Text = "label2";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(159, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 33);
            this.label12.TabIndex = 3;
            this.label12.Text = "label2";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(159, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 33);
            this.label7.TabIndex = 3;
            this.label7.Text = "label2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(159, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 33);
            this.label6.TabIndex = 3;
            this.label6.Text = "label2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(159, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 33);
            this.label5.TabIndex = 3;
            this.label5.Text = "label2";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(0, 199);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(146, 33);
            this.label15.TabIndex = 2;
            this.label15.Text = "ReaderSize";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 249);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(150, 33);
            this.label11.TabIndex = 2;
            this.label11.Text = "StopLength";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "ReadState";
            // 
            // NowTime
            // 
            this.NowTime.AutoSize = true;
            this.NowTime.Location = new System.Drawing.Point(6, 86);
            this.NowTime.Name = "NowTime";
            this.NowTime.Size = new System.Drawing.Size(135, 33);
            this.NowTime.TabIndex = 1;
            this.NowTime.Text = "Now Time";
            // 
            // StartTime
            // 
            this.StartTime.AutoSize = true;
            this.StartTime.Location = new System.Drawing.Point(6, 36);
            this.StartTime.Name = "StartTime";
            this.StartTime.Size = new System.Drawing.Size(139, 33);
            this.StartTime.TabIndex = 0;
            this.StartTime.Text = "Start Time";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.SizeDone);
            this.groupBox4.Controls.Add(this.ReaderProgress);
            this.groupBox4.Controls.Add(this.SaveSize);
            this.groupBox4.Controls.Add(this.RestSize);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(1284, 36);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 315);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rest";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label14.Location = new System.Drawing.Point(161, 199);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 33);
            this.label14.TabIndex = 3;
            this.label14.Text = "label2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(161, 139);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 33);
            this.label10.TabIndex = 3;
            this.label10.Text = "label2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(161, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 33);
            this.label9.TabIndex = 3;
            this.label9.Text = "label2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(161, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 33);
            this.label8.TabIndex = 3;
            this.label8.Text = "label2";
            // 
            // SizeDone
            // 
            this.SizeDone.AutoSize = true;
            this.SizeDone.Location = new System.Drawing.Point(6, 86);
            this.SizeDone.Name = "SizeDone";
            this.SizeDone.Size = new System.Drawing.Size(132, 33);
            this.SizeDone.TabIndex = 2;
            this.SizeDone.Text = "Size Done";
            // 
            // ReaderProgress
            // 
            this.ReaderProgress.AutoSize = true;
            this.ReaderProgress.Location = new System.Drawing.Point(7, 197);
            this.ReaderProgress.Name = "ReaderProgress";
            this.ReaderProgress.Size = new System.Drawing.Size(135, 33);
            this.ReaderProgress.TabIndex = 1;
            this.ReaderProgress.Text = "RProgress";
            // 
            // SaveSize
            // 
            this.SaveSize.AutoSize = true;
            this.SaveSize.Location = new System.Drawing.Point(6, 139);
            this.SaveSize.Name = "SaveSize";
            this.SaveSize.Size = new System.Drawing.Size(126, 33);
            this.SaveSize.TabIndex = 1;
            this.SaveSize.Text = "Save Size";
            // 
            // RestSize
            // 
            this.RestSize.AutoSize = true;
            this.RestSize.Location = new System.Drawing.Point(6, 36);
            this.RestSize.Name = "RestSize";
            this.RestSize.Size = new System.Drawing.Size(122, 33);
            this.RestSize.TabIndex = 0;
            this.RestSize.Text = "Rest Size";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.SaveExtension);
            this.groupBox3.Controls.Add(this.Extension);
            this.groupBox3.Controls.Add(this.FileName);
            this.groupBox3.Controls.Add(this.OrignalSize);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(532, 315);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.Location = new System.Drawing.Point(209, 185);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 33);
            this.label13.TabIndex = 3;
            this.label13.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(209, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 33);
            this.label4.TabIndex = 3;
            this.label4.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(209, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 33);
            this.label3.TabIndex = 3;
            this.label3.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(209, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // SaveExtension
            // 
            this.SaveExtension.AutoSize = true;
            this.SaveExtension.Location = new System.Drawing.Point(6, 185);
            this.SaveExtension.Name = "SaveExtension";
            this.SaveExtension.Size = new System.Drawing.Size(185, 33);
            this.SaveExtension.TabIndex = 1;
            this.SaveExtension.Text = "SaveExtension";
            // 
            // Extension
            // 
            this.Extension.AutoSize = true;
            this.Extension.Location = new System.Drawing.Point(6, 139);
            this.Extension.Name = "Extension";
            this.Extension.Size = new System.Drawing.Size(126, 33);
            this.Extension.TabIndex = 1;
            this.Extension.Text = "Extention";
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.Location = new System.Drawing.Point(6, 36);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(132, 33);
            this.FileName.TabIndex = 1;
            this.FileName.Text = "File Name";
            // 
            // OrignalSize
            // 
            this.OrignalSize.AutoSize = true;
            this.OrignalSize.Location = new System.Drawing.Point(6, 86);
            this.OrignalSize.Name = "OrignalSize";
            this.OrignalSize.Size = new System.Drawing.Size(155, 33);
            this.OrignalSize.TabIndex = 0;
            this.OrignalSize.Text = "Orignal Size";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1803, 813);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ProgressForm02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1803, 813);
            this.Controls.Add(this.groupBox1);
            this.Name = "ProgressForm02";
            this.Text = "ProgressForm02";
            this.Load += new System.EventHandler(this.ProgressForm02_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label NowTime;
        private System.Windows.Forms.Label StartTime;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label SizeDone;
        private System.Windows.Forms.Label SaveSize;
        private System.Windows.Forms.Label RestSize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Extension;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Label OrignalSize;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label SaveExtension;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label ReaderProgress;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}