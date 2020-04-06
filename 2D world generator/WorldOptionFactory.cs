using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WorldGenerator2D
{
    public partial class WorldOptionFactory
    {
        public static void Save(string filename, WorldOption option)
        {
            StreamWriter writer = new StreamWriter(filename);

            List<XMLLevelWorldColor> entries = XMLLevelWorldColor.ToList(option.WorldColor);

            XMLOption xmlOption = new XMLOption(option.Width, option.Height, option.Thresold, entries);
            XmlSerializer xs = new XmlSerializer(typeof(XMLOption));
            xs.Serialize(writer, xmlOption);

            writer.Close();
        }

        public static WorldOption Load(string filename)
        {
            if (!File.Exists(filename)) throw new System.Exception("Option file is missing !");

            StreamReader reader = new StreamReader(filename);
            XmlSerializer serializer = new XmlSerializer(typeof(XMLOption));
            XMLOption option = (XMLOption)serializer.Deserialize(reader);
            reader.Close();

            return new WorldOption(option.Width, option.Height, option.Thresold, XMLLevelWorldColor.ToDict(option.Worldcolors));
        }
    }
}
