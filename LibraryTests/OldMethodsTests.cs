using System;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class OldMethodsTests {
        private       BeatMap stdMap;
        private       BeatMap savedMap;
        private const string  savePath = "TestFiles/OsuSTDWrite.osu";

        [Test]
        public void BeatmapLoad() {
            Assert.DoesNotThrow(() => { stdMap = loader.LoadDotOsu("TestFiles/OsuSTD.osu"); }, "Failed to load beatmap");
            Assert.That(stdMap, Is.Not.Null, @"Beatmap is null/hasn't loaded properly");
        }

        [Test]
        public void BeatmapSave() {
            Assert.Multiple(() => {
                Assert.DoesNotThrow(() => { loader.SaveDotOsu(savePath, stdMap); },    "Failed to save beatmap");
                Assert.DoesNotThrow(() => { savedMap = loader.LoadDotOsu(savePath); }, "Failed to load saved beatmap");
                Assert.That(savedMap, Is.EqualTo(stdMap), "Saved beatmap is not equal to original");
            });
        }

        [OneTimeTearDown]
        public void TestCleanup() {
            if (System.IO.File.Exists(savePath)) System.IO.File.Delete(savePath);
        }
    }
}