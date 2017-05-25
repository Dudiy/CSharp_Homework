/*
 * B17_Ex02.cs
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/
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
