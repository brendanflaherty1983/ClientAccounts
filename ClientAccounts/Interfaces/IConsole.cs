using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAccounts.Interfaces
{
    public interface IConsole
    {
        void Write(string message);
        void WriteLine(string message);
        string ReadLine();
    }

    public class ConsoleWrapperProduction : IConsole
    {
        public List<string> LinesToRead = new List<string>();

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }

    public class ConsoleWrapperTest : IConsole
    {
        public List<string> LinesToRead = new List<string>();
        public List<string> LinesRead = new List<string>();

        public ConsoleWrapperTest(List<string> linesToRead)
        {
            LinesToRead = linesToRead;
        }

        public void Write(string message)
        {
            LinesRead.Add(message);
        }

        public void WriteLine(string message)
        {
            LinesRead.Add(message);
        }

        public string ReadLine()
        {
            string result = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return result;
        }
    }
}
