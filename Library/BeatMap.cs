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
using System.Linq;
using System.Reflection;

namespace OsuLoader {
    /// <summary>
    /// Represent all the data from a .osu file (use osu loader to initialize this object.)
    /// </summary>
    public class BeatMap : IEquatable<BeatMap> {

        /// <summary>
        /// fileVersion of the .osu file.
        /// </summary>
        public string OsuVersion { get; set; } = "v14";

        #region General

        /// <summary>
        /// The name of the file mp3.
        /// </summary>
        public string AudioFilename { get; set; }

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
        public bool LetterboxInBreaks { get; set; } = false;

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
        public bool WidescreenStoryboard { get; set; }

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
        public List<IHitObject> HitObjects { get; set; } = new List<IHitObject>();

        #endregion

        public bool Equals(BeatMap other) {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            else
                return string.Equals(OsuVersion,    other.OsuVersion,    StringComparison.Ordinal)   &&
                       string.Equals(AudioFilename, other.AudioFilename, StringComparison.Ordinal)   &&
                       AudioLeadIn == other.AudioLeadIn                                              &&
                       string.Equals(AudioHash, other.AudioHash, StringComparison.Ordinal)           &&
                       PreviewTime == other.PreviewTime                                              &&
                       Countdown   == other.Countdown                                                &&
                       string.Equals(SampleSet, other.SampleSet, StringComparison.Ordinal)           &&
                       StackLeniency.Equals(other.StackLeniency)                                     &&
                       Mode                == other.Mode                                             &&
                       LetterboxInBreaks   == other.LetterboxInBreaks                                &&
                       StoryFireInFront    == other.StoryFireInFront                                 &&
                       UseSkinSprites      == other.UseSkinSprites                                   &&
                       AlwaysShowPlayfield == other.AlwaysShowPlayfield                              &&
                       OverlayPosition     == other.OverlayPosition                                  &&
                       string.Equals(SkinPreference, other.SkinPreference, StringComparison.Ordinal) &&
                       EpilepsyWarning          == other.EpilepsyWarning                             &&
                       CountdownOffset          == other.CountdownOffset                             &&
                       SpecialStyle             == other.SpecialStyle                                &&
                       WidescreenStoryboard     == other.WidescreenStoryboard                        &&
                       SamplesMatchPlaybackRate == other.SamplesMatchPlaybackRate                    &&
                       DistanceSpacing.Equals(other.DistanceSpacing)                                 &&
                       BeatDivisor == other.BeatDivisor                                              &&
                       GridSize    == other.GridSize                                                 &&
                       TimelineZoom.Equals(other.TimelineZoom)                                       &&
                       string.Equals(Title,         other.Title,         StringComparison.Ordinal)   &&
                       string.Equals(TitleUnicode,  other.TitleUnicode,  StringComparison.Ordinal)   &&
                       string.Equals(Artist,        other.Artist,        StringComparison.Ordinal)   &&
                       string.Equals(ArtistUnicode, other.ArtistUnicode, StringComparison.Ordinal)   &&
                       string.Equals(Creator,       other.Creator,       StringComparison.Ordinal)   &&
                       string.Equals(Version,       other.Version,       StringComparison.Ordinal)   &&
                       string.Equals(Source,        other.Source,        StringComparison.Ordinal)   &&
                       Tags.All(other.Tags.Contains)                                                 &&
                       Tags.Count   == other.Tags.Count                                              &&
                       BeatmapId    == other.BeatmapId                                               &&
                       BeatmapSetId == other.BeatmapSetId                                            &&
                       HpDrainRate.Equals(other.HpDrainRate)                                         &&
                       CircleSize.Equals(other.CircleSize)                                           &&
                       OverallDifficulty.Equals(other.OverallDifficulty)                             &&
                       ApproachRate.Equals(other.ApproachRate)                                       &&
                       SliderMultiplier.Equals(other.SliderMultiplier)                               &&
                       SliderTickRate.Equals(other.SliderTickRate)                                   &&
                       Events.All(other.Events.Contains)                                             && Events.Count       == other.Events.Count       &&
                       TimingPoints.All(other.TimingPoints.Contains)                                 && TimingPoints.Count == other.TimingPoints.Count &&
                       Colours.All(other.Colours.Contains)                                           && Colours.Count      == other.Colours.Count      &&
                       HitObjects.All(other.HitObjects.Contains)                                     && HitObjects.Count   == other.HitObjects.Count;
        }

        public override bool Equals(object obj) {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != typeof(BeatMap))
                return false;
            return Equals((BeatMap)obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = (OsuVersion != null ? StringComparer.InvariantCulture.GetHashCode(OsuVersion) : 0);
                hashCode = (hashCode * 397) ^ (AudioFilename != null ? StringComparer.InvariantCulture.GetHashCode(AudioFilename) : 0);
                hashCode = (hashCode * 397) ^ AudioLeadIn;
                hashCode = (hashCode * 397) ^ (AudioHash != null ? StringComparer.InvariantCulture.GetHashCode(AudioHash) : 0);
                hashCode = (hashCode * 397) ^ PreviewTime;
                hashCode = (hashCode * 397) ^ (int)Countdown;
                hashCode = (hashCode * 397) ^ (SampleSet != null ? StringComparer.InvariantCulture.GetHashCode(SampleSet) : 0);
                hashCode = (hashCode * 397) ^ StackLeniency.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Mode;
                hashCode = (hashCode * 397) ^ LetterboxInBreaks.GetHashCode();
                hashCode = (hashCode * 397) ^ StoryFireInFront.GetHashCode();
                hashCode = (hashCode * 397) ^ UseSkinSprites.GetHashCode();
                hashCode = (hashCode * 397) ^ AlwaysShowPlayfield.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)OverlayPosition;
                hashCode = (hashCode * 397) ^ (SkinPreference != null ? StringComparer.InvariantCulture.GetHashCode(SkinPreference) : 0);
                hashCode = (hashCode * 397) ^ EpilepsyWarning.GetHashCode();
                hashCode = (hashCode * 397) ^ CountdownOffset;
                hashCode = (hashCode * 397) ^ SpecialStyle.GetHashCode();
                hashCode = (hashCode * 397) ^ WidescreenStoryboard.GetHashCode();
                hashCode = (hashCode * 397) ^ SamplesMatchPlaybackRate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Bookmarks != null ? Bookmarks.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DistanceSpacing.GetHashCode();
                hashCode = (hashCode * 397) ^ BeatDivisor;
                hashCode = (hashCode * 397) ^ GridSize;
                hashCode = (hashCode * 397) ^ TimelineZoom.GetHashCode();
                hashCode = (hashCode * 397) ^ (Title         != null ? StringComparer.InvariantCulture.GetHashCode(Title) : 0);
                hashCode = (hashCode * 397) ^ (TitleUnicode  != null ? StringComparer.InvariantCulture.GetHashCode(TitleUnicode) : 0);
                hashCode = (hashCode * 397) ^ (Artist        != null ? StringComparer.InvariantCulture.GetHashCode(Artist) : 0);
                hashCode = (hashCode * 397) ^ (ArtistUnicode != null ? StringComparer.InvariantCulture.GetHashCode(ArtistUnicode) : 0);
                hashCode = (hashCode * 397) ^ (Creator       != null ? StringComparer.InvariantCulture.GetHashCode(Creator) : 0);
                hashCode = (hashCode * 397) ^ (Version       != null ? StringComparer.InvariantCulture.GetHashCode(Version) : 0);
                hashCode = (hashCode * 397) ^ (Source        != null ? StringComparer.InvariantCulture.GetHashCode(Source) : 0);
                hashCode = (hashCode * 397) ^ (Tags          != null ? Tags.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ BeatmapId;
                hashCode = (hashCode * 397) ^ BeatmapSetId;
                hashCode = (hashCode * 397) ^ HpDrainRate.GetHashCode();
                hashCode = (hashCode * 397) ^ CircleSize.GetHashCode();
                hashCode = (hashCode * 397) ^ OverallDifficulty.GetHashCode();
                hashCode = (hashCode * 397) ^ ApproachRate.GetHashCode();
                hashCode = (hashCode * 397) ^ SliderMultiplier.GetHashCode();
                hashCode = (hashCode * 397) ^ SliderTickRate.GetHashCode();
                hashCode = (hashCode * 397) ^ (Events       != null ? Events.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TimingPoints != null ? TimingPoints.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Colours      != null ? Colours.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (HitObjects   != null ? HitObjects.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(BeatMap left, BeatMap right) {
            return Equals(left, right);
        }

        public static bool operator !=(BeatMap left, BeatMap right) {
            return !Equals(left, right);
        }
    }

    public static class BeatMapExtensions {
        internal static readonly string[] GeneralSectionProps = new string[] {
            "AudioFilename",
            "AudioLeadIn",
            "AudioHash",
            "PreviewTime",
            "Countdown",
            "SampleSet",
            "StackLeniency",
            "Mode",
            "LetterboxInBreaks",
            "StoryFireInFront",
            "UseSkinSprites",
            "AlwaysShowPlayfield",
            "OverlayPosition",
            "SkinPreference",
            "EpilepsyWarning",
            "CountdownOffset",
            "SpecialStyle",
            "WidescreenStoryboard",
            "SamplesMatchPlaybackRate"
        };

        public static string GetVersionString(this BeatMap bm) {
            return $"osu file format {bm.OsuVersion}";
        }

        public static string[] GetGeneralSection(this BeatMap bm) {
            List<string> sectionSrt = new List<string>() { "[General]" };
            EmitIniKeyValues(GeneralSectionProps, ref sectionSrt, bm);
            return sectionSrt.ToArray();
        }

        private static void EmitIniKeyValues(string[] props, ref List<string> section, BeatMap bm) {
            foreach (string prop in props) {
                PropertyInfo pi    = typeof(BeatMap).GetProperty(prop);
                object       value = pi!.GetValue(bm);
                if (value != null) {
                    if (value is bool b)
                        section.Add($"{prop}: {(b ? 1 : 0)}");
                    else if (value is OverlayPosition)
                        section.Add($"{prop}: {((OverlayPosition)value).ToIniString()}");
                    else if (value is GameMode)
                        section.Add($"{prop}: {((GameMode)value).ToIniString()}");
                    else if (value is CountdownType)
                        section.Add($"{prop}: {((CountdownType)value).ToIniString()}");
                    else if (value is TimingEffect)
                        section.Add($"{prop}: {((TimingEffect)value).ToIniString()}");
                    else if (value is float f)
                        section.Add($"{prop}: {f:0.##}");
                    else
                        section.Add($"{prop}: {value}");
                }
            }
        }
    }
}