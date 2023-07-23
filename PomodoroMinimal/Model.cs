using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
namespace PomodoroMinimal;

public enum State { Work, ShortBreak, LongBreak, Settings };

public abstract class PropertyChangedRaiser : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    // raises the above event
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class Config : PropertyChangedRaiser
{
    private byte? _work = 30;

    [Required]
    public byte? Work
    {
        get => _work;
        set
        {
            _work = value;
            RaisePropertyChanged(nameof(ValidSettings));
        }
    }

    private byte? _shortBreak = 5;

    [Required]
    public byte? ShortBreak
    {
        get => _shortBreak;
        set
        {
            _shortBreak = value;
            RaisePropertyChanged(nameof(ValidSettings));
        }
    }

    private byte? _longBreak = 15;

    [Required]
    public byte? LongBreak
    {
        get => _longBreak;
        set
        {
            _longBreak = value;
            RaisePropertyChanged(nameof(ValidSettings));
        }
    }

    // how many shortBreaks are between two longBreaks
    private byte? _longBreakPeriod = 2;

    [Required]
    public byte? LongBreakPeriod
    {
        get => _longBreakPeriod;
        set
        {
            _longBreakPeriod = value;
            RaisePropertyChanged(nameof(ValidSettings));
        }
    }

    public bool ValidSettings => Work.HasValue && ShortBreak.HasValue &&
                                 LongBreak.HasValue && LongBreakPeriod.HasValue;
}

public class PomodoroTimer
{
    public bool On = false;
    public byte Minutes;
    public byte Seconds;
    // how many short breaks have I gone through
    private byte _periodCounter = 0;

    private readonly Config _config;
    // this value would be used if user didnt fill the input
    // because the apply button is enabled only with valid settings, this just suppresses errors
    private const byte _defaultTimingOption = 0;

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
                        Minutes = _config.LongBreak ?? _defaultTimingOption;
                    }
                    else
                    { 
                        state = State.ShortBreak;
                        Minutes = _config.ShortBreak ?? _defaultTimingOption;
                    }
                    break;
                case State.ShortBreak:
                case State.LongBreak:
                    state = State.Work;
                    Minutes = _config.Work ?? _defaultTimingOption;
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
        Minutes = config.Work ?? _defaultTimingOption;
    }
}
public class Model : PropertyChangedRaiser
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
    
    public Model()
    {
    }

    public void ApplyClick()
    {
        _state = State.Work;
        RaisePropertyChanged(nameof(Activity));
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