using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class Round
    {
        private string m_Result = String.Empty;
        private LetterSequence m_Sequence;

        public Round(LetterSequence i_Sequence)
        {
            m_Sequence = i_Sequence;           
        }
    }
}
