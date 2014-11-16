using System;

namespace AwesomeTestLogger
{
    public class OutputFormatter
    {
        private const int ColumnWidth = 78;
        private int _column = 0;

        public void WriteSuccess()
        {
            WriteTestOutput(".", ConsoleColor.Green);
        }
        
        public void WriteFailed()
        {
            WriteTestOutput("F", ConsoleColor.Red);
        }

        public void WriteLine(ConsoleColor color, string text, params object[] obj)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text, obj);
            Console.ResetColor();
        }

        public void WriteLine(string text, params object[] obj)
        {
            Console.WriteLine(text, obj);
        }

        private void WriteTestOutput(string output, ConsoleColor color)
        {
            if (ShouldGoToNewLine())
            {
                NewLine();
            }

            Console.ForegroundColor = color;
            Console.Write(output);
            Console.ResetColor();

            _column++;
        }

        public void NewLine()
        {
            Console.WriteLine();
            _column = 0;
        }

        private bool ShouldGoToNewLine()
        {
            return _column == ColumnWidth;
        }
    }
}