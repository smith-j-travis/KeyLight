using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace KeyLight.Views
{
    public class MainWindow : Window
    {
        private const int DIVISOR = 10;
        private static SolidColorBrush WHITE = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        private static SolidColorBrush BLACK = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private Dictionary<int, KeyLightWindow> _openedWindows = new Dictionary<int, KeyLightWindow>();

        public MainWindow()
        {
            InitializeComponent();
            var allScreens = this.Screens.All;
            var mainFormCanvas = this.FindControl<Canvas>("displayCanvas");
            var monitorGrid = new Dictionary<int, List<int>>();

            for(var i = 0; i < allScreens.Count; i++) {
                var screen = allScreens[i];

                if(!monitorGrid.ContainsKey(screen.Bounds.Y)) {
                    monitorGrid.Add(screen.Bounds.Y, new List<int> { 0, 0 });
                }
                    
                monitorGrid[screen.Bounds.Y][0] += screen.Bounds.Width;
                monitorGrid[screen.Bounds.Y][1] += screen.Bounds.Height;

                var button = new Button {
                    Content = "Screen " + i,
                    Margin = new Avalonia.Thickness(screen.Bounds.X / DIVISOR, screen.Bounds.Y / DIVISOR),
                    Width = screen.Bounds.Width / DIVISOR,
                    Height = screen.Bounds.Height / DIVISOR,
                    Background = WHITE,
                    Foreground = BLACK,
                    BorderBrush = WHITE,
                    BorderThickness = new Avalonia.Thickness(1)
                };
            
                var iCopy = i;

                // don't allow setting it on primary monitor
                if(screen.IsPrimary) {
                    button.Click += delegate { 
                        new MessageBox("Cannot set keylight", "You cannot set a keylight on the primary monitor.").ShowDialog(this);
                    };
                }
                else {
                    button.Click += delegate { this.OpenKeyLight(iCopy, button); };
                }
                    
                mainFormCanvas.Children.Add(button);
            }

            var canvasSize = GetCanvasSize(monitorGrid);
            mainFormCanvas.Width = canvasSize.Item1;
            mainFormCanvas.Height = canvasSize.Item2;

            this.Closing += (s, e) =>
            {
                foreach(var kvp in _openedWindows) {
                    kvp.Value.Close();
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OpenKeyLight(int screenId, Button button) {
            if(this._openedWindows.ContainsKey(screenId)) {
                button.Foreground = BLACK;
                button.Background = WHITE;

                this._openedWindows[screenId].Close();

                this._openedWindows.Remove(screenId);
                return;
            }

            var keyLightWindow = new KeyLightWindow {
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            var window = this.Screens.All[screenId];
            keyLightWindow.Width = window.WorkingArea.Width;
            keyLightWindow.Height = window.WorkingArea.Height;

            keyLightWindow.Show();
            button.Foreground = WHITE;
            button.Background = BLACK;
            this._openedWindows.Add(screenId, keyLightWindow);

            keyLightWindow.Position = window.Bounds.Position;
        }

        private Tuple<int, int> GetCanvasSize(Dictionary<int, List<int>> monitorGrid) {

            var canvasHeight = 0;
            var canvasWidth = 0;
            foreach(var kvp in monitorGrid) {
                if(kvp.Value[0] > canvasWidth) {
                    canvasWidth = kvp.Value[0];
                }
                    
                if(kvp.Value[1] > canvasHeight) {
                    canvasHeight = kvp.Value[1];
                }  
            }

            return new Tuple<int, int> (canvasWidth / (DIVISOR * 2), canvasHeight / (DIVISOR * 2));
        }
    }
}