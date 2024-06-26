# Changelog

All notable changes to this project will be documented in this file.

## [unreleased]

### 🚀 Features

- Added .idea config files
- Added git cliff config
- Added new stub methods for loading beatmaps.
- Added Countdown type enum
- Added GameMode type enum
- Added OverlayPosition type enum
- Added IEvent interface, EventType enum and Background, Video and Break Event support.
- Added default values for all field that have a known default value
- Added all the missing fields in the osu file specification. Including deprecated values for maximum compatibility.
- Added support for slider color overrides in Color section
- Tags are now read as a list rather than a single string.
- Added support for all timing effects rather than just Kiai time.
- [**breaking**] Added HitObjects Supporting data structures.
- [**breaking**] Removed SingleNotes and LongNotes
- Added Osu Map File Version property to the BeatMap class
- [**breaking**] IHitObject now implements all of the shared properties.
- [**breaking**] Hit sound data has been grouped into HitSample struct, containing both hitSound and hitSample data.
- [**breaking**] Removed shitty old code in favor of the rewritten, faster implementation.
- Implemented custom Equals() method in BeatMap class.
- Added GetVersionString() and GetGeneralSection() Methods
- Added ToIniString() extension method to OverlayPosition, GameMode and CountdownType enums

### 🐛 Bug Fixes

- Some Colors in the colors section could have been skipped due to faulty logic
- Timing points structs properties naming has been changed to reflect actual .osu spec
- Solved a stackOverflow when setting volume property on TimingPoint struct.
- Added auto init for HitObjects list in BeatMap class.
- [**breaking**] Converted HitObjectType and HitSoundType to bitFlags for faster conversion.
- Changed logic in keypairs analysis while reading sections, trimming strings and skipping non key-value pairs
- Fixed bools not being parsed due to them being expressed in numeral form
- Adjusted strings to match the correct capitalization of osu file standard.
- Standardized more property names to conform with the file format spec
- Removed default value in OsuVersion property
- Added handling for double value in EmitIniKeysValue
- Added support for undocumented legacy values in event type
- Switched offset and MillisecondPerBeat to double as that is what they should be (contrary to what the wiki says)
- Improved the GetKeyPairs method, now correctly ignoring comments

### 🚜 Refactor

- *(project)* Removed root dir duplication
- *(project)* Updated project to .Net Framework 4.8
- *(project)* Cleaned up imports in csproj
- Updated gitignore
- Rearranged lib project files to a Library project.
- Renamed lib project to OsuLoaderLib
- Reconfigured build settings
- Standardized naming all around the main class code
- [**breaking**] Marked current load and save methods as obsolete due to them using P/Invoke API available only on windows.
- [**breaking**] Added a dependency to Microsoft.Extensions.Configuration.Ini for a more portable Ini parsing system that doesn't rely too much on WinAPI
- Moved enums to a separate file to declutter the BeatMap.cs file.
- Moved IEvent related structs to their own file.
- Moved color struct to it's own file.
- Changed overall solution code style
- Cleaned up usings in OsuLoader.cs
- Removed some empty statements in event parsing section switch case.
- [**breaking**] Removed useless dependencies.
- Changed NewMethodsTest.cs to LoaderTest.cs to reflect the new project structure.
- Upgraded project to .Net 8.0

### 📚 Documentation

- Updated README.md
- Added words to idea IDE internal dictionary
- Fixed some wrong xml docs strings in OsuLoader.cs
- Updated changelog.

### 🧪 Testing

- Added unit testing project
- Added one time set up for all other tests
- Adjusted test file with more controlled test data
- Added dummy tests for missing sections in the beatmap file.
- Renamed old test methods into OldMethodsTests.cs
- Added NewMethodsTest.cs
- Added data checking tests in BeatmapTest.cs
- Reverted unreleased language feature usage for wider compatibility in testData preparation.
- Cleaned up tests
- Added GeneralSectionIni emission test.
- Changed private methods to internal and made internal visible to test project in order to have unit testing for private methods.
- Improved testing for GetKeyPairs method
- Added generalSectionTest

<!-- generated by git-cliff -->
