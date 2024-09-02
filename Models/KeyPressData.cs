using System;
using System.Collections.Generic;

namespace ServerApi
{
    public class KeyPressData
    {
        public string KeyCode { get; set; }
        public List<string> PressTimes { get; set; }
        public List<string> ReleaseTimes { get; set; }
    }

    public class KeyPressDataWrapper
    {
        public List<KeyPressData> KeyPressDataList { get; set; }
    }
}
