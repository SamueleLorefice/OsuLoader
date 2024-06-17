using System;

namespace OsuLoader {
    public enum OverlayPosition
    {
        NoChange = 0,
        Below = 1,
        Above = 2
    }
    
    public enum GameMode
    {
        Osu = 0,
        Taiko = 1,
        CatchTheBeat = 2,
        Mania = 3
    }
    
    public enum CountdownType
    {
        None = 0,
        Normal = 1,
        Half = 2,
        Double = 3
    }
    
    [Flags]
    public enum TimingEffect
    {
        None = 0,
        Kiai = 1,
        OmitFirstBarLine = 2 
    }
}