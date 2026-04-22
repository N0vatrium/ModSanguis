namespace ModSanguis
{
    internal class Helpers
    {
        public static void WriteLog(object log, bool warning = false, bool error = false)
        {
            var level = BepInEx.Logging.LogLevel.Message;
            if (warning)
            {
                level = BepInEx.Logging.LogLevel.Warning;
            }

            if (error)
            {
                level = BepInEx.Logging.LogLevel.Error;
            }

            Plugin.Logger.Log(level, log);
        }
    }
}
