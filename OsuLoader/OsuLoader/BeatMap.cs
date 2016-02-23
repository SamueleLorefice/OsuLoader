using System.Collections.Generic;

namespace OsuLoader
{
    /// <summary>
    /// Represent all the data from a .osu file (use osu loader to initzialize this object.)
    /// </summary>
    public class BeatMap {
        #region General
        /// <summary>
        /// The name of the file mp3.
        /// </summary>
        public string FileName{ get; set;}
        /// <summary>
        /// The time before the actual track start to play. (in milliseconds)
        /// </summary>
        public int AudioLeadIn{ get; set;}
        /// <summary>
        /// The preview time. (the millisecond where the track starts to play in the menù when selected)
        /// </summary>
        public int PreviewTime{ get; set;}
        /// <summary>
        /// Specifies if there is a count down at the beginning of the level.
        /// </summary>
        public bool Countdown{ get; set;}
        /// <summary>
        /// The game mode this level is mapped to. 0 is Osu!, 1 Taiko, 2 CTB, 3 Mania
        /// </summary>
        public int Mode{ get; set;}
        #endregion

        #region Metadata
        /// <summary>
        /// Romanised title of the track
        /// </summary>
        public string Title{ get; set;}
        /// <summary>
        /// Unicode Title (Original title as referred in the beatmap editor)
        /// </summary>
        public string TitleUnicode{ get; set;}
        /// <summary>
        /// Romanised artist name
        /// </summary>
        public string Artist{ get; set;}
        /// <summary>
        /// Unicode artist title (Original artist name as referred in the beatmap editor)
        /// </summary>
        public string ArtistUnicode{ get; set;}
        /// <summary>
        /// Username of the mapper
        /// </summary>
        public string Creator{ get; set;}
        /// <summary>
        /// Difficulty name
        /// </summary>
        public string Version{ get; set;}
        /// <summary>
        /// Source of the BeatMap
        /// </summary>
        public string Source{ get; set;}
        #endregion
        
        #region Difficulty
        /// <summary>
        /// HP drain rate of the difficulty
        /// </summary>
        public float HPDrainRate{ get; set;}
        /// <summary>
        /// Overall Difficulty (OD) of the difficulty
        /// </summary>
        public float OverallDifficulty{ get; set;}
        #endregion
        
        #region Timing Points    
        /// <summary>
        /// List of all timing points
        /// </summary>
        public List<TimingPoint> TimingPoints{ get; set;}
        #endregion
        
        #region Hit Objects
        /// <summary>
        /// List of the single notes (non-hold notes)
        /// </summary>
        public List<Note> SingleNotes{ get; set;}
        /// <summary>
        /// List of hold notes (sliders)
        /// </summary>
        public List<LongNote> LongNotes{ get; set;}
        #endregion
    }
    
}