using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class Round
    {
        private string m_Result = String.Empty;
        private LetterSequence m_Sequence;

        public Round(string i_Sequence)
        {
            m_Sequence = new LetterSequence(i_Sequence);
            // TODO compute guesses
        }
    }
}
