using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Chakad.Logging.File
{
    public class FileLoggerProcessorConsumerEngeen : IDisposable
    {
        private const int MaxQueuedMessages = 1024;

        private readonly BlockingCollection<string> _messageQueue =
            new BlockingCollection<string>(MaxQueuedMessages);

        public IFileLog FileLog;

        public FileLoggerProcessorConsumerEngeen()
        {
            // Start Console message queue processor
            var outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "file logger queue processing thread"
            };
            
            outputThread.Start();
        }
        public virtual void EnqueueMessage(string message)
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
        internal virtual void WriteMessage(string message)
        {
            FileLog.WriteLine(message);

            FileLog.Flush();
        }

        private void ProcessLogQueue()
        {
            try
            {
                foreach (var message in _messageQueue.GetConsumingEnumerable())
                {
                    WriteMessage(message);
                }
            }
            catch
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