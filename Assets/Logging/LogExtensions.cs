using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Logging
{
    public static class LogExtensions
    {
        public static void Log(this object context, 
            string message, 
            LogType type = LogType.Log,
            [CallerMemberName] string memberName = null, 
            [CallerLineNumber] int lineNumber = 0)
        {
            string className = context?.GetType().Name ?? "null";
            string formatted = FormatMessage(className, memberName, lineNumber, message, type);

            switch (type)
            {
                case LogType.Log:
                    Debug.Log(formatted);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(formatted);
                    break;
                case LogType.Error:
                    Debug.LogError(formatted);
                    break;
            }
        }
    
        private static string FormatMessage(string className, string methodName, int line, string msg, LogType type)
        {
            string color = LogHandler.GetColor(className);
            string typeLog = "";
            switch (type)
            {
                case LogType.Error:
                    typeLog = $"<color=red>[ERROR]</color> ";
                    break;
                case LogType.Warning:
                    typeLog = $"<color=yellow>[Warning]</color> ";
                    break;
                    
            }
            string nameString = methodName == null ? "" : $".{methodName}";
            string lineString = (line == 0) ? "" : $":{line}" ;
            string prefix = $"{typeLog}<color=#{color}><b>[{className}{nameString}{lineString}]</b></color> ";
            return prefix + msg;
        }
    }
}