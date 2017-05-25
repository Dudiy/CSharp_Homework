/*
 * B17_Ex02: Program.cs
 * 
 * This program is a guessing game.
 * First the computer generates a random sequence of letters
 * The user defines how many guesses he would like to have,
 * and then tried to guess what the computer's random sequence is.
 * In each round the number of Vs in the result will be 
 * the number of correct letters in the correct positions,
 * and the number of Xs will be the number of correct letters
 * that are in the wrong positions.
 * 
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
