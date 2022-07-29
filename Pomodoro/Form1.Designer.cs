namespace Pomodoro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.bControl = new System.Windows.Forms.Button();
            this.lStatus = new System.Windows.Forms.Label();
            this.lTime = new System.Windows.Forms.Label();
            this.tDistraction = new System.Windows.Forms.TextBox();
            this.bDistToggle = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nWork = new System.Windows.Forms.NumericUpDown();
            this.nShort = new System.Windows.Forms.NumericUpDown();
            this.nLong = new System.Windows.Forms.NumericUpDown();
            this.nPeriod = new System.Windows.Forms.NumericUpDown();
            this.lWork = new System.Windows.Forms.Label();
            this.lShort = new System.Windows.Forms.Label();
            this.lLong = new System.Windows.Forms.Label();
            this.lPeriod = new System.Windows.Forms.Label();
            this.bBack = new System.Windows.Forms.Button();
            this.bApply = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nShort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nLong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // bControl
            // 
            this.bControl.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bControl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bControl.Location = new System.Drawing.Point(132, 119);
            this.bControl.Name = "bControl";
            this.bControl.Size = new System.Drawing.Size(75, 23);
            this.bControl.TabIndex = 0;
            this.bControl.TabStop = false;
            this.bControl.Text = "START";
            this.bControl.UseVisualStyleBackColor = false;
            this.bControl.Click += new System.EventHandler(this.bControl_Click);
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lStatus.Location = new System.Drawing.Point(149, 9);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(45, 21);
            this.lStatus.TabIndex = 1;
            this.lStatus.Text = "work";
            // 
            // lTime
            // 
            this.lTime.AutoSize = true;
            this.lTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lTime.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lTime.Location = new System.Drawing.Point(77, 30);
            this.lTime.Name = "lTime";
            this.lTime.Size = new System.Drawing.Size(191, 86);
            this.lTime.TabIndex = 2;
            this.lTime.Text = "30:00";
            this.lTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tDistraction
            // 
            this.tDistraction.Location = new System.Drawing.Point(12, 177);
            this.tDistraction.Multiline = true;
            this.tDistraction.Name = "tDistraction";
            this.tDistraction.Size = new System.Drawing.Size(317, 82);
            this.tDistraction.TabIndex = 3;
            this.tDistraction.Visible = false;
            // 
            // bDistToggle
            // 
            this.bDistToggle.FlatAppearance.BorderSize = 0;
            this.bDistToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDistToggle.Location = new System.Drawing.Point(159, 148);
            this.bDistToggle.Name = "bDistToggle";
            this.bDistToggle.Size = new System.Drawing.Size(20, 23);
            this.bDistToggle.TabIndex = 4;
            this.bDistToggle.Text = "v";
            this.bDistToggle.UseVisualStyleBackColor = true;
            this.bDistToggle.Click += new System.EventHandler(this.bDistToggle_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 26);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // nWork
            // 
            this.nWork.Location = new System.Drawing.Point(200, 12);
            this.nWork.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nWork.Name = "nWork";
            this.nWork.Size = new System.Drawing.Size(41, 23);
            this.nWork.TabIndex = 6;
            this.nWork.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // nShort
            // 
            this.nShort.Location = new System.Drawing.Point(200, 41);
            this.nShort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nShort.Name = "nShort";
            this.nShort.Size = new System.Drawing.Size(41, 23);
            this.nShort.TabIndex = 7;
            this.nShort.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nLong
            // 
            this.nLong.Location = new System.Drawing.Point(200, 70);
            this.nLong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nLong.Name = "nLong";
            this.nLong.Size = new System.Drawing.Size(41, 23);
            this.nLong.TabIndex = 8;
            this.nLong.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // nPeriod
            // 
            this.nPeriod.Location = new System.Drawing.Point(200, 99);
            this.nPeriod.Name = "nPeriod";
            this.nPeriod.Size = new System.Drawing.Size(41, 23);
            this.nPeriod.TabIndex = 9;
            this.nPeriod.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lWork
            // 
            this.lWork.AutoSize = true;
            this.lWork.Location = new System.Drawing.Point(84, 15);
            this.lWork.Name = "lWork";
            this.lWork.Size = new System.Drawing.Size(35, 15);
            this.lWork.TabIndex = 10;
            this.lWork.Text = "Work";
            // 
            // lShort
            // 
            this.lShort.AutoSize = true;
            this.lShort.Location = new System.Drawing.Point(84, 43);
            this.lShort.Name = "lShort";
            this.lShort.Size = new System.Drawing.Size(67, 15);
            this.lShort.TabIndex = 11;
            this.lShort.Text = "Short break";
            // 
            // lLong
            // 
            this.lLong.AutoSize = true;
            this.lLong.Location = new System.Drawing.Point(84, 72);
            this.lLong.Name = "lLong";
            this.lLong.Size = new System.Drawing.Size(66, 15);
            this.lLong.TabIndex = 12;
            this.lLong.Text = "Long break";
            // 
            // lPeriod
            // 
            this.lPeriod.AutoSize = true;
            this.lPeriod.Location = new System.Drawing.Point(84, 101);
            this.lPeriod.Name = "lPeriod";
            this.lPeriod.Size = new System.Drawing.Size(103, 15);
            this.lPeriod.TabIndex = 13;
            this.lPeriod.Text = "Long break period";
            // 
            // bBack
            // 
            this.bBack.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bBack.Location = new System.Drawing.Point(84, 139);
            this.bBack.Name = "bBack";
            this.bBack.Size = new System.Drawing.Size(75, 23);
            this.bBack.TabIndex = 14;
            this.bBack.TabStop = false;
            this.bBack.Text = "BACK";
            this.bBack.UseVisualStyleBackColor = false;
            this.bBack.Click += new System.EventHandler(this.bBack_Click);
            // 
            // bApply
            // 
            this.bApply.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.bApply.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.bApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bApply.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bApply.Location = new System.Drawing.Point(200, 139);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(75, 23);
            this.bApply.TabIndex = 15;
            this.bApply.TabStop = false;
            this.bApply.Text = "APPLY";
            this.bApply.UseVisualStyleBackColor = false;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(340, 174);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.bApply);
            this.Controls.Add(this.bBack);
            this.Controls.Add(this.lPeriod);
            this.Controls.Add(this.lLong);
            this.Controls.Add(this.lShort);
            this.Controls.Add(this.lWork);
            this.Controls.Add(this.nPeriod);
            this.Controls.Add(this.nLong);
            this.Controls.Add(this.nShort);
            this.Controls.Add(this.nWork);
            this.Controls.Add(this.bDistToggle);
            this.Controls.Add(this.tDistraction);
            this.Controls.Add(this.lTime);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.bControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(356, 213);
            this.MinimumSize = new System.Drawing.Size(356, 213);
            this.Name = "Form1";
            this.Text = "Timer";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nWork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nShort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nLong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Button bControl;
        private Label lStatus;
        private Label lTime;
        private TextBox tDistraction;
        private Button bDistToggle;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private NumericUpDown nWork;
        private NumericUpDown nShort;
        private NumericUpDown nLong;
        private NumericUpDown nPeriod;
        private Label lWork;
        private Label lShort;
        private Label lLong;
        private Label lPeriod;
        private Button bBack;
        private Button bApply;
    }
}