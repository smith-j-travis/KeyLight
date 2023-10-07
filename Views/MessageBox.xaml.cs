using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KeyLight.ViewModels;

namespace KeyLight.Views
{
    public class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }

        public MessageBox(string title, string message)
        {
            this.DataContext = new MessageBoxViewModel {
                Title = title,
                Message = message
            };

            InitializeComponent();

            var okButton = this.FindControl<Button>("OKButton");
            okButton.IsDefault = true;
            okButton.Click += (s, e) => {
                this.Close();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}