namespace OsuLoader {
    public enum EventType
    {
        Background = 0,
        Video = 1,
        Break = 2,
        Sprite,
        Animation
    }
    
    public interface IEvent {
        public EventType GetEventType();
    }
        
    /// <summary>
    /// Holds Background Event Data
    /// </summary>
    public struct BackgroundEvent : IEvent
    {
        public EventType GetEventType() => EventType.Background;
        public int StartTime;
        public string Filename; 
        public int XOffset;
        public int YOffset;
    }
    
    public struct VideoEvent : IEvent
    {
        public EventType GetEventType() => EventType.Video;
        public int StartTime;
        public string Filename;
        public int XOffset;
        public int YOffset;
    }
        
    public struct BreakEvent : IEvent
    {
        public EventType GetEventType() => EventType.Break;
        public int StartTime;
        public int EndTime;
    }
}