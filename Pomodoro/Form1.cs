namespace Pomodoro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bControl_Click(object sender, EventArgs e)
        {
            if (bControl.Text == "START")
                { bControl.Text = "STOP"; }
            else
                { bControl.Text = "START"; }
        }

        private void bDistToggle_Click(object sender, EventArgs e)
        {
            if (bDistToggle.Text == "v")
            {
                bDistToggle.Text = "^";
                Size extended = new Size(356, 313);
                this.MaximumSize = extended;
                this.MinimumSize = extended;
                this.tDistraction.Visible = true;
            }
            else
            {
                bDistToggle.Text = "v";
                Size normal = new Size(356, 213);
                this.MaximumSize = normal;
                this.MinimumSize = normal;
                this.tDistraction.Visible = false;
            }
        }
    }
}