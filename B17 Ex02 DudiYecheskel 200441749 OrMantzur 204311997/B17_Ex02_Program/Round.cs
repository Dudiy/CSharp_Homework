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

        public void playRound(LetterSequence i_ComputerSequence)
        {
            m_Result = m_Sequence.Compare(i_ComputerSequence);
        }
    }
}
