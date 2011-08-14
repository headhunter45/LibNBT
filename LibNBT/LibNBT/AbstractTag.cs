using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace LibNBT
{
    public enum TagType{End, Byte, Short, Int, Long, Float, Double, ByteArray, String, List, Compound};

    public abstract class AbstractTag
    {
        public abstract TagType Type { get; }
        public virtual String Name { get; set; }

        public abstract void Write(Stream output);
        public abstract void WriteUnnamed(Stream output);
        
        public static AbstractTag Read(Stream input)
        {
            int temp = input.ReadByte();
            if(temp != (temp & 0xFF)){
                throw new Exception();
            }

            switch ((TagType)temp)
            {
                case TagType.End:
                    return new TagEnd();
                case TagType.Byte:
                    return new TagByte(input);
                case TagType.Short:
                    return new TagShort(input);
                case TagType.Int:
                    return new TagInt(input);
                case TagType.Long:
                    return new TagLong(input);
                case TagType.Float:
                    return new TagFloat(input);
                case TagType.Double:
                    return new TagDouble(input);
                case TagType.ByteArray:
                    return new TagByteArray(input);
                case TagType.String:
                    return new TagString(input);
                case TagType.List:
                    return new TagList(input);
                case TagType.Compound:
                    return new TagCompound(input);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
