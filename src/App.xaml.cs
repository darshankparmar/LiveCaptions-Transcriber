using System.Windows;

using LiveCaptionsTranscriber.utils;

namespace LiveCaptionsTranscriber
{
    public partial class App : Application
    {
        App()
        {
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            Transcriber.Setting?.Save();

            Task.Run(() => Transcriber.SyncLoop());
            Task.Run(() => Transcriber.TranscribeLoop());
            Task.Run(() => Transcriber.DisplayLoop());
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            if (Transcriber.Window != null)
            {
                LiveCaptionsHandler.RestoreLiveCaptions(Transcriber.Window);
                LiveCaptionsHandler.KillLiveCaptions(Transcriber.Window);
            }
        }
    }
}
