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

namespace OsuLoader {
	/// <summary>
	/// Represent the data for a hold note (slider)
	/// </summary>
	public class LongNote {
		/// <summary>
		/// Column where this note is located
		/// </summary>
		public int Column { get; set; }

		/// <summary>
		/// Time (in milliseconds) where this slider begins to be pressed
		/// </summary>
		public int StartTime { get; set; }

		/// <summary>
		/// Time (in milliseconds) where this slider gets released
		/// </summary>
		/// <value>The end time.</value>
		public int EndTime { get; set; }
	}
}