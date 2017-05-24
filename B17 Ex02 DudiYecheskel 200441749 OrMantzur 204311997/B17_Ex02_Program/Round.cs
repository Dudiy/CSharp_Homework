using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    public class Round
    {
        private LetterSequence m_Sequence;
        private byte m_NumOfCorrectGuess;
        private byte m_NumOfCorrectLetterWrongPosition; // TODO name 2X
        private bool m_WinRound = false;

        public Round(string i_SequenceStr)
        {
            m_Sequence = new LetterSequence(i_SequenceStr);
        }

        public string Sequence
        {
            get
            {
                return m_Sequence.SequenceStr;
            }
        }

        public byte NumOfCorrectGuess
        {
            get
            {
                return m_NumOfCorrectGuess;
            }
        }

        public byte NumOfCorrectLetterWrongPosition
        {
            get
            {
                return m_NumOfCorrectLetterWrongPosition;
            }
        }

        public void PlayRound(LetterSequence i_ComputerSequence)
        {
            m_Sequence.Compare(i_ComputerSequence, out m_NumOfCorrectGuess, out m_NumOfCorrectLetterWrongPosition);
            m_WinRound = (m_NumOfCorrectGuess == LetterSequence.LengthOfSequence);
        }

        public bool IsWinRound()
        {
            return m_WinRound;          // TODO change to m_playerWon and use getter?
        }
    }
}
