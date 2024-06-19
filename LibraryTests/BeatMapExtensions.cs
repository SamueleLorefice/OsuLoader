using NUnit.Framework;
using OsuLoader;
using loader = OsuLoader.OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class BeatMapExtensions {
        [Test]
        public void GetGeneralSection_ReturnsCorrectIniFormat_WhenCalled()
        {
            var beatMap = new BeatMap
            {
                AudioFilename            = "test.mp3",
                AudioLeadIn              = 2000,
                PreviewTime              = 5000,
                Countdown                = CountdownType.Normal,
                SampleSet                = "Normal",
                StackLeniency            = 0.7f,
                Mode                     = GameMode.Osu,
                LetterboxInBreaks        = false,
                UseSkinSprites           = false,
                OverlayPosition          = OverlayPosition.NoChange,
                EpilepsyWarning          = false,
                CountdownOffset          = 0,
                SpecialStyle             = false,
                WidescreenStoryboard     = false,
                SamplesMatchPlaybackRate = false
            };

            var result = beatMap.GetGeneralSection();

            int     cursor = 0;
            BeatMap decoded = new BeatMap();
            loader.ParseGeneralSection(loader.GetKeyPairs(result, ref cursor), ref decoded);
            
            Assert.That(beatMap, Is.EqualTo(decoded));
        }
    }
}