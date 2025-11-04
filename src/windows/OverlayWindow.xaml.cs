using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using Wpf.Ui.Controls;

using LiveCaptionsTranscriber.utils;

namespace LiveCaptionsTranscriber
{
    public partial class OverlayWindow : Window
    {
        private Dictionary<int, Brush> ColorList = new Dictionary<int, Brush> {
            {1, Brushes.White},
            {2, Brushes.Yellow},
            {3, Brushes.LimeGreen},
            {4, Brushes.Aqua},
            {5, Brushes.Blue},
            {6, Brushes.DeepPink},
            {7, Brushes.Red},
            {8, Brushes.Black},
        };
        private int onlyMode = 0;

        public int OnlyMode
        {
            get => onlyMode;
            set
            {
                onlyMode = value;
                // No resize needed for transcription-only mode
            }
        }

        public OverlayWindow()
        {
            InitializeComponent();
            DataContext = Transcriber.Caption;

            Loaded += (s, e) => Transcriber.Caption.PropertyChanged += TranslatedChanged;
            Unloaded += (s, e) => Transcriber.Caption.PropertyChanged -= TranslatedChanged;

            this.FullTranscriptionText.Foreground = ColorList[Transcriber.Setting.OverlayWindow.FontColor];
            this.BorderBackground.Background = ColorList[Transcriber.Setting.OverlayWindow.BackgroundColor];
            this.BorderBackground.Opacity = Transcriber.Setting.OverlayWindow.Opacity;

            ApplyFontSize();
            ApplyBackgroundOpacity();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void TopThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = this.Height - e.VerticalChange;

            if (newHeight >= this.MinHeight)
            {
                this.Top += e.VerticalChange;
                this.Height = newHeight;
            }
        }

        private void BottomThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newHeight = this.Height + e.VerticalChange;

            if (newHeight >= this.MinHeight)
            {
                this.Height = newHeight;
            }
        }

        private void LeftThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = this.Width - e.HorizontalChange;

            if (newWidth >= this.MinWidth)
            {
                this.Left += e.HorizontalChange;
                this.Width = newWidth;
            }
        }

        private void RightThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            double newWidth = this.Width + e.HorizontalChange;

            if (newWidth >= this.MinWidth)
            {
                this.Width = newWidth;
            }
        }

        private void TopLeftThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            TopThumb_OnDragDelta(sender, e);
            LeftThumb_OnDragDelta(sender, e);
        }

        private void TopRightThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            TopThumb_OnDragDelta(sender, e);
            RightThumb_OnDragDelta(sender, e);
        }

        private void BottomLeftThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            BottomThumb_OnDragDelta(sender, e);
            LeftThumb_OnDragDelta(sender, e);
        }

        private void BottomRightThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            BottomThumb_OnDragDelta(sender, e);
            RightThumb_OnDragDelta(sender, e);
        }

        private void TranslatedChanged(object sender, PropertyChangedEventArgs e)
        {
            ApplyFontSize();
            
            // Auto-scroll to bottom when new transcription is added
            if (e.PropertyName == nameof(Transcriber.Caption.FullTranscriptionText))
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    OverlayScrollViewer.ScrollToBottom();
                }), DispatcherPriority.Background);
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            ControlPanel.Visibility = Visibility.Visible;
            OverlayControlButtons.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            ControlPanel.Visibility = Visibility.Hidden;
            OverlayControlButtons.Visibility = Visibility.Hidden;
        }

        private void OverlayCopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Transcriber.Caption.FullTranscriptionText))
                {
                    Clipboard.SetText(Transcriber.Caption.FullTranscriptionText);
                    ShowOverlayMessage("Copied!");
                }
                else
                {
                    ShowOverlayMessage("No text to copy");
                }
            }
            catch
            {
                ShowOverlayMessage("Copy failed");
            }
        }

        private void OverlayClearButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Transcriber.Caption.ClearFullTranscription();
                ShowOverlayMessage("Cleared");
            }
            catch
            {
                ShowOverlayMessage("Clear failed");
            }
        }

        private async void ShowOverlayMessage(string message)
        {
            var originalText = OverlayCopyButton.Content;
            OverlayCopyButton.Content = message;
            await Task.Delay(1000);
            OverlayCopyButton.Content = originalText;
        }

        private void FontIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (Transcriber.Setting.OverlayWindow.FontSize + 1 < 60)
            {
                Transcriber.Setting.OverlayWindow.FontSize++;
                ApplyFontSize();
            }
        }

        private void FontDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (Transcriber.Setting.OverlayWindow.FontSize - 1 > 8)
            {
                Transcriber.Setting.OverlayWindow.FontSize--;
                ApplyFontSize();
            }
        }

        private void FontColorCycle_Click(object sender, RoutedEventArgs e)
        {
            Transcriber.Setting.OverlayWindow.FontColor++;
            if (Transcriber.Setting.OverlayWindow.FontColor > ColorList.Count)
                Transcriber.Setting.OverlayWindow.FontColor = 1;
            FullTranscriptionText.Foreground = ColorList[Transcriber.Setting.OverlayWindow.FontColor];
        }

        private void OpacityIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (Transcriber.Setting.OverlayWindow.Opacity + 20 < 251)
                Transcriber.Setting.OverlayWindow.Opacity += 20;
            else
                Transcriber.Setting.OverlayWindow.Opacity = 251;
            ApplyBackgroundOpacity();
        }

        private void OpacityDecrease_Click(object sender, RoutedEventArgs e)
        {
            if (Transcriber.Setting.OverlayWindow.Opacity - 20 > 1)
                Transcriber.Setting.OverlayWindow.Opacity -= 20;
            else
                Transcriber.Setting.OverlayWindow.Opacity = 1;
            ApplyBackgroundOpacity();
        }

        private void BackgroundColorCycle_Click(object sender, RoutedEventArgs e)
        {
            Transcriber.Setting.OverlayWindow.BackgroundColor++;
            if (Transcriber.Setting.OverlayWindow.BackgroundColor > ColorList.Count)
                Transcriber.Setting.OverlayWindow.BackgroundColor = 1;
            BorderBackground.Background = ColorList[Transcriber.Setting.OverlayWindow.BackgroundColor];

            BorderBackground.Opacity = Transcriber.Setting.OverlayWindow.Opacity;
            ApplyBackgroundOpacity();
        }



        private void ClickThrough_Click(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = WindowsAPI.GetWindowLong(hwnd, WindowsAPI.GWL_EXSTYLE);
            WindowsAPI.SetWindowLong(hwnd, WindowsAPI.GWL_EXSTYLE, extendedStyle | WindowsAPI.WS_EX_TRANSPARENT);
            ControlPanel.Visibility = Visibility.Collapsed;
        }

        public void ApplyFontSize()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.FullTranscriptionText.FontSize = Transcriber.Setting.OverlayWindow.FontSize;
            }), DispatcherPriority.Background);
        }

        public void ApplyBackgroundOpacity()
        {
            Color color = ((SolidColorBrush)BorderBackground.Background).Color;
            BorderBackground.Background = new SolidColorBrush(
                Color.FromArgb(Transcriber.Setting.OverlayWindow.Opacity, color.R, color.G, color.B));
        }
    }
}
