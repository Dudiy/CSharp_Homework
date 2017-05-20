using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class GameManager
    {
        private LetterSequence m_ComputerSequence;
        private int m_maxRoundNum;                  // TODO change to short ?
        private Round[] m_RoundsOfGame;
        
        public void Start()
        {
            init();
        }

        private void init()
        {
            // TODO produce computer sequence
            getMaxRoundNumFromUser();
            // TODO allocate m_RoundOfGame
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
    }
}
