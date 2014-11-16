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
        public static OutputFormatter Formatter = new OutputFormatter();
        public static MessageHandler MessageHandler = new MessageHandler(Formatter);
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
            MessageHandler.Handle(e.Level, e.Message);
        }

        private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
        {
            Formatter.NewLine();
            Formatter.NewLine();

            WriteSummary(e);
            MessageHandler.WriteSummary();
            Collector.WriteSummary();
        }

        private static void WriteSummary(TestRunCompleteEventArgs e)
        {
            Formatter.WriteLine("All: {0}", e.TestRunStatistics.ExecutedTests);
            Formatter.WriteLine("Passed:  {0}", e.TestRunStatistics[TestOutcome.Passed]);
            Formatter.WriteLine("Failed:  {0}", e.TestRunStatistics[TestOutcome.Failed]);
            Formatter.WriteLine("Not found: {0}, Skipped: {1}", e.TestRunStatistics[TestOutcome.NotFound], e.TestRunStatistics[TestOutcome.Skipped]);
            Formatter.NewLine();
            Formatter.WriteLine("Total time: {0}", e.ElapsedTimeInRunningTests);
            Formatter.NewLine();
        }

        private void OnTestResult(object sender, TestResultEventArgs e)
        {
            MessageHandler.TestsStarted();

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
