namespace Automation.Sdk.UIWrappers.Services.FileSystem
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reactive.Disposables;
    using Automation.Sdk.UIWrappers.Services.Logging;
    using Automation.Sdk.UIWrappers.Services.ResourcesServices;

    public class BackupFileService
    {
        private readonly ResourceConsumer _resourceConsumer;
        private readonly Logger _logger;

        private readonly List<string> _registeredFiles;

        public BackupFileService(ResourceConsumer resourceConsumer, Logger logger)
        {
            _resourceConsumer = resourceConsumer;
            _logger = logger;

            _registeredFiles = new List<string>();
        }

        public virtual void Consume(string path)
        {
            if (_registeredFiles.Exists(x => x == path))
            {
                return;
            }

            // Get the full name of the newly created Temporary file. 
            // Note that the GetTempFileName() method actually creates
            // a 0-byte file and returns the name of the created file.
            var fileName = Path.GetTempFileName();

            _logger.Write($"Generated temprorary file \"{fileName}\" for file \"{path}\"");

            // Create a FileInfo object to set the file's attributes
            FileInfo fileInfo = new FileInfo(fileName);

            // Set the Attribute property of this file to Temporary. 
            // Although this is not completely necessary, the .NET Framework is able 
            // to optimize the use of Temporary files by keeping them cached in memory.
            fileInfo.Attributes = FileAttributes.Temporary;

            File.Copy(path, fileName, true);

            var anchor = Disposable.Create(() =>
            {
                File.Copy(fileName, path, true);
                File.Delete(fileName);

                _logger.Write($"Temprorary file \"{fileName}\" has been deleted");
            });

            _resourceConsumer.Consume(anchor);

            _registeredFiles.Add(path);
        }
    }
}
