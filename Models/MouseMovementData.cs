using System;

namespace ServerApi
{
    public class MouseMovementData
    {
        public string Timestamp { get; set; }
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }
        public float DeltaZ{ get; set; }
    }

    public class MouseMovementDataWrapper
    {
        public List<MouseMovementData> MouseMovementDataList { get; set; }
    }
}
