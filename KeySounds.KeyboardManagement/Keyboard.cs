using System;
using System.IO;
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
        #region Non-Serialized Properties

        [JsonIgnore]
        public string RootDirectory { get; set; }
        [JsonIgnore]
        public string MainImagePath { get { return Path.Combine(RootDirectory, Constants.MainImageFileName); } }
        [JsonIgnore]
        public string ExtraImagePath { get { return Path.Combine(RootDirectory, Constants.ExtraImageFileName); } }

        #endregion

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
            var js = new JsonSerializer { Formatting = Formatting.Indented };

            using (var sw = new StreamWriter(Path.Combine(RootDirectory, Constants.JsonFileName), false))
            using (var jw = new JsonTextWriter(sw))
            {
                js.Serialize(jw, this);
            }
        }

        public string GetPathForKeySound(int vkCode, KeyStrokeDirection direction)
        {
            return Path.Combine(RootDirectory, String.Format(Constants.KeySoundFormatString, vkCode, direction.ToString().ToUpper()));
        }

        public static void TestSerializeKeyboard()
        {
            (new Keyboard
            {
                RootDirectory = @"C:\Users\Charles\Desktop",
                Connector = "PS/2",
                KeyType = "Buckling Spring",
                Manufacturer = "IBM",
                Model = "Model M (1391506)",
                NumberOfKeys = 105,
                YearsInProduction = "1987 - 1999"
            }).Save();
        }
    }
}