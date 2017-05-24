using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    public class LetterSequence
    {
        private string m_Sequence;
        private const byte k_LengthOfSequence = 4;           // this value must be less than or equal to (k_MaxLetterInSequence-'A')
        private const char k_MaxLetterInSequence = 'H';

        public LetterSequence()
        {
            Random randomizer = new Random();       // TODO add seed at the beginning of the program?
            char tempChar = (char)randomizer.Next('A', k_MaxLetterInSequence);

            for (int i = 0; i < k_LengthOfSequence; i++)
            {
                while (m_Sequence.Contains(tempChar.ToString()))
                {
                    tempChar = (char)randomizer.Next('A', k_MaxLetterInSequence);
                }

                m_Sequence = string.Concat(m_Sequence, tempChar);
            }
        }

        // asumption - given sequence is a valid sequence, with no spaces and all uppercase letters
        public LetterSequence(string i_Sequence)
        {
            m_Sequence = i_Sequence;
        }
        
        public string SequenceStr
        {
            get { return m_Sequence; }
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
        public static bool IsValidSequence(string i_Sequence)
        {
            // valid = sequence of upper or lower case letters between 'A' and "k_MaxLetterInSequence"
            bool isValid = true;

            // check letters of sequence
            foreach (char ch in i_Sequence)
            {
                if (!(ch >= 'A' && ch <= k_MaxLetterInSequence))
                {
                    isValid = false;
                    break;
                }
            }

            return isValid;
        }

        public void Compare(LetterSequence i_CompareTo, 
            out byte o_CorrectGuessCounter, out byte o_CorrectLetterWrongPositionCounter)
        {
            StringBuilder result = new StringBuilder();
            byte currentIndex = 0;

            o_CorrectGuessCounter = 0;
            o_CorrectLetterWrongPositionCounter = 0;
            foreach (char ch in m_Sequence)
            {
                // if the i_CompareTo's sequence has the current letter
                if (i_CompareTo.m_Sequence.Contains(ch.ToString()))                 
                {
                    // if the index in the i_CompareTo's sequence is the same as the current letter's
                    if (currentIndex == i_CompareTo.m_Sequence.IndexOf(ch))         
                    {
                        o_CorrectGuessCounter++;
                    }
                    else
                    {
                        o_CorrectLetterWrongPositionCounter++;
                    }
                }

                currentIndex++;
            }
        }
    }
}
