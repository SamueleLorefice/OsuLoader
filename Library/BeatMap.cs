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

namespace OsuLoader {
    /// <summary>
    /// Represent all the data from a .osu file (use osu loader to initialize this object.)
    /// </summary>
    public class BeatMap {

        /// <summary>
        /// fileVersion of the .osu file.
        /// </summary>
        public string OsuVersion { get; set; } = "v14";

        #region General

        /// <summary>
        /// The name of the file mp3.
        /// </summary>
        public string AudioFileName { get; set; }

        /// <summary>
        /// The time before the actual track start to play. (in milliseconds)
        /// </summary>
        public int AudioLeadIn { get; set; } = 0;

        /// <summary>
        /// deprecated, here for compatibility.
        /// </summary>
        public string AudioHash { get; set; }

        /// <summary>
        /// The preview time. (the millisecond where the track starts to play in the menù when selected)
        /// </summary>
        public int PreviewTime { get; set; } = -1;

        /// <summary>
        /// Specifies if there is a countdown at the beginning of the level.
        /// </summary>
        public CountdownType Countdown { get; set; } = CountdownType.Normal;

        /// <summary>
        /// Specifies what is the default sampleset used.
        /// </summary>
        public string SampleSet { get; set; } = "Normal";

        /// <summary>
        /// Specifies how often closely placed hit objects will be stacked together.
        /// </summary>
        public float StackLeniency { get; set; } = 0.7f;

        /// <summary>
        /// The game mode this level is mapped to. 0 is Osu!, 1 Taiko, 2 CTB, 3 Mania.
        /// </summary>
        public GameMode Mode { get; set; } = GameMode.Osu;

        /// <summary>
        /// Specifies if the screen should use letterbox while in breaks.
        /// </summary>
        public bool LetterBoxInBreaks { get; set; } = false;

        /// <summary>
        /// deprecated, here for compatibility.
        /// </summary>
        public bool StoryFireInFront { get; set; } = true;

        /// <summary>
        /// if storyboard should use userskin sprites.
        /// </summary>
        public bool UseSkinSprites { get; set; } = false;

        /// <summary>
        /// deprecated, here for compatibility.
        /// </summary>
        public bool AlwaysShowPlayfield { get; set; } = false;

        /// <summary>
        /// specifies the position of hitcircles overlay in respect to hit numbers.
        /// </summary>
        public OverlayPosition OverlayPosition { get; set; } = OverlayPosition.NoChange;

        /// <summary>
        /// Preferred skin to use during gameplay.
        /// </summary>
        public string SkinPreference { get; set; }

        /// <summary>
        /// Epilepsy warning
        /// </summary>
        public bool EpilepsyWarning { get; set; } = false;

        /// <summary>
        /// Offset in beats before the countdown starts.
        /// </summary>
        public int CountdownOffset { get; set; } = 0;

        /// <summary>
        /// If the map should use N+1 key layout in mania.
        /// </summary>
        public bool SpecialStyle { get; set; } = false;

        /// <summary>
        /// specifies whether the storyboard should be widescreen.
        /// </summary>
        public bool WideScreenStoryboard { get; set; }

        /// <summary>
        /// If the sound samples should be stretched when using song speed modifiers.
        /// </summary>
        public bool SamplesMatchPlaybackRate { get; set; } = false;

        #endregion

        #region Editor

        /// <summary>
        /// time in milliseconds of each bookmark.
        /// </summary>
        public List<int> Bookmarks { get; set; } = new List<int>();

        /// <summary>
        /// distance snap multiplier
        /// </summary>
        public float DistanceSpacing { get; set; }

        /// <summary>
        /// BeatSnap divisor
        /// </summary>
        public int BeatDivisor { get; set; }

        /// <summary>
        /// Grid size
        /// </summary>
        public int GridSize { get; set; }

        /// <summary>
        /// Scale factor for the timeline.
        /// </summary>
        public float TimelineZoom { get; set; }

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
        public List<string> Tags { get; set; } = new List<string>();

        ///<summary>
        /// ID of the difficulty.
        /// </summary>
        public int BeatmapId { get; set; }

        ///<summary>
        /// ID of the beatmap set.
        /// </summary>
        public int BeatmapSetId { get; set; }

        #endregion

        #region Difficulty

        /// <summary>
        /// HP drain rate of the difficulty
        /// </summary>
        public float HpDrainRate { get; set; }

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

        /// <summary>
        /// List of Event data structures. Use GetEventType() to determine the type of event.
        /// </summary>
        public List<IEvent> Events { get; set; } = new List<IEvent>();

        #endregion

        #region Timing Points

        /// <summary>
        /// List of all timing points
        /// </summary>
        public List<TimingPoint> TimingPoints { get; set; } = new List<TimingPoint>();

        #endregion

        #region Colours

        /// <summary>
        /// Tuple containing Colors section data.
        /// </summary>
        /// <value>string will hold Combo#/SliderTrackOverride/SliderBorder, Color will always hold an RGB value.</value>
        public List<Tuple<string, Color>> Colours { get; set; } = new List<Tuple<string, Color>>();

        #endregion

        #region Hit Objects

        /// <summary>
        /// list of all hitObjects contained in the beatmap.
        /// </summary>
        private List<IHitObject> HitObjects { get; set; }

        #endregion
    }
}