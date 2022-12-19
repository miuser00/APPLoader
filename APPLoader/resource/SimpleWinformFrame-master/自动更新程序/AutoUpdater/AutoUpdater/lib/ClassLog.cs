using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace AutoUpdater.lib
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class ClassLog
    {
        //日志文件所在路径
        //private static string logPath = string.Empty;
        private static string logPath = AppDomain.CurrentDomain.BaseDirectory + @"\log\";

        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                return logPath;
            }
            set { logPath = value; }
        }
        //日志前缀说明信息
        //private static string logFielPrefix = string.Empty;
        private static string logFielPrefix = "update";

        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }
        /// <summary>
        /// 写日志
        /// <param name="logType">日志类型</param>
        /// <param name="msg">日志内容</param> 
        /// </summary>
        public static void WriteLog(string logType, string msg)
        {
            System.IO.StreamWriter sw = null;
            try
            {

                //同一天同一类日志以追加形式保存
                sw = File.AppendText(
                    LogPath + LogFielPrefix + "_" +
                    DateTime.Now.ToString("yyyyMMdd") + ".log"
                    );
                sw.WriteLine(logType + "\t#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss \t>") + msg);
            }
            catch
            { }
            finally
            {
                sw.Close();
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        public void WriteLog(LogType logType, string msg)
        {
            WriteLog(logType.ToString(), msg);
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 调试信息
            /// </summary>
            Debug,

            /// <summary>
            /// 日常信息
            /// </summary>
            Info,

            /// <summary>
            /// 警告信息
            /// </summary>
            Warning,

            /// <summary>
            /// 错误信息应该包含对象名、发生错误点所在的方法名称、具体错误信息
            /// </summary>
            Error,

            /// <summary>
            /// 与数据库相关的信息
            /// </summary>
            SQL
        }
    }
}
