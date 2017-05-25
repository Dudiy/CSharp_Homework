/*
 * B17_Ex02: UI.cs
 * 
 * This class manages all UI of the game itself
 * this UI is made for playing via the console.
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/

using System;
using System.Text;

namespace B17_Ex02
{
    public class ConsoleUI
    {
        private GameLogic m_CurrentGame = null;
        private readonly byte r_MaxWordLenWithSpaces = (byte)((2 * LetterSequence.LengthOfSequence) - 1);

        // ================================================ getting input from user ================================================
        private byte getMaxNumOfGuessesFromUser()
        {
            string userInputStr = string.Empty;
            bool isValidInput = false;
            bool inputIsByte = false;
            byte userInputByte = GameLogic.MinNumOfGuesses;

            while (!isValidInput)
            {
                Console.WriteLine(
@"Please input max number of guesses (a number between {0} and {1}):",
GameLogic.MinNumOfGuesses, 
GameLogic.MaxNumOfGuesses);
                userInputStr = Console.ReadLine();
                if (!(inputIsByte = byte.TryParse(userInputStr, out userInputByte)))
                {
                    Console.WriteLine(
@"Invalid input, please try again.
");
                }
                else if (!GameLogic.IsValidNumOfGuesses(userInputByte))
                {
                    Console.WriteLine(
@"The number is out of range, please input a number between 4 and 10.
");
                }
                else
                {
                    isValidInput = true;
                }
            }

            return userInputByte;
        }

        // return valid input: valid sequence or "Q" gameManager checks length of input, Letter sequence validates the input
        private string getSequenceFromUser()
        {
            string userInput = string.Empty;
            byte expectedInputLen = LetterSequence.LengthOfSequence;
            bool endOfInput = false;

            while (!endOfInput)
            {
                Console.WriteLine(
@"Please type your next guess <A B C D> or 'Q' to quit");
                // get the input - remove all spaces and set to uppercase letters
                userInput = Console.ReadLine().Replace(" ", string.Empty).ToUpper();
                if (userInput.ToUpper().Equals("Q"))
                {
                    endOfInput = true;
                }
                else if (userInput.Length != expectedInputLen)
                {
                    Console.WriteLine(
@"Length of sequence must be {0} letters long. Try again",
expectedInputLen);
                }
                else if (!LetterSequence.IsValidSequence(userInput))
                {
                    Console.WriteLine(
@"Only upper/lower case letters between 'A' and '{0}' are valid",
LetterSequence.MaxLetterInSequence);
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

            Console.WriteLine(
@"Would you like to start a new game? <Y/N>");
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
                    Console.WriteLine(
@"Error - please insert Y/N");
                    userInput = Console.ReadLine();
                }
            }
        }

        // ================================================ printing output to console ================================================
        private void printBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(
@"Current board status:");
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
            rowTitleString.Append('║');
            // +2 for space near border
            rowTitleString.Append("Pins:".PadRight(r_MaxWordLenWithSpaces + 2));
            rowTitleString.Append('║');
            rowTitleString.Append("Result:".PadRight(r_MaxWordLenWithSpaces));
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
            // +2 for space near border
            rowSeperator.Append(new string('═', r_MaxWordLenWithSpaces + 2));
            // Result column
            rowSeperator.Append('╬');
            rowSeperator.Append(new string('═', r_MaxWordLenWithSpaces));
            rowSeperator.Append('╣');
            Console.WriteLine(rowSeperator);
        }

        private void printRounds()
        {
            string pinsString = string.Empty;
            string resultString = string.Empty;
            byte numOfCorrectGuesses;
            byte numOfCorrectLetterInWrongPositions;

            for (int i = 0; i < m_CurrentGame.MaxNumOfGuessesFromPlayer; i++)
            {
                // if (i < m_CurrentGame.NumRound) then Round[i] has occured else print blank row
                if (i < m_CurrentGame.GetNumOfRoundsPlayed())
                {
                    pinsString = m_CurrentGame.GetRoundLetterSequenceStr(i);
                    numOfCorrectGuesses = m_CurrentGame.GetNumOfCorrectGuesses(i);
                    numOfCorrectLetterInWrongPositions = m_CurrentGame.GetNumOfCorrectLetterInWrongPositions(i);
                    resultString = createResultString(numOfCorrectGuesses, numOfCorrectLetterInWrongPositions);
                }
                else
                {
                    pinsString = string.Empty;
                    resultString = string.Empty;
                }

                printRow(pinsString, resultString);
                printRowSeperator();
            }
        }

        private string createResultString(byte i_NumOfCorrectGuesses, byte i_NumOfCorrectLetterInWrongPositions)
        {
            StringBuilder resultString = new StringBuilder();

            resultString.AppendFormat(new string('V', i_NumOfCorrectGuesses));
            resultString.AppendFormat(new string('X', i_NumOfCorrectLetterInWrongPositions));

            return resultString.ToString();
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
            m_CurrentGame = new GameLogic(getMaxNumOfGuessesFromUser());
            printBoard();
            while (m_CurrentGame.GameState.Equals(GameLogic.eGameState.Running))
            {
                run();
            }
        }

        private void run()
        {
            while (m_CurrentGame.GameState.Equals(GameLogic.eGameState.Running))
            {
                playRound();
            }

            switch (m_CurrentGame.GameState)
            {
                case GameLogic.eGameState.PlayerWon:
                    winGame();
                    break;
                case GameLogic.eGameState.PlayerLost:
                    loseGame();
                    break;
                case GameLogic.eGameState.GameEnded:
                    endGame();
                    break;
                default:
                    break;
            }
        }

        private void playRound()
        {
            string userInput = getSequenceFromUser();

            if (!userInput.ToUpper().Equals("Q"))
            {
                m_CurrentGame.PlayRound(userInput);
                printBoard();
            }
            else
            {
                m_CurrentGame.EndGame();
            }
        }

        private void winGame()
        {
            Console.WriteLine(
@"You guessed after {0} steps!",
m_CurrentGame.GetNumOfRoundsPlayed());
            promptUserForRestart();
        }

        private void loseGame()
        {
            Console.WriteLine(
@"No more guesses allowed. You Lost.");
            Console.WriteLine(
@"The correct sequence is: {0}",
m_CurrentGame.ComputerSequence);
            promptUserForRestart();
        }

        private void endGame()
        {
            Console.WriteLine(
@"Goodbye");
        }
    }
}
