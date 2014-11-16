using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace AwesomeTestLogger
{
    [ExtensionUri("logger://Microsoft/TestPlatform/AwesomeLogger/v1")]
    [FriendlyName("AwesomeLogger")]
    public class TestLogger : ITestLogger
    {
        public static TestFormatter Formatter = new TestFormatter();
        public static FailedTestCollector Collector = new FailedTestCollector();

        public void Initialize(TestLoggerEvents events, string testRunDirectory)
        {
            Console.WriteLine("Running tests in {0}", testRunDirectory);
            events.TestRunMessage += OnTestRunMessage;
            events.TestRunComplete += OnTestRunComplete;
            events.TestResult += OnTestResult;
        }

        private void OnTestRunMessage(object sender, TestRunMessageEventArgs e)
        {
            switch (e.Level)
            {
                case TestMessageLevel.Informational:
                    break;
                case TestMessageLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case TestMessageLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("All: {0}",  e.TestRunStatistics.ExecutedTests);
            Console.WriteLine("Passed:  {0}", e.TestRunStatistics[TestOutcome.Passed]);
            Console.WriteLine("Failed:  {0}", e.TestRunStatistics[TestOutcome.Failed]);
            Console.WriteLine();
            Console.WriteLine("Total time: {0}", e.ElapsedTimeInRunningTests);
            Console.WriteLine();
            Console.WriteLine("Errors:");
            Collector.WriteSummary();
        }

        private void OnTestResult(object sender, TestResultEventArgs e)
        {
            var result = e.Result;

            switch (result.Outcome)
            {
                case TestOutcome.Passed:
                    Formatter.WriteSuccess();
                    break;
                case TestOutcome.Failed:
                    Formatter.WriteFailed();
                    Collector.Collect(e.Result);
                    break;
            }
        }
    }
}
