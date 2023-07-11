using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Threading;

namespace PomodoroMinimal;

public enum State { Work, ShortBreak, LongBreak };

public class Config
{
    public byte workTime = 25;
    public byte shortBreakTime = 5;
    public byte longBreakTime = 15;
    // how many shortBreaks are between two longBreaks
    public byte longBreakPeriod = 2;
    public string[] descriptors = new string[] { "work", "short break", "long break" };
}

public class PomodoroTimer
{
    public bool On = true;
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
        seconds--;
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
    private Config _config = new();
    public State state;
    private DispatcherTimer _clock;
    public PomodoroTimer Timer { get; }
    public string Time => $"{Timer.minutes:00}:{Timer.seconds:00}";
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    // raises the above event
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public Model()
    {
        Timer = new PomodoroTimer(_config);
        _clock = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.MaxValue,
            new System.EventHandler(TimerTick));
    }
    
    private void TimerTick(object? sender, EventArgs e)
    {
        if (Timer.On)
        {
            state = Timer.SecondUpdate(state);
            RaisePropertyChanged(nameof(Time));
        }
    }
}