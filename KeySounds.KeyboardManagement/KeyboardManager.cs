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
            _keyCapturer = new KeyCapturer(KeyDownCallback, KeyUpCallback);
            _keySoundPlayer = new KeySoundPlayer();
            Keyboard.TestSerializeKeyboard();
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

        public void KeyDownCallback(int vkCode)
        {
            Debug.WriteLine("keydown: {0}", vkCode); return;
            var soundPath = SelectedKeyboard.GetPathForKeySound(vkCode, Keyboard.KeyStrokeDirection.Down);
            _keySoundPlayer.PlaySound(soundPath);
        }

        public void KeyUpCallback(int vkCode)
        {
            Debug.WriteLine("keyup:   {0}", vkCode); return;
            var soundPath = SelectedKeyboard.GetPathForKeySound(vkCode, Keyboard.KeyStrokeDirection.Up);
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
