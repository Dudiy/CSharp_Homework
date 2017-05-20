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
            get { return m_Result; }
        }
    }
}
