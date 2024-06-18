using System;
using System.Collections.Generic;
using System.Numerics;

namespace OsuLoader {

    public enum HitObjectType {
        HitCircle  = 0,
        Slider     = 1,
        NewCombo   = 2,
        Spinner    = 3,
        ComboSkip1 = 4,
        ComboSkip2 = 5,
        ComboSkip3 = 6,
        ManiaHold  = 7
    }

    public enum HitSoundType {
        Normal  = 0,
        Whistle = 1,
        Finish  = 2,
        Clap    = 3
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

        public int Volume {
            get => Volume;
            set => Volume = Math.Max(Math.Min(value, 100), 0); // Clamp between 0 and 100
        }

        public string Filename { get; set; }
    }

    public interface IHitObject {
        public HitObjectType GetHitObjType();
    }

    public struct HitCircle : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.HitCircle;
        }
        public int          X            { get; set; }
        public int          Y            { get; set; }
        public int          Time         { get; set; }
        public HitSoundType HitSound     { get; set; }
        public HitSoundBank HitSoundBank { get; set; }
    }

    public struct Slider : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.Slider;
        }
        public int                                     X           { get; set; }
        public int                                     Y           { get; set; }
        public int                                     Time        { get; set; }
        public HitSoundType                            HitSound    { get; set; }
        public CurveType                               CurveType   { get; set; }
        public List<Vector2>                           CurvePoints { get; set; }
        public int                                     Slides      { get; set; }
        public float                                   Length      { get; set; }
        public List<int>                               EdgeSounds  { get; set; }
        public List<Tuple<HitSoundType, HitSoundBank>> EdgeSets    { get; set; }
    }

    public struct Spinner : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.Spinner;
        }
        public int          X         { get; set; }
        public int          Y         { get; set; }
        public int          Time      { get; set; }
        public HitSoundType HitSound  { get; set; }
        public int          EndTime   { get; set; }
        public HitSoundBank HitSample { get; set; }
    }

    public struct ManiaHold : IHitObject {
        public HitObjectType GetHitObjType() {
            return HitObjectType.ManiaHold;
        }
        public int          X         { get; set; }
        public int          Y         { get; set; }
        public int          Time      { get; set; }
        public HitSoundType HitSound  { get; set; }
        public int          EndTime   { get; set; }
        public HitSoundBank HitSample { get; set; }
    }
}