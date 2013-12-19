using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using OpenTK.Graphics;
using OpenTK;

namespace PushEngine
{
    [Serializable]
    public class ConfigurationData
    {
        public Color SystemBackgroundColor = Color.Black;
        public Size WindowSize = new Size(800, 600);
        public Vector2 virtualWindowTopLeft = new Vector2(-400, 300);
        public Vector2 virtualWindowDownRight = new Vector2(400, -300);
        public string WindowTitle = "PushEngine";
        public int bpp = 32;
    }

    internal class Configuration
    {
        public ConfigurationData configurationData = new ConfigurationData();
        private static string configFile = "config.bin";
        public GraphicsMode graphicsMode = null;

        internal void ApplyConfiguration()
        {
            PushEngineCore.Instance.Width = configurationData.WindowSize.Width;
            PushEngineCore.Instance.Height = configurationData.WindowSize.Height;
            graphicsMode = new GraphicsMode(new ColorFormat(configurationData.bpp));


        }

        internal void ReadConfigFile()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(configFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                //configurationData = (ConfigurationData)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (FileNotFoundException)
            {
                // It is ok, no problem.
            }
            catch (Exception)
            {
            }
        }

        internal void SaveConfigFile()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(configFile, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, configurationData);
                stream.Close();
            }
            catch (Exception)
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}
