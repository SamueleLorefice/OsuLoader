using System;
using NUnit;
using NUnit.Framework;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests
{
    [TestFixture]
    public class Tests
    {
        private BeatMap stdMap;
        
        [Test, Order(0)]
        public void BeatmapLoading()
        {
            Assert.DoesNotThrow(() => { stdMap = loader.LoadDotOsu("TestFiles/OsuSTD.osu"); }, "Failed to load beatmap");
            Assert.That(stdMap, Is.Not.Null, @"Beatmap is null/hasn't loaded properly");
        }

        [Test, Order(1)]
        public void GeneralSectionTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(stdMap.AudioFileName,           Is.EqualTo("Music.mp3"),        "AudioFilename is not equal to TestAudio.mp3");
                Assert.That(stdMap.AudioLeadIn,             Is.EqualTo(1000),               "AudioLeadIn is not equal to 1000");
                Assert.That(stdMap.PreviewTime,             Is.EqualTo(5000),               "PreviewTime is not equal to 5000");
                Assert.That(stdMap.Countdown,               Is.EqualTo(1),                  "Countdown is not equal to 1");
                Assert.That(stdMap.SampleSet,               Is.EqualTo("TestSampleSet"),    "SampleSet is not equal to TestSampleSet");
                Assert.That(stdMap.StackLeniency,           Is.EqualTo(0.7),                "StackLeniency is not equal to 0.7");
                Assert.That(stdMap.Mode,                    Is.EqualTo(0),                  "Mode is not equal to 0");
                Assert.That(stdMap.LetterBoxInBreaks,       Is.EqualTo(1),                  "LetterboxInBreaks is not equal to 1");
                Assert.That(stdMap.WideScreenStoryboard,    Is.EqualTo(1),                  "WidescreenStoryboard is not equal to 1");
                Assert.That(stdMap.EpilepsyWarning,         Is.EqualTo(1),                  "EpilepsyWarning is not equal to 1");
            });
        }
        
        [Test]
        public void EditorSectionTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(stdMap.DistanceSpacing,     Is.EqualTo(1.0),    "DistanceSpacing is not equal to 1.0");
                Assert.That(stdMap.BeatDivisor,         Is.EqualTo(4),      "BeatDivisor is not equal to 4");
                Assert.That(stdMap.GridSize,            Is.EqualTo(16),     "GridSize is not equal to 16");
            });
        }
        
        [Test]
        public void MetadataSectionTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(stdMap.Title,   Is.EqualTo("TestTitle"),    "Title is not equal to TestTitle");
                Assert.That(stdMap.Artist,  Is.EqualTo("TestArtist"),   "Artist is not equal to TestArtist");
                Assert.That(stdMap.Creator, Is.EqualTo("TestCreator"),  "Creator is not equal to TestCreator");
                Assert.That(stdMap.Version, Is.EqualTo("TestVersion"),  "Version is not equal to TestVersion");
                Assert.That(stdMap.Source,  Is.EqualTo("TestSource"),   "Source is not equal to TestSource");
            });
        }
    }
}