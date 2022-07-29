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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(340, 174);
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
    }
}