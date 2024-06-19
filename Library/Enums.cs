using System;

namespace OsuLoader {
    public enum OverlayPosition {
        NoChange = 0,
        Below    = 1,
        Above    = 2
    }

    public enum GameMode {
        Osu          = 0,
        Taiko        = 1,
        CatchTheBeat = 2,
        Mania        = 3
    }

    public enum CountdownType {
        None   = 0,
        Normal = 1,
        Half   = 2,
        Double = 3
    }

    [Flags]
    public enum TimingEffect {
        None             = 0,
        Kiai             = 1,
        OmitFirstBarLine = 2
    }
    
    public static class EnumExtension
    {
        public static string ToIniString(this OverlayPosition overlayPosition)
            => overlayPosition switch
            {
                OverlayPosition.NoChange => "0",
                OverlayPosition.Below    => "1",
                OverlayPosition.Above    => "2",
                _                        => throw new ArgumentOutOfRangeException(nameof(overlayPosition), overlayPosition, null)
            };
        
        public static string ToIniString(this GameMode gameMode)
            => gameMode switch
            {
                GameMode.Osu          => "0",
                GameMode.Taiko        => "1",
                GameMode.CatchTheBeat => "2",
                GameMode.Mania        => "3",
                _                     => throw new ArgumentOutOfRangeException(nameof(gameMode), gameMode, null)
            };
        
        public static string ToIniString(this CountdownType countdownType)
            => countdownType switch
            {
                CountdownType.None   => "0",
                CountdownType.Normal => "1",
                CountdownType.Half   => "2",
                CountdownType.Double => "3",
                _                    => throw new ArgumentOutOfRangeException(nameof(countdownType), countdownType, null)
            };

        public static string ToIniString(this TimingEffect timingEffect)
            => ((byte)timingEffect).ToString();
    }
}