
using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class Game
    {
        public enum eGameState
        {
            Running = 0,
            PlayerWon,
            PlayerLost,
            PropmtForRestart,
            GameEnded
        }

        private const byte k_MinNumOfGuesses = 4;
        private const byte k_MaxNumOfGuesses = 10;
        private byte? m_MaxNumOfGuessesFromPlayer = null;                 //initialized to a valid number
        private byte m_CurrRoundNum = 1;                                  // TODO why not use m_RoundsPlayed.Count?
        private bool m_RunGameFlag = true;
        private LetterSequence m_ComputerSequence = new LetterSequence();   //empty ctor generates a random sequence
        private List<Round> m_RoundsPlayed = new List<Round>();
        private eGameState m_CurrentGameState = eGameState.Running;

        //asumption i_MaxNumOfGuessesFromPlayer is a valid input
        public Game(byte i_MaxNumOfGuessesFromPlayer)                                 //TODO changed from "start" to ctor
        {
            m_MaxNumOfGuessesFromPlayer = i_MaxNumOfGuessesFromPlayer;
        }

        //==========================Getters Setters=======================
        public static byte MinNumOfGuesses
        {
            get { return k_MinNumOfGuesses; }
        }

        public static byte MaxNumOfGuesses
        {
            get { return k_MaxNumOfGuesses; }
        }

        public byte MaxNumOfGuessesFromPlayer
        {
            get
            {
                // TODO throw execption if null
                return m_MaxNumOfGuessesFromPlayer ?? 4;
            }
        }

        public bool RunGameFlag
        {
            get { return m_RunGameFlag; }
        }

        public List<Round> RoundsPlayedList
        {
            get { return m_RoundsPlayed; }
        }

        public byte CurrRoundNum
        {
            get { return m_CurrRoundNum; }
        }

        public eGameState GameState
        {
            get { return m_CurrentGameState; }
        }

        public LetterSequence ComputerSequence
        {
            get { return m_ComputerSequence; }
        }

        public static bool isValidNumOfGuesses(byte i_NumOfGuessesFromUser)
        {
            return (k_MinNumOfGuesses <= i_NumOfGuessesFromUser && i_NumOfGuessesFromUser <= k_MaxNumOfGuesses);
        }

        // get input from user and update the curren Round
        public void PlayRound(string i_UserInput)
        {
            Round currentRound;

            // sequence input
            currentRound = new Round(i_UserInput);
            currentRound.PlayRound(m_ComputerSequence);
            m_RoundsPlayed.Add(currentRound);
            m_CurrRoundNum++;
            if (currentRound.IsWinRound())
            {
                m_CurrentGameState = eGameState.PlayerWon;
            }
            if (m_CurrRoundNum > m_MaxNumOfGuessesFromPlayer)
            {
                m_CurrentGameState = eGameState.PlayerLost;
            }
        }

        public void EndGame()
        {
            m_CurrentGameState = eGameState.GameEnded;            
        }
    }
}
