namespace Automation.Sdk.UIWrappers.Services
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Linq;

    using JetBrains.Annotations;
    using NUnit.Framework;

    public sealed class FormattedString
    {
        private const string Opentag = "<";
        private const string Closetag = ">";

        private readonly string _input;
        private readonly string _output;

        public FormattedString([NotNull] string input)
        {
            _input = input;
            _output = ReplacePlaceholders(_input);
        }

        public string Input => _input;

        public override string ToString() => _output;

        public static implicit operator string(FormattedString x)
        {
            return x.ToString();
        }

        public static implicit operator FormattedString(string x)
        {
            return new FormattedString(x);
        }

        public int Length => _output.Length;

        private string ReplacePlaceholders([NotNull] string input)
        {
            if (!input.Contains(Opentag, Closetag))
            {
                return input;
            }

            if (input.Contains("<TestID>"))
            {
                string testId;
                Assert.IsTrue(ContextStorage.TryGetValue(SpecIds.TestID, out testId));
                input = input.Replace("<TestID>", testId);
            }

            if (input.Contains("<br>"))
            {
                input = input.Replace("<br>", "\r\n");
            }

            if (input.Contains("<NewLine>") || input.Contains("<nl>"))
            {
                input = input.Replace("<NewLine>", Environment.NewLine);
                input = input.Replace("<nl>", Environment.NewLine);
            }

            if (input.Contains("<r>"))
            {
                input = input.Replace("<r>", "\r");
            }

            if (input.Contains("<space>"))
            {
                input = input.Replace("<space>", " ");
            }

            if (input.Contains("<pipe>"))
            {
                input = input.Replace("<pipe>", "|");
            }

            if (input.Contains("<AppDataLocal>"))
            {
                input = input.Replace(
                    "<AppDataLocal>",
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            }

            if (input.Contains("<TODAY>"))
            {
                input = input.Replace("<TODAY>", DateTime.Now.ToString("MM/dd/yyyy"));
            }

            if (input.Contains("<domain>"))
            {
                input = input.Replace("<domain>", Methods.GetDomain());
            }

            if (input.Contains(Opentag, Closetag))
            {
                var placeHolders = Regex.Matches(input, "<[A-Za-z0-9_]*>");

                for (int i = 0; i < placeHolders.Count; ++i)
                {
                    var placeHolder = placeHolders[i].Value;
                    var key = placeHolder.Substring(1, placeHolder.Length - 2);

                    if (ContextStorage.Current.ContainsKey(key))
                    {
                        input = input.Replace(placeHolder, (string)ContextStorage.Current[key]);
                    }
                    else if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                    {
                        input = input.Replace(placeHolder, ConfigurationManager.AppSettings[key]);
                    }
                }
            }

            return input;
        }
    }
}