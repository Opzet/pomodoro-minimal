using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
namespace PomodoroMinimal;

public enum State { Work, ShortBreak, LongBreak, Settings };

public class Config
{
    public byte? WorkTimeInput { get; set; } = 30;
    public byte? ShortBreakTimeInput { get; set; } = 5;
    public byte? LongBreakTimeInput { get; set; } = 15;
    public byte? LongBreakPeriodInput { get; set; } = 3;

    public void ProcessConfigInput()
    {
        WorkTime = (byte)WorkTimeInput!;
        ShortBreakTime = (byte)ShortBreakTimeInput!;
        LongBreakTime = (byte)LongBreakTimeInput!;
        LongBreakPeriod = (byte)LongBreakPeriodInput!;
    }
    
    public byte WorkTime { get; private set; }
    public byte ShortBreakTime { get; private set; }
    public byte LongBreakTime { get; private set; }
    // how many shortBreaks are between two longBreaks
    public byte LongBreakPeriod { get; private set; }
}

public class PomodoroTimer
{
    public bool On = false;
    public byte Minutes;
    public byte Seconds;
    // how many short breaks have I gone through
    private byte _periodCounter = 0;

    private readonly Config _config;
    

    // updates the clock and returns the after-state
    public State SecondUpdate(State currentState)
    {
        State state = currentState;
        if (Seconds == 0)
        { 
            Seconds = 60;
            Minutes--;
        }

        Seconds -= 1;
        // time is over
        if (Minutes == 0 && Seconds == 0)
        {
            switch (state)
            {
                case State.Work:
                    _periodCounter = (byte)((_periodCounter + 1) % (_config.LongBreakPeriod + 1));
                    if (_periodCounter == 0)
                    { 
                        state = State.LongBreak;
                        Minutes = _config.LongBreakTime;
                    }
                    else
                    { 
                        state = State.ShortBreak;
                        Minutes = _config.ShortBreakTime;
                    }
                    break;
                case State.ShortBreak:
                case State.LongBreak:
                    state = State.Work;
                    Minutes = _config.WorkTime;
                    break;
                default:
                    break;
            }

            On = false;
        }
        return state;
    }


    public PomodoroTimer(Config config)
    {
        _config = config;
        Minutes = config.WorkTime;
    }
}
public class Model : INotifyPropertyChanged
{
    public Config Config { get; } = new();
    private State _state = State.Settings;
    public bool IsInSettings => _state == State.Settings;
    
    // Timer setup
    private DispatcherTimer _clock;
    public PomodoroTimer Timer { get; set; }
    public string Time => $"{Timer.Minutes:00}:{Timer.Seconds:00}";
    
    // Start button setup
    public bool StartButtonOn { get; private set; } = true;
    private bool _startButtonOnStart = true;
    private readonly string[] StartButtonLabels = new[] { "START", "STOP" };

    public string StartButtonText => _startButtonOnStart ? StartButtonLabels[0] : StartButtonLabels[1];
    
    // Activity text setup
    private string[] _activities = new string[] { "work", "short break", "long break" };
    public string Activity => _activities[(int)_state];
    
    // Note toggle setup
    //private string[] _toggleTexts = new string[] { "v", "^" };
    public bool NotesDisplayed { get; set; }
    public string Notes { get; set; }
    private string _pathToNotes = "distractions.txt";
    private readonly string[] _notesToggleLabels = new string[] { "v", "^" };
    public string NotesToggleLabel => NotesDisplayed ? _notesToggleLabels[1] : _notesToggleLabels[0];

    public PopupWindow Popup { get; set; } = new ();
    
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
        _state = State.Work;
        RaisePropertyChanged(nameof(Activity));
        Config.ProcessConfigInput();
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
            _state = Timer.SecondUpdate(_state);
            RaisePropertyChanged(nameof(Time));
            // if the update turned off the timer, it is due to activity change
            if (!Timer.On)
            {
                _startButtonOnStart = true;
                RaisePropertyChanged(nameof(StartButtonText));
                RaisePropertyChanged(nameof(Activity));
                // displays the popup over other application, which makes the main window invisible (see xaml)
                // closing the popup show it again
                Popup = new PopupWindow();
                Popup.Show();
                RaisePropertyChanged(nameof(Popup));
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
            using (StreamWriter sw = File.AppendText(_pathToNotes))
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