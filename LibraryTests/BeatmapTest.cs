using System;
using NUnit.Framework;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class BeatmapTest {
        private static Tuple<BeatMap, string>[] testCases = {
            new Tuple<BeatMap, string>(loader.GetBeatMapFromFile("TestFiles/OsuSTD.osu"), "newMethod")
        };

        [TestCaseSource(nameof(testCases))]
        public void GeneralSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Multiple(() => {
                Assert.That(stdmap.AudioFileName,        Is.EqualTo("Music.mp3"),     "AudioFilename is not equal to TestAudio.mp3");
                Assert.That(stdmap.AudioLeadIn,          Is.EqualTo(1000),            "AudioLeadIn is not equal to 1000");
                Assert.That(stdmap.PreviewTime,          Is.EqualTo(5000),            "PreviewTime is not equal to 5000");
                Assert.That(stdmap.Countdown,            Is.EqualTo(1),               "Countdown is not equal to 1");
                Assert.That(stdmap.SampleSet,            Is.EqualTo("TestSampleSet"), "SampleSet is not equal to TestSampleSet");
                Assert.That(stdmap.StackLeniency,        Is.EqualTo(0.7f),            "StackLeniency is not equal to 0.7");
                Assert.That(stdmap.Mode,                 Is.EqualTo(0),               "Mode is not equal to 0");
                Assert.That(stdmap.LetterBoxInBreaks,    Is.EqualTo(1),               "LetterboxInBreaks is not equal to 1");
                Assert.That(stdmap.WideScreenStoryboard, Is.EqualTo(1),               "WidescreenStoryboard is not equal to 1");
                Assert.That(stdmap.EpilepsyWarning,      Is.EqualTo(1),               "EpilepsyWarning is not equal to 1");
            });
        }

        [TestCaseSource(nameof(testCases))]
        public void EditorSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Multiple(() => {
                Assert.That(stdmap.DistanceSpacing, Is.EqualTo(1.0f), "DistanceSpacing is not equal to 1.0");
                Assert.That(stdmap.BeatDivisor,     Is.EqualTo(4),    "BeatDivisor is not equal to 4");
                Assert.That(stdmap.GridSize,        Is.EqualTo(16),   "GridSize is not equal to 16");
            });
        }

        [TestCaseSource(nameof(testCases))]
        public void MetadataSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Multiple(() => {
                Assert.That(stdmap.Title,   Is.EqualTo("TestTitle"),           "Title is not equal to TestTitle");
                Assert.That(stdmap.Artist,  Is.EqualTo("TestArtist"),          "Artist is not equal to TestArtist");
                Assert.That(stdmap.Creator, Is.EqualTo("TestCreator"),         "Creator is not equal to TestCreator");
                Assert.That(stdmap.Version, Is.EqualTo("TestVersion"),         "Version is not equal to TestVersion");
                Assert.That(stdmap.Source,  Is.EqualTo("TestSource"),          "Source is not equal to TestSource");
                Assert.That(stdmap.Tags,    Is.EqualTo("Test Tags Some More"), "Tags is not equal to TestTags");
            });
        }

        [TestCaseSource(nameof(testCases))]
        public void DifficultySectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Multiple(() => {
                Assert.That(stdmap.HpDrainRate,       Is.EqualTo(5.0f), $"HPDrainRate is not equal to 5.0, {stdmap.HpDrainRate}");
                Assert.That(stdmap.CircleSize,        Is.EqualTo(4.0f), $"CircleSize is not equal to 4.0, {stdmap.CircleSize}");
                Assert.That(stdmap.OverallDifficulty, Is.EqualTo(3.0f), $"OverallDifficulty is not equal to 3.0, {stdmap.OverallDifficulty}");
                Assert.That(stdmap.ApproachRate,      Is.EqualTo(2.0f), $"ApproachRate is not equal to 2.0, {stdmap.ApproachRate}");
                Assert.That(stdmap.SliderMultiplier,  Is.EqualTo(1.4f), $"SliderMultiplier is not equal to 1.4, {stdmap.SliderMultiplier}");
                Assert.That(stdmap.SliderTickRate,    Is.EqualTo(1.0f), $"SliderTickRate is not equal to 1.0, {stdmap.SliderTickRate}");
            });
        }

        [TestCaseSource(nameof(testCases))]
        public void EventsSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Inconclusive("Not yet implemented");
        }

        [TestCaseSource(nameof(testCases))]
        public void TimingPointsSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Inconclusive("Not yet implemented");
        }

        [TestCaseSource(nameof(testCases))]
        public void ColoursSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Inconclusive("Not yet implemented");
        }

        [TestCaseSource(nameof(testCases))]
        public void HitObjectsSectionTest(Tuple<BeatMap, string> map) {
            BeatMap stdmap = map.Item1;
            Assert.Inconclusive("Not yet implemented");
        }
    }
}