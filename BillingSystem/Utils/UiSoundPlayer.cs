using System;
using System.IO;
using System.Media;

namespace BillingSystem.Utils
{
    internal static class UiSoundPlayer
    {
        private static readonly object SyncRoot = new object();
        private static SoundPlayer? _clickPlayer;
        private static SoundPlayer? _errorPlayer;
        private static bool _clickLoadAttempted;
        private static bool _errorLoadAttempted;

        public static void PlayClick()
        {
            Play(_clickPlayer ??= LoadPlayer(ref _clickLoadAttempted, "click.wav"));
        }

        public static void PlayError()
        {
            Play(_errorPlayer ??= LoadPlayer(ref _errorLoadAttempted, "ErrorSound.wav"));
        }

        private static void Play(SoundPlayer? player)
        {
            if (player == null)
                return;

            try
            {
                player.Stop();
                player.Play();
            }
            catch
            {
                // Ignore UI sound failures so the app keeps working normally.
            }
        }

        private static SoundPlayer? LoadPlayer(ref bool loadAttempted, string fileName)
        {
            if (loadAttempted)
                return fileName.Equals("click.wav", StringComparison.OrdinalIgnoreCase)
                    ? _clickPlayer
                    : _errorPlayer;

            lock (SyncRoot)
            {
                if (loadAttempted)
                    return fileName.Equals("click.wav", StringComparison.OrdinalIgnoreCase)
                        ? _clickPlayer
                        : _errorPlayer;

                loadAttempted = true;

                string soundPath = Path.Combine(AppContext.BaseDirectory, "Resources", fileName);
                if (!File.Exists(soundPath))
                    return null;

                try
                {
                    var player = new SoundPlayer(soundPath);
                    player.Load();
                    return player;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
