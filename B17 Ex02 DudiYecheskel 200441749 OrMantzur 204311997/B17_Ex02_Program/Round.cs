using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class Round
    {
        private LetterSequence m_Sequence;
        private string m_Result = String.Empty;

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

        public Round(LetterSequence i_Sequence)
        {
            m_Sequence = i_Sequence;
        }

        // updates the value of result, returns true if the guess is correct
        public bool PlayRound(LetterSequence i_ComputerSequence)
        {
            m_Result = m_Sequence.Compare(i_ComputerSequence);
            
            //return true if the player won: length of result is the same as the sequence and there are no 'X' chars
            return (m_Result.Length == LetterSequence.LengthOfSequence && !m_Result.Contains("X"));     
        }
    }
}
