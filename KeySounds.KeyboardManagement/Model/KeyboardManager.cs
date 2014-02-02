using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KeySounds.KeyboardManagement.Model
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
            Keyboards = new List<Keyboard>();

            //Keyboard.TestSerializeKeyboard();
            LoadKeyboards();
            SelectedKeyboard = Keyboards.First();
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
            // only single sample for return and space keys for the time being
            if ((vkCode == 13 || vkCode == 32) && direction == Keyboard.KeyStrokeDirection.Up) return;

            var soundPath = SelectedKeyboard.GetPathForKeySound(vkCode, direction);
            _keySoundPlayer.PlaySound(soundPath);
        }

        #endregion
        #region Private Methods

        private void LoadKeyboards()
        {
            var keyboards = Directory.EnumerateFiles(Constants.ApplicationRoot,
                                                     Constants.JsonFileName,
                                                     SearchOption.AllDirectories)
                                     .ToList();

            foreach (var kb in keyboards)
            {
                Keyboards.Add(Keyboard.Load(kb));
            }

            //         .ForEach(kb => Keyboards.Add(Keyboard.Load(kb)));
        }

        #endregion
    }
}
