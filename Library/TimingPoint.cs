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
using System.Configuration;

namespace OsuLoader {
    /// <summary>
    /// Timing point
    /// </summary>
    public struct TimingPoint {
        /// <summary>
        /// Offset of the track timing (if inherithed is the distance from the last non inherithed timing point)
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// Time for each beat, if inherited, negative inverse slider velocity multiplier instead.
        /// </summary>
        public double MilliSecondPerBeat { get; set; }

        /// <summary>
        /// Beats per measure, ignored if inherited
        /// </summary>
        public int Meter { get; set; }

        /// <summary>
        /// Define the sample set
        /// </summary>
        public HitSoundBank SampleSet { get; set; }
        
        /// <summary>
        /// Custom sample index. 0 = default.
        /// </summary>
        public int SampleIndex { get; set; }

        private int volume;
        /// <summary>
        /// Define the samples volume
        /// </summary>
        public int Volume {
            get => volume;
            set => volume = value < 0 ? 0 : value > 100 ? 100 : value;
        }

        /// <summary>
        /// Indicate whether this TimingPoint is inherithed.
        /// </summary>
        public bool Uninherited { get; set; }

        /// <summary>
        /// Indicate whether this TimingPoint is under kiai.
        /// </summary>
        public TimingEffect Effects { get; set; }
    }
}