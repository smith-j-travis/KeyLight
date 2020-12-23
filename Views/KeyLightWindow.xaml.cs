using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KeyLight.Views
{
    public class KeyLightWindow : Window
    {
        public KeyLightWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}