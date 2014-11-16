using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace AwesomeTestLogger
{
    public class MessageHandler
    {
        private readonly OutputFormatter _formatter;
        private readonly IList<Message> _messages = new List<Message>();
        private bool _testsStarted;

        public MessageHandler(OutputFormatter formatter)
        {
            _formatter = formatter;
        }

        public void TestsStarted()
        {
            _testsStarted = true;
        }

        public void Handle(TestMessageLevel level, string text)
        {
            var message = new Message {Level = level, Text = text};
            if (_testsStarted)
                _messages.Add(message);
            else
                WriteMessage(message);
        }

        private void WriteMessage(Message e)
        {
            switch (e.Level)
            {
                case TestMessageLevel.Informational:
                    _formatter.WriteLine(e.Text);
                    break;
                case TestMessageLevel.Warning:
                    _formatter.WriteLine(ConsoleColor.Yellow, e.Text);
                    break;
                case TestMessageLevel.Error:
                    _formatter.WriteLine(ConsoleColor.Red, e.Text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _formatter.NewLine();
        }

        public void WriteSummary()
        {
            if (_messages.Any())
                _formatter.WriteLine("Messages:");
                _messages.Apply(WriteMessage);
        }

        private class Message
        {
            public TestMessageLevel Level { get; set; }
            public string Text { get; set; }
        }
    }
}