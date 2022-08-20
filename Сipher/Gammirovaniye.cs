using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сipher
{ //Шифровщик по Алгоритму модульного гаммирования
    class Gammirovaniye
    {
        protected List<string> answers = null;
        protected int topBorder = 10;//верхняя граница коэфф. k0
        private int gammFoo(int value)//функция получения k i-го
        {
            return (7 * value + 1) % 32;
        }
        private List<int> gammSeq(int k0,int count)//генерируем последовательность
        {
            List<int> subsequence = new List<int>() { k0};
            for (int i = 0; i < count; ++i)
                subsequence.Add(gammFoo(subsequence.ElementAt(i)));
            return subsequence;
        }
        public int getK0(int variant)//генерация k0
        {
            ++variant;
            while (variant >= 10)
                variant -= 9;
            return variant % topBorder;
        }

        public string getWord(int variant)
        {
            var seq = gammSeq(getK0(variant), answers.ElementAt(variant).Length);
            string newWord = "";
            string oldWord = answers.ElementAt(variant);//алгоритм модульного гаммирования
            for (int i = 0; i < oldWord.Length; ++i)
            {
                int temp = oldWord.ElementAt(i) - 'А' + 1 + seq.ElementAt(i);
                if (temp > 32)
                    temp -= 32;
                newWord += (char)(temp + 'А' - 1);
            }
            return newWord;
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
