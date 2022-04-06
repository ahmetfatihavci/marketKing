using UnityEngine;

namespace Managers
{
    public enum LogType
    {
        Regular,
        Warning,
        Error
    }

    public class LogManager
    {
        public static void Log(object txt, LogType type = LogType.Regular)
        {
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
            {
                switch (type)
                {
                    case LogType.Regular:
                        Debug.Log(txt);
                        break;
                    case LogType.Error:
                        Debug.LogError(txt);
                        break;
                    case LogType.Warning:
                        Debug.Log(txt);
                        break;
                    default:
                        Debug.Log(txt);
                        break;
                }
            }
        }
    }
}