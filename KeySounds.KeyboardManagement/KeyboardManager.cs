namespace KeySounds.KeyboardManagement
{
    public class KeyboardManager
    {
        #region Private Fields

        private KeyCapturer _keyCapturer;
        private object _keySoundPlayer;

        #endregion
        #region Public Properties

        public Keyboard SelectedKeyboard { get; set; }
        public Keyboard ReserveKeyboard { get; set; }

        #endregion
        #region Constructor

        public KeyboardManager()
        {
            
        }

        #endregion
        #region Public Methods

        public void ToggleKeyboards()
        {
            var temp = SelectedKeyboard;
            SelectedKeyboard = ReserveKeyboard;
            ReserveKeyboard = temp;
        }

        #endregion
        #region Private Methods


        #endregion
    }
}
