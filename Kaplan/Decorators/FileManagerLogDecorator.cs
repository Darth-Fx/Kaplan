using ZipLib.Interfaces;

namespace Kaplan.Decorators
{
    /// <summary>
    /// Decorator Extension of the FileManager class for logging before and after execution of the method(s).
    /// (a con working with decorators: You need to implement all interface members)
    /// </summary>
    public class FileManagerLogDecorator : IFileManager
    {
        private IFileManager _fileManager;
        public FileManagerLogDecorator(IFileManager filemanager)
        {
            _fileManager = filemanager;
        }
        public void DeleteAllFilesInDir(string dirpath)
        {
            Logger.Instance.Add("Remove Files");
            _fileManager.DeleteAllFilesInDir(dirpath);
            Logger.Instance.Add("zip Files removed");
        }

        public void CopyOneDirectory(string sourcePath, string targetPath)
        {
            _fileManager.CopyOneDirectory(sourcePath, targetPath);
        }

        public void CopySingleFile(string sourcePath, string targetPath, string filename)
        {
            _fileManager.CopySingleFile(sourcePath, targetPath, filename);
        }
        
        public void DeleteDir(string dirpath)
        {
            _fileManager.DeleteDir(dirpath);
        }

        public void DeleteFile(string filepath)
        {
            _fileManager.DeleteFile(filepath);
        }
    }
}
