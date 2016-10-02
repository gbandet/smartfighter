using System;
using System.IO;


namespace SmartFighter.Hooks
{
    class Logger
    {
        public static void log(String msg)
        {
            File.AppendAllText(
                Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Hooks.log"),
                msg + "\n");
        }
    }
}
