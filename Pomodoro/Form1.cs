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

        public byte workTime = 7;
        public byte shortBreakTime = 2;
        public byte longBreakTime = 4;
        // how many shortBreaks are between two longBreaks
        private byte longBreakPeriod = 3;
        private byte periodCounter = 0;
        public State state = State.work;
        private string[] descriptors = new string[] { "work", "short break", "long break" };

        private byte minutes;
        private byte seconds;

        private void UpdateTime()
        {
            lTime.Text = $"{string.Format("{0:00}", minutes)}:{string.Format("{0:00}", seconds)}";
        }

        private void UpdateStatus()
        {
            lStatus.Text = $"{descriptors[(int)state]}";
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
                using (StreamWriter sw = File.AppendText("distractions.txt"))
                {
                    if (tDistraction.Text != "")
                        { sw.WriteLine(tDistraction.Text); }
                }
                tDistraction.Text = "";
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
                string previous = descriptors[(int)state];
                switch (state)
                {
                    case State.work:
                        periodCounter = (byte)((periodCounter + 1) % (longBreakPeriod + 1));
                        if (periodCounter == 0)
                        { 
                            state = State.longBreak;
                            minutes = longBreakTime;
                        }
                        else
                        { 
                            state = State.shortBreak;
                            minutes = shortBreakTime;
                        }
                        break;
                    case State.shortBreak:
                    case State.longBreak:
                        state = State.work;
                        minutes = workTime;
                        break;
                    default:
                        break;
                }
                timer1.Enabled = false;
                bControl.Text = "START";
                UpdateStatus();
                UpdateTime();
                MessageBox.Show($"Your {previous} has ended.");
            }
            UpdateTime();
        }
    }
}