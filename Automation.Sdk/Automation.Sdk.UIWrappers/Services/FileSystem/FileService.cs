namespace Automation.Sdk.UIWrappers.Services.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    using JetBrains.Annotations;
    using NUnit.Framework;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    [UsedImplicitly]
    public class FileService 
    {
        private readonly BackupFileService _backupFileService;

        public FileService(BackupFileService backupFileService)
        {
            _backupFileService = backupFileService;
        }

        public virtual IEnumerable<string> ReadFile(string name)
        {
            var expandedName = GetFileName(name);
            if (!File.Exists(expandedName))
            {
                Assert.Fail($"File {name} does not exists. Expanded name is {expandedName}");
            }
            return File.ReadLines(expandedName);
        }

        public virtual void WriteToFile(string name, IEnumerable<string> content)
        {
            var expandedName = GetFileName(name);
            if (!File.Exists(expandedName))
            {
                Assert.Fail($"File {name} does not exists. Expanded name is {expandedName}");
            }

            _backupFileService.Consume(expandedName);

            File.WriteAllLines(expandedName, content);
        }

        private string GetFileName(string name)
        {
            // Replace machine-level environment variables with their values
            var expandedName = Methods.ReplaceEnvironmentVariables(name, EnvironmentVariableTarget.Machine);
            return expandedName;
        }
    }
}
