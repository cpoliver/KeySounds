using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KeySounds.KeyboardManagement
{
    public class KeyboardManager
    {
        #region Private Fields

        private readonly KeyCapturer _keyCapturer;
        private readonly KeySoundPlayer _keySoundPlayer;

        #endregion
        #region Public Properties

        public List<Keyboard> Keyboards { get; private set; }
        public Keyboard SelectedKeyboard { get; set; }
        public Keyboard ReserveKeyboard { get; set; }

        #endregion
        #region Constructor

        public KeyboardManager()
        {
            _keyCapturer = new KeyCapturer(KeyEventCallback);
            _keySoundPlayer = new KeySoundPlayer();
            //Keyboard.TestSerializeKeyboard();
            LoadKeyboards();
        }

        #endregion
        #region Public Methods

        public void ToggleKeyboards()
        {
            var temp = SelectedKeyboard;
            SelectedKeyboard = ReserveKeyboard;
            ReserveKeyboard = temp;
        }

        public void KeyEventCallback(int vkCode, Keyboard.KeyStrokeDirection direction)
        {
            Debug.WriteLine("keyevent: {0} {1}", direction, vkCode); return;
            var soundPath = SelectedKeyboard.GetPathForKeySound(vkCode, direction);
            _keySoundPlayer.PlaySound(soundPath);
        }

        #endregion
        #region Private Methods

        private void LoadKeyboards()
        {
            Task.Factory.StartNew(() => Directory.EnumerateFiles(Constants.ApplicationRoot,
                                                                 Constants.JsonFileName,
                                                                 SearchOption.AllDirectories)
                                                 .ToList()
                                                 .ForEach(kb => Keyboards.Add(Keyboard.Load(kb))));
        }

        #endregion
    }
}
