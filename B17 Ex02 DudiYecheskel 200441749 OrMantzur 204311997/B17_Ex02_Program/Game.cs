/*
 * B17_Ex02: Game.cs
 * 
 * This class manages the logic of the game.
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/

using System.Collections.Generic;

namespace B17_Ex02
{
    public class Game
    {
        public enum eGameState
        {
            Running,
            PlayerWon,
            PlayerLost,
            GameEnded
        }

        private const byte k_MinNumOfGuesses = 4;
        private const byte k_MaxNumOfGuesses = 10;
        private List<Round> m_RoundsPlayed = new List<Round>();
        private LetterSequence m_ComputerSequence = new LetterSequence();   // empty ctor generates a random sequence
        private eGameState m_CurrentGameState = eGameState.Running;
        private byte m_MaxNumOfGuessesFromPlayer;

        // asumption i_MaxNumOfGuessesFromPlayer is a valid input
        public Game(byte i_MaxNumOfGuessesFromPlayer)
        {
            m_MaxNumOfGuessesFromPlayer = i_MaxNumOfGuessesFromPlayer;
        }

        // ==========================Getters Setters=======================
        public static byte MinNumOfGuesses
        {
            get { return k_MinNumOfGuesses; }
        }

        public static byte MaxNumOfGuesses
        {
            get { return k_MaxNumOfGuesses; }
        }

        public static bool IsValidNumOfGuesses(byte i_NumOfGuessesFromUser)
        {
            return (k_MinNumOfGuesses <= i_NumOfGuessesFromUser) && (i_NumOfGuessesFromUser <= k_MaxNumOfGuesses);
        }

        public eGameState GameState
        {
            get { return m_CurrentGameState; }
        }

        public string ComputerSequence
        {
            get { return m_ComputerSequence.SequenceStr; }
        }

        public byte MaxNumOfGuessesFromPlayer
        {
            get
            {
                return m_MaxNumOfGuessesFromPlayer;
            }
        }

        public byte GetNumOfRoundsPlayed()
        {
            return (byte)m_RoundsPlayed.Count;
        }

        public string GetRoundLetterSequenceStr(int i_RoundInd)
        {
            return m_RoundsPlayed[i_RoundInd].SequenceStr;
        }

        public byte GetNumOfCorrectGuesses(int i_RoundInd)
        {
            return m_RoundsPlayed[i_RoundInd].NumOfCorrectGuesses;
        }

        public byte GetNumOfCorrectLetterInWrongPositions(int i_RoundInd)
        {
            return m_RoundsPlayed[i_RoundInd].NumOfCorrectLetterInWrongPositions;
        }

        // get input from user and update the current Round
        public void PlayRound(string i_UserInput)
        {
            Round currentRound = new Round(i_UserInput);

            currentRound.PlayRound(m_ComputerSequence);
            m_RoundsPlayed.Add(currentRound);
            if (currentRound.IsWinningRound)
            {
                m_CurrentGameState = eGameState.PlayerWon;
            }

            if (m_RoundsPlayed.Count >= m_MaxNumOfGuessesFromPlayer)
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
