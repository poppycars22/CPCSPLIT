using System;
using System.Collections.Generic;
using System.Text;

namespace CPCCore.Utilities
{
    public class CPCDebug
    {
        public static void Log(object message)
        {
           if(Config.isDebugBuild)
            {
                UnityEngine.Debug.Log(message);
            }

        }
    }
}