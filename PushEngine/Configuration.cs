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
        public Vector2d virtualWindowTopLeft = new Vector2d(-400, 300);
        public Vector2d virtualWindowDownRight = new Vector2d(400, -300);

        public string WindowTitle = "PushEngine";
        public int bpp = 32;
    }

    internal class Configuration : ConfigurationData
    {
        private static Configuration conf = new Configuration();

        private const string configFile = "config.bin";
        public GraphicsMode graphicsMode = null;

        internal static Configuration ReadConfigFile()
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

            conf.graphicsMode = new GraphicsMode(new ColorFormat(conf.bpp));
            return conf;
        }

        internal static void SaveConfigFile()
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(configFile, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, conf as ConfigurationData);
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
