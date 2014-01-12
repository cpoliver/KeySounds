using System;
using System.Diagnostics;
using System.Windows;
using KeySounds.KeyCapture;

namespace KeySounds.UI.Views
{
    public partial class TrayIcon : Window
    {
        private readonly KeyCapturer _keyCapturer;

        public TrayIcon()
        {
            InitializeComponent();

            var a = new Action<int>((i) => Debug.WriteLine("Callback fired."));
            _keyCapturer = new KeyCapturer(a);
        }

        ~TrayIcon()
        {
            _keyCapturer.Dispose();
        }
    }
}
