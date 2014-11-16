using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AwesomeTestLogger
{
    public class FailedTestCollector
    {
        private readonly TestFormatter _formatter;
        private readonly IList<TestResult> _results = new List<TestResult>();

        public FailedTestCollector(TestFormatter formatter)
        {
            _formatter = formatter;
        }

        public void Collect(TestResult result)
        {
            _results.Add(result);
        }

        public void WriteSummary()
        {
            _results.Apply((result, i) =>
            {
                ShowTestName(i, result);
                ShowErrorMessage(result);
                ShowStackTrace(result);
            });
        }

        private void ShowTestName(int i, TestResult result)
        {
            _formatter.WriteLine(ConsoleColor.Red, "{0}) {1}", i + 1, result.TestCase);
            _formatter.NewLine();
        }

        private void ShowErrorMessage(TestResult result)
        {
            _formatter.WriteLine(ConsoleColor.Yellow, result.ErrorMessage.Indent(4));
            _formatter.NewLine();
        }

        private void ShowStackTrace(TestResult result)
        {
            _formatter.WriteLine("Stack trace:".Indent(4));
            _formatter.WriteLine(result.ErrorStackTrace.Substring(1).Indent(3));
            _formatter.NewLine();
        }
    }
}