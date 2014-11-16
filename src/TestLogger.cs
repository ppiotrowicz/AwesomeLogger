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
        }

        private void OnTestResult(object sender, TestResultEventArgs e)
        {
            // single test result
        }
    }
}
