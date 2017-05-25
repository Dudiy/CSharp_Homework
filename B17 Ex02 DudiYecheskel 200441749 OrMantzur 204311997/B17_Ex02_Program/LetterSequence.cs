using System;
using System.Text;

namespace B17_Ex02
{
    public class LetterSequence
    {
        private string m_SequenceStr = string.Empty;
        private const byte k_LengthOfSequence = 4;           // this value must be less than or equal to the number of valid unique letters
        private const char k_MaxLetterInSequence = 'H';
        private static Random s_randomizer = new Random();      // TODO add seed

        public LetterSequence()
        {
            char currentRandomChar = (char)s_randomizer.Next('A', k_MaxLetterInSequence);

            for (int i = 0; i < k_LengthOfSequence; i++)
            {
                while (m_SequenceStr.Contains(currentRandomChar.ToString()))
                {
                    currentRandomChar = (char)s_randomizer.Next('A', k_MaxLetterInSequence);
                }

                m_SequenceStr = string.Concat(m_SequenceStr, currentRandomChar);
            }
        }

        // asumption - given sequence is a valid sequence, with no spaces and all uppercase letters
        public LetterSequence(string i_SequenceStr)
        {
            m_SequenceStr = i_SequenceStr;
        }

        public string SequenceStr
        {
            get { return m_SequenceStr; }
        }

        public static byte LengthOfSequence
        {
            get { return k_LengthOfSequence; }
        }

        public static char MaxLetterInSequence
        {
            get { return k_MaxLetterInSequence; }
        }

        // checks if a given string is a valid sequence. 
        // assumption: given string is already "k_LengthOfSequence" long, has no spaces, and is all uppercase letters
        public static bool IsValidSequence(string i_SequenceStr)
        {
            // valid = sequence of upper or lower case letters between 'A' and "k_MaxLetterInSequence"
            bool isValid = true;

            // check letters of sequence
            foreach (char ch in i_SequenceStr)
            {
                if (!(ch >= 'A' && ch <= k_MaxLetterInSequence))
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        public void Compare(LetterSequence i_CompareTo, out byte o_NumOfCorrectGuesses, out byte o_NumOfCorrectLettersInWrongPositions)
        {
            StringBuilder result = new StringBuilder();
            byte currentIndex = 0;

            o_NumOfCorrectGuesses = 0;
            o_NumOfCorrectLettersInWrongPositions = 0;
            foreach (char ch in m_SequenceStr)
            {
                // if the i_CompareTo's sequence has the current letter
                if (i_CompareTo.m_SequenceStr.Contains(ch.ToString()))
                {
                    // if the index in the i_CompareTo's sequence is the same as the current letter's
                    if (currentIndex == i_CompareTo.m_SequenceStr.IndexOf(ch))
                    {
                        o_NumOfCorrectGuesses++;
                    }
                    else
                    {
                        o_NumOfCorrectLettersInWrongPositions++;
                    }
                }

                currentIndex++;
            }
        }
    }
}
