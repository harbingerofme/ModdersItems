using BepInEx.Logging;

namespace ModdersItems
{
    internal class MILog
    {
        internal static ManualLogSource logger = null;

        public MILog(ManualLogSource log)
        {
            logger = log;
        }

        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            logger.Log(level, data);
        }
        public static void LogMessage(object data)
        {
            logger.LogMessage(data);
        }
        public static void LogDebug(object data)
        {
            logger.LogDebug(data);
        }
        public static void LogWarning(object data)
        {
            logger.LogWarning(data);
        }
        public static void LogError(object data)
        {
            logger.LogError(data);
        }
        public static void LogInfo(object data)
        {
            logger.LogInfo(data);
        }
        public static void LogFatal(object data)
        {
            logger.LogFatal(data);
        }
    }
}
