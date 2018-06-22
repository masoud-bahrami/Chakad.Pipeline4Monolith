namespace Chakad.Logging.File
{
    public interface IFileLog
    {
        void Write(string message);
        void WriteLine(string message);
        void Flush();
    }
}