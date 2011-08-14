using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace LibNBT
{
    public class TagCompound: AbstractTag
    {
        private Dictionary<String, AbstractTag> _dictionary;

        public override TagType Type
        {
            get { return TagType.Compound; }
        }

        public override void Write(Stream output)
        {
            output.WriteByte((byte)Type);
            TagString.WriteString(output, Name);
            TagCompound.WriteCompound(output, this);
        }

        private static void WriteCompound(Stream output, TagCompound tagCompound)
        {
            var enumerator = tagCompound._dictionary.GetEnumerator();

            while (enumerator.MoveNext())
            {
                enumerator.Current.Value.Write(output);
            }

            TagEnd.Singleton.Write(output);
        }

        public TagCompound()
        {
            Name = String.Empty;
            _dictionary = new Dictionary<String, AbstractTag>();
        }

        public TagCompound(Stream input)
        {
            Name = TagString.ReadString(input);
            _dictionary = ReadDictionary(input);
        }

        internal static Dictionary<String, AbstractTag> ReadDictionary(Stream input)
        {
            Dictionary<String, AbstractTag> dict = new Dictionary<String, AbstractTag>();
            AbstractTag tag = AbstractTag.Read(input);
            try
            {
                while (tag.Type != TagType.End)
                {
                    dict[tag.Name] = tag;
                    tag = AbstractTag.Read(input);
                }
            }
            catch (Exception) { }

            return dict;
        }

        internal static TagCompound ReadUnnamedTagCompound(Stream input)
        {
            return new TagCompound() { _dictionary = ReadDictionary(input) };
        }

        public void SetTag(String name, AbstractTag tag)
        {
            tag.Name = name;
            _dictionary[name] = tag;
        }

        public TagByte GetByte(String name)
        {
            return _dictionary[name] as TagByte;
        }

        public TagByteArray GetByteArray(String name)
        {
            return _dictionary[name] as TagByteArray;
        }

        public TagCompound GetCompound(String name)
        {
            return _dictionary[name] as TagCompound;
        }

        public TagDouble GetDouble(String name)
        {
            return _dictionary[name] as TagDouble;
        }

        public TagFloat GetFloat(String name)
        {
            return _dictionary[name] as TagFloat;
        }

        public TagInt GetInt(String name)
        {
            return _dictionary[name] as TagInt;
        }

        public TagList GetList(String name)
        {
            return _dictionary[name] as TagList;
        }

        public TagLong GetLong(String name)
        {
            return _dictionary[name] as TagLong;
        }

        public TagShort GetShort(String name)
        {
            return _dictionary[name] as TagShort;
        }

        public TagString GetString(String name)
        {
            return _dictionary[name] as TagString;
        }

        public AbstractTag GetAbstractTag(String Name)
        {
            return _dictionary[Name];
        }

        public void WriteToFile(String filename)
        {
            using (FileStream output = File.Open(filename, FileMode.Create))
            {
                using (GZipStream gzipStream = new GZipStream(output, CompressionMode.Compress))
                {
                    Write(gzipStream);
                }
            }
        }

        public override void WriteUnnamed(Stream output)
        {
            WriteCompound(output, this);
        }

        public static AbstractTag ReadFromFile(String filename)
        {
            using (FileStream input = File.Open(filename, FileMode.Open))
            {
                using (GZipStream gzipStream = new GZipStream(input, CompressionMode.Decompress)){
                    return AbstractTag.Read(gzipStream);
                }
            }
        }
    }
}
