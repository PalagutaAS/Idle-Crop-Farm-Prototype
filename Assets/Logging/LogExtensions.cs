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
            
            DebugLogService.Instance.AppendLog(formatted);
            Debug.Log(message);
        }
    
        private static string FormatMessage(string className, string methodName, int line, string msg, LogType type)
        {
            string color = LogHandler.GetColor(className);
            string lineString = (line == 0) ? "" : $":{line}" ;
            string prefix = $"<color=#{color}><b>[{className}.{methodName}{lineString}]</b></color> ";
            return type == LogType.Error ? $"<color=red>{prefix}{msg}</color>" :
                type == LogType.Warning ? $"<color=yellow>{prefix}{msg}</color>" :
                prefix + msg;
        }
    }
}