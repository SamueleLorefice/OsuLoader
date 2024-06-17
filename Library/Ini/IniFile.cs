﻿//OsuLoader - A small library to parse and manage .osu files
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
using System.Linq;
using System.Runtime.InteropServices;

namespace Ini {
	class IniFile {
		/// <summary>
		/// Create a New INI file object to store or load data
		/// </summary>

		public string path;

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileSectionW",
			SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int GetPrivateProfileSection(string lpAppName, string lpReturnedString, int nSize,
		                                                         string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileSectionNamesW",
			SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int GetPrivateProfileSectionNames(string lpReturnedString, int nSize, string lpFileName);

		[DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW",
			SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault,
		                                                        string lpReturnString, int nSize, string lpFilename);

		[DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW",
			SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString,
		                                                          string lpFilename);

		[DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileSectionW",
			SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		private static extern int WritePrivateProfileSection(string lpAppName, string lpString, string lpFilename);

		/// <summary>
		/// INIFile Constructor.
		/// </summary>
		/// <PARAM name="INIPath"></PARAM>
		public IniFile(string INIPath) {
			path = INIPath;
		}

		/// <summary>
		/// Write Data to the INI File
		/// </summary>
		/// <PARAM name="Section"></PARAM>
		/// Section name
		/// <PARAM name="Key"></PARAM>
		/// Key Name
		/// <PARAM name="Value"></PARAM>
		/// Value Name
		public void WriteValue(string Section, string Key, string Value) {
			WritePrivateProfileString(Section, Key, Value, this.path);
		}

		/// <summary>
		/// Works as WriteAllSection("TEST","Key=Val\nKey1=val\nKey2=val");
		/// </summary>
		public void WriteAllSection(string section, string keyvalues) {

			//TODO: check size
			//65,535
			//WriteAllSection("CacheExceptionsSection","Key=Val\nKey1=val\nKey2=val");
			WritePrivateProfileSection(section, keyvalues, path);
		}

		/// <summary>
		/// Read Data Value From the Ini File
		/// </summary>
		/// <PARAM name="Section"></PARAM>
		/// <PARAM name="Key"></PARAM>
		/// <PARAM name="Path"></PARAM>
		/// <returns></returns>
		public string ReadValue(string section, string Key) {
			string returnString = new string(' ', 1024);
			GetPrivateProfileString(section, Key, null, returnString, 1024, this.path);
			return returnString;
		}

		/// <summary>
		/// Returns All Key,Values of a Section as Dictionary
		/// </summary>
		public Dictionary<string, string> GetKeyValuesPair(string section) {
			List<string> tlist = GetKeyValues(section);
			Dictionary<string, string> tdic = new Dictionary<string, string>();
			if (tlist == null || !tlist.Any())
				return tdic;
			foreach (var str in tlist) {
				if (string.IsNullOrEmpty(str))
					continue;
				var vl = str.Split(':');
				tdic.Add(vl[0], vl[1]);
			}
			return tdic;
		}

		/// <summary>
		/// Returns All Key,Values of a Section as List -see GetKeyValuesPair for seperated values as Dictionary
		/// </summary>
		public List<string> GetKeyValues(string section) {
			string returnString = new string(' ', 65536);
			GetPrivateProfileSection(section, returnString, 65536, this.path);
			List<string> result = new List<string>(returnString.Split('\0'));
			result.RemoveRange(result.Count - 2, 2);
			return result;
		}

		/// <summary>
		/// Returns All Section Names using method A
		/// </summary>
		public List<string> GetSectionsA() {
			string returnString = new string(' ', 65536);
			GetPrivateProfileSectionNames(returnString, 65536, this.path);
			List<string> result = new List<string>(returnString.Split('\0'));
			result.RemoveRange(result.Count - 2, 2);
			return result;
		}

		/// <summary>
		/// Returns All Section Names using method B - Same Output as A
		/// </summary>
		public List<string> GetSectionsB() {
			string returnString = new string(' ', 65536);
			GetPrivateProfileString(null, null, null, returnString, 65536, this.path);
			List<string> result = new List<string>(returnString.Split('\0'));
			result.RemoveRange(result.Count - 2, 2);
			return result;
		}

		/// <summary>
		/// Returns Only Keys from a Given Section
		/// </summary>
		public List<string> GetKeysOnly(string section) {
			string returnString = new string(' ', 32768);
			GetPrivateProfileString(section, null, null, returnString, 32768, this.path);
			List<string> result = new List<string>(returnString.Split('\0'));
			result.RemoveRange(result.Count - 2, 2);
			return result;
		}
	}
}