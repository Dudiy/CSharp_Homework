using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    public class UI
    {
        private byte m_MaxWordLenWithSpaces = (byte)((2 * LetterSequence.LengthOfSequence) - 1);
        private Game currentGame = null;

        // ================================================ getting input from user ================================================
        private byte getMaxNumOfGuessesFromUser()
        {
            string userInputStr = string.Empty;
            bool isValidInput = false;
            bool inputIsByte = false;
            byte userInputByte = 0;         // TODO Important!!! should not be 0 how do we use nullable here?

            while (!isValidInput)
            {
                Console.WriteLine("Please input max number of guesses (a number between {0} and {1}):", Game.MinNumOfGuesses, Game.MaxNumOfGuesses);
                userInputStr = Console.ReadLine();
                if (!(inputIsByte = byte.TryParse(userInputStr, out userInputByte)))
                {
                    Console.WriteLine("Invalid input, please try again.\n");
                }
                else if (!Game.isValidNumOfGuesses(userInputByte))
                {
                    Console.WriteLine("The number is out of range, please input a number between 4 and 10.\n");
                }
                else
                {
                    isValidInput = true;
                }
            }

            return userInputByte;
        }

        // return valid input: valid sequence or "Q" gmaeManager checks length of input, Letter sequence validates the input
        private string getInputFromUser()
        {
            string userInput = string.Empty;
            byte expectedInputLen = LetterSequence.LengthOfSequence;
            bool endOfInput = false;

            while (!endOfInput)
            {
                Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                userInput = Console.ReadLine().Replace(" ", string.Empty).ToUpper();     // get the input - remove all spaces and set to uppercase letters
                if (userInput.ToUpper().Equals("Q"))
                {
                    endOfInput = true;
                }
                else if (userInput.Length != expectedInputLen)
                {
                    Console.WriteLine("Length of sequence must be {0} letters long. Try again", expectedInputLen);
                }
                else if (!LetterSequence.IsValidSequence(userInput))
                {
                    Console.WriteLine("Only upper/lower case letters between 'A' and '{0}' are valid", LetterSequence.MaxLetterInSequence);
                }
                else
                {
                    endOfInput = true;
                }
            }

            return userInput;
        }

        private void promptUserForRestart()
        {
            string userInput = string.Empty;
            bool endOfInput = false;

            Console.WriteLine("Would you like to start a new game? <Y/N>");
            userInput = Console.ReadLine();
            while (!endOfInput)
            {
                if (userInput.ToUpper().Equals("Y"))
                {
                    StartNewGame();
                    endOfInput = true;
                }
                else if (userInput.ToUpper().Equals("N"))
                {
                    endGame();
                    endOfInput = true;
                }
                else
                {
                    Console.WriteLine("Error - please insert Y/N");
                    userInput = Console.ReadLine();
                }
            }
        }

        // ================================================ printing output to console ================================================
        private void printBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Current board status:");

            // print the title row and the first row
            printFirstTwoRows();

            // print the rest of the table
            printRounds();
        }

        private void printFirstTwoRows()
        {
            StringBuilder rowTitleString = new StringBuilder();
            string hiddenComputerSequenceStr = new string('#', LetterSequence.LengthOfSequence);

            // first line in table
            // rowTitleString.Append(m_verticalBorderChar);
            rowTitleString.Append('║');
            rowTitleString.Append("Pins:".PadRight(m_MaxWordLenWithSpaces + 2)); // 2 for space near border
            rowTitleString.Append('║');
            rowTitleString.Append("Result:".PadRight(m_MaxWordLenWithSpaces));
            rowTitleString.Append('║');
            Console.WriteLine(rowTitleString);
            printRowSeperator();

            // second line in table
            printRow(hiddenComputerSequenceStr, string.Empty);
            printRowSeperator();
        }

        private void printRowSeperator()
        {
            StringBuilder rowSeperator = new StringBuilder();

            // Pins column
            rowSeperator.Append('╠');
            rowSeperator.Append(new string('═', m_MaxWordLenWithSpaces + 2)); // 2 for space near border
            // Result column
            rowSeperator.Append('╬');
            rowSeperator.Append(new string('═', m_MaxWordLenWithSpaces));
            rowSeperator.Append('╣');
            Console.WriteLine(rowSeperator);
        }

        private void printRounds()
        {
            string pinsString = string.Empty;
            string resultString = string.Empty;

            for (int i = 0; i < currentGame.MaxNumOfGuessesFromPlayer; i++)
            {
                // the (i+1) round has already occurred
                if (i < currentGame.RoundsPlayedList.Count)
                {
                    pinsString = currentGame.RoundsPlayedList[i].Sequence;
                    resultString = currentGame.RoundsPlayedList[i].Result;
                }
                else
                {
                    pinsString = resultString = string.Empty;
                }

                printRow(pinsString, resultString);
                printRowSeperator();
            }
        }

        private void printRow(string i_PinsString, string i_ResultString)
        {
            StringBuilder rowString = new StringBuilder();
            string formattedPinsStr = separateLettersStringWithSpaces(i_PinsString);
            string formattedResultStr = separateLettersStringWithSpaces(i_ResultString);

            rowString.Append('║');
            rowString.Append(' ');
            rowString.Append(formattedPinsStr.PadRight(2 * LetterSequence.LengthOfSequence));
            rowString.Append('║');
            rowString.Append(formattedResultStr.PadRight((2 * LetterSequence.LengthOfSequence) - 1));
            rowString.Append('║');
            Console.WriteLine(rowString);
        }

        // gets a string, and adds a space between every two chars
        private string separateLettersStringWithSpaces(string i_Str)
        {
            StringBuilder outputStr = new StringBuilder();

            foreach (char letter in i_Str)
            {
                outputStr.Append(letter);
                outputStr.Append(' ');
            }

            // remove the last space in outputStr
            if (outputStr.Length != 0)
            {
                outputStr.Remove(outputStr.Length - 1, 1);
            }

            return outputStr.ToString();
        }

        // ================================================ IO<->Game methods ================================================
        public void StartNewGame()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            currentGame = new Game(getMaxNumOfGuessesFromUser());
            printBoard();
            while (currentGame.GameState.Equals(Game.eGameState.Running))
            {
                run();
            }
        }

        public void run()
        {
            while (currentGame.GameState.Equals(Game.eGameState.Running))
            {
                playRound();
            }

            switch (currentGame.GameState)
            {
                case Game.eGameState.PlayerWon:
                    winGame();
                    break;
                case Game.eGameState.PlayerLost:
                    loseGame();
                    break;
                case Game.eGameState.GameEnded:
                    promptUserForRestart();
                    break;
                default:
                    break;
            }
        }

        private void playRound()
        {
            string userInput = getInputFromUser();

            if (!userInput.ToUpper().Equals("Q"))
            {
                currentGame.PlayRound(userInput);
                printBoard();
            }
            else
            {
                endGame();
            }
        }

        private void endGame()
        {
            Console.WriteLine("The game ended.");
            currentGame.EndGame();
        }

        private void winGame()
        {
            Console.WriteLine("You guessed after {0} steps!", currentGame.CurrRoundNum - 1);
            promptUserForRestart();
        }

        private void loseGame()
        {
            Console.WriteLine("No more guesses allowed. You Lost.");
            Console.WriteLine("The correct sequence is: {0}", currentGame.ComputerSequence.SequenceStr);
            promptUserForRestart();
        }
    }
}
