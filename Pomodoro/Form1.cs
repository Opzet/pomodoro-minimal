namespace Pomodoro
{
    public enum State { work, shortBreak, longBreak };
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private byte minutes = 30;
        private byte seconds = 0;

        public byte workTime = 30;
        public State state = State.work;

        private void UpdateTime()
        {
            lTime.Text = $"{string.Format("{0:00}", minutes)}:{string.Format("{0:00}", seconds)}";
        }

        private void bControl_Click(object sender, EventArgs e)
        {
            if (bControl.Text == "START")
            {
                timer1.Enabled = true;
                bControl.Text = "STOP";
            }
            else
            {
                timer1.Enabled = false;
                bControl.Text = "START"; 
            }
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (seconds == 0)
            { 
                seconds = 60;
                minutes--;
            }
            seconds--;
            if (minutes == 0 && seconds == 0)
            {
                // switch state and set timer accordingly
                minutes = 30;
                timer1.Enabled = false;
                bControl.Text = "START";
            }
            UpdateTime();
        }
    }
}