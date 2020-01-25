namespace Automation.Sdk.UIWrappers.Services.FileSystem
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using JetBrains.Annotations;

    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    // Can be inherited in test suites
    public class ConfigurationFileService
    {
        private readonly FileService _fileService;

        public ConfigurationFileService([NotNull] FileService fileService)
        {
            _fileService = fileService;
        }

        public virtual void VerifySetting(string fileName, string name, string value)
        {
            var keys = _fileService.ReadFile(fileName).Where(x => x.StartsWith($"{name}=")).ToList();
            keys.Should().HaveCount(1);
            keys.Single().Should().Be($"{name}={value}");
        }

        public virtual void SetSetting(string fileName, string name, string value)
        {
            var lines = _fileService.ReadFile(fileName).ToList();
            IEnumerable<string> updatedLines;

            if (lines.Any(x => x.StartsWith($"{name}=")))
            {
                updatedLines = lines.Select(line => ReplaceValue(line, name, value));
            }
            else
            {
                updatedLines = lines.Union(Enumerable.Repeat($"{name}={value}", 1));
            }

            _fileService.WriteToFile(fileName, updatedLines);
        }

        private string ReplaceValue(string line, string key, string value)
        {
            if (line.StartsWith($"{key}="))
            {
                return $"{key}={value}";
            }
            return line;
        }
    }
}
