using ReactiveUI;
namespace StressCheckAvalonia.ViewModels;

public class MainViewModel : ReactiveObject
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
}