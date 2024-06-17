using System;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests
{
    [TestFixture]
    public class Tests
    {
        private BeatMap stdMap;
        
        [OneTimeSetUp]
        public void TestSetup()
        {
            BeatmapLoading();
        }
        
        [Test]
        public void BeatmapLoading()
        {
            Assert.DoesNotThrow(() => { stdMap = loader.LoadDotOsu("TestFiles/OsuSTD.osu"); }, "Failed to load beatmap");
            Assert.That(stdMap, Is.Not.Null, @"Beatmap is null/hasn't loaded properly");
        }
        
        [Test]
        public void GeneralSectionTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(stdMap.AudioFileName,           Is.EqualTo("Music.mp3"),        "AudioFilename is not equal to TestAudio.mp3");
                Assert.That(stdMap.AudioLeadIn,             Is.EqualTo(1000),               "AudioLeadIn is not equal to 1000");
                Assert.That(stdMap.PreviewTime,             Is.EqualTo(5000),               "PreviewTime is not equal to 5000");
                Assert.That(stdMap.Countdown,               Is.EqualTo(1),                  "Countdown is not equal to 1");
                Assert.That(stdMap.SampleSet,               Is.EqualTo("TestSampleSet"),    "SampleSet is not equal to TestSampleSet");
                Assert.That(stdMap.StackLeniency,           Is.EqualTo(0.7f),                "StackLeniency is not equal to 0.7");
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
                Assert.That(stdMap.DistanceSpacing,     Is.EqualTo(1.0f),    "DistanceSpacing is not equal to 1.0");
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
                Assert.That(stdMap.Tags,    Is.EqualTo("Test Tags Some More"),     "Tags is not equal to TestTags");
            });
        }
        
        [Test]
        public void DifficultySectionTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(stdMap.HPDrainRate,     Is.EqualTo(5.0f),    $"HPDrainRate is not equal to 5.0, {stdMap.HPDrainRate}");
                Assert.That(stdMap.CircleSize,      Is.EqualTo(4.0f),    $"CircleSize is not equal to 4.0, {stdMap.CircleSize}");
                Assert.That(stdMap.OverallDifficulty, Is.EqualTo(3.0f),  $"OverallDifficulty is not equal to 3.0, {stdMap.OverallDifficulty}");
                Assert.That(stdMap.ApproachRate,    Is.EqualTo(2.0f),    $"ApproachRate is not equal to 2.0, {stdMap.ApproachRate}");
                Assert.That(stdMap.SliderMultiplier,Is.EqualTo(1.4f),    $"SliderMultiplier is not equal to 1.4, {stdMap.SliderMultiplier}");
                Assert.That(stdMap.SliderTickRate,  Is.EqualTo(1.0f),    $"SliderTickRate is not equal to 1.0, {stdMap.SliderTickRate}");
            });
        }
        
        [Test]
        public void EventsSectionTest()
        {
            Assert.Inconclusive("Not yet implemented");
        }
        
        [Test]
        public void TimingPointsSectionTest()
        {
            Assert.Inconclusive("Not yet implemented");
        }
        
        [Test]
        public void ColoursSectionTest()
        {
            Assert.Inconclusive("Not yet implemented");
        }
        
        [Test]
        public void HitObjectsSectionTest()
        {
            Assert.Inconclusive("Not yet implemented");
        }
    }
}