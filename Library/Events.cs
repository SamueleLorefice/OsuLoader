namespace OsuLoader {
    public enum EventType {
        Background = 0,
        Video      = 1,
        Break      = 2,
        Colour     = 3,
        Sprite     = 4,
        Sample     = 5,
        Animation  = 6
    }

    public interface IEvent {
        public EventType GetEventType();
    }

    /// <summary>
    /// Holds Background Event Data
    /// </summary>
    public struct BackgroundEvent : IEvent {
        public EventType GetEventType() {
            return EventType.Background;
        }
        public int    StartTime;
        public string Filename;
        public int    XOffset;
        public int    YOffset;
    }

    public struct VideoEvent : IEvent {
        public EventType GetEventType() {
            return EventType.Video;
        }
        public int    StartTime;
        public string Filename;
        public int    XOffset;
        public int    YOffset;
    }

    public struct BreakEvent : IEvent {
        public EventType GetEventType() {
            return EventType.Break;
        }
        public int StartTime;
        public int EndTime;
    }
}