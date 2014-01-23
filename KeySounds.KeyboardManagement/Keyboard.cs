using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KeySounds.KeyboardManagement
{
    public class Keyboard
    {
        public enum KeyStrokeDirection { Down, Up };

        #region Serialized Properties

        public string Manufacturer { get; private set; }
        public string Model { get; private set; }
        public int NumberOfKeys { get; private set; }
        public string KeyType { get; private set; }
        public string Connector { get; private set; }
        public string YearsInProduction { get; private set; }

        #endregion

        [NonSerialized]
        private string _rootDirectory;
        public string RootDirectory {
            get { return _rootDirectory; }
            private set { _rootDirectory = value; }
        }

        public string MainImagePath { get { return Path.Combine(RootDirectory, Constants.MainImageFileName); } }
        public string ExtraImagePath { get { return Path.Combine(RootDirectory, Constants.ExtraImageFileName); } }

        public static Keyboard Load(string jsonPath)
        {
            var js = new JsonSerializer();

            using (var sr = new StreamReader(jsonPath))
            using (var jr = new JsonTextReader(sr))
            {
                var kb = js.Deserialize<Keyboard>(jr);
                    kb.RootDirectory = Path.GetDirectoryName(jsonPath);
                return kb;
            }
        }

        public void Save()
        {
            var js = new JsonSerializer();

            using (var sw = new StreamWriter(Path.Combine(RootDirectory, Constants.JsonFileName), append:false))
            using (var jw = new JsonTextWriter(sw))
            {
                js.Serialize(jw, this);
            }
        }

        public string GetPathForKeySound(int vkCode, KeyStrokeDirection direction)
        {
            return Path.Combine(RootDirectory, String.Format(Constants.KeySoundFormatString, vkCode, direction.ToString().ToUpper()));
        }
    }
}
