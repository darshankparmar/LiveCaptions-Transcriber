using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using LiveCaptionsTranscriber.utils;

namespace LiveCaptionsTranscriber
{
    public partial class CaptionPage : Page
    {
        public const int CARD_HEIGHT = 110;

        private static CaptionPage instance;
        public static CaptionPage Instance => instance;

        public CaptionPage()
        {
            InitializeComponent();
            DataContext = Transcriber.Caption;
            instance = this;

            Loaded += (s, e) =>
            {
                Transcriber.Caption.PropertyChanged += TranslatedChanged;
            };
            Unloaded += (s, e) =>
            {
                Transcriber.Caption.PropertyChanged -= TranslatedChanged;
            };
        }

        private async void TextBlock_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                try
                {
                    Clipboard.SetText(textBlock.Text);
                    textBlock.ToolTip = "Copied!";
                }
                catch
                {
                    textBlock.ToolTip = "Error to Copy";
                }
                await Task.Delay(500);
                textBlock.ToolTip = "Click to Copy";
            }
        }

        private void TranslatedChanged(object sender, PropertyChangedEventArgs e)
        {
            // Auto-scroll to bottom when new transcription is added
            if (e.PropertyName == nameof(Transcriber.Caption.FullTranscriptionText))
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    TranscriptionScrollViewer.ScrollToBottom();
                }), DispatcherPriority.Background);
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Transcriber.Caption.FullTranscriptionText))
                {
                    Clipboard.SetText(Transcriber.Caption.FullTranscriptionText);
                    ShowTemporaryMessage("Copied to clipboard!");
                }
                else
                {
                    ShowTemporaryMessage("No transcription to copy");
                }
            }
            catch
            {
                ShowTemporaryMessage("Failed to copy");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Transcriber.Caption.ClearFullTranscription();
                ShowTemporaryMessage("Transcription cleared");
            }
            catch
            {
                ShowTemporaryMessage("Failed to clear");
            }
        }

        private async void ShowTemporaryMessage(string message)
        {
            var originalText = CopyButton.Content;
            CopyButton.Content = message;
            await Task.Delay(1500);
            CopyButton.Content = originalText;
        }

        public void CollapseTranslatedCaption(bool isCollapsed)
        {
            // Not needed in transcription-only mode
        }


    }
}
