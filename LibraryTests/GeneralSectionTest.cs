using NUnit.Framework;
using OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class GeneralSectionTest {
        string[] fullSection = new []
        {
            "AudioFilename:Music.mp3",
            "AudioLeadIn:1000",
            "AudioHash:1234567890",
            "PreviewTime:5000",
            "Countdown:1",
            "SampleSet:TestSampleSet",
            "StackLeniency:0.7",
            "Mode:0",
            "LetterboxInBreaks:1",
            "StoryFireInFront:1",
            "UseSkinSprites:1",
            "AlwaysShowPlayfield:1",
            "OverlayPosition:NoChange",
            "SkinPreference:TestSkin",
            "EpilepsyWarning:1",
            "CountdownOffset:10",
            "SpecialStyle:1",
            "WidescreenStoryboard:1",
            "SamplesMatchPlaybackRate:1"
        };

        [Test]
        public void GeneralSectionParsesTest() {
            BeatMap stdmap = new BeatMap();
            int cursor = 0;
            var data = OsuLoader.OsuLoader.GetKeyPairs(fullSection, ref cursor);
            OsuLoader.OsuLoader.ParseGeneralSection(data, ref stdmap);
            
            Assert.Multiple(() => {
                Assert.That(stdmap.AudioFilename,            Is.EqualTo("Music.mp3"));
                Assert.That(stdmap.AudioLeadIn,              Is.EqualTo(1000));
                Assert.That(stdmap.AudioHash,                Is.EqualTo("1234567890"));
                Assert.That(stdmap.PreviewTime,              Is.EqualTo(5000));
                Assert.That(stdmap.Countdown,                Is.EqualTo(CountdownType.Normal));
                Assert.That(stdmap.SampleSet,                Is.EqualTo("TestSampleSet"));
                Assert.That(stdmap.StackLeniency,            Is.EqualTo(0.7f));
                Assert.That(stdmap.Mode,                     Is.EqualTo(GameMode.Osu));
                Assert.That(stdmap.LetterboxInBreaks,        Is.EqualTo(true));
                Assert.That(stdmap.StoryFireInFront,         Is.EqualTo(true));
                Assert.That(stdmap.UseSkinSprites,           Is.EqualTo(true));
                Assert.That(stdmap.AlwaysShowPlayfield,      Is.EqualTo(true));
                Assert.That(stdmap.OverlayPosition,          Is.EqualTo(OverlayPosition.NoChange));
                Assert.That(stdmap.SkinPreference,           Is.EqualTo("TestSkin"));
                Assert.That(stdmap.EpilepsyWarning,          Is.EqualTo(true));
                Assert.That(stdmap.CountdownOffset,          Is.EqualTo(10));
                Assert.That(stdmap.SpecialStyle,             Is.EqualTo(true));
                Assert.That(stdmap.WidescreenStoryboard,     Is.EqualTo(true));
                Assert.That(stdmap.SamplesMatchPlaybackRate, Is.EqualTo(true));
            });
        }
    }
}