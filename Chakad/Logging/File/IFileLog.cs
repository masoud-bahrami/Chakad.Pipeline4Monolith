namespace Chakad.Logging.File
{
    public interface IFileLog
    {
        void Write(LogMessageEntry message);
        void WriteLine(LogMessageEntry message);
        void Flush();
    }
}