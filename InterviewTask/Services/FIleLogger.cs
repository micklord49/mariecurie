using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InterviewTask.Services
{
    public class FileLogger : ILogger
    {
        private string _logFile;

        public FileLogger(string logFile)
        {
            _logFile = logFile;
        }

        public LoggerVerbosity Verbosity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Parameters { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Initialize(IEventSource eventSource)
        {
            throw new NotImplementedException();
        }

        public void Log(string message)
        {
            File.AppendAllText(_logFile, $"{DateTime.Now.ToString()} - {message}\n");
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}