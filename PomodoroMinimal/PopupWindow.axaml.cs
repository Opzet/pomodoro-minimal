using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PomodoroMinimal;

public partial class PopupWindow : Window
{
    public PopupWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}