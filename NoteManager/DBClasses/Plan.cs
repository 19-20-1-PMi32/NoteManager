using System;

namespace NoteManager.DBClasses
{
    public class Plan
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime DeadLineTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
