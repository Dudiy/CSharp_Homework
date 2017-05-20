using System;
using System.Collections.Generic;
using System.Text;

namespace B17_Ex02
{
    class LetterSequence
    {
        private const char k_MaxLetterInSequence = 'H';
        private const byte k_LengthOfSequence = 4;           //this value must be less than or equal to (k_MaxLetterInSequence-'A')
        private string m_Sequence = String.Empty;

        public LetterSequence()
        {
            Random randomizer = new Random();       //TODO add seed at the beginning of the program?
            char tempChar = (char)(randomizer.Next('A', k_MaxLetterInSequence));

            for (int i = 0; i < k_LengthOfSequence; i++)
            {
                while (m_Sequence.Contains(tempChar.ToString()))
                {
                    tempChar = (char)(randomizer.Next('A', k_MaxLetterInSequence));
                }

                m_Sequence = String.Concat(m_Sequence, tempChar);
            }
        }

        public LetterSequence(string i_Sequence)
        {
            m_Sequence = i_Sequence.Replace(" ", "");

            if (!IsValidSequence(i_Sequence,out string dummy))
            {
                //Throw execption;
            }

        public LetterSequence(string i_Sequence)
        {
            //valid =   sequence of exactly "k_LengthOfSequence" letters, upper or lower case letters between 'A' and "k_MaxLetterInSequence"
            //          same letter can appear more than once
            bool isValidSequence = false;

            /*
            while (i_Sequence.Length != k_LengthOfSequence)
            {
                //TODO throw execption?
                Console.WriteLine("The sequence given is too long, please try again: ");
                i_Sequence = Console.ReadLine();
            }

            while (!isValidSequence)
            {
                foreach (char ch in i_Sequence)
                {
                    // TODO change to ">=" and "<="
                    if ('a' > ch && ch < Char.ToLower(k_MaxLetterInSequence) ||
                        'A' > ch && ch < k_MaxLetterInSequence)
                    {
                        isValidSequence = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid sequence, please use upper/lower case letters between 'A' and '{0}':", k_MaxLetterInSequence);
                        i_Sequence = Console.ReadLine();
                    }
                }
            }
            */
        }

        public static byte LengthOfSequence
        {
            get { return k_LengthOfSequence; }
        }

        public static bool IsValidSequence(string i_Sequence, out string o_validationString)
        {
            //valid =   sequence of exactly "k_LengthOfSequence" letters, upper or lower case letters between 'A' and "k_MaxLetterInSequence"
            //          same letter can appear more than once
            bool isValid = true;
            string validationResultString = "The input is valid";

            i_Sequence = i_Sequence.Replace(" ", "");           //remove all spaces in the input string
            if (i_Sequence.Length != k_LengthOfSequence)        //check length of sequence
            {
                isValid = false;
                validationResultString = "length of sequence is invalid";
            }
            else
            {                                                   
                foreach (char ch in i_Sequence)                 //check letters of sequence
                {
                    if (ch < 'a' || Char.ToLower(k_MaxLetterInSequence) > ch &&
                        ch < 'A' && k_MaxLetterInSequence > ch)
                    {
                        validationResultString = String.Format("Invalid sequence, please use upper/lower case letters between 'A' and '{0}':", k_MaxLetterInSequence);
                        break;
                    }
                }
            }

            o_validationString = validationResultString;
            return isValid;
        }

        public string Compare(LetterSequence i_CompareTo)
        {
            StringBuilder result = new StringBuilder();
            byte correctGuessCounter = 0;
            byte correctLetterWrongPositionCounter = 0;
            byte currentIndex = 0;

            // TODO the order that foreach scan is the order of the string ? 
            // maybe for make sure 
            // if (m_Sequence.IndexOf(ch) == i_CompareTo.m_Sequence.IndexOf(ch))
            foreach (char ch in m_Sequence)
            {
                if (i_CompareTo.m_Sequence.Contains(ch.ToString()))         //if the computer's sequence has the current letter
                {
                    if (currentIndex == i_CompareTo.m_Sequence.IndexOf(ch))        //if the index in the computer's sequence is the same as the current letter's
                    {
                        correctGuessCounter++;
                    }
                    else
                    {
                        correctLetterWrongPositionCounter++;
                    }
                }

                currentIndex++;
            }

            result.Capacity = correctGuessCounter + correctLetterWrongPositionCounter;
            result.Append('V', correctGuessCounter);
            result.Append('X', correctLetterWrongPositionCounter);

            return result.ToString();
        }
    }
}
