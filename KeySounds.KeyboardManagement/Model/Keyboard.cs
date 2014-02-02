using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;

namespace KeySounds.KeyboardManagement.Model
{
    public class Keyboard
    {
        public enum KeyStrokeDirection { Down, Up };

        #region Serialized Properties

        public string Manufacturer { get; private set; }
        public string Model { get; private set; }
        public string Layout { get; private set; }
        public string KeyType { get; private set; }
        public string Connector { get; private set; }
        public string YearsInProduction { get; private set; }

        #endregion
        #region Non-Serialized Properties

        [JsonIgnore]
        public DirectoryInfo RootDirectory { get; set; }
        [JsonIgnore]
        public string MainImagePath { get { return Path.Combine(RootDirectory.FullName, Constants.MainImageFileName); } }
        [JsonIgnore]
        public string ExtraImagePath { get { return Path.Combine(RootDirectory.FullName, Constants.ExtraImageFileName); } }

        #endregion

        public static Keyboard Load(string jsonPath)
        {
            var js = new JsonSerializer();

            try
            {
                using (var sr = new StreamReader(jsonPath))
                using (var jr = new JsonTextReader(sr))
                {
                    var kb = js.Deserialize<Keyboard>(jr);
                        kb.RootDirectory = new DirectoryInfo(Path.GetDirectoryName(jsonPath));
                    return kb;
                }
            }
            catch (NullReferenceException)
            {
                // TODO: Handle exception
                return null;
            }
        }

        public void Save()
        {
            var js = new JsonSerializer { Formatting = Formatting.Indented };

            using (var sw = new StreamWriter(Path.Combine(RootDirectory.FullName, Constants.JsonFileName), false))
            using (var jw = new JsonTextWriter(sw))
            {
                js.Serialize(jw, this);
            }
        }

        // TODO: Tidy this the hell up once we know exactly which samples we're using!!
        public string GetPathForKeySound(int vkCode, KeyStrokeDirection direction)
        {
            var soundFiles = RootDirectory.EnumerateFiles("*" + Constants.SoundFileExtension);

            if (CompareVkCodeToName(vkCode, "return"))
            {
                return soundFiles.Single(f => f.Name.Contains("return")).FullName;
            }

            if (CompareVkCodeToName(vkCode, "space"))
            {
                return soundFiles.Single(f => f.Name.Contains("space")).FullName;
            }

            var rand = soundFiles.Where(f => f.Name.Split(new char[]{'.'}).First().EndsWith(direction.ToString().ToLower().Substring(0,1))).ToArray();
            return rand[new Random().Next(rand.Count())].FullName;
        }

        private static bool CompareVkCodeToName(int vkCode, string name)
        {
            return String.Compare(KeyInterop.KeyFromVirtualKey(vkCode).ToString(), name, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public static void TestSerializeKeyboard()
        {
            (new Keyboard
            {
                RootDirectory = new DirectoryInfo(@"C:\Users\Charles\Documents\GitHub\KeySounds\KeySounds.KeyboardManagement\Modules\IBM SSK"),
                Connector = "PS/2, 3151 RJ-45",
                KeyType = "Buckling Spring over Membrane",
                Manufacturer = "IBM",
                Model = "Space Saving Keyboard AKA Model M (SSK)",
                Layout = "84-key ANSI or 85-key ISO",
                YearsInProduction = "1987 - 1999"
            }).Save();
        }
    }
}