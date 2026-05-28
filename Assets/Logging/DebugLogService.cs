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
            this.Log("DebugLogService Constructor");
        }
        
        public void Dispose()
        {
            Debug.unityLogger.logHandler = _originalHandler;
            OnDrawDebug = null;
        }

        public void Clear()
        {
            _logBuilder.Clear();
        }
        
        private void AppendLog(string finalMessage)
        {
            _logBuilder.AppendLine(finalMessage);
            OnDrawDebug?.Invoke();
        }
        
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            string message = string.Format(format, args);
            
            var stackTrace = new StackTrace(4, true);
            
            switch (logType)
            {
                case LogType.Error:
                    if (!string.IsNullOrEmpty(stackTrace.ToString()))
                        message += $"\n<color=#FF8888>{stackTrace}</color>";
                    break;
            }
            
            AppendLog(message);
            _originalHandler.LogFormat(logType, context, "{0}", message);
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