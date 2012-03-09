using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibNBT;
using System.IO;
using System.IO.Compression;

namespace LibNBT_Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //Sanity test it always passes
        [TestMethod]
        public void TestPass()
        {
        }

        [TestMethod]
        public void TestLoadSimpleNbt()
        {
            String filename = @"..\..\..\LibNBT Tests\Data\test.nbt";
            AbstractTag tag = TagCompound.ReadFromFile(filename);

            Assert.IsNotNull(tag);
            Assert.IsInstanceOfType(tag, typeof(TagCompound));
            TagCompound root = tag as TagCompound;
            Assert.AreEqual<String>("hello world", root.Name);

            TagString tagStr = root.GetString("name");
            Assert.AreEqual<String>("name", tagStr.Name);

            Assert.AreEqual<String>("Bananrama", tagStr.Value);
        }

        [TestMethod]
        public void TestLoadComplexNbt()
        {
            String filename = @"..\..\..\LibNBT Tests\Data\bigtest.nbt";
            AbstractTag tag = TagCompound.ReadFromFile(filename);

            Assert.IsNotNull(tag);
            Assert.IsInstanceOfType(tag, typeof(TagCompound));
            TagCompound level = tag as TagCompound;
            Assert.AreEqual<String>("Level", level.Name);

            TagShort shortTest = level.GetShort("shortTest");
            Assert.IsNotNull(shortTest);
            Assert.AreEqual<String>("shortTest", shortTest.Name);
            Assert.AreEqual<short>(32767, shortTest.Value);

            TagLong longTest = level.GetLong("longTest");
            Assert.IsNotNull(longTest);
            Assert.AreEqual<String>("longTest", longTest.Name);
            Assert.AreEqual<long>(9223372036854775807, longTest.Value);

            TagFloat floatTest = level.GetFloat("floatTest");
            Assert.IsNotNull(floatTest);
            Assert.AreEqual<String>("floatTest", floatTest.Name);
            Assert.AreEqual<float>(0.49823147f, floatTest.Value);

            TagString stringTest = level.GetString("stringTest");
            Assert.IsNotNull(stringTest);
            Assert.AreEqual<String>("stringTest", stringTest.Name);
            Assert.AreEqual<String>("HELLO WORLD THIS IS A TEST STRING ÅÄÖ!", stringTest.Value);

            TagInt intTest = level.GetInt("intTest");
            Assert.IsNotNull(intTest);
            Assert.AreEqual<String>("intTest", intTest.Name);
            Assert.AreEqual<int>(2147483647, intTest.Value);

            TagCompound nestedCompoundTest = level.GetCompound("nested compound test");
            Assert.IsNotNull(nestedCompoundTest);
            Assert.AreEqual<String>("nested compound test", nestedCompoundTest.Name);

            TagCompound ham = nestedCompoundTest.GetCompound("ham");
            Assert.IsNotNull(ham);
            Assert.AreEqual<String>("ham", ham.Name);

            TagString ham_name = ham.GetString("name");
            Assert.IsNotNull(ham_name);
            Assert.AreEqual<String>("name", ham_name.Name);
            Assert.AreEqual<String>("Hampus", ham_name.Value);
            
            TagFloat ham_value = ham.GetFloat("value");
            Assert.IsNotNull(ham_value);
            Assert.AreEqual<String>("value", ham_value.Name);
            Assert.AreEqual<float>(0.75f, ham_value.Value);

            TagCompound egg = nestedCompoundTest.GetCompound("egg");
            Assert.IsNotNull(egg);
            Assert.AreEqual<String>("egg", egg.Name);

            TagString egg_name = egg.GetString("name");
            Assert.IsNotNull(egg_name);
            Assert.AreEqual<String>("name", egg_name.Name);
            Assert.AreEqual<String>("Eggbert", egg_name.Value);
            
            TagFloat egg_value = egg.GetFloat("value");
            Assert.IsNotNull(egg_value);
            Assert.AreEqual<String>("value", egg_value.Name);
            Assert.AreEqual<float>(0.5f, egg_value.Value);

            TagByte byteTest = level.GetByte("byteTest");
            Assert.IsNotNull(byteTest);
            Assert.AreEqual<String>("byteTest", byteTest.Name);
            Assert.AreEqual<byte>(0x7f, byteTest.Value);

            TagDouble doubleTest = level.GetDouble("doubleTest");
            Assert.IsNotNull(doubleTest);
            Assert.AreEqual<String>("doubleTest", doubleTest.Name);
            Assert.AreEqual<double>(0.4931287132182315, doubleTest.Value);

            TagList listTest_long = level.GetList("listTest (long)");
            Assert.IsNotNull(listTest_long);
            Assert.AreEqual<String>("listTest (long)", listTest_long.Name);
            Assert.IsNotNull(listTest_long.Value);
            Assert.AreEqual<int>(5, listTest_long.Value.Count);
            Assert.AreEqual<long>(11, (listTest_long.Value[0] as TagLong).Value);
            Assert.AreEqual<long>(12, (listTest_long.Value[1] as TagLong).Value);
            Assert.AreEqual<long>(13, (listTest_long.Value[2] as TagLong).Value);
            Assert.AreEqual<long>(14, (listTest_long.Value[3] as TagLong).Value);
            Assert.AreEqual<long>(15, (listTest_long.Value[4] as TagLong).Value);

            TagList listTest_compound = level.GetList("listTest (compound)");
            Assert.IsNotNull(listTest_compound);
            Assert.AreEqual<String>("listTest (compound)", listTest_compound.Name); 
            Assert.IsNotNull(listTest_compound.Value);
            Assert.AreEqual<int>(2, listTest_compound.Value.Count);
            TagCompound listTest_compound_tag0 = listTest_compound.Value[0] as TagCompound;
            Assert.IsNotNull(listTest_compound_tag0);
            TagString listTest_compound_tag0_name = listTest_compound_tag0.GetString("name");
            Assert.IsNotNull(listTest_compound_tag0_name);
            Assert.AreEqual<String>("name", listTest_compound_tag0_name.Name);
            Assert.AreEqual<String>("Compound tag #0", listTest_compound_tag0_name.Value);
            TagLong listTest_compound_tag0_createdOn = listTest_compound_tag0.GetLong("created-on");
            Assert.IsNotNull(listTest_compound_tag0_createdOn);
            Assert.AreEqual<String>("created-on", listTest_compound_tag0_createdOn.Name);
            Assert.AreEqual<long>(1264099775885, listTest_compound_tag0_createdOn.Value);

            TagCompound listTest_compound_tag1 = listTest_compound.Value[1] as TagCompound;
            Assert.IsNotNull(listTest_compound_tag1);
            TagString listTest_compound_tag1_name = listTest_compound_tag1.GetString("name");
            Assert.IsNotNull(listTest_compound_tag1_name);
            Assert.AreEqual<String>("name", listTest_compound_tag1_name.Name);
            Assert.AreEqual<String>("Compound tag #1", listTest_compound_tag1_name.Value);
            TagLong listTest_compound_tag1_createdOn = listTest_compound_tag1.GetLong("created-on");
            Assert.IsNotNull(listTest_compound_tag1_createdOn);
            Assert.AreEqual<String>("created-on", listTest_compound_tag1_createdOn.Name);
            Assert.AreEqual<long>(1264099775885, listTest_compound_tag1_createdOn.Value);

            TagByteArray byteArrayTest = level.GetByteArray("byteArrayTest (the first 1000 values of (n*n*255+n*7)%100, starting with n=0 (0, 62, 34, 16, 8, ...))");
            Assert.IsNotNull(byteArrayTest);
            Assert.AreEqual<String>("byteArrayTest (the first 1000 values of (n*n*255+n*7)%100, starting with n=0 (0, 62, 34, 16, 8, ...))", byteArrayTest.Name);
            Assert.IsNotNull(byteArrayTest.Value);
            Assert.AreEqual<int>(1000, byteArrayTest.Value.Length);
        }

        [TestMethod]
        public void TestSave()
        {
            String filename = @"..\..\..\LibNBT Tests\Data\bigtest.nbt";
            TagCompound tag = TagCompound.ReadFromFile(filename) as TagCompound;

            MemoryStream ms = new MemoryStream();
            MemoryStream ms2;
            FileStream fs = File.OpenRead(filename);
            GZipStream gzStream = new GZipStream(fs, CompressionMode.Decompress);
            
            tag.Write(ms);

            ms2 = new MemoryStream((int)ms.Length);
            byte[] buffer = new byte[ms.Length];
            Assert.AreEqual<long>(ms.Length, gzStream.Read(buffer, 0, (int)ms.Length));

            Assert.AreEqual<int>(-1, gzStream.ReadByte());
            byte[] buffer2 = ms.GetBuffer();

            FileStream fs2 = File.OpenWrite(@"..\..\..\LibNBT Tests\Data\savetest.raw");
            fs2.Write(buffer2, 0, (int)ms.Length);
            for (long i = 0; i < ms.Length; i++)
            {
                Assert.AreEqual<byte>(buffer[i], buffer2[i]);
            }
        }

        [TestMethod]
        public void TestAnvilRegion()
        {
            String filename = @"..\..\..\LibNBT Tests\Data\r.0.0.mca";
            FileStream input = File.OpenRead(filename);
            int[] locations = new int[1024];
            byte[] buffer = new byte[4096];
            input.Read(buffer, 0, 4096);
            for (int i = 0; i < 1024; i++)
            {
                locations[i] = BitConverter.ToInt32(buffer, i * 4);
            }

            int[] timestamps = new int[1024];
            input.Read(buffer, 0, 4096);
            for (int i = 0; i < 1024; i++)
            {
                timestamps[i] = BitConverter.ToInt32(buffer, i * 4);
            }

            input.Read(buffer, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                BitHelper.SwapBytes(buffer, 0, 4);
            }
            int sizeOfChunkData = BitConverter.ToInt32(buffer, 0) - 1;

            int compressionType = input.ReadByte();
            buffer = new byte[sizeOfChunkData];
            input.Read(buffer, 0, sizeOfChunkData);

            Stream inputStream = null;
            
            if(compressionType == 1){
                inputStream = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress);
            }else if(compressionType == 2){
                inputStream = new DeflateStream(new MemoryStream(buffer, 2, buffer.Length - 6), CompressionMode.Decompress);
            }

            TagCompound tag = AbstractTag.Read(inputStream) as TagCompound;
            string strTag = tag.ToString();

            Assert.IsNotNull(tag);

            Assert.AreEqual<TagType>(TagType.Compound, tag.GetAbstractTag("Level").Type);
            TagCompound levelTag = tag.GetCompound("Level");

            AbstractTag aTag = levelTag.GetAbstractTag("Entities");
            Assert.AreEqual<TagType>(TagType.List, aTag.Type);
            TagList entitiesTag = aTag as TagList;
            Assert.AreEqual<int>(0, entitiesTag.Value.Count);

            aTag = levelTag.GetAbstractTag("Biomes");
            Assert.AreEqual<TagType>(TagType.ByteArray, aTag.Type);
            TagByteArray biomesTag = aTag as TagByteArray;
            Assert.AreEqual<int>(256, biomesTag.Value.Length);

            aTag = levelTag.GetAbstractTag("LastUpdate");
            Assert.AreEqual<TagType>(TagType.Long, aTag.Type);
            TagLong lastUpdateTag = aTag as TagLong;
            Assert.AreEqual<long>(2861877, lastUpdateTag.Value);

            aTag = levelTag.GetAbstractTag("xPos");
            Assert.AreEqual<TagType>(TagType.Int, aTag.Type);
            TagInt xPosTag = aTag as TagInt;
            Assert.AreEqual<int>(10, xPosTag.Value);

            aTag = levelTag.GetAbstractTag("zPos");
            Assert.AreEqual<TagType>(TagType.Int, aTag.Type);
            TagInt zPosTag = aTag as TagInt;
            Assert.AreEqual<int>(0, zPosTag.Value);

            aTag = levelTag.GetAbstractTag("TileEntities");
            Assert.AreEqual<TagType>(TagType.List, aTag.Type);
            TagList tileEntitiesTag = aTag as TagList;
            Assert.AreEqual<int>(0, tileEntitiesTag.Value.Count);

            aTag = levelTag.GetAbstractTag("TerrainPopulated");
            Assert.AreEqual<TagType>(TagType.Byte, aTag.Type);
            TagByte terrainPopulatedTag = aTag as TagByte;
            Assert.AreEqual<byte>(1, terrainPopulatedTag.Value);
        }

        [TestMethod]
        public void TestReadNonExistantTagFromCompound()
        {
            String filename = @"..\..\..\LibNBT Tests\Data\test.nbt";

            TagCompound newTag = new TagCompound();

            AbstractTag aTag = newTag.GetAbstractTag("nope");

            Assert.IsNull(aTag);

            TagCompound fileTag = AbstractTag.ReadFromFile(filename) as TagCompound;

            aTag = fileTag.GetAbstractTag("Entities");

            Assert.IsNull(aTag);
        }
    }
}
