using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KeySounds.KeyboardManagement;
using KeySounds.KeyboardManager;

namespace KeySounds.UI.ViewModels
{
    public class TrayIconViewModel : ViewModelBase
    {
        private readonly KeyCapturer _keyCapturer;
        private bool _isMuted;

        public bool IsMuted
        {
            get { return _isMuted; }
            set { _isMuted = value; RaisePropertyChanged(() => IsMuted); }
        }

        public TrayIconViewModel()
        {
            IsMuted = false;
            _keyCapturer = new KeyCapturer(KeySoundPlayer.PlaySound);
        }

        public RelayCommand ToggleMuteCommand
        {
            get
            {
                return new RelayCommand(() => IsMuted = !IsMuted);
            }
        }

        public RelayCommand ExitCommand
        {
            get
            {
                return new RelayCommand(() => Application.Current.Shutdown());
            }
        }
    }
}