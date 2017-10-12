using Entities;
using log4net;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Task2Controller : Task1Controller
    {
        public enum LogLevel { llFatal, llError, llWarn, llInfo, llDebug };

        public delegate void LogString(string logMessage, LogLevel logLevel);
        public LogString logString { get; set; }

        public enum LogMode { lmConsole, lmFileLogger };

        public bool MuteLog { get; set; }

        private LogMode _logMode;

        public LogMode logMode
        {
            get { return _logMode; }
            set
            {
                if (value == LogMode.lmConsole)
                {
                    logString = LogToConsole;
                }
                else
                {
                    logString = LogToFileNet;
                }
                _logMode = value;
            }
        }

        public Task2Controller()
        {
            MuteLog = false;
        }

        //Declare an instance for log4net
        private static readonly ILog _Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private void LogToConsole(string logMessage, LogLevel logLevel)
        {
            if (MuteLog) return;

            if (logLevel == LogLevel.llFatal || logLevel == LogLevel.llError)
                Console.Beep();
            Console.WriteLine(logMessage);
        }

        private void LogToFileNet(string logMessage, LogLevel logLevel)
        {
            if (MuteLog) return;

            switch (logLevel)
            {
                case LogLevel.llFatal:
                    _Logger.Fatal(logMessage);
                    break;

                case LogLevel.llError:
                    _Logger.Error(logMessage);
                    break;

                case LogLevel.llWarn:
                    _Logger.Warn(logMessage);
                    break;

                case LogLevel.llInfo:
                    _Logger.Info(logMessage);
                    break;

                case LogLevel.llDebug:
                    _Logger.Debug(logMessage);
                    break;
            }
        }

        public void ShowProductList(List<Product> productList)

        {
            if (productList == null)
                throw new ArgumentException("FATAL: Undefined argument!");

            if (logString == null)
                throw new Exception("FATAL: Undefined log method!");

            foreach (Product cp in productList)
                logString(string.Format("Code: {0} \t ID: {1} \t Name: {2} \t Price: {3} \t GroupID {4} ",
                cp.Code, cp.ID, cp.Name, cp.Price, cp.GroupID), LogLevel.llInfo);
        }
    }

}