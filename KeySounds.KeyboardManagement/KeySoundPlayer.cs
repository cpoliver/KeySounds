using System.Collections.Generic;
using System.Linq;
using NAudio.Wave;
using System;
using System.IO;
using System.Reflection;
using NAudio.Wave.SampleProviders;

namespace KeySounds.KeyboardManagement
{
    public class KeySoundPlayer
    {
        private static readonly IWavePlayer WaveOutDevice = new WaveOut();

        private float _volume = 1F;

        public float Volume
        {
            get { return _volume; }
            set { _volume = (new [] {value, 1F}).Min(); }
        }
        public bool IsMuted { get; set; }

        public void PlaySound(string path)
        {
            if (!File.Exists(path)) return;

            var audio = new AudioFileReader(path);
            var vol = new VolumeSampleProvider(audio) { Volume = Volume };
            WaveOutDevice.Init(vol);
            WaveOutDevice.Play();
        }
    }
}
