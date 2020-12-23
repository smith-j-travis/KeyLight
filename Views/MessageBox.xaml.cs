using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}