namespace Automation.Sdk.UIWrappers.Services.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using JetBrains.Annotations;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    public class SafeFileService
    {
        private readonly FileService _fileService;

        public SafeFileService([NotNull] FileService fileService)
        {
            _fileService = fileService;
        }

        [UsedImplicitly, ItemNotNull]
        public virtual List<string> ReadFileContents([NotNull] string fileName, int timeout = 15000, int interval = 1000)
        {
            List<string> lines = null;

            Func<bool> linesWereRead = () =>
            {
                try
                {
                    lines = _fileService.ReadFile(fileName).ToList();
                }
                catch
                {
                    return false;
                }

                return true;
            };
            var readResult = Methods.WaitUntil(linesWereRead, timeout, interval);
            readResult.Should().BeTrue($"Cannot read file {fileName}. Probably in use by another process.");

            return lines;
        }
    }
}
