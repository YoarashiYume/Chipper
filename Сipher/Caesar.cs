using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сipher
{
    class Caesar
    {//Шифровщик по Алгоритму Цезаря rot3

        protected int key = 3;//смещение алгоритма
        protected int alphabetPower = 33;//можнось алфавита
        protected char firstLetter = 'А';//для коррекции char`ов

        protected List<string> answers = null;

        //сдвиг символов на key позиций
        protected char encryptCh(char ch)
        {
            int encrypted = (int)ch + key;
            return encrypted >= alphabetPower + firstLetter -1? (char)(encrypted - alphabetPower +1) : (char)encrypted;
        }
        protected char decryptCh(char ch)
        {
            int decrypted = (int)ch - key;
            return decrypted < firstLetter ? (char)(firstLetter + alphabetPower -(firstLetter - decrypted)-1) : (char)decrypted;
        }

        protected string encrypt(int index)
        {
            string newWord = "";
            foreach (char ch in answers.ElementAt(index))
                newWord += encryptCh(ch);
            return newWord;
        }
        //возвращает слово по варианту, шифруя его
        public string getWord(int index)
        {
            if (index >= answers.Count)
                return"";
            return encrypt(index);
        }

        public bool checkWord(in string word, int index)
        {
            return word == answers.ElementAt(index);
        }
        public void setAnswer(List<string> answer)
        {
            answers = answer;
        }

    }
}
