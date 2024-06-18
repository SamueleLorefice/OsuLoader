using System;
using System.Collections.Generic;
using NUnit.Framework;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class BeatmapTest {
        BeatMap                    map;
        [OneTimeSetUp] public void TestSetup() => map = loader.GetBeatMapFromFile("TestFiles/OsuSTD.osu");
        
        [Test]
        public void GeneralSectionTest() {
            BeatMap stdmap = map;
            Assert.Multiple(() => {
                Assert.That(stdmap.AudioFileName,            Is.EqualTo("Music.mp3"));
                Assert.That(stdmap.AudioLeadIn,              Is.EqualTo(1000));
                Assert.That(stdmap.AudioHash,                Is.EqualTo("1234567890"));
                Assert.That(stdmap.PreviewTime,              Is.EqualTo(5000));
                Assert.That(stdmap.Countdown,                Is.EqualTo(CountdownType.Normal));
                Assert.That(stdmap.SampleSet,                Is.EqualTo("TestSampleSet"));
                Assert.That(stdmap.StackLeniency,            Is.EqualTo(0.7f));
                Assert.That(stdmap.Mode,                     Is.EqualTo(GameMode.Osu));
                Assert.That(stdmap.LetterBoxInBreaks,        Is.EqualTo(true));
                Assert.That(stdmap.StoryFireInFront,         Is.EqualTo(true));
                Assert.That(stdmap.UseSkinSprites,           Is.EqualTo(true));
                Assert.That(stdmap.AlwaysShowPlayfield,      Is.EqualTo(true));
                Assert.That(stdmap.OverlayPosition,          Is.EqualTo(OverlayPosition.Below));
                Assert.That(stdmap.SkinPreference,           Is.EqualTo("TestSkin"));
                Assert.That(stdmap.EpilepsyWarning,          Is.EqualTo(true));
                Assert.That(stdmap.CountdownOffset,          Is.EqualTo(10));
                Assert.That(stdmap.SpecialStyle,             Is.EqualTo(true));
                Assert.That(stdmap.WideScreenStoryboard,     Is.EqualTo(true));
                Assert.That(stdmap.SamplesMatchPlaybackRate, Is.EqualTo(true));
            });
        }
        
        [Test]
        public void EditorSectionTest() {
            BeatMap stdmap = map;
            Assert.Multiple(() => {
                Assert.That(stdmap.DistanceSpacing, Is.EqualTo(1.0f), "DistanceSpacing is not equal to 1.0");
                Assert.That(stdmap.BeatDivisor,     Is.EqualTo(4),    "BeatDivisor is not equal to 4");
                Assert.That(stdmap.GridSize,        Is.EqualTo(16),   "GridSize is not equal to 16");
            });
        }
        
        [Test]
        public void MetadataSectionTest() {
            BeatMap stdmap = map;
            Assert.Multiple(() => {
                Assert.That(stdmap.Title,   Is.EqualTo("TestTitle"),   "Title is not equal to TestTitle");
                Assert.That(stdmap.Artist,  Is.EqualTo("TestArtist"),  "Artist is not equal to TestArtist");
                Assert.That(stdmap.Creator, Is.EqualTo("TestCreator"), "Creator is not equal to TestCreator");
                Assert.That(stdmap.Version, Is.EqualTo("TestVersion"), "Version is not equal to TestVersion");
                Assert.That(stdmap.Source,  Is.EqualTo("TestSource"),  "Source is not equal to TestSource");
                Assert.That(stdmap.Tags, Is.EqualTo(new List<string> {
                    "Test",
                    "Tags",
                    "Some",
                    "More"
                }), "Tags is not equal to TestTags");
            });
        }
        
        [Test]
        public void DifficultySectionTest() {
            BeatMap stdmap = map;
            Assert.Multiple(() => {
                Assert.That(stdmap.HpDrainRate,       Is.EqualTo(5.0f), $"HPDrainRate is not equal to 5.0, {stdmap.HpDrainRate}");
                Assert.That(stdmap.CircleSize,        Is.EqualTo(4.0f), $"CircleSize is not equal to 4.0, {stdmap.CircleSize}");
                Assert.That(stdmap.OverallDifficulty, Is.EqualTo(3.0f), $"OverallDifficulty is not equal to 3.0, {stdmap.OverallDifficulty}");
                Assert.That(stdmap.ApproachRate,      Is.EqualTo(2.0f), $"ApproachRate is not equal to 2.0, {stdmap.ApproachRate}");
                Assert.That(stdmap.SliderMultiplier,  Is.EqualTo(1.4f), $"SliderMultiplier is not equal to 1.4, {stdmap.SliderMultiplier}");
                Assert.That(stdmap.SliderTickRate,    Is.EqualTo(1.0f), $"SliderTickRate is not equal to 1.0, {stdmap.SliderTickRate}");
            });
        }
        
        [Test]
        public void EventsSectionTest() {
            BeatMap stdmap = map;
            Assert.Inconclusive("Not yet implemented");
        }
        
        [Test]
        public void TimingPointsSectionTest() {
            BeatMap stdmap = map;
            Assert.Inconclusive("Not yet implemented");
        }
        
        [Test]
        public void ColoursSectionTest() {
            BeatMap stdmap = map;
            Assert.Inconclusive("Not yet implemented");
        }
        
        [Test]
        public void HitObjectsSectionTest() {
            BeatMap stdmap = map;
            Assert.Inconclusive("Not yet implemented");
        }
    }
}