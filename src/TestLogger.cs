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
        public static FailedTestCollector Collector = new FailedTestCollector(Formatter);

        public void Initialize(TestLoggerEvents events, string testRunDirectory)
        {
            Formatter.WriteLine("Running tests in {0}", testRunDirectory);
            events.TestRunMessage += OnTestRunMessage;
            events.TestRunComplete += OnTestRunComplete;
            events.TestResult += OnTestResult;
        }

        private void OnTestRunMessage(object sender, TestRunMessageEventArgs e)
        {
            switch (e.Level)
            {
                case TestMessageLevel.Informational:
                    Formatter.WriteLine(e.Message);
                    break;
                case TestMessageLevel.Warning:
                    Formatter.WriteLine(ConsoleColor.Yellow, e.Message);
                    break;
                case TestMessageLevel.Error:
                    Formatter.WriteLine(ConsoleColor.Red, e.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
        {
            Formatter.NewLine();
            Formatter.NewLine();

            Formatter.WriteLine("All: {0}",  e.TestRunStatistics.ExecutedTests);
            Formatter.WriteLine("Passed:  {0}", e.TestRunStatistics[TestOutcome.Passed]);
            Formatter.WriteLine("Failed:  {0}", e.TestRunStatistics[TestOutcome.Failed]);
            Formatter.NewLine();

            Formatter.WriteLine("Total time: {0}", e.ElapsedTimeInRunningTests);
            Formatter.NewLine();
            Formatter.WriteLine("Errors:");
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
