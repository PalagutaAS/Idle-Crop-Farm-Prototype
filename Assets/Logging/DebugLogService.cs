using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace Logging
{
    public class DebugLogService : IDebugLogService, IDisposable, ILogHandler
    {
        private readonly StringBuilder _logBuilder = new StringBuilder();
        private readonly ILogHandler _originalHandler;
        
        public event Action OnDrawDebug;
        public string LogText => _logBuilder.ToString();

        public DebugLogService()
        {
            _originalHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = this;
        }
        
        public void Dispose()
        {
            OnDrawDebug = null;
        }

        public void Clear()
        {
            _logBuilder.Clear();
        }

        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            var stackTrace = new StackTrace(4, true);
            var frame = stackTrace.GetFrame(0);

            string callerInfo = "";
            if (frame != null)
            {
                string className = frame.GetMethod()?.DeclaringType?.Name ?? "Unknown";
                string methodName = frame.GetMethod()?.Name ?? "?";
                int lineNumber = frame.GetFileLineNumber();
                var color = LogHandler.GetColor(className);

                callerInfo = $"<color=#{color}><b>[{className}.{methodName}:{lineNumber}]</b></color>";
            }
            
            string message = string.Format(format, args);
            switch (logType)
            {
                case LogType.Error:
                    message = $"<color=red>{message}</color>";
                    if (!string.IsNullOrEmpty(stackTrace.ToString()))
                        message += $"\n<color=#FF8888>{stackTrace}</color>";
                    break;
                case LogType.Warning:
                    message = $"<color=yellow>{message}</color>";
                    break;
                default:
                    message = string.Format(format, args);
                    break;
            }
            
            string finalMessage = callerInfo + message;

            _originalHandler.LogFormat(logType, context, "{0}", finalMessage);

            _logBuilder.AppendLine(finalMessage);
            OnDrawDebug?.Invoke();
        }
        
        public void LogException(Exception exception, Object context)
        {
            _originalHandler.LogException(exception, context);
        }
    }

    public interface IDebugLogService
    {
        public event Action OnDrawDebug;
        public string LogText { get; }
        public void Clear();
    }
}