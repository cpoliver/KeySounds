using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KeySounds.KeyboardManagement
{
    public class Constants
    {
        // compile time contants
        public const string SoundFileExtension = ".wav";
        public const string JsonFileName = "keyboard.json";

        public const string MainImageFileName = "main.jpg";
        public const string ExtraImageFileName = "extra.jpg";

        public const string KeySoundFormatString = "{0}_{1}" + SoundFileExtension;

        // runtime constants
        public static string ApplicationRoot
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }
    }
}
