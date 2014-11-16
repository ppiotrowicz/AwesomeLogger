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
        public void Initialize(TestLoggerEvents events, string testRunDirectory)
        {
            events.TestRunMessage += OnTestRunMessage;
            events.TestRunComplete += OnTestRunComplete;
            events.TestResult += OnTestResult;
        }

        private void OnTestRunMessage(object sender, TestRunMessageEventArgs e)
        {
            // ?
        }

        private void OnTestRunComplete(object sender, TestRunCompleteEventArgs e)
        {
            // complete test run
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("All: {0}",  e.TestRunStatistics.ExecutedTests);
            Console.WriteLine("Passed:  {0}", e.TestRunStatistics[TestOutcome.Passed]);
            Console.WriteLine("Failed:  {0}", e.TestRunStatistics[TestOutcome.Failed]);
            Console.WriteLine();
            Console.WriteLine("Total time: {0}", e.ElapsedTimeInRunningTests);
        }

        private void OnTestResult(object sender, TestResultEventArgs e)
        {
            var result = e.Result;

            switch (result.Outcome)
            {
                case TestOutcome.None:
                    break;
                case TestOutcome.Passed:
                    Console.Write(".");
                    break;
                case TestOutcome.Failed:
                    Console.Write("F");
                    break;
                case TestOutcome.Skipped:
                    Console.WriteLine("S");
                    break;
                case TestOutcome.NotFound:
                    Console.WriteLine("?");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
