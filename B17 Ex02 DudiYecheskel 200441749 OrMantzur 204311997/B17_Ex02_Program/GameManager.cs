
using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class GameManager
    {
        private LetterSequence m_ComputerSequence = new LetterSequence();
        private int m_maxRoundNum;                              // TODO change to "int?"
        private List<Round> m_RoundsOfGame = new List<Round>();
        private int m_currRoundNum = 1;
        private char m_borderChar = '|';                     // TODO change 124 221
        private int m_maxWordLenWithSpace = 2 * LetterSequence.LengthOfSequence - 1;
        private bool m_runGameFlag = true;

        public void Start()
        {
            getMaxRoundNumFromUser();
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard();
            run();
        }

        private void getMaxRoundNumFromUser()
        {
            bool endOfInput = false;

            Console.Write("Please enter max number of guesses: ");
            Console.WriteLine();
            while (!endOfInput)
            {
                string maxRoundNumInput = Console.ReadLine();

                // check if the user enter a possitive number and update m_maxRoundNum
                if (int.TryParse(maxRoundNumInput, out m_maxRoundNum) &&
                    m_maxRoundNum > 0)
                {
                    endOfInput = true;
                }
                else
                {
                    Console.WriteLine("Error, please enter a possitive number");
                }
            }
        }

        private void printBoard()
        {
            StringBuilder rowTitleString = new StringBuilder();

            // main title
            Console.WriteLine("Current board status:");
            // first line in table
            rowTitleString.Append(m_borderChar);
            rowTitleString.Append("Pins:".PadRight(m_maxWordLenWithSpace + 2)); // 2 for space near border
            rowTitleString.Append(m_borderChar);
            rowTitleString.Append("Result:".PadRight(m_maxWordLenWithSpace));
            rowTitleString.Append(m_borderChar);
            Console.WriteLine(rowTitleString);
            printSeparateRow();
            // continue of table
            printRounds();
        }

        private void printRow(string i_PinsString, string i_ResultString)
        {
            StringBuilder rowString = new StringBuilder();
            string pinsPrintFormat = separateLettersStringWithSpace(i_PinsString);
            string resultPrintFormat = separateLettersStringWithSpace(i_ResultString);

            rowString.Append(m_borderChar);
            rowString.Append(' ');
            rowString.Append(pinsPrintFormat.PadRight(2 * LetterSequence.LengthOfSequence));
            rowString.Append(m_borderChar);
            rowString.Append(resultPrintFormat.PadRight(2 * LetterSequence.LengthOfSequence - 1));
            rowString.Append(m_borderChar);
            Console.WriteLine(rowString);
        }

        private string separateLettersStringWithSpace(string i_Str)
        {
            StringBuilder separateWithSpaceString = new StringBuilder();

            foreach (char letter in i_Str)
            {
                separateWithSpaceString.Append(letter);
                separateWithSpaceString.Append(' ');
            }

            // remove the last space in separateWithSpaceString
            if (separateWithSpaceString.Length != 0)
            {
                separateWithSpaceString.Remove(separateWithSpaceString.Length - 1, 1);
            }

            return separateWithSpaceString.ToString();
        }

        private void printRounds()
        {
            // second line in table
            string firstPinsValue = new string('#', LetterSequence.LengthOfSequence);
            printRow(firstPinsValue, string.Empty);
            printSeparateRow();

            // continue of table
            string pinsString = string.Empty;
            string resultString = string.Empty;

            for (int i = 0; i < m_maxRoundNum; i++)
            {
                // the (i+1) round is already occurred
                if (i < m_RoundsOfGame.Count)
                {
                    pinsString = m_RoundsOfGame[i].Sequence;
                    resultString = m_RoundsOfGame[i].Result;
                }
                else
                {
                    pinsString = resultString = string.Empty;
                }

                printRow(pinsString, resultString);
                printSeparateRow();
            }
        }

        private void printSeparateRow()
        {
            StringBuilder separateRow = new StringBuilder();

            // Pins column
            separateRow.Append(m_borderChar);
            separateRow.Append(new string('=', m_maxWordLenWithSpace + 2)); // 2 for space near border
            // Result column
            separateRow.Append(m_borderChar);
            separateRow.Append(new string('=', m_maxWordLenWithSpace));
            separateRow.Append(m_borderChar);
            Console.WriteLine(separateRow);
        }

        private void run()
        {
            Round currentRound;
            string userInput;

            while(checkRunGameFlag())
            {
                userInput = getInputFromUser();
                // sequence input
                if(!userInput.ToUpper().Equals("Q"))
                {
                    currentRound = new Round(userInput);
                    currentRound.PlayRound(m_ComputerSequence);
                    m_RoundsOfGame.Add(currentRound);
                    Ex02.ConsoleUtils.Screen.Clear();
                    printBoard();
                    if (currentRound.IsWinRound())
                    {
                        winGame();
                    }
                }
                // quit input
                else
                {
                    endGame();
                }
                
            }
            loseGame();
        }

        private bool checkRunGameFlag()
        {
            return (m_runGameFlag && m_currRoundNum <= m_maxRoundNum);
        }

        // return valid input: valid sequence or "Q"
        private string getInputFromUser()
        {
            string userInput = string.Empty;
            string validationResult;
            bool endOfInput = false;

            while (!endOfInput)
            {
                Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                userInput = Console.ReadLine();
                // check if input: valid sequence or "Q"
                if (LetterSequence.IsValidSequence(userInput, out validationResult) ||
                    userInput.ToUpper().Equals("Q"))
                {
                    endOfInput = true;
                }
                // invalid input, print the kind of error
                else
                {
                    Console.WriteLine("{0} Try again:", validationResult);
                }
            }

            return userInput;
        }

        private void winGame()
        {
            Console.WriteLine("You guessed after {0} steps!", m_currRoundNum);
            userActionIfRestartGame();
        }

        private void loseGame()
        {
            Console.WriteLine("No more guesses allowed. You lose.");
            Console.WriteLine("The sequence is: {0}", m_ComputerSequence.SequenceStr);
            checkIfRestartGame();
        }

        private void userActionIfRestartGame()
        {
            if (checkIfRestartGame())
            {
                startNewGame();
            }
            else
            {
                endGame();
            }
        }

        private bool checkIfRestartGame()
        {
            string userInput = string.Empty;
            bool endOfInput = false;
            bool isRestartGame=false;               // TODO nullable ?

            Console.WriteLine("Would you like to start a new game? <Y/N>");
            userInput = Console.ReadLine();
            while(!endOfInput)
            {
                isRestartGame = userInput.ToUpper().Equals("Y");
                if (!isRestartGame && !userInput.ToUpper().Equals("N"))
                {
                    Console.WriteLine("Error: insert Y/N");
                    userInput = Console.ReadLine();
                }
                else
                {
                    endOfInput = true;
                }
            }

            return isRestartGame;
        }

        private void startNewGame()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_ComputerSequence = new LetterSequence();
            getMaxRoundNumFromUser();
            m_RoundsOfGame = new List<Round>();
            m_currRoundNum = 1;
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard();
        }

        private void endGame()
        {
            Console.WriteLine("The game ended.");
            m_runGameFlag = false;
        }
    }
}
