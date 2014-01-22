using NAudio.Wave;
using System;
using System.IO;
using System.Reflection;

namespace KeySounds.KeyboardManagement
{
    public class KeySoundPlayer
    {
        private string GetFilePath(int virtualKeyCode)
        {
            var exePath = Assembly.GetExecutingAssembly().FullName;
            var soundDirectory = Path.Combine(exePath.FullName);
            var filePath = Path.Combine(soundDirectory,
                String.Format("{0}.{1}", virtualKeyCode, Constants.SoundFileExtension));

            if (File.Exists(filePath))
                PlaySound(virtualKeyCode);
            else
                PlayAltSound(virtualKeyCode);
        }


        public void PlaySound(int virtualKeyCode)
        {
            var audio = new AudioFileReader(GetFilePath(virtualKeyCode));
            WaveOutDevice.Init(audio);
            WaveOutDevice.Play();
        }

        public void PlayAltSound(int virtualKeyCode)
        {
            // TODO: build logic to find similar alternative key if the specific sound file doesn't exist for a specific key code
            PlaySound(virtualKeyCode);
        }

        // TODO: Replace below code...
        private static readonly IWavePlayer WaveOutDevice = new WaveOut();
        private static Stream GetResourcePath(int virtualKeyCode)
        {
            var assembly = Assembly.GetExecutingAssembly();

            switch (virtualKeyCode)
            {
                case 13:
                    return assembly.GetManifestResourceStream("KeySounds.KeyboardManagement.KeySounds.Return.mp3");
                case 32:
                    return assembly.GetManifestResourceStream("KeySounds.KeyboardManagement.KeySounds.Space.mp3");
                default:
                    return assembly.GetManifestResourceStream(String.Format("KeySounds.KeyboardManagement.KeySounds.{0}.mp3", new Random().Next(8)));
            }
        }
        public static void PlaySoundOld(int virtualKeyCode)
        {
            var mp3 = new Mp3FileReader(GetResourcePath(virtualKeyCode));
            WaveOutDevice.Init(mp3);
            WaveOutDevice.Play();
        }
    }
}
