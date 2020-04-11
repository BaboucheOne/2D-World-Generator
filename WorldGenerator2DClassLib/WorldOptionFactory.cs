using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WorldGenerator2DClassLib
{
    public partial class WorldOptionFactory
    {
        private const string fileExtension = ".xml";

        public static void Save(string filename, WorldOption option)
        {
            StreamWriter writer = new StreamWriter(filename);

            List<XMLLevelWorldColor> entries = XMLLevelWorldColor.ToList(option.WorldColor);

            XMLOption xmlOption = new XMLOption(option.Width, option.Height, option.Thresold, entries);
            XmlSerializer xs = new XmlSerializer(typeof(XMLOption));
            xs.Serialize(writer, xmlOption);

            writer.Close();
        }

        public static WorldOption Load(string path)
        {
            if (Path.GetExtension(path) != fileExtension)
            {
                throw new System.ArgumentException("File extension must be xml.");
            }

            if (!File.Exists(path)) throw new System.Exception("Option file is missing !");

            StreamReader reader = new StreamReader(path);
            XmlSerializer serializer = new XmlSerializer(typeof(XMLOption));
            XMLOption option = (XMLOption)serializer.Deserialize(reader);
            reader.Close();

            return new WorldOption(option.Width, option.Height, option.Thresold, XMLLevelWorldColor.ToDict(option.Worldcolors));
        }
    }
}
