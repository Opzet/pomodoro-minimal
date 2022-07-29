namespace Pomodoro
{
    public enum State { work, shortBreak, longBreak };
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            minutes = workTime;
            seconds = 0;
            UpdateTime();
            UpdateStatus();
        }

        public byte workTime = 15;
        public byte playTime = 5;
        public State state = State.work;

        private byte minutes;
        private byte seconds;

        private void UpdateTime()
        {
            lTime.Text = $"{string.Format("{0:00}", minutes)}:{string.Format("{0:00}", seconds)}";
        }

        private void UpdateStatus()
        {
            lStatus.Text = $"{state}";
            lStatus.Left = ClientSize.Width / 2 - lStatus.Width / 2;
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
            // time is over
            if (minutes == 0 && seconds == 0)
            {
                switch (state)
                {
                    case State.work:
                        state = State.shortBreak;
                        minutes = playTime;
                        break;
                    case State.shortBreak:
                        state = State.work;
                        minutes = workTime;
                        break;
                    default:
                        break;
                }
                timer1.Enabled = false;
                bControl.Text = "START";
                UpdateStatus();
            }
            UpdateTime();
        }
    }
}