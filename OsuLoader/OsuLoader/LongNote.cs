
namespace OsuLoader
{
    /// <summary>
    /// Represent the data for a hold note (slider)
    /// </summary>
    public class LongNote
    {
        /// <summary>
        /// Column where this note is located
        /// </summary>
        public int Column{ get; set;}
        /// <summary>
        /// Time (in milliseconds) where this slider begins to be pressed
        /// </summary>
        public int StartTime{ get; set;}
        /// <summary>
        /// Time (in milliseconds) where this slider gets released
        /// </summary>
        /// <value>The end time.</value>
        public int EndTime{ get; set;}
    }
}