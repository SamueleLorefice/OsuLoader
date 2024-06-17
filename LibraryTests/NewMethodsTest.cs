using System.IO;
using NUnit.Framework;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class NewMethodsTest {
        private const string Path = "TestFiles/OsuSTD.osu";
        private string beatMapSrc = "";
        private BeatMap stdMap;
        
        [OneTimeSetUp]
        public void TestSetup()
        {
            beatMapSrc = File.ReadAllText(Path);
        }
        
        [Test, Order(1)]
        public void BeatmapLoadingFromDisk()
        {
            BeatMap loadedMap = null;
            Assert.Multiple(() => 
            {
                Assert.DoesNotThrow(() => { loadedMap = loader.GetBeatMapFromFile(Path); }, "Failed to load beatmap");
                Assert.That(loadedMap, Is.Not.Null, @"Beatmap is null/hasn't loaded properly");   
            });
        }
        
        [Test, Order(0)]
        public void BeatmapDecodingFromString()
        {
            Assert.Multiple(() => 
            {
                Assert.DoesNotThrow(() => { stdMap = loader.GetBeatMap(beatMapSrc); }, "Failed to load beatmap");
                Assert.That(stdMap, Is.Not.Null, @"Beatmap is null/hasn't loaded properly");   
            });
        }
    }
}