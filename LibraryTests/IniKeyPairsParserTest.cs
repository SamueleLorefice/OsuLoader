using System;
using System.Linq;
using NUnit.Framework;
using OsuLoader;

namespace LibraryTests {
    [TestFixture]
    public class IniKeyPairsParserTest {
        string[] goodData = new[] {
            "[Section]",
            "[Section 2]",
            "Key1:Value1",
            "Key2 : Value2",
            "Key3: Value3",
            "Key4 :Value4",
            "_key5: Val-ue_5",
            "\"KeySome\"thingElse\" : \"ValueSomethingElse\"",
            "\"KeySomething\": \"ValueSomething\"",
            "Key.Key:Value.Value",
            "",
            "//Comment",
            "#Comment",
            ";Alternative comment",
            "[ActualNewSection]",
        };
        string[] badData = new[] {
            "Key5:",
            "Key6 :",
            ":Value7",
            ":Value8",
            ":",
            "Key9",
            "[FakeNewSection",
            "]",
            "KeySomethingElse:ValueSomethingElse:AnotherKey:AnotherValue",
            "Key10:Value10//Comment",
            "Key11 :Value11 //Comment",
            "Key12: Value12//Comment",
            "Key13 : Value13  //Comment",
            "KeySomething:ValueSomething;Alternative comment",
            "[ActualNewNewSection]"
        };
            
        [Test]
        public void TestKeyPairParsingGood(){
            int cursor = 1;

            var result = OsuLoader.OsuLoader.GetKeyPairs(goodData, ref cursor);
            
            Assert.That(result.Count, Is.EqualTo(8));
            Assert.Multiple(()=>
            {
                Assert.That(result["Key1"], Is.EqualTo("Value1"));
                Assert.That(result["Key2"], Is.EqualTo("Value2"));
                Assert.That(result["Key3"], Is.EqualTo("Value3"));
                Assert.That(result["Key4"], Is.EqualTo("Value4"));
                Assert.That(result["_key5"], Is.EqualTo("Val-ue_5"));
                Assert.That(result["\"KeySome\"thingElse\""], Is.EqualTo("\"ValueSomethingElse\""));
                Assert.That(result["\"KeySomething\""], Is.EqualTo("\"ValueSomething\""));
                Assert.That(result["Key.Key"], Is.EqualTo("Value.Value"));
            });
        }
        
        [Test]
        public void TestKeyPairParsingBad(){
            int cursor = 0;
            Assert.Multiple(() => {
                var result = OsuLoader.OsuLoader.GetKeyPairs(badData[..8], ref cursor);
                Assert.That(result.Count, Is.EqualTo(0), $"expected 0, got:\n  {string.Join("\n  ", result.Select(kvp => $"{kvp.Key}:{kvp.Value}"))}");
                result = OsuLoader.OsuLoader.GetKeyPairs(badData[8..], ref cursor);
                Assert.That(result.Count, Is.EqualTo(5), $"expected 5, got: {result.Count}");
                Assert.That(result["Key10"], Is.EqualTo("Value10"));
                Assert.That(result["Key11"], Is.EqualTo("Value11"));
                Assert.That(result["Key12"], Is.EqualTo("Value12"));
                Assert.That(result["Key13"], Is.EqualTo("Value13"));
                Assert.That(result["KeySomething"], Is.EqualTo("ValueSomething"));
            });
        }
    }
}