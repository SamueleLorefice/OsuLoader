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
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration.Ini;

namespace OsuLoader
{
    /// <summary>
    /// Static class that manages the loading a deserialization of .osu files.
    /// </summary>
    public static class OsuLoader
    {
        
        /// <summary>
        /// Load and deserialize a specified .osu file
        /// </summary>
        /// <returns>The .osu file as BeatMap object</returns>
        /// <param name="path">File location of the .osu (including the filename itself)</param>
        [Obsolete("Old method that uses internal MS only P/Invoke functions, use GetBeatmap instead. This  will be removed in the future", false)] 
        public static BeatMap LoadDotOsu(string path)
        {
            #region Read of the file as ini
            IniFile beatmap = new IniFile(path);
            Dictionary<string, string> generalKeyPairs = beatmap.GetKeyValuesPair("General");
            Dictionary<string, string> metadataKeyPairs = beatmap.GetKeyValuesPair("Metadata");
            Dictionary<string, string> difficultyKeyPairs = beatmap.GetKeyValuesPair("Difficulty");
            Dictionary<string, string> coloursKeyPairs = beatmap.GetKeyValuesPair("Colours");
            #endregion
            #region read of the file as raw text
            string rawBeatmap = File.ReadAllText(path);
            //Instantiate the return object
            BeatMap parsedMap = new BeatMap();
            #endregion
            //--------------------------------------------------------------
            //Conversion of the values from ini to a dynamic object.
            #region General

            if (generalKeyPairs.TryGetValue("AudioFilename", out var value))
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
            for (int i = 0; i < coloursKeyPairs.Count; i++)
            {
                string[] splitRgb = new string[3];
                string comboN = Convert.ToString(i + 1);

                if (coloursKeyPairs.TryGetValue("Combo" + comboN + " ", out value))
                {
                    splitRgb = value.Split(',');
                    splitRgb[0] = splitRgb[0].Trim();
                    parsedMap.Colours.Add(new Tuple<string, Color>($"Combo{comboN}", new Color(int.Parse(splitRgb[0]), int.Parse(splitRgb[1]), int.Parse(splitRgb[2]))));
                }else if (coloursKeyPairs.TryGetValue("SliderTrackOverride", out value)){
                    splitRgb = value.Split(',');
                    splitRgb[0] = splitRgb[0].Trim();
                    parsedMap.Colours.Add(new Tuple<string, Color>("SliderTrackOverride", new Color(int.Parse(splitRgb[0]), int.Parse(splitRgb[1]), int.Parse(splitRgb[2]))));
                }else if (coloursKeyPairs.TryGetValue("SliderBorder", out value)){
                    splitRgb = value.Split(',');
                    splitRgb[0] = splitRgb[0].Trim();
                    parsedMap.Colours.Add(new Tuple<string, Color>("SliderBorder", new Color(int.Parse(splitRgb[0]), int.Parse(splitRgb[1]), int.Parse(splitRgb[2]))));
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
            int hitIndex = Array.IndexOf(partialBm, "[HitObjects]");
            //initialize parsedMap timing point list and notes point list
            parsedMap.TimingPoints = new List<TimingPoint>();
            /*
            parsedMap.SingleNotes = new List<Note>();
            parsedMap.LongNotes = new List<LongNote>();*/
            //read the timing points
            for (int i = timingIndex; i < hitIndex; i++)
            {
                TimingPoint parsed = ParseTimingPoint(partialBm[i]);
                if (parsed.Offset != 0)
                    parsedMap.TimingPoints.Add(parsed);
            }
            //TODO: read the hit objects
            #endregion
            return parsedMap;
        }
        
        /// <summary>
        /// Get a BeatMap object from a .osu file
        /// </summary>
        /// <param name="fileString">The .osu file as a string</param>
        /// <returns>The BeatMap object</returns>
        /// <remarks>Note that this version requires that you load the osu file from the filesystem.</remarks>
        public static BeatMap GetBeatMap(string fileString)
        {
            
            return null;
        }
        
        /// <summary>
        /// Get a BeatMap object from a .osu file
        /// </summary>
        /// <param name="path">The path to the .osu file</param>
        /// <returns>The BeatMap object</returns>
        /// <remarks>This version, unlike the other, loads the osu file from the filesystem using a ReadAllText method.</remarks>
        public static BeatMap GetBeatMapFromFile(string path)
        {
            string fileString = File.ReadAllText(path);
            return GetBeatMap(fileString);
        }

        /// <summary>
        /// Saves the BeatMap object as .osu file in the specified path
        /// </summary>
        /// <param name="path">File save path</param>
        /// <param name="toWrite">Object to write</param>
        [Obsolete("Old methods that relies on internal MS only P/Invoke functions, use WriteBeatmap instead. This will be removed in the future", false)]
        public static void SaveDotOsu(string path, BeatMap toWrite)
        {
            IniFile beatmap = new IniFile(path);
            beatmap.WriteAllSection("General",
                "AudioFilename: " + toWrite.AudioFileName +
                "\r\nAudioLeadIn: " + toWrite.AudioLeadIn +
                "\r\nPreviewTime: " + toWrite.PreviewTime +
                "\r\nCountDown: " + toWrite.Countdown +
                "\r\nSampleSet: " + toWrite.SampleSet +
                "\r\nStackLeniency: " + toWrite.StackLeniency +
                "\r\nMode: " + toWrite.Mode +
                "\r\nLetterBoxInBreaks: " + toWrite.LetterBoxInBreaks +
                "\r\nWideScreenStoryboard" + toWrite.WideScreenStoryboard);
            beatmap.WriteAllSection("Metadata",
                "Title: " + toWrite.Title +
                "\r\nTitleUnicode: " + toWrite.TitleUnicode +
                "\r\nArtist: " + toWrite.Artist +
                "\r\nArtistUnicode: " + toWrite.ArtistUnicode +
                "\r\nCreator: " + toWrite.Creator +
                "\r\nVersion: " + toWrite.Version +
                "\r\nSource: " + toWrite.Source +
                "\r\nTags: " + toWrite.Tags +
                "\r\nBeatmapID: " + toWrite.BeatmapId +
                "\r\nBeatmapSetID: " + toWrite.BeatmapSetId);
            beatmap.WriteAllSection("Difficulty",
                "HPDrainRate: " + toWrite.HpDrainRate +
                "\r\nCircleSize: " + toWrite.CircleSize +
                "\r\nOverallDifficulty: " + toWrite.OverallDifficulty +
                "\r\nApproachRate: " + toWrite.ApproachRate +
                "\r\nSliderMultiplier: " + toWrite.SliderMultiplier +
                "\r\nSliderTickRate: " + toWrite.SliderTickRate);
            beatmap.WriteAllSection("Colours", GetColoursToSave(toWrite));
            beatmap.WriteAllSection("TimingPoints", GetTimingPointString(toWrite.TimingPoints));
            //TODO: write hit objects
        }
        
        static string GetColoursToSave(BeatMap toWrite)
        {
            string coloursToSave = null;
            int number = 1;
            foreach (Tuple<string, Color> rgb in toWrite.Colours)
            {
                coloursToSave += $"{rgb.Item1} : {rgb.Item2.R},{rgb.Item2.G},{rgb.Item2.B}\r\n";
                number++;
            }
            return coloursToSave;
        }

        static string GetTimingPointString(List<TimingPoint> timings)
        {
            string timingStr = "";
            foreach (var tp in timings)
            {
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
        private static TimingPoint ParseTimingPoint(string toParse)
        {
            string[] split = toParse.Split(new string[] { "," }, StringSplitOptions.None);
            if (split.Length != 8)
            {
                return new TimingPoint();
            }
            else {
                TimingPoint toReturn = new TimingPoint
                {
                    Offset = int.Parse(split[0]),
                    MilliSecondPerBeat = float.Parse(split[1]),
                    Meter = int.Parse(split[2]),
                    //inherited check
                    Uninherited = int.Parse(split[6]) == 0,
                    //kiai check
                    Effects = int.Parse(split[7]) == 1 ? TimingEffect.Kiai : TimingEffect.None
                };
                //return
                return toReturn;
            }
        }

        static string GetHitObjectsString(List<IHitObject> notes)
        {
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
    }
}