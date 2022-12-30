using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace EnigmaPuzzle
{
    /// <summary>
    /// Static class to read, hold and save the app settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Helpfile name
        /// </summary>
        public const string C_Help_File = "\\EnigmaPuzzle.pdf";

        /// <summary>
        /// Key for the Level
        /// </summary>
        public const string C_Key_Level = "Level";

        /// <summary>
        /// Key for the rotation delay
        /// </summary>
        public const string C_Key_RotationDelay = "RotationDelay";

        /// <summary>
        /// Key for the rotations steps
        /// </summary>
        public const string C_Key_RotationSteps = "RotationSteps";

        /// <summary>
        /// Key for the swing steps
        /// </summary>
        public const string C_Key_SwingSteps = "SwingSteps";

        /// <summary>
        /// Key for the swing setting
        /// </summary>
        public const string C_Key_Swing = "Swing";

        /// <summary>
        /// Key for number of turns on new game
        /// </summary>
        public const string C_Key_NumTurns = "NumTurns";

        /// <summary>
        /// Key for the flag if turns shall be shown
        /// </summary>
        public const string C_Key_ShowTurns = "ShowTurns";

        /// <summary>
        /// Key for the colorarray
        /// </summary>
        public const string C_Key_Colors = "Colors";

        /// <summary>
        /// Key for the flag if a game is active
        /// </summary>
        public const string C_Key_GameActive = "GameActive";

        /// <summary>
        /// The rotations to restore the current game
        /// </summary>
        public const string C_Key_Rotations = "Rotations";

        /// <summary>
        /// The elapsed time of a game
        /// </summary>
        public const string C_Key_Time = "Time";

        /// <summary>
        /// Fullscreen on or off
        /// </summary>
        public const string C_Key_FullScreen = "FullScreen";

        /// <summary>
        /// Filename of the settings file
        /// </summary>
        private const string C_FileName = "\\EnigmaSettings.txt";

        /// <summary>
        /// The path for the settings file
        /// </summary>
        private const string C_PathName = "\\Enigma";

        /// <summary>
        /// Hash-table with the current settings
        /// </summary>
        private static Dictionary<string, string> m_settings = new Dictionary<string, string>();

        /// <summary>
        /// Gets a string value from the settings-hash
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            if (m_settings.ContainsKey(key))
            {
                return (m_settings[key]);
            }

            return "";
        }


        /// <summary>
        /// Set a string value in the settings hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void SetValue(string key, string val)
        {
            if (m_settings.ContainsKey(key))
            {
                m_settings[key] = val;
            }
            else
            {
                m_settings.Add(key, val);
            }
        }


        /// <summary>
        /// Gets the array of brushes based on a list of colors
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Brush[] GetBrushes(string key)
        {
            Brush[] arr;

            string br = GetValue(key);
            string[] brs = br.Split(',');

            arr = new Brush[brs.Count()];
            for (int i = 0; i < brs.Count(); i++)
            {
                arr[i] = new SolidBrush(ColorTranslator.FromHtml(brs[i]));
            }
            return arr;
        }


        /// <summary>
        /// Set the colors of an array of brushes
        /// </summary>
        /// <param name="key"></param>
        /// <param name="brs"></param>
        public static void SetBrushes(string key, Brush[] brs)
        {
            string br = "";
            for (int i = 0; i < brs.Count(); i++)
            {
                Pen pen = new Pen(brs[i]);
                br += ColorTranslator.ToHtml(pen.Color);
                if (i < brs.Count() - 1)
                {
                    br += ",";
                }
            }
            SetValue(key, br);
        }


        /// <summary>
        /// Gets an int-value from the settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            string val = GetValue(key);
            int ret;
            Int32.TryParse(val, out ret);
            return ret;
        }


        /// <summary>
        /// Sets an int value to the settings hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void SetInt(string key, int val)
        {
            SetValue(key, val.ToString());
        }


        /// <summary>
        /// Gets a long-value from the settings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long GetLong(string key)
        {
            string val = GetValue(key);
            long ret;
            Int64.TryParse(val, out ret);
            return ret;
        }


        /// <summary>
        /// Sets a long value to the settings hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void SetLong(string key, long val)
        {
            SetValue(key, val.ToString());
        }



        /// <summary>
        /// Gets a bool value from the settings hash
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetBool(string key)
        {
            bool ret = false;

            string b = GetValue(key);

            bool.TryParse(b, out ret);
            return ret;
        }


        /// <summary>
        /// Sets a bool value in the settings hash
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bVal"></param>
        public static void SetBool(string key, bool bVal)
        {
            SetValue(key, bVal.ToString());
        }


        /// <summary>
        /// Sets default values to all settings
        /// </summary>
        public static void FillDefault()
        {
            m_settings.Clear();
            m_settings.Add(C_Key_RotationDelay, "5");
            m_settings.Add(C_Key_RotationSteps, "4");
            m_settings.Add(C_Key_SwingSteps, "2");
            m_settings.Add(C_Key_Swing, "true");

            m_settings.Add(C_Key_Colors, "DarkGreen,Gold,DarkOrange,Teal,Purple,Brown,DarkBlue,White,Black");
            m_settings.Add(C_Key_Level, "9");
            m_settings.Add(C_Key_NumTurns, "20");
            m_settings.Add(C_Key_ShowTurns, "false");
            m_settings.Add(C_Key_GameActive, "false");
            m_settings.Add(C_Key_Rotations, "0");
            m_settings.Add(C_Key_Time, "0");
            m_settings.Add(C_Key_FullScreen, "true");
        }


        /// <summary>
        /// Read the settings from a file
        /// </summary>
        public static void Read()
        {
            FillDefault();
            
            String line;
            try
            {
                string sFileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + C_PathName + C_FileName;
                if (File.Exists(sFileName))
                {
                    StreamReader sr = new StreamReader(sFileName);
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Count() >= 2)
                        {
                            if (m_settings.ContainsKey(parts[0]))
                            {
                                m_settings[parts[0]] = parts[1];
                            }
                            else
                            {
                                m_settings.Add(parts[0], parts[1]);
                            }
                        }

                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        
        /// <summary>
        /// Save the settings to a file
        /// </summary>
        public static void Write()
        {
            try
            {
                string sPathName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + C_PathName;
                if (!Directory.Exists(sPathName))
                {
                    Directory.CreateDirectory(sPathName);
                }
                string sFileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + C_PathName + C_FileName;

                StreamWriter sw = new StreamWriter(sFileName);
                foreach (KeyValuePair<string, string> kvp in m_settings)
                {
                    sw.WriteLine(kvp.Key + "=" + kvp.Value);
                }

                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

    }

}
