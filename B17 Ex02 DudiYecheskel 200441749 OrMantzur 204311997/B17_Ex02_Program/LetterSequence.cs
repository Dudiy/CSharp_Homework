/*
 * B17_Ex02: LetterSequence.cs
 * 
 * This class manages the logic for each guess of the player (letter sequence)
 * 
 * Written by:
 * 204311997 - Or Mantzur
 * 200441749 - Dudi Yecheskel 
*/

using System;
using System.Text;

namespace B17_Ex02
{
    public class LetterSequence
    {
        private const byte k_LengthOfSequence = 4;           // this value must be less than or equal to the number of valid unique letters
        private const char k_MaxLetterInSequence = 'H';
        private static Random s_Randomizer = new Random();
        private string m_SequenceStr = string.Empty;

        // empty ctor - generates a random letter sequence
        public LetterSequence()
        {
            char currentRandomChar = (char)s_Randomizer.Next('A', k_MaxLetterInSequence);

            for (int i = 0; i < k_LengthOfSequence; i++)
            {
                while (m_SequenceStr.Contains(currentRandomChar.ToString()))
                {
                    currentRandomChar = (char)s_Randomizer.Next('A', k_MaxLetterInSequence);
                }

                m_SequenceStr = string.Concat(m_SequenceStr, currentRandomChar);
            }
        }
        
        // ctor that gets a valid sequence of letters and creates a LetterSequence object accordingly
        public LetterSequence(string i_SequenceStr)
        {
            m_SequenceStr = i_SequenceStr;
        }

        // ==================================================== Getters Setters ====================================================
        // asumption - given sequence is a valid sequence, with no spaces and all uppercase letters
        public static byte LengthOfSequence
        {
            get { return k_LengthOfSequence; }
        }

        public static char MaxLetterInSequence
        {
            get { return k_MaxLetterInSequence; }
        }

        public string SequenceStr
        {
            get { return m_SequenceStr; }
        }

        // ==================================================== Methods ====================================================
        /* 
         * checks if a given string is a valid sequence. 
         * valid sequence = sequence of upper or lower case letters between 'A' and "k_MaxLetterInSequence".
         * assumption: given string is already "k_LengthOfSequence" long, 
         * has no spaces, and is all uppercase letters 
         */
        public static bool IsValidSequence(string i_SequenceStr)
        {
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

        /*
         * Method that compares two LetterSequence objects by m_SequenceStr.
         * out parameters:
         *  - o_NumOfCorrectGuesses = the number of letters that are equivalent in both strings
         *  - o_CorrectLettersInWrongPositions = the number of letters that are the same but not at the same index        
         */
        public void Compare(LetterSequence i_CompareTo, out byte o_NumOfCorrectGuesses, out byte o_CorrectLettersInWrongPositions)
        {
            StringBuilder result = new StringBuilder();
            byte currentIndex = 0;

            o_NumOfCorrectGuesses = 0;
            o_CorrectLettersInWrongPositions = 0;
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
                        o_CorrectLettersInWrongPositions++;
                    }
                }

                currentIndex++;
            }
        }
    }
}
