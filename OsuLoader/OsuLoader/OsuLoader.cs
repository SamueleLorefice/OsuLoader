using System;
using System.Collections.Generic;
using System.IO;
using Ini;

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
            //Read of the file as ini
            IniFile beatmap = new IniFile(path);
            Dictionary<string, string> generalKeyPairs = beatmap.GetKeyValuesPair("General");
            Dictionary<string, string> metadataKeyPairs = beatmap.GetKeyValuesPair("Metadata");
            Dictionary<string, string> difficultyKeyPairs = beatmap.GetKeyValuesPair("Difficulty");
            //read of the file as raw text
            string rawBeatmap = File.ReadAllText(path);
            //Istantiate the return object
            BeatMap parsedMap = new BeatMap();
            //--------------------------------------------------------------
            //Conversion of the values from ini to a dynamic object.
            //General section
            string value;
            if (generalKeyPairs.TryGetValue("AudioFilename", out value)) parsedMap.FileName = value;
            if (generalKeyPairs.TryGetValue("AudioLeadIn", out value)) parsedMap.AudioLeadIn = Convert.ToInt32(value);
            if (generalKeyPairs.TryGetValue("PreviewTime", out value)) parsedMap.PreviewTime = Convert.ToInt32(value);
            if (generalKeyPairs.TryGetValue("CountDown", out value)) parsedMap.Countdown = Convert.ToBoolean(value);
            if (generalKeyPairs.TryGetValue("Mode", out value)) parsedMap.Mode = Convert.ToInt32(value);
            //Metadata section
            if (metadataKeyPairs.TryGetValue("Title", out value)) parsedMap.Title = value;
            if (metadataKeyPairs.TryGetValue("TitleUnicode", out value)) parsedMap.TitleUnicode = value;
            if (metadataKeyPairs.TryGetValue("Artist", out value)) parsedMap.Artist = value;
            if (metadataKeyPairs.TryGetValue("ArtistUnicode", out value)) parsedMap.ArtistUnicode = value;
            if (metadataKeyPairs.TryGetValue("Creator", out value)) parsedMap.Creator = value;
            if (metadataKeyPairs.TryGetValue("Version", out value)) parsedMap.Version = value;
            if (metadataKeyPairs.TryGetValue("Source", out value)) parsedMap.Source = value;
            //Difficulty section
            if (difficultyKeyPairs.TryGetValue("HPDrainRate", out value)) parsedMap.HPDrainRate = Convert.ToSingle(value);
            if (difficultyKeyPairs.TryGetValue("OverallDifficulty", out value)) parsedMap.OverallDifficulty = Convert.ToSingle(value);
            //--------------------------------------------------------------
            //Conversion of the values from the raw text file
            //Subdivision of the string over sections
            string[] partialBM = rawBeatmap.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
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
            return parsedMap;
        }

        /// <summary>
        /// Parse a timing point
        /// </summary>
        /// <returns>The timing point</returns>
        /// <param name="toParse">string containing a timing point</param>
        public static TimingPoint ParseTimingPoint(string toParse)
        {
            string[] splitted = toParse.Split(new string[] {","}, StringSplitOptions.None);
            if (splitted.Length != 8)
            {
                return new TimingPoint();
            }
            else
            {
                TimingPoint toReturn = new TimingPoint();
                toReturn.Offset = Convert.ToInt32(splitted[0]);
                toReturn.MilliSecondPerBeat = Convert.ToSingle(splitted[1]);
                toReturn.Meter = Convert.ToInt32(splitted[2]);
                //inherited check
                if (Convert.ToInt32(splitted[6]) == 0)
                    toReturn.Inherithed = true;
                else
                    toReturn.Inherithed = false;
                //kiai check
                if (Convert.ToInt32(splitted[7]) == 0)
                    toReturn.Kiai = false;
                else
                    toReturn.Kiai = true;
                //return
                return toReturn;
            }
        }

        /// <summary>
        /// Parse a single note
        /// </summary>
        /// <returns>The single note</returns>
        /// <param name="toParse">string containing a single note</param>
        public static Note ParseSingleNote(string toParse)
        {
            string[] splitted = toParse.Split(new string[] { ","}, StringSplitOptions.None);
            if (splitted.Length != 6 || splitted[3] != "1")
            {
                return new Note();
            }
            else
            {
                //check if single note
                Note toReturn = new Note();
                //Detect the note column
                switch (Convert.ToInt32(splitted[0]))
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
                toReturn.Time = Convert.ToInt32(splitted[2]);
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
            else
            {
                LongNote toReturn = new LongNote();
                //Detect the note column
                switch (Convert.ToInt32(splitted[0]))
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
                toReturn.StartTime = Convert.ToInt32(splitted[2]);
                string[] endCode = splitted[5].Split(new string[] {":"}, StringSplitOptions.None);
                toReturn.EndTime = Convert.ToInt32(endCode[0]);
                return toReturn;
            }
        }
    }
}