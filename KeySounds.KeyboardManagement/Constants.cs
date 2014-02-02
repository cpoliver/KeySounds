using System.IO;
using System.Reflection;

namespace KeySounds.KeyboardManagement
{
    public class Constants
    {
        // compile time constants
        public const string SoundFileExtension = ".wav";
        public const string JsonFileName = "keyboard.json";

        public const string MainImageFileName = "main.jpg";
        public const string ExtraImageFileName = "extra.jpg";

        // runtime constants
        public static string ApplicationRoot
        {
            get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\"); }
        }
    }
}
