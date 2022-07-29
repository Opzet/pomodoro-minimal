namespace Pomodoro
{
    public enum State { work, shortBreak, longBreak };

    public enum Screen { settings, timer };
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bBack.Enabled = false;
            DisplayScreen(Screen.settings);
        }

        public byte workTime;
        public byte shortBreakTime;
        public byte longBreakTime;
        // how many shortBreaks are between two longBreaks
        private byte longBreakPeriod;
        private byte periodCounter = 0;
        public State state;
        private string[] descriptors = new string[] { "work", "short break", "long break" };

        private byte minutes;
        private byte seconds;
        private int microseconds;

        private void SetupTimer()
        {
            minutes = workTime;
            seconds = 0;
            microseconds = 0;
            state = State.work;
            UpdateTime();
            UpdateStatus();
        }

        private void TurnTimer(bool on)
        {
            timer1.Enabled = on;
            if (on)
                { bControl.Text = "STOP"; }
            else 
                { bControl.Text = "START"; }
        }
        private void UpdateTime()
        {
            lTime.Text = $"{string.Format("{0:00}", minutes)}:{string.Format("{0:00}", seconds)}";
        }

        private void UpdateStatus()
        {
            lStatus.Text = $"{descriptors[(int)state]}";
            lStatus.Left = ClientSize.Width / 2 - lStatus.Width / 2;
        }

        private void DisplayScreen(Screen screen)
        {
            bool toSettings;
            if (screen == Screen.settings)
                { toSettings = true; }
            else 
                { toSettings = false; }

            lStatus.Visible = !toSettings;
            lTime.Visible = !toSettings;
            bControl.Visible = !toSettings;
            bDistToggle.Visible = !toSettings;

            lWork.Visible = toSettings;
            nWork.Visible = toSettings;
            lShort.Visible = toSettings;
            nShort.Visible = toSettings;
            lLong.Visible = toSettings;
            nLong.Visible = toSettings;
            lPeriod.Visible = toSettings;
            nPeriod.Visible = toSettings;
            bBack.Visible = toSettings;
            bApply.Visible = toSettings;
        }

        private void bControl_Click(object sender, EventArgs e)
        {
            if (bControl.Text == "START")
            {
                TurnTimer(true);
            }
            else
            {
                TurnTimer(false); 
            }
        }

        private void bDistToggle_Click(object sender, EventArgs e)
        {
            if (bDistToggle.Text == "v")
            {
                if (state == State.work)
                { 
                    timer1.Enabled = false;
                    bControl.Enabled = false;
                }
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
                if (bControl.Text == "STOP")
                    { timer1.Enabled = true; }
                bControl.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            microseconds = microseconds - timer1.Interval;
            if (microseconds <= 0)
                { microseconds = 1000; }
            if (seconds == 0)
            { 
                seconds = 60;
                minutes--;
            }
            if (microseconds == 1000)
                { seconds--; }
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
                TurnTimer(false);
                UpdateStatus();
                UpdateTime();
                MessageBox.Show($"Your {previous} has ended.");
            }
            UpdateTime();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TurnTimer(false);
            if (bDistToggle.Text == "^")
                { bDistToggle.PerformClick(); }
            DisplayScreen(Screen.settings);
        }

        private void bApply_Click(object sender, EventArgs e)
        {
            workTime = (byte)nWork.Value;
            shortBreakTime = (byte)nShort.Value;
            longBreakTime = (byte)nLong.Value;
            longBreakPeriod = (byte)nPeriod.Value;

            SetupTimer();
            DisplayScreen(Screen.timer);
            bBack.Enabled = true;
        }

        private void bBack_Click(object sender, EventArgs e)
        {
            DisplayScreen(Screen.timer);
            TurnTimer(true);
        }
    }
}