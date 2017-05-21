using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class Round
    {
        private LetterSequence m_Sequence;
        private string m_Result = String.Empty;
        private bool m_winRound = false;

        public Round(string i_SequenceStr)
        {
            m_Sequence = new LetterSequence(i_SequenceStr);
        }

        public string Sequence
        {
            get { return m_Sequence.SequenceStr; }
        }

        public string Result
        {
            get
            {
                if (m_Result.Equals(String.Empty))
                {
                    //TODO throw error??                    
                }
                return m_Result;
            }
        }
        
        // updates the value of result, returns true if the guess is correct
        public void PlayRound(LetterSequence i_ComputerSequence)
        {
            m_Result = m_Sequence.Compare(i_ComputerSequence);

            // win round if: length of result is the same as the sequence and there are no 'X' chars
            m_winRound = (m_Result.Length == LetterSequence.LengthOfSequence && !m_Result.Contains("X"));     
        }

        public bool IsWinRound()
        {
            return m_winRound;
        }
    }
}
