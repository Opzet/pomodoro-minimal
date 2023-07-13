using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
namespace PomodoroMinimal;

public enum State { Work, ShortBreak, LongBreak, Settings };

public class Config
{
    public byte workTime { get; set; } = 1;
    public byte shortBreakTime { get; set; } = 2;
    public byte longBreakTime { get; set; } = 3;
    // how many shortBreaks are between two longBreaks
    public byte longBreakPeriod { get; set; } = 1;
}

public class PomodoroTimer
{
    public bool On = false;
    public byte minutes;
    public byte seconds;
    // how many short breaks have I gone through
    private byte periodCounter = 0;

    private Config _config;
    

    // updates the clock and returns the after-state
    public State SecondUpdate(State currentState)
    {
        State state = currentState;
        if (seconds == 0)
        { 
            seconds = 60;
            minutes--;
        }

        seconds -= 20;
        // time is over
        if (minutes == 0 && seconds == 0)
        {
            switch (state)
            {
                case State.Work:
                    periodCounter = (byte)((periodCounter + 1) % (_config.longBreakPeriod + 1));
                    if (periodCounter == 0)
                    { 
                        state = State.LongBreak;
                        minutes = _config.longBreakTime;
                    }
                    else
                    { 
                        state = State.ShortBreak;
                        minutes = _config.shortBreakTime;
                    }
                    break;
                case State.ShortBreak:
                case State.LongBreak:
                    state = State.Work;
                    minutes = _config.workTime;
                    break;
                default:
                    break;
            }

            On = false;
        }
        Console.WriteLine($"{minutes:00}:{seconds:00}");
        return state;
    }


    public PomodoroTimer(Config config)
    {
        _config = config;
        minutes = config.workTime;
    }
}
public class Model : INotifyPropertyChanged
{
    public Config Config { get; } = new();
    public State state = State.Settings;
    public bool IsInSettings => state == State.Settings;
    
    // Timer setup
    private DispatcherTimer _clock;
    public PomodoroTimer Timer { get; set; }
    public string Time => $"{Timer.minutes:00}:{Timer.seconds:00}";
    
    // Start button setup
    public bool StartButtonOn { get; private set; } = true;
    private bool _startButtonOnStart = true;
    private readonly string[] StartButtonLabels = new[] { "START", "STOP" };

    public string StartButtonText => _startButtonOnStart ? StartButtonLabels[0] : StartButtonLabels[1];
    
    // Activity text setup
    private string[] _activities = new string[] { "work", "short break", "long break" };
    public string Activity => _activities[(int)state];
    
    // Note toggle setup
    //private string[] _toggleTexts = new string[] { "v", "^" };
    public bool NotesDisplayed { get; set; }
    public string Notes { get; set; }
    private string PathToNotes = "distractions.txt";
    private readonly string[] _notesToggleLabels = new string[] { "v", "^" };
    public string NotesToggleLabel => NotesDisplayed ? _notesToggleLabels[1] : _notesToggleLabels[0];

    public PopupWindow popup { get; set; } = new ();
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    // raises the above event
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public Model()
    {
    }

    public void ApplyClick()
    {
        state = State.Work;
        Timer = new PomodoroTimer(Config);
        _clock = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.MaxValue,
            new System.EventHandler(TimerTick));
        RaisePropertyChanged(nameof(Time));
        RaisePropertyChanged(nameof(IsInSettings));
    }
    
    private void TimerTick(object? sender, EventArgs e)
    {
        if (Timer.On)
        {
            var previousState = state;
            state = Timer.SecondUpdate(state);
            RaisePropertyChanged(nameof(Time));
            // if the update turned off the timer, it is due to activity change
            if (!Timer.On)
            {
                _startButtonOnStart = true;
                RaisePropertyChanged(nameof(StartButtonText));
                RaisePropertyChanged(nameof(Activity));
                // displays the popup over other application, which makes the main window invisible (see xaml)
                // closing the popup show it again
                popup = new PopupWindow();
                popup.Show();
                RaisePropertyChanged(nameof(popup));
            }
        }
    }

    public void StartStopClick()
    {
        Timer.On = !Timer.On;
        _startButtonOnStart = !_startButtonOnStart;
        RaisePropertyChanged(nameof(StartButtonText));
    }

    public void NotesToggled()
    {
        StartButtonOn = NotesDisplayed;
        RaisePropertyChanged(nameof(StartButtonOn));
        if (NotesDisplayed)
        {
            using (StreamWriter sw = File.AppendText("distractions.txt"))
            {
                if (Notes != "")
                {
                    sw.WriteLine(Notes);
                    Notes = "";
                    RaisePropertyChanged(nameof(Notes));
                }
            }
            Timer.On = !_startButtonOnStart;
        }
        else
        {
            Timer.On = false;
        }

        NotesDisplayed = !NotesDisplayed;
        RaisePropertyChanged(nameof(NotesToggleLabel));
        RaisePropertyChanged(nameof(NotesDisplayed));
    }
}