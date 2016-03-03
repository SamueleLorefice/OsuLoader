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

using System.Collections.Generic;

namespace OsuLoader
{
    public struct colour
    {
        public int Red, Green, Blue;
        public colour(int r, int g, int b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }
    }
    /// <summary>
    /// Represent all the data from a .osu file (use osu loader to initzialize this object.)
    /// </summary>
    public class BeatMap
    {
        #region General

        /// <summary>
        /// The name of the file mp3.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The time before the actual track start to play. (in milliseconds)
        /// </summary>
        public int AudioLeadIn { get; set; }

        /// <summary>
        /// The preview time. (the millisecond where the track starts to play in the menù when selected)
        /// </summary>
        public int PreviewTime { get; set; }

        /// <summary>
        /// Specifies if there is a count down at the beginning of the level.
        /// </summary>
        public bool Countdown { get; set; }

        /// <summary>
        /// Specifies what is the default sampleset used.
        /// </summary>
        public string SampleSet { get; set; }

        /// <summary>
        /// Specifies how often closely placed hit objects will be stacked together.
        /// </summary>
        public float StackLeniency { get; set; }

        /// <summary>
        /// The game mode this level is mapped to. 0 is Osu!, 1 Taiko, 2 CTB, 3 Mania.
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// Specifies if the screen should use letterbox while in breaks.
        /// </summary>
        public bool LetterBoxInBreaks { get; set; }

        /// <summary>
        /// specifies whether or not the storyboard should be widescreen.
        /// </summary>
        public bool WideScreenStoryboard { get; set; }

        #endregion

        #region Editor

        //coming soon...

        #endregion

        #region Metadata

        /// <summary>
        /// Romanised title of the track
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Unicode Title (Original title as referred in the beatmap editor)
        /// </summary>
        public string TitleUnicode { get; set; }

        /// <summary>
        /// Romanised artist name
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Unicode artist title (Original artist name as referred in the beatmap editor)
        /// </summary>
        public string ArtistUnicode { get; set; }

        /// <summary>
        /// Username of the mapper
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Difficulty name
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Source of the BeatMap
        /// </summary>
        public string Source { get; set; }

        ///<summary>
        /// Collection of tags separated by spaces.
        /// </summary>
        public string Tags { get; set; }

        ///<summary>
        /// ID of the single beatmap.
        /// </summary>
        public int BeatmapID { get; set; }

        ///<summary>
        /// ID of the beatmap set.
        /// </summary>
        public int BeatmapSetID { get; set; }

        #endregion

        #region Difficulty

        /// <summary>
        /// HP drain rate of the difficulty
        /// </summary>
        public float HPDrainRate { get; set; }

        ///<summary>
        /// Size of the hitobjects (CS)
        /// </summary>
        public float CircleSize { get; set; }

        /// <summary>
        /// Size of the hit-window of this beatmap (OD)
        /// </summary>
        public float OverallDifficulty { get; set; }

        /// <summary>
        /// Specifies the amount of time taken for the approach circle and hit object to appear. (AR)
        /// </summary>
        public float ApproachRate { get; set; }

        /// <summary>
        /// Specifies a multiplier for the slider velocity. Default value is 1.4 .
        /// </summary>
        public float SliderMultiplier { get; set; }

        /// <summary>
        /// Specifies how often slider ticks appear. Default value is 1.
        /// </summary>
        public float SliderTickRate { get; set; }

        #endregion

        #region Events

        //Coming soon...

        #endregion

        #region Timing Points

        /// <summary>
        /// List of all timing points
        /// </summary>
        public List<TimingPoint> TimingPoints { get; set; }

        #endregion

        #region Colours

        public List<colour> Colours { get; set; }

        #endregion

        #region Hit Objects

        /// <summary>
        /// List of the single notes (non-hold notes)
        /// </summary>
        public List<Note> SingleNotes { get; set; }

        /// <summary>
        /// List of hold notes (sliders)
        /// </summary>
        public List<LongNote> LongNotes { get; set; }

        #endregion
    }

}