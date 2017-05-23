using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace B17_Ex02
{
    public class Program
    {
        public static void Main()
        {
            runUI();
        }
        
        private static void runUI()
        {
            UI userInterface = new UI();

            userInterface.StartNewGame();
        }
    }
}
