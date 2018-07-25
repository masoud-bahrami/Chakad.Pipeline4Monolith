using System;
using System.IO;

namespace Chakad.Logging.File
{
    public class FileLog : IFileLog
    {
        private readonly string _fileAddres;
        [ThreadStatic]
        readonly object obj = new object();

        public FileLog(string fileAddress)
        {
            _fileAddres = fileAddress;
        }

        public void Write(LogMessageEntry logMessage)
        {
            lock (obj)
            {
                using (var file = System.IO.File.Exists(_fileAddres)
                    ? System.IO.File.Open(_fileAddres, FileMode.Append)
                    : System.IO.File.Open(_fileAddres, FileMode.CreateNew))

                using (var stream = new StreamWriter(_fileAddres))
                {
                    stream.Write(logMessage.Message);
                }
            }
        }

        public void WriteLine(LogMessageEntry logMessage)
        {
            lock (obj)
            {
                string path = _fileAddres;

                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                }

                TextWriter tw = new StreamWriter(path, true, System.Text.Encoding.UTF8);
                tw.WriteLine(logMessage.LevelString);
                tw.WriteLine(logMessage.Message);
                tw.Close();
            }
        }


        public void Flush()
        {
            lock (obj)
            {
                string path = _fileAddres;

                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                }

                TextWriter tw = new StreamWriter(path, false, System.Text.Encoding.UTF8);
                tw.Write("");
                tw.Close();
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }

        private bool IsFileLocked(string fileAddress)
        {
            FileStream stream = null;

            try
            {
                using (stream = new FileStream(fileAddress, FileMode.Open))
                {
                    // File/Stream manipulating code here
                }
            }
            catch
            {
                //check here why it failed and ask user to retry if the file is in use.
            }
            finally
            {
                stream?.Close();
            }

            //file is not locked
            return false;
        }
    }
}