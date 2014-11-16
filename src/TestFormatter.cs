using System;

namespace AwesomeTestLogger
{
    public class TestFormatter
    {
        private const int ColumnWidth = 78;
        private int _column = 0;

        public void WriteSuccess()
        {
            Write(".", ConsoleColor.Green);
        }
        
        public void WriteFailed()
        {
            Write("F", ConsoleColor.Red);
        }

        private void Write(string output, ConsoleColor color)
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

        private void NewLine()
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