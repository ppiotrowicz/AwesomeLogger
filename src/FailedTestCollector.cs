using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace AwesomeTestLogger
{
    public class FailedTestCollector
    {
        readonly IList<TestResult> _results = new List<TestResult>();

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

        private static void ShowTestName(int i, TestResult result)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}) {1}", i + 1, result.TestCase);
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void ShowErrorMessage(TestResult result)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(result.ErrorMessage.Indent(4));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void ShowStackTrace(TestResult result)
        {
            Console.WriteLine("Stack trace:".Indent(4));
            Console.WriteLine(result.ErrorStackTrace.Substring(1).Indent(3));
            Console.WriteLine();
        }
    }
}