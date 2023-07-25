using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using Avalonia.Threading;

using static PomodoroMinimal.Model;

namespace PomodoroMinimal;


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
    private byte? _work = (int)States.Work;

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

    private byte? _shortBreak = (int)States.ShortBreak;

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

    private byte? _longBreak = (int)States.LongBreak;

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
    private byte? _longBreakPeriod = (int)States.LongBreakPeriod;

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

    private readonly Config _config = new Config();
    // this value would be used if user didnt fill the input
    // because the apply button is enabled only with valid settings, this just suppresses errors
    private const byte _defaultTimingOption = 0;

    // updates the clock and returns the after-state
    public States SecondUpdate(States currentState)
    {
        States state = currentState;
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
                case States.Work:
                    _periodCounter = (byte)((_periodCounter + 1) % (_config.LongBreakPeriod + 1));
                    if (_periodCounter == 0)
                    {
                        state = States.LongBreak;
                        Minutes = _config.LongBreak ?? _defaultTimingOption;
                    }
                    else
                    {
                        state = States.ShortBreak;
                        Minutes = _config.ShortBreak ?? _defaultTimingOption;
                    }
                    break;
                case States.ShortBreak:
                case States.LongBreak:
                    state = States.Work;
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
    private States _state = States.Settings;
    public bool IsInSettings => _state == States.Settings;

    // Timer setup
    private DispatcherTimer _clock = new DispatcherTimer();
    public PomodoroTimer Timer { get; set; } = new PomodoroTimer(new Config());
    public string Time => $"{Timer.Minutes:00}:{Timer.Seconds:00}";

    // Start button setup
    public bool StartButtonOn { get; private set; } = true;
    private bool _startButtonOnStart = true;
    private readonly string[] StartButtonLabels = new[] { "START", "STOP" };

    public string StartButtonText => _startButtonOnStart ? StartButtonLabels[0] : StartButtonLabels[1];

    /// <summary>
    /// Simplify by using reflection to set/get state from enum  ;)
    /// Makes for easy setup
    /// </summary>
    [System.ComponentModel.Description("States")]
    public enum States : Int32
    {
        // Note: Cannot have same startup time periods for short and long break, need to be unique times as they are an index
        [System.ComponentModel.DataAnnotations.Display(Name = "Settings")]
        Settings = 0,
        LongBreakPeriod = 2, //Not used as a string so no need for dataannotation
        [System.ComponentModel.DataAnnotations.Display(Name = "Short Break")]
        ShortBreak = 5,
        [System.ComponentModel.DataAnnotations.Display(Name = "Long Break")]
        LongBreak = 15,
        [System.ComponentModel.DataAnnotations.Display(Name = "Work")]
        Work = 30,
    }

    public string Activity => EnumIdToString<States>(_state);

    // Note toggle setup
    //private string[] _toggleTexts = new string[] { "v", "^" };
    public bool NotesDisplayed { get; set; }
    public string? Notes { get; set; }
    private string _pathToNotes = "distractions.txt";
    private readonly string[] _notesToggleLabels = new string[] { "v", "^" };
    public string NotesToggleLabel => NotesDisplayed ? _notesToggleLabels[1] : _notesToggleLabels[0];

    public PopupWindow Popup { get; set; } = new();

    public Model()
    {
    }

    /// <summary>
    /// Convert Enum Id to Data Annotations string
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="EnumObj"></param>
    /// <returns></returns>
    public static string EnumIdToString<TEnum>(object EnumObj)
    {
        if (EnumObj == null)
            return " ";

        int EnumId = (int)EnumObj;

        var enumType = typeof(TEnum);
        var fields = enumType.GetMembers()
            .OfType<System.Reflection.FieldInfo>()
            .Where(p => p.MemberType == MemberTypes.Field)
            .Where(p => p.IsLiteral)
            .ToList();

        var valuesByName = new Dictionary<string, object>();

        foreach (var field in fields)
        {

            //Need reference to Data Annotation 
            var dataAttribute = field.GetCustomAttribute(typeof(DisplayAttribute), false) as DisplayAttribute;

            var value = (int)field.GetValue(null);
            var description = string.Empty;

            if (!string.IsNullOrEmpty(dataAttribute?.Name))
            {
                description = dataAttribute.Name;
            }
            else
            {
                description = field.Name;
            }

            if (value == EnumId)
            {
                if ((description.Trim() == "-") || (description.Trim().ToUpper() == "NOT APPLICABLE"))
                    return " ";
                else
                    return description;
            }
        }

        return " ";
    }

    public void ApplyClick()
    {
        _state = States.Work;
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
            States state = Enum.TryParse<States>(_state.ToString(), out var result) ? result : States.Settings;

            _state = Timer.SecondUpdate(state);
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