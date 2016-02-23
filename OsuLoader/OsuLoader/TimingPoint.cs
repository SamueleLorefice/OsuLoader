namespace OsuLoader
{
    /// <summary>
    /// Timing point
    /// </summary>
    public struct TimingPoint
    {
        /// <summary>
        /// Offset of the track timing (if inherithed is the distance from the last non inherithed timing point)
        /// </summary>
        int Offset{get; set;}
        /// <summary>
        /// Time for each beat
        /// </summary>
        float MilliSecondPerBeat{get; set;}
        /// <summary>
        /// Meter
        /// </summary>
        int meter{get; set;}
        /// <summary>
        /// Indicate whether this <see cref="OsuLoader.TimingPoint"/> is inherithed.
        /// </summary>
        /// <value><c>true</c> if inherithed; otherwise, <c>false</c>.</value>
        bool inherithed{get; set;}
        /// <summary>
        /// Indicate whether this <see cref="OsuLoader.TimingPoint"/> is under kiai.
        /// </summary>
        /// <value><c>true</c> if kiai; otherwise, <c>false</c>.</value>
        bool kiai{get; set;}
    }
}