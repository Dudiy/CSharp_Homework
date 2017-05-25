using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    public class Round
    {
        private LetterSequence m_Sequence;
        private byte m_NumOfCorrectGuesses;
        private byte m_NumOfCorrectLettersInWrongPositions;
        private bool m_IsWinningRound = false;

        public Round(string i_SequenceStr)
        {
            m_Sequence = new LetterSequence(i_SequenceStr);
        }

        public string SequenceStr
        {
            get { return m_Sequence.SequenceStr; }
        }

        public byte NumOfCorrectGuesses
        {
            get { return m_NumOfCorrectGuesses; }
        }

        public byte NumOfCorrectLettersInWrongPositions
        {
            get { return m_NumOfCorrectLettersInWrongPositions; }
        }

        public bool IsWinningRound
        {
            get { return m_IsWinningRound; }
        }

        public void PlayRound(LetterSequence i_ComputerSequence)
        {
            m_Sequence.Compare(i_ComputerSequence, out m_NumOfCorrectGuesses, out m_NumOfCorrectLettersInWrongPositions);
            m_IsWinningRound = (m_NumOfCorrectGuesses == LetterSequence.LengthOfSequence);
        }
    }
}
