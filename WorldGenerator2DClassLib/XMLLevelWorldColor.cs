using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace WorldGenerator2DClassLib
{
    public class XMLLevelWorldColor
    {
        public float Level;
        public int A;
        public int R;
        public int G;
        public int B;
            
        public Color Color => Color.FromArgb(A, R, G, B);

        public XMLLevelWorldColor() 
        {

        }

        public XMLLevelWorldColor(float level, Color color) : this(level, color.A, color.R, color.G, color.B)
        {

        }

        public XMLLevelWorldColor(float level, int a, int r, int g, int b)
        {
            Level = level;
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static List<XMLLevelWorldColor> ToList(Dictionary<float, Color> dictionary)
        {
            List<XMLLevelWorldColor> entries = new List<XMLLevelWorldColor>(dictionary.Count);
            foreach (float key in dictionary.Keys)
            {
                entries.Add(new XMLLevelWorldColor(key, dictionary[key]));
            }

            return entries;
        }

        public static Dictionary<float, Color> ToDict(List<XMLLevelWorldColor> entries)
        {
            Dictionary<float, Color> dictionary = new Dictionary<float, Color>();
            foreach (XMLLevelWorldColor entry in entries)
            {
                dictionary[entry.Level] = entry.Color;
            }

            return dictionary;
        }

        public static void Serialize(TextWriter writer, Dictionary<float, Color> dictionary)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<XMLLevelWorldColor>));
            serializer.Serialize(writer, ToList(dictionary));
            writer.Close();
        }

        public static Dictionary<float, Color> Deserialize(TextReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<XMLLevelWorldColor>));
            List<XMLLevelWorldColor> list = (List<XMLLevelWorldColor>)serializer.Deserialize(reader);
            reader.Close();

            return ToDict(list);
        }
    }
}
