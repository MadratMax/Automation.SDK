namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.IO;
    using System.Reflection;

    public class PrintUtils
    {
        static string callClass = Assembly.GetCallingAssembly().GetName().Name + ".log";
        static string appPath = FileUtils.getAppPath();
        static string logFile = Path.Combine(appPath, callClass);

        public static void WriteOutput(String line)
        {
            string message = string.Concat(DateTime.Now.ToString(), " ", line, "\r\n");
            Console.Write(message);
            File.AppendAllText(logFile, message);
        }

        public static void WriteError(String line)
        {
            string message = string.Concat(DateTime.Now.ToString(), " Error:: ", line, "\r\n");
            Console.Write(message);
            File.AppendAllText(logFile, message);
        }

        public static void PrintException(String action, String name, Exception ex)
        {
            Exception tempEx = ex;
            string msg = tempEx.Message;
            while (tempEx != null)
            {
                msg = msg + "\n\t" + tempEx.Message;
                tempEx = tempEx.InnerException;
            }

            string message = string.Format("Exception {0} {1}: {2}", action, name, msg) + "\r\n";
            WriteError(message);
            WriteError(ex.StackTrace);
            File.AppendAllText(logFile, message);
        }
    }
}
