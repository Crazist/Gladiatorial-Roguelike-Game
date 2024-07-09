using System;
using UI;
using UI.Type;

namespace Data
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}