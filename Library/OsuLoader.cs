//OsuLoader - A small library to parse and manage .osu files
//Copyright (C) 2016  Samuele Lorefice
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using Ini;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace OsuLoader {
    /// <summary>
    /// Static class that manages the loading a deserialization of .osu files.
    /// </summary>
    public static class OsuLoader {
        #region OldShittyCode

        /// <summary>
        /// Load and deserialize a specified .osu file
        /// </summary>
        /// <returns>The .osu file as BeatMap object</returns>
        /// <param name="path">File location of the .osu (including the filename itself)</param>
        [Obsolete("Old method that uses internal MS only P/Invoke functions, use GetBeatmap instead. This  will be removed in the future", false)]
        public static BeatMap LoadDotOsu(string path) {
            #region Read of the file as ini

            IniFile                    beatmap            = new IniFile(path);
            Dictionary<string, string> generalKeyPairs    = beatmap.GetKeyValuesPair("General");
            Dictionary<string, string> metadataKeyPairs   = beatmap.GetKeyValuesPair("Metadata");
            Dictionary<string, string> difficultyKeyPairs = beatmap.GetKeyValuesPair("Difficulty");
            Dictionary<string, string> coloursKeyPairs    = beatmap.GetKeyValuesPair("Colours");

            #endregion
            #region read of the file as raw text

            string rawBeatmap = File.ReadAllText(path);
            //Instantiate the return object
            BeatMap parsedMap = new BeatMap();

            #endregion
            //--------------------------------------------------------------
            //Conversion of the values from ini to a dynamic object.
            #region General

            if (generalKeyPairs.TryGetValue("AudioFilename", out string value))
                parsedMap.AudioFileName = value;
            if (generalKeyPairs.TryGetValue("AudioLeadIn", out value))
                parsedMap.AudioLeadIn = int.Parse(value);
            if (generalKeyPairs.TryGetValue("PreviewTime", out value))
                parsedMap.PreviewTime = int.Parse(value);
            if (generalKeyPairs.TryGetValue("CountDown", out value))
                parsedMap.Countdown = (CountdownType)int.Parse(value);
            if (generalKeyPairs.TryGetValue("SampleSet", out value))
                parsedMap.SampleSet = value;
            if (generalKeyPairs.TryGetValue("StackLeniency", out value))
                parsedMap.StackLeniency = float.Parse(value);
            if (generalKeyPairs.TryGetValue("Mode", out value))
                parsedMap.Mode = (GameMode)int.Parse(value);
            if (generalKeyPairs.TryGetValue("LetterBoxInBreaks", out value))
                parsedMap.LetterBoxInBreaks = bool.Parse(value);
            if (generalKeyPairs.TryGetValue("WideScreenStoryboard", out value))
                parsedMap.WideScreenStoryboard = bool.Parse(value);

            #endregion
            #region Metadata

            if (metadataKeyPairs.TryGetValue("Title", out value))
                parsedMap.Title = value;
            if (metadataKeyPairs.TryGetValue("TitleUnicode", out value))
                parsedMap.TitleUnicode = value;
            if (metadataKeyPairs.TryGetValue("Artist", out value))
                parsedMap.Artist = value;
            if (metadataKeyPairs.TryGetValue("ArtistUnicode", out value))
                parsedMap.ArtistUnicode = value;
            if (metadataKeyPairs.TryGetValue("Creator", out value))
                parsedMap.Creator = value;
            if (metadataKeyPairs.TryGetValue("Version", out value))
                parsedMap.Version = value;
            if (metadataKeyPairs.TryGetValue("Source", out value))
                parsedMap.Source = value;
            if (metadataKeyPairs.TryGetValue("Tags", out value))
                parsedMap.Tags = value.Split(' ').ToList();
            if (metadataKeyPairs.TryGetValue("BeatmapID", out value))
                parsedMap.BeatmapId = int.Parse(value);
            if (metadataKeyPairs.TryGetValue("BeatmapSetID", out value))
                parsedMap.BeatmapSetId = int.Parse(value);

            #endregion
            #region Difficulty

            if (difficultyKeyPairs.TryGetValue("HPDrainRate", out value))
                parsedMap.HpDrainRate = float.Parse(value);
            if (difficultyKeyPairs.TryGetValue("CircleSize", out value))
                parsedMap.CircleSize = float.Parse(value);
            if (difficultyKeyPairs.TryGetValue("OverallDifficulty", out value))
                parsedMap.OverallDifficulty = float.Parse(value);
            if (difficultyKeyPairs.TryGetValue("ApproachRate", out value))
                parsedMap.ApproachRate = float.Parse(value);
            if (difficultyKeyPairs.TryGetValue("SliderMultiplier", out value))
                parsedMap.SliderMultiplier = float.Parse(value);
            if (difficultyKeyPairs.TryGetValue("SliderTickRate", out value))
                parsedMap.SliderTickRate = float.Parse(value);

            #endregion
            #region Colours

            parsedMap.Colours = new List<Tuple<string, Color>>();
            for (int i = 0; i < coloursKeyPairs.Count; i++) {
                string[] splitRgb = new string[3];
                string   comboN   = Convert.ToString(i + 1);

                if (coloursKeyPairs.TryGetValue("Combo" + comboN + " ", out value)) {
                    splitRgb    = value.Split(',');
                    splitRgb[0] = splitRgb[0].Trim();
                    parsedMap.Colours.Add(new Tuple<string, Color>($"Combo{comboN}",
                        new Color(int.Parse(splitRgb[0]), int.Parse(splitRgb[1]), int.Parse(splitRgb[2]))));
                }
                else if (coloursKeyPairs.TryGetValue("SliderTrackOverride", out value)) {
                    splitRgb    = value.Split(',');
                    splitRgb[0] = splitRgb[0].Trim();
                    parsedMap.Colours.Add(new Tuple<string, Color>("SliderTrackOverride",
                        new Color(int.Parse(splitRgb[0]), int.Parse(splitRgb[1]), int.Parse(splitRgb[2]))));
                }
                else if (coloursKeyPairs.TryGetValue("SliderBorder", out value)) {
                    splitRgb    = value.Split(',');
                    splitRgb[0] = splitRgb[0].Trim();
                    parsedMap.Colours.Add(new Tuple<string, Color>("SliderBorder",
                        new Color(int.Parse(splitRgb[0]), int.Parse(splitRgb[1]), int.Parse(splitRgb[2]))));
                }
            }

            #endregion
            //--------------------------------------------------------------
            //Conversion of the values from the raw text file
            #region from raw text

            //Subdivision of the string over sections
            string[] partialBm = rawBeatmap.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //Recognize where timing points and hit objects are
            int timingIndex = Array.IndexOf(partialBm, "[TimingPoints]");
            int hitIndex    = Array.IndexOf(partialBm, "[HitObjects]");
            //initialize parsedMap timing point list and notes point list
            parsedMap.TimingPoints = new List<TimingPoint>();
            /*
            parsedMap.SingleNotes = new List<Note>();
            parsedMap.LongNotes = new List<LongNote>();*/
            //read the timing points
            for (int i = timingIndex; i < hitIndex; i++) {
                TimingPoint parsed = ParseTimingPoint(partialBm[i]);
                if (parsed.Offset != 0)
                    parsedMap.TimingPoints.Add(parsed);
            }
            //TODO: read the hit objects

            #endregion
            return parsedMap;
        }

        /// <summary>
        /// Saves the BeatMap object as .osu file in the specified path
        /// </summary>
        /// <param name="path">File save path</param>
        /// <param name="toWrite">Object to write</param>
        [Obsolete("Old methods that relies on internal MS only P/Invoke functions, use WriteBeatmap instead. This will be removed in the future", false)]
        public static void SaveDotOsu(string path, BeatMap toWrite) {
            IniFile beatmap = new IniFile(path);
            beatmap.WriteAllSection("General",
                "AudioFilename: "          + toWrite.AudioFileName     +
                "\r\nAudioLeadIn: "        + toWrite.AudioLeadIn       +
                "\r\nPreviewTime: "        + toWrite.PreviewTime       +
                "\r\nCountDown: "          + toWrite.Countdown         +
                "\r\nSampleSet: "          + toWrite.SampleSet         +
                "\r\nStackLeniency: "      + toWrite.StackLeniency     +
                "\r\nMode: "               + toWrite.Mode              +
                "\r\nLetterBoxInBreaks: "  + toWrite.LetterBoxInBreaks +
                "\r\nWideScreenStoryboard" + toWrite.WideScreenStoryboard);
            beatmap.WriteAllSection("Metadata",
                "Title: "             + toWrite.Title         +
                "\r\nTitleUnicode: "  + toWrite.TitleUnicode  +
                "\r\nArtist: "        + toWrite.Artist        +
                "\r\nArtistUnicode: " + toWrite.ArtistUnicode +
                "\r\nCreator: "       + toWrite.Creator       +
                "\r\nVersion: "       + toWrite.Version       +
                "\r\nSource: "        + toWrite.Source        +
                "\r\nTags: "          + toWrite.Tags          +
                "\r\nBeatmapID: "     + toWrite.BeatmapId     +
                "\r\nBeatmapSetID: "  + toWrite.BeatmapSetId);
            beatmap.WriteAllSection("Difficulty",
                "HPDrainRate: "           + toWrite.HpDrainRate       +
                "\r\nCircleSize: "        + toWrite.CircleSize        +
                "\r\nOverallDifficulty: " + toWrite.OverallDifficulty +
                "\r\nApproachRate: "      + toWrite.ApproachRate      +
                "\r\nSliderMultiplier: "  + toWrite.SliderMultiplier  +
                "\r\nSliderTickRate: "    + toWrite.SliderTickRate);
            beatmap.WriteAllSection("Colours",      GetColoursToSave(toWrite));
            beatmap.WriteAllSection("TimingPoints", GetTimingPointString(toWrite.TimingPoints));
            //TODO: write hit objects
        }

        private static string GetColoursToSave(BeatMap toWrite) {
            string coloursToSave = null;
            int    number        = 1;
            foreach (Tuple<string, Color> rgb in toWrite.Colours) {
                coloursToSave += $"{rgb.Item1} : {rgb.Item2.R},{rgb.Item2.G},{rgb.Item2.B}\r\n";
                number++;
            }
            return coloursToSave;
        }

        private static string GetTimingPointString(List<TimingPoint> timings) {
            string timingStr = "";
            foreach (TimingPoint tp in timings) {
                timingStr += tp.Offset + "," + tp.MilliSecondPerBeat + "," + tp.Meter;
                if (tp.Uninherited)
                    timingStr += "0,";
                else
                    timingStr += "1,";

                if (tp.Effects.HasFlag(TimingEffect.Kiai))
                    timingStr += "1";
                else
                    timingStr += "0";

                timingStr += "\r\n";
            }
            return timingStr;
        }

        /// <summary>
        /// Parse a timing point
        /// </summary>
        /// <returns>The timing point</returns>
        /// <param name="toParse">string containing a timing point</param>
        private static TimingPoint ParseTimingPoint(string toParse) {
            string[] split = toParse.Split(new string[] { "," }, StringSplitOptions.None);
            if (split.Length != 8) {
                return new TimingPoint();
            }
            else {
                TimingPoint toReturn = new TimingPoint {
                    Offset             = int.Parse(split[0]),
                    MilliSecondPerBeat = float.Parse(split[1]),
                    Meter              = int.Parse(split[2]),
                    //inherited check
                    Uninherited = int.Parse(split[6]) == 0,
                    //kiai check
                    Effects = int.Parse(split[7]) == 1 ? TimingEffect.Kiai : TimingEffect.None
                };
                //return
                return toReturn;
            }
        }

        private static string GetHitObjectsString(List<IHitObject> notes) {
            string hitObjStr = "";
            /*
            foreach (var note in sn)
            {
                //x,y,time,type,hitSound,addition
                switch (note.Column)
                {
                    case 0:
                        hitObjStr += "64,";
                        break;
                    case 1:
                        hitObjStr += "192,";
                        break;
                    case 2:
                        hitObjStr += "320,";
                        break;
                    case 3:
                        hitObjStr += "448,";
                        break;
                }
                hitObjStr += "192,";
                hitObjStr += note.Time + ",";
                hitObjStr += "1,0,0:0:0:0:\r\n";
            }
            foreach (var note in ln)
            {
                //x,  y, time,type,hitSound, end, addition (ln's in mania are saved as spinners)
                switch (note.Column)
                {
                    case 0:
                        hitObjStr += "64,";
                        break;
                    case 1:
                        hitObjStr += "192,";
                        break;
                    case 2:
                        hitObjStr += "320,";
                        break;
                    case 3:
                        hitObjStr += "448,";
                        break;
                }
                hitObjStr += "192,";
                hitObjStr += note.StartTime + ",";
                hitObjStr += "128,0,";
                hitObjStr += note.EndTime;
                hitObjStr += "0:0:0:0:\r\n";
            }
            return hitObjStr;*/
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Get a BeatMap object from a .osu file
        /// </summary>
        /// <param name="path">The path to the .osu file</param>
        /// <returns>The BeatMap object</returns>
        /// <remarks>This version, unlike the other, loads the osu file from the filesystem using a ReadAllText method.</remarks>
        public static BeatMap GetBeatMapFromFile(string path) {
            string[] osuFile = File.ReadAllLines(path);
            return GetBeatMap(osuFile);
        }

        /// <summary>
        /// Get a BeatMap object from a .osu file
        /// </summary>
        /// <param name="osuFile">The .osu file as a strings array</param>
        /// <returns>The BeatMap object</returns>
        /// <remarks>Note that this version requires that you load the osu file from the filesystem.</remarks>
        public static BeatMap GetBeatMap(string[] osuFile) {
            BeatMap parsedMap = new BeatMap();
            int     cursor    = 0;

            //Detect Version
            Regex  versionRegex = new Regex(@"(osu file format v[0-9]+)");
            string version      = versionRegex.Match(osuFile[0]).Value;

            for (int i = 0; i < osuFile.Length; i++)
                if (versionRegex.IsMatch(osuFile[i])) {
                    parsedMap.OsuVersion = versionRegex.Match(osuFile[i]).Value;
                    Console.WriteLine("Detected version: " + version);
                    cursor = i;
                }

            if (parsedMap.OsuVersion == null)
                throw new Exception("Invalid .osu file format, can't detect version.");

            //Scan ahead for General Section
            cursor = SearchForSection(osuFile, "[General]");
            if (cursor != -1) {
                Console.WriteLine("General Section found at line " + (cursor+1));
                //Gather the data from the General Section
                Dictionary<string, string> generalKeyPairs = GetKeyPairs(osuFile, ref cursor);
                //and parse it
                ParseGeneralSection(generalKeyPairs, ref parsedMap);
            }
            else {
                //throw exception if the General Section is not found, it's not optional
                throw new Exception("General Section not found, invalid .osu file format.");
            }

            //Scan ahead for Editor Section
            cursor = SearchForSection(osuFile, "[Editor]");
            if (cursor != -1) {
                Console.WriteLine("Editor Section found at line " + (cursor+1));
                Dictionary<string, string> editorKeyPairs = GetKeyPairs(osuFile, ref cursor);
                ParseEditorSection(editorKeyPairs, ref parsedMap);
            }
            else {
                Console.WriteLine("Editor Section not found, skipping.");
            }

            //Scan ahead for Metadata Section
            cursor = SearchForSection(osuFile, "[Metadata]");
            if (cursor != -1) {
                Console.WriteLine("Metadata Section found at line " + (cursor+1));
                Dictionary<string, string> metadataKeyPairs = GetKeyPairs(osuFile, ref cursor);
                ParseMetadataSection(metadataKeyPairs, ref parsedMap);
            }
            else {
                //throw exception if the Metadata Section is not found, it's not optional
                throw new Exception("Metadata Section not found, invalid .osu file format.");
            }

            //Scan ahead for Difficulty Section
            cursor = SearchForSection(osuFile, "[Difficulty]");
            if (cursor != -1) {
                Console.WriteLine("Difficulty Section found at line " + (cursor+1));
                Dictionary<string, string> difficultyKeyPairs = GetKeyPairs(osuFile, ref cursor);
                ParseDifficultySection(difficultyKeyPairs, ref parsedMap);
            } else {
                //throw exception if the Difficulty Section is not found, it's not optional
                throw new Exception("Difficulty Section not found, invalid .osu file format.");
            }
            
            //Scan ahead for Events Section
            cursor = SearchForSection(osuFile, "[Events]");
            if (cursor != -1) {
                Console.WriteLine("Events Section found at line " + (cursor+1));
                cursor++;
                int eventStart = cursor; 
                GetNextSection(ref osuFile, ref cursor);
                int eventEnd = cursor-1;
                ParseEventSection(osuFile, eventStart, eventEnd, ref parsedMap);
            } else {
                Console.WriteLine("Events Section not found, skipping.");
            }
            
            //Scan ahead for TimingPoints Section
            cursor = SearchForSection(osuFile, "[TimingPoints]");
            if (cursor != -1) {
                Console.WriteLine("TimingPoints Section found at line " + (cursor + 1));
                cursor++;
                int timingStart = cursor;
                GetNextSection(ref osuFile, ref cursor);
                int timingEnd = cursor - 1;
                ParseTimingPointsSection(osuFile, timingStart, timingEnd, ref parsedMap);
            } else {
                throw new Exception("Timing Section not found, invalid .osu file format.");
            }
            
            //Scan ahead for Colours Section
            cursor = SearchForSection(osuFile, "[Colours]");
            if (cursor != -1) {
                Console.WriteLine("Colours Section found at line " + (cursor + 1));
                cursor++;
                int colourStart = cursor;
                GetNextSection(ref osuFile, ref cursor);
                int colourEnd = cursor - 1;
                ParseColoursSection(osuFile, colourStart, colourEnd, ref parsedMap);
            } else {
                Console.WriteLine("Colours Section not found, skipping.");
            }

            //Scan ahead for HitObjects Section
            cursor = SearchForSection(osuFile, "[HitObjects]");
            if (cursor != -1) {
                Console.WriteLine("HitObjects Section found at line " + (cursor + 1));
                cursor++;
                int hitStart = cursor;
                GetNextSection(ref osuFile, ref cursor);
                int hitEnd = cursor - 1;
                ParseHitObjectsSection(osuFile, hitStart, hitEnd, ref parsedMap);
            } else {
                throw new Exception("HitObjects Section not found, invalid .osu file format.");
            }
            
            return parsedMap;
        }

        private static void GetNextSection(ref string[] osuFile, ref int cursor) {
            for (int i = cursor; i < osuFile.Length; i++)
                if (osuFile[i].StartsWith("[")) {
                    cursor = i;
                    break;
                }
        }

        private static int SearchForSection(string[] osuFile, string section) {
            for (int i = 0; i < osuFile.Length; i++)
                if (osuFile[i].StartsWith(section))
                    return i;
            return -1;
        }

        private static Dictionary<string, string> GetKeyPairs(string[] osuFile, ref int cursor) {
            Dictionary<string, string> keyPairs = new Dictionary<string, string>();
            for (int i = cursor; i < osuFile.Length; i++) {
                //skip comments
                if (osuFile[i].StartsWith("//")) continue;
                //check if we reached the next section
                if (osuFile[i].StartsWith("[")) {
                    cursor = i;
                    break;
                }
                string[] split = osuFile[i].Split(':');
                keyPairs.Add(split[0], split[1]);
            }
            return keyPairs;
        }

        private static void ParseGeneralSection(Dictionary<string, string> keyPairs, ref BeatMap beatMap) {
            foreach (KeyValuePair<string, string> pair in keyPairs)
                switch (pair.Key) {
                    case "AudioFilename":
                        beatMap.AudioFileName = pair.Value;
                        break;
                    case "AudioLeadIn":
                        beatMap.AudioLeadIn = int.Parse(pair.Value);
                        break;
                    case "AudioHash":
                        beatMap.AudioHash = pair.Value;
                        break;
                    case "PreviewTime":
                        beatMap.PreviewTime = int.Parse(pair.Value);
                        break;
                    case "Countdown":
                        beatMap.Countdown = (CountdownType)int.Parse(pair.Value);
                        break;
                    case "SampleSet":
                        beatMap.SampleSet = pair.Value;
                        break;
                    case "StackLeniency":
                        beatMap.StackLeniency = float.Parse(pair.Value);
                        break;
                    case "Mode":
                        beatMap.Mode = (GameMode)int.Parse(pair.Value);
                        break;
                    case "LetterBoxInBreaks":
                        beatMap.LetterBoxInBreaks = bool.Parse(pair.Value);
                        break;
                    case "StoryFireInFront":
                        beatMap.StoryFireInFront = bool.Parse(pair.Value);
                        break;
                    case "UseSkinSprites":
                        beatMap.UseSkinSprites = bool.Parse(pair.Value);
                        break;
                    case "AlwaysShowPlayfield":
                        beatMap.AlwaysShowPlayfield = bool.Parse(pair.Value);
                        break;
                    case "OverlayPosition":
                        beatMap.OverlayPosition = (OverlayPosition)int.Parse(pair.Value);
                        break;
                    case "SkinPreference":
                        beatMap.SkinPreference = pair.Value;
                        break;
                    case "EpilepsyWarning":
                        beatMap.EpilepsyWarning = bool.Parse(pair.Value);
                        break;
                    case "CountdownOffset":
                        beatMap.CountdownOffset = int.Parse(pair.Value);
                        break;
                    case "SpecialStyle":
                        beatMap.SpecialStyle = bool.Parse(pair.Value);
                        break;
                    case "WideScreenStoryboard":
                        beatMap.WideScreenStoryboard = bool.Parse(pair.Value);
                        break;
                    case "SamplesMatchPlaybackRate":
                        beatMap.SamplesMatchPlaybackRate = bool.Parse(pair.Value);
                        break;
                    default:
                        Console.WriteLine($"Unknown/Unhandled general key: \"{pair.Key}\"");
                        break;
                }
        }

        private static void ParseEditorSection(Dictionary<string, string> keyPairs, ref BeatMap beatMap) {
            foreach (KeyValuePair<string, string> pair in keyPairs)
                switch (pair.Key) {
                    case "Bookmarks":
                        beatMap.Bookmarks = pair.Value.Split(',').Select(int.Parse).ToList();
                        break;
                    case "DistanceSpacing":
                        beatMap.DistanceSpacing = float.Parse(pair.Value);
                        break;
                    case "BeatDivisor":
                        beatMap.BeatDivisor = int.Parse(pair.Value);
                        break;
                    case "GridSize":
                        beatMap.GridSize = int.Parse(pair.Value);
                        break;
                    case "TimelineZoom":
                        beatMap.TimelineZoom = float.Parse(pair.Value);
                        break;
                    default:
                        Console.WriteLine($"Unknown/Unhandled editor key: \"{pair.Key}\"");
                        break;
                }
        }

        private static void ParseMetadataSection(Dictionary<string, string> keyPairs, ref BeatMap beatMap) {
            foreach (KeyValuePair<string, string> pair in keyPairs)
                switch (pair.Key) {
                    case "Title":
                        beatMap.Title = pair.Value;
                        break;
                    case "TitleUnicode":
                        beatMap.TitleUnicode = pair.Value;
                        break;
                    case "Artist":
                        beatMap.Artist = pair.Value;
                        break;
                    case "ArtistUnicode":
                        beatMap.ArtistUnicode = pair.Value;
                        break;
                    case "Creator":
                        beatMap.Creator = pair.Value;
                        break;
                    case "Version":
                        beatMap.Version = pair.Value;
                        break;
                    case "Source":
                        beatMap.Source = pair.Value;
                        break;
                    case "Tags":
                        beatMap.Tags = pair.Value.Split(' ').ToList();
                        break;
                    case "BeatmapID":
                        beatMap.BeatmapId = int.Parse(pair.Value);
                        break;
                    case "BeatmapSetID":
                        beatMap.BeatmapSetId = int.Parse(pair.Value);
                        break;
                    default:
                        Console.WriteLine($"Unknown/Unhandled metadata key: \"{pair.Key}\"");
                        break;
                }
        }
        
        private static void ParseDifficultySection(Dictionary<string, string> keyPairs, ref BeatMap beatMap) {
            foreach (KeyValuePair<string, string> pair in keyPairs)
                switch (pair.Key) {
                    case "HPDrainRate":
                        beatMap.HpDrainRate = float.Parse(pair.Value);
                        break;
                    case "CircleSize":
                        beatMap.CircleSize = float.Parse(pair.Value);
                        break;
                    case "OverallDifficulty":
                        beatMap.OverallDifficulty = float.Parse(pair.Value);
                        break;
                    case "ApproachRate":
                        beatMap.ApproachRate = float.Parse(pair.Value);
                        break;
                    case "SliderMultiplier":
                        beatMap.SliderMultiplier = float.Parse(pair.Value);
                        break;
                    case "SliderTickRate":
                        beatMap.SliderTickRate = float.Parse(pair.Value);
                        break;
                    default:
                        Console.WriteLine($"Unknown/Unhandled difficulty key: \"{pair.Key}\"");
                        break;
                }
        }
        
        private static void ParseEventSection(string[] osuFile, int start, int end, ref BeatMap beatMap) {
            for (int i = start; i < end; i++) {
                if (osuFile[i].StartsWith("//")) continue;
                
                string[] eventString = osuFile[i].Split(',');
                eventString[0] = eventString[0].Trim();
                switch (eventString[0].ToLower()) {
                    case "0":
                    case "background":
                        BackgroundEvent bg = new BackgroundEvent {
                            StartTime = int.Parse(eventString[1]),
                            Filename  = eventString[2].Trim(), 
                        };
                        if (eventString.Length > 3) {
                            bg.XOffset = int.Parse(eventString[3]);
                            bg.YOffset = int.Parse(eventString[4]);
                        }
                        beatMap.Events.Add(bg);
                        break;
                    case "1":
                    case "video":
                        VideoEvent vid = new VideoEvent {
                            StartTime = int.Parse(eventString[1]),
                            Filename  = eventString[2].Trim(),
                        };
                        if (eventString.Length > 3) {
                            vid.XOffset = int.Parse(eventString[3]);
                            vid.YOffset = int.Parse(eventString[4]);
                        }
                        beatMap.Events.Add(vid);
                        break;
                    case "2":
                    case "break":
                        BreakEvent brk = new BreakEvent {
                            StartTime = int.Parse(eventString[1]),
                            EndTime   = int.Parse(eventString[2])
                        };
                        beatMap.Events.Add(brk);
                        break;
                    default:
                        Console.WriteLine($"Unknown/Unhandled event type: \"{osuFile[i]}\" at line {i+1}");
                        break;
                }
            }
        }
        
        private static void ParseTimingPointsSection(string[] osuFile, int start, int end, ref BeatMap beatMap) {
            for (int i = start; i < end; i++) {
                if (osuFile[i].StartsWith("//")) continue;
                
                string[] timingString = osuFile[i].Split(',');
                if (timingString.Length != 8) {
                    Console.WriteLine($"Invalid timing point at line {i+1}");
                    continue;
                }
                TimingPoint tp = new TimingPoint {
                    Offset             = int.Parse(timingString[0]),
                    MilliSecondPerBeat = float.Parse(timingString[1]),
                    Meter              = int.Parse(timingString[2]),
                    SampleSet          = (HitSoundBank)int.Parse(timingString[3]),
                    SampleIndex        = int.Parse(timingString[4]),
                    Volume             = int.Parse(timingString[5]),
                    Uninherited        = int.Parse(timingString[6]) == 1,
                    Effects            = int.Parse(timingString[7]) == 1 ? TimingEffect.Kiai :
                                         int.Parse(timingString[7]) == 3 ? TimingEffect.OmitFirstBarLine : TimingEffect.None
                    
                };
                beatMap.TimingPoints.Add(tp);
            }
        }
        
        private static void ParseColoursSection(string[] osuFile, int start, int end, ref BeatMap beatMap) {
            for (int i = start; i < end; i++) {
                if (osuFile[i].StartsWith("//")) continue;
                
                string[] colourString = osuFile[i].Split(':');
                if (colourString.Length != 2) {
                    Console.WriteLine($"Invalid colour at line {i+1}");
                    continue;
                }
                string[] rgb = colourString[1].Split(',');
                if (rgb.Length != 3) {
                    Console.WriteLine($"Invalid colour at line {i+1}");
                    continue;
                }
                Color colour = new Color(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));
                beatMap.Colours.Add(new Tuple<string, Color>(colourString[0], colour));
            }
        }
        
        private static void ParseHitObjectsSection(string[] osuFile, int start, int end, ref BeatMap beatMap) {
            for (int i = start; i < end; i++) {
                if (osuFile[i].StartsWith("//")) continue;
                string[] hitLine = osuFile[i].Split(',');
                
                if (hitLine.Length < 6) {
                    Console.WriteLine($"Invalid hit object at line {i+1}");
                    continue;
                }

                IHitObject hitObj;
                var        typeFlag = (HitObjectType)int.Parse(hitLine[3]);
                
                if ((typeFlag & HitObjectType.HitCircle) == HitObjectType.HitCircle)
                    hitObj = new HitCircle();
                else if ((typeFlag & HitObjectType.Slider) == HitObjectType.Slider)
                    hitObj = new Slider();
                else if ((typeFlag & HitObjectType.Spinner) == HitObjectType.Spinner)
                    hitObj = new Spinner();
                else if ((typeFlag & HitObjectType.ManiaHold) == HitObjectType.ManiaHold)
                    hitObj = new ManiaHold();
                else {
                    Console.WriteLine($"Unknown/Unhandled hit object type: \"{hitLine[3]}\" at line {i+1}");
                    continue;
                }
                
                hitObj.X = int.Parse(hitLine[0]);
                hitObj.Y = int.Parse(hitLine[1]);
                hitObj.Time = int.Parse(hitLine[2]);

                if (hitLine.Length == hitObj.ParamsCount) { //check if we have extended sample data.
                    int hitSampleOffset = hitLine.Length - 5;
                    hitObj.HitSoundData = new HitSample {
                        NormalSet   = (HitSoundType)int.Parse(hitLine[hitSampleOffset]),
                        AdditionSet = (HitSoundBank)int.Parse(hitLine[hitSampleOffset + 1]),
                        Index       = int.Parse(hitLine[hitSampleOffset + 2]),
                        Volume      = int.Parse(hitLine[hitSampleOffset + 3]),
                        Filename    = hitLine[hitSampleOffset + 4]
                    };
                } else { //otherwise we just store the normal set
                    hitObj.HitSoundData = new HitSample() {
                        NormalSet = (HitSoundType)int.Parse(hitLine[4])
                    };
                }
                switch (hitObj.GetHitObjType()) {
                    case HitObjectType.HitCircle:
                        //do nothing because it doesn't have extra data
                        break;
                    case HitObjectType.Slider:
                        var    curveData = hitLine[5].Split('|');
                        Slider slider = (Slider)hitObj;
                        slider.CurveType = curveData[0][0] switch {
                            'B' => CurveType.Bezier,
                            'C' => CurveType.Centripetal,
                            'L' => CurveType.Linear,
                            'P' => CurveType.PerfectCircle,
                            _   => CurveType.Linear
                        };
                        for (int y = 1; y < curveData.Length; y++) {
                            string[] point = curveData[y].Split(':');
                            slider.CurvePoints.Add(new Vector2(float.Parse(point[0]), float.Parse(point[1])));
                        }
                        slider.Slides = int.Parse(hitLine[6]);
                        slider.Length = float.Parse(hitLine[7]);
                        var edgeSounds = hitLine[8].Split('|');
                        slider.EdgeSounds = edgeSounds.Select(int.Parse).ToList();
                        var edgeSets = hitLine[9].Split('|');
                        slider.EdgeSets = edgeSets.Select()
                        break;
                    case HitObjectType.Spinner:
                        break;
                    case HitObjectType.ManiaHold:
                        break;
                    default:
                        Console.WriteLine($"Unknown/Unhandled hit object type: \"{hitLine[3]}\" at line {i+1}");
                        break;
                }
                beatMap.HitObjects.Add(hitObj);
            }
        }
    }
}