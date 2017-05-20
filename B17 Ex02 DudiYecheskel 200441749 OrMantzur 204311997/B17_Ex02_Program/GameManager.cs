
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
        private char m_borderChar = '|';                        // TODO change
        private int m_maxWordLenWithSpace = 2 * LetterSequence.k_LengthOfSequence - 1;

        public void Start()
        {
            getMaxRoundNumFromUser();
            printBoard();
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
            rowString.Append(pinsPrintFormat.PadRight(2* LetterSequence.k_LengthOfSequence));
            rowString.Append(m_borderChar);
            rowString.Append(resultPrintFormat.PadRight(2 * LetterSequence.k_LengthOfSequence - 1));
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
            if(separateWithSpaceString.Length != 0)
            {
                separateWithSpaceString.Remove(separateWithSpaceString.Length - 1, 1);
            }

            return separateWithSpaceString.ToString();
        }

        private void printRounds()
        {
            // second line in table
            string firstPinsValue = new string('#', LetterSequence.k_LengthOfSequence);
            printRow(firstPinsValue, string.Empty);

            // continue of table
            for (int i = 0; i < m_maxRoundNum; i++)
            {
                // the (i+1) round is already occurred
                if (i < m_RoundsOfGame.Count)
                {
                    // TODO print the round
                }
                else
                {
                    // print empty row
                    printRow(string.Empty, string.Empty);
                }
            }
        }
    }
}
