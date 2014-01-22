using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KeySounds.KeyboardManagement;

namespace KeySounds.UI.ViewModels
{
    public class TrayIconViewModel : ViewModelBase
    {
        #region Backing Fields

        private readonly KeyCapturer _keyCapturer;
        private bool _isMuted;
        private float _volume;

        #endregion
        #region Properties

        public bool IsMuted
        {
            get { return _isMuted; }
            set { _isMuted = value; RaisePropertyChanged(() => IsMuted); }
        }

        public float Volume
        {
            get { return _volume; }
            set { _volume = value; RaisePropertyChanged(() => Volume); }
        }

        #endregion
        #region Constructor

        public TrayIconViewModel()
        {
            IsMuted = false;
            _keyCapturer = new KeyCapturer(KeySoundPlayer.PlaySoundOld);
        }

        #endregion
        #region Commands

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

        #endregion
    }
}