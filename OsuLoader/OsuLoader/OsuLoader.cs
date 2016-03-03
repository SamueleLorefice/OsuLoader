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

namespace OsuLoader
{
    /// <summary>
    /// Static class that manages the loading an deserialization of .osu files.
    /// </summary>
    public static class OsuLoader
    {
        /// <summary>
        /// Load and deserialize a specified .osu file
        /// </summary>
        /// <returns>The .osu file as BeatMap object</returns>
        /// <param name="path">File location of the .osu (including the filename itself)</param>
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
            //Istantiate the return object
            BeatMap parsedMap = new BeatMap();
            #endregion
            //--------------------------------------------------------------
            //Conversion of the values from ini to a dynamic object.
            #region General
            string value;
            if (generalKeyPairs.TryGetValue("AudioFilename", out value))
                parsedMap.FileName = value;
            if (generalKeyPairs.TryGetValue("AudioLeadIn", out value))
                parsedMap.AudioLeadIn = int.Parse(value);
            if (generalKeyPairs.TryGetValue("PreviewTime", out value))
                parsedMap.PreviewTime = int.Parse(value);
            if (generalKeyPairs.TryGetValue("CountDown", out value))
                parsedMap.Countdown = bool.Parse(value);
            if (generalKeyPairs.TryGetValue("SampleSet", out value))
                parsedMap.SampleSet = value;
            if (generalKeyPairs.TryGetValue("StackLeniency", out value))
                parsedMap.StackLeniency = float.Parse(value);
            if (generalKeyPairs.TryGetValue("Mode", out value))
                parsedMap.Mode = int.Parse(value);
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
                parsedMap.Tags = value;
            if (metadataKeyPairs.TryGetValue("BeatmapID", out value))
                parsedMap.BeatmapID = int.Parse(value);
            if (metadataKeyPairs.TryGetValue("BeatmapSetID", out value))
                parsedMap.BeatmapSetID = int.Parse(value);

            #endregion
            #region Difficulty
            if (difficultyKeyPairs.TryGetValue("HPDrainRate", out value))
                parsedMap.HPDrainRate = float.Parse(value);
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
            parsedMap.Colours = new List<colour>();
            for (int i = 0; i < coloursKeyPairs.Count; i++)
            {
                string[] SplittedRGB = new string[3];
                string ComboN = Convert.ToString(i + 1);

                if (coloursKeyPairs.TryGetValue("Combo" + ComboN + " ", out value))
                {
                    SplittedRGB = value.Split(',');
                    SplittedRGB[0] = SplittedRGB[0].Trim();
                    parsedMap.Colours.Add(new colour(int.Parse(SplittedRGB[0]), int.Parse(SplittedRGB[1]), int.Parse(SplittedRGB[2])));
                }
            }
            #endregion
            //--------------------------------------------------------------
            //Conversion of the values from the raw text file
            #region from raw text
            //Subdivision of the string over sections
            string[] partialBM = rawBeatmap.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //Recognize where timing points and hit objects are
            int timingIndex = Array.IndexOf(partialBM, "[TimingPoints]");
            int hitIndex = Array.IndexOf(partialBM, "[HitObjects]");
            //initzialize parsedMap timing point list and notes point list
            parsedMap.TimingPoints = new List<TimingPoint>();
            parsedMap.SingleNotes = new List<Note>();
            parsedMap.LongNotes = new List<LongNote>();
            //read the timing points
            for (int i = timingIndex; i < hitIndex; i++)
            {
                TimingPoint parsed = ParseTimingPoint(partialBM[i]);
                if (parsed.Offset != 0)
                    parsedMap.TimingPoints.Add(parsed);
            }
            //read the single notes
            for (int i = hitIndex; i < partialBM.Length; i++)
            {
                Note parsed = ParseSingleNote(partialBM[i]);
                if (parsed.Time != 0)
                    parsedMap.SingleNotes.Add(parsed);
            }
            //read the long notes
            for (int i = hitIndex; i < partialBM.Length; i++)
            {
                LongNote parsed = ParseLongNote(partialBM[i]);
                if (parsed.StartTime != 0)
                    parsedMap.LongNotes.Add(parsed);
            }
            #endregion
            return parsedMap;
        }

        /// <summary>
        /// Saves the BeatMap object as .osu file in the specified path
        /// </summary>
        /// <param name="path">File save path</param>
        /// <param name="toWrite">Object to write</param>
        public static void SaveDotOsu(string path, BeatMap toWrite)
        {
            IniFile beatmap = new IniFile(path);
            beatmap.WriteAllSection("General",
                "AudioFilename: " + toWrite.FileName +
                "\r\nAudioLeadin: " + toWrite.AudioLeadIn +
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
                "\r\nBeatmapID: " + toWrite.BeatmapID +
                "\r\nBeatmapSetID: " + toWrite.BeatmapSetID);
            beatmap.WriteAllSection("Difficulty",
                "HPDrainRate: " + toWrite.HPDrainRate +
                "\r\nCircleSize: " + toWrite.CircleSize +
                "\r\nOverallDifficulty: " + toWrite.OverallDifficulty +
                "\r\nApproachRate: " + toWrite.ApproachRate +
                "\r\nSliderMultiplier: " + toWrite.SliderMultiplier +
                "\r\nSliderTickRate: " + toWrite.SliderTickRate);
            beatmap.WriteAllSection("Colours", GetColoursToSave(toWrite));
            beatmap.WriteAllSection("TimingPoints", GetTimingPointString(toWrite.TimingPoints));
            beatmap.WriteAllSection("HitObjects", GetHitObjectsString(toWrite.SingleNotes, toWrite.LongNotes));
        }
        static string GetColoursToSave(BeatMap toWrite)
        {
            string ColoursToSave = null;
            int number = 1;
            foreach (colour rgb in toWrite.Colours)
            {
                ColoursToSave += "Combo" + number + " : " + rgb.Red + ',' + rgb.Green + ',' + rgb.Blue + "\r\n";
                number++;
            }
            return ColoursToSave;
        }

        static string GetTimingPointString(List<TimingPoint> timings)
        {
            string _out = "";
            foreach (var tp in timings)
            {
                _out += tp.Offset + "," + tp.MilliSecondPerBeat + "," + tp.Meter;
                if (tp.Inherithed)
                    _out += "0,";
                else
                    _out += "1,";
                if (tp.Kiai)
                    _out += "1";
                else
                    _out += "0";
                _out += "\r\n";
            }
            return _out;
        }

        /// <summary>
        /// Parse a timing point
        /// </summary>
        /// <returns>The timing point</returns>
        /// <param name="toParse">string containing a timing point</param>
        public static TimingPoint ParseTimingPoint(string toParse)
        {
            string[] splitted = toParse.Split(new string[] { "," }, StringSplitOptions.None);
            if (splitted.Length != 8)
            {
                return new TimingPoint();
            }
            else {
                TimingPoint toReturn = new TimingPoint();
                toReturn.Offset = int.Parse(splitted[0]);
                toReturn.MilliSecondPerBeat = float.Parse(splitted[1]);
                toReturn.Meter = int.Parse(splitted[2]);
                //inherited check
                if (int.Parse(splitted[6]) == 0)
                    toReturn.Inherithed = true;
                else
                    toReturn.Inherithed = false;
                //kiai check
                if (int.Parse(splitted[7]) == 0)
                    toReturn.Kiai = false;
                else
                    toReturn.Kiai = true;
                //return
                return toReturn;
            }
        }

        static string GetHitObjectsString(List<Note> sn, List<LongNote> ln)
        {
            string _out = "";
            foreach (var note in sn)
            {
                //x,y,time,type,hitSound,addition
                switch (note.Column)
                {
                    case 0:
                        _out += "64,";
                        break;
                    case 1:
                        _out += "192,";
                        break;
                    case 2:
                        _out += "320,";
                        break;
                    case 3:
                        _out += "448,";
                        break;
                }
                _out += "192,";
                _out += note.Time + ",";
                _out += "1,0,0:0:0:0:\r\n";
            }
            foreach (var note in ln)
            {
                //x,  y, time,type,hitSound, end, addition (ln's in mania are saved as spinners)
                switch (note.Column)
                {
                    case 0:
                        _out += "64,";
                        break;
                    case 1:
                        _out += "192,";
                        break;
                    case 2:
                        _out += "320,";
                        break;
                    case 3:
                        _out += "448,";
                        break;
                }
                _out += "192,";
                _out += note.StartTime + ",";
                _out += "128,0,";
                _out += note.EndTime;
                _out += "0:0:0:0:\r\n";
            }
            return _out;
        }

        /// <summary>
        /// Parse a single note
        /// </summary>
        /// <returns>The single note</returns>
        /// <param name="toParse">string containing a single note</param>
        public static Note ParseSingleNote(string toParse)
        {
            string[] splitted = toParse.Split(new string[] { "," }, StringSplitOptions.None);
            if (splitted.Length != 6 || splitted[3] != "1")
            {
                return new Note();
            }
            else {
                //check if single note
                Note toReturn = new Note();
                //Detect the note column
                switch (int.Parse(splitted[0]))
                {
                    case 64:
                        toReturn.Column = 0;
                        break;
                    case 192:
                        toReturn.Column = 1;
                        break;
                    case 320:
                        toReturn.Column = 2;
                        break;
                    case 448:
                        toReturn.Column = 3;
                        break;
                    default:
                        return new Note();
                }
                //detect time
                toReturn.Time = int.Parse(splitted[2]);
                return toReturn;
            }
        }

        /// <summary>
        /// Parse a long note
        /// </summary>
        /// <returns>The long note</returns>
        /// <param name="toParse">string containing a long note</param>
        public static LongNote ParseLongNote(string toParse)
        {
            string[] splitted = toParse.Split(new string[] { "," }, StringSplitOptions.None);
            if (splitted.Length != 6 || splitted[3] != "128")
            {
                return new LongNote();
            }
            else {
                LongNote toReturn = new LongNote();
                //Detect the note column
                switch (int.Parse(splitted[0]))
                {
                    case 64:
                        toReturn.Column = 0;
                        break;
                    case 192:
                        toReturn.Column = 1;
                        break;
                    case 320:
                        toReturn.Column = 2;
                        break;
                    case 448:
                        toReturn.Column = 3;
                        break;
                    default:
                        return new LongNote();
                }
                //detect time
                toReturn.StartTime = int.Parse(splitted[2]);
                string[] endCode = splitted[5].Split(new string[] { ":" }, StringSplitOptions.None);
                toReturn.EndTime = int.Parse(endCode[0]);
                return toReturn;
            }
        }
    }
}