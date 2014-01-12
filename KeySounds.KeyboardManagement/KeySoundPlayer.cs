using System;
using System.IO;
using System.Reflection;
using NAudio.Wave;

namespace KeySounds.KeyboardManagement
{
    public static class KeySoundPlayer
    {
        private static readonly IWavePlayer WaveOutDevice = new WaveOut();

        private static Stream GetResourcePath(int virtualKeyCode)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            var x = assembly.GetManifestResourceNames();

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

        public static void PlaySound(int virtualKeyCode)
        {
            var mp3 = new Mp3FileReader(GetResourcePath(virtualKeyCode));
            WaveOutDevice.Init(mp3);
            WaveOutDevice.Play();
        }
    }
}
