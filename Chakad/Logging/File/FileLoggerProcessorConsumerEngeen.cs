using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Chakad.Logging.File
{
    public class FileLoggerProcessorConsumerEngeen : IDisposable
    {
        private const int MaxQueuedMessages = 1024;

        private readonly static BlockingCollection<LogMessageEntry> _messageQueue =
            new BlockingCollection<LogMessageEntry>(MaxQueuedMessages);

        public static IFileLog FileLog;

        static FileLoggerProcessorConsumerEngeen()
        {
           
            // Start Console message queue processor
            var outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "file logger queue processing thread"
            };
            
            outputThread.Start();
        }
        public static void EnqueueMessage(LogMessageEntry message)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                try
                {
                    _messageQueue.Add(message);
                    return;
                }
                catch (InvalidOperationException)
                {
                }
            }

            // Adding is completed so just log the message
            WriteMessage(message);
        }
        // for testing
        internal static void WriteMessage(LogMessageEntry message)
        {
            FileLog.WriteLine(message);
        }

        private static void ProcessLogQueue()
        {
            try
            {
                foreach (var message in _messageQueue.GetConsumingEnumerable())
                {
                    WriteMessage(message);
                }
            }
            catch(Exception ex)
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch
                {
                    // ignored
                }
            }
        }

        public void Dispose()
        {
        }
    }
}