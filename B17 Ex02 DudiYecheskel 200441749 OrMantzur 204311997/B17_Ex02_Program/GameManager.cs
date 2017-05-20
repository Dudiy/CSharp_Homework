
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
        private char m_borderChar = '|';                     // TODO change 124 221
        private int m_maxWordLenWithSpace = 2 * LetterSequence.LengthOfSequence - 1;

        public void Start()
        {
            getMaxRoundNumFromUser();
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

                printRow(string.Empty, string.Empty);
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
            LetterSequence userInput;
            bool playerWon = false;

            for (int i = 0; i < m_maxRoundNum; i++)
            {
                userInput = getInputFromUser();
                currentRound = new Round(userInput);
                playerWon = currentRound.PlayRound(m_ComputerSequence);
                m_RoundsOfGame.Add(currentRound);
                Ex02.ConsoleUtils.Screen.Clear();
                if (playerWon)
                {
                    printWinningScreen();
                }                   
                else
                {
                    printBoard();
                }
            }
            printLosingScreen();
        }

        private LetterSequence getInputFromUser()
        {
            string userInputStr = Console.ReadLine();
            string validationResult;

            while (!LetterSequence.IsValidSequence(userInputStr, out validationResult))
            {
                if (userInputStr.Equals("Q"))
                {
                    quitGame();
                }

                Console.WriteLine("{0} Try again:");
                userInputStr = Console.ReadLine();
            }

            return new LetterSequence(validationResult);
        }

        private void printLosingScreen()
        {

        }

        private void quitGame()
        {

        }

        private void printWinningScreen()
        {
            Console.WriteLine("No more guesses allowed. You lose.");
            Console.WriteLine("The sequence is: {0}", m_ComputerSequence.SequenceStr);
            Console.WriteLine("Would you like to start a new game? <Y/N>");

        }

        //private void startNewGame()
        //{
        //    Ex02.ConsoleUtils.Screen.Clear();
        //    getMaxRoundNumFromUser();
        //    printBoard();
        //}
    }
}
