using System;
using System.Collections.Generic;
using System.Numerics;

namespace OsuLoader {

    [Flags]
    public enum HitObjectType {
        HitCircle  = 1,
        Slider     = 2,
        NewCombo   = 4,
        Spinner    = 8,
        ComboSkip1 = 16,
        ComboSkip2 = 32,
        ComboSkip3 = 64,
        ManiaHold  = 128
    }

    [Flags]
    public enum HitSoundType {
        Normal  = 1,
        Whistle = 2,
        Finish  = 4,
        Clap    = 8
    }

    public enum HitSoundBank {
        None   = 0,
        Normal = 1,
        Soft   = 2,
        Drum   = 3
    }

    public enum CurveType {
        Bezier,
        Centripetal,
        Linear,
        PerfectCircle
    }

    public struct HitSample {
        public HitSoundType NormalSet   { get; set; }
        public HitSoundBank AdditionSet { get; set; }
        public int          Index       { get; set; }

        private int volume;
        public int Volume {
            get => volume;
            set => volume = Math.Max(Math.Min(value, 100), 0); // Clamp between 0 and 100
        }

        public string Filename { get; set; }
    }

    public interface IHitObject {
        public HitObjectType GetHitObjType();
        public int           X            { get; set; }
        public int           Y            { get; set; }
        public int           Time         { get; set; }
        public HitSample     HitSoundData { get; set; }
        public int           ParamsCount  { get; }
    }

    public struct HitCircle : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.HitCircle;
        }
        public int       X            { get; set; }
        public int       Y            { get; set; }
        public int       Time         { get; set; }
        public HitSample HitSoundData { get; set; }
        public int       ParamsCount  => 6;
    }

    public struct Slider : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.Slider;
        }
        public int                                     X           { get; set; }
        public int                                     Y           { get; set; }
        public int                                     Time        { get; set; }
        public HitSample                               HitSoundData { get; set; }
        public CurveType                               CurveType   { get; set; }
        public List<Vector2>                           CurvePoints { get; set; }
        public int                                     Slides      { get; set; }
        public float                                   Length      { get; set; }
        public List<int>                               EdgeSounds  { get; set; }
        public List<Tuple<HitSoundType, HitSoundBank>> EdgeSets    { get; set; }
        public int                                     ParamsCount => 11;
    }

    public struct Spinner : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.Spinner;
        }
        public int       X            { get; set; }
        public int       Y            { get; set; }
        public int       Time         { get; set; }
        public HitSample HitSoundData { get; set; }
        public int       EndTime      { get; set; }
        public int       ParamsCount  => 7;
    }

    public struct ManiaHold : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.ManiaHold;
        }
        public int          X            { get; set; }
        public int          Y            { get; set; }
        public int          Time         { get; set; }
        public HitSample    HitSoundData { get; set; }
        public int          EndTime      { get; set; }
        public int          ParamsCount => 6;
    }
}