using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class Round
    {
        private LetterSequence m_sequence;
        private string m_result = string.Empty;

        public Round(string i_Sequence)
        {
            m_sequence = new LetterSequence(i_Sequence);
            // TODO compute guesses
        }
    }
}
