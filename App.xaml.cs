using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KeyLight.ViewModels;
using KeyLight.Views;

namespace KeyLight
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                    WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.Manual
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}