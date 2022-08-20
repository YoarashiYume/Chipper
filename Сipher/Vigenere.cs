using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сipher
{
    //Шифровщик по Алгоритму Виженера
    class Vigenere :  Caesar
    {
        class Temp//используется для получения ответов(приватное поле(как минимум ответы Виженера их перезапишут))
                  //на задания с шифром цезаря
        {
            /*для получения информации будет использоваться аналог
             reinterpret_cast<>() из C++. Ошибок выдавать не должен*/
            public readonly int key;
            public readonly int alphabetPower;
            public readonly char firstLetter;

            public List<string> answers;
        }
        private string stringKey = "";//текущтй ключ шифра
        protected new List<string> answers = null;

        private Caesar cs = null;
        public Vigenere(ref Caesar other)
        {
            cs = other;
        }

        protected new string encrypt(int index)//сам алгоритм шифрования Виженера
        {
            string newWord = "";
            string oldWord = answers.ElementAt(index);
            for (int i = 0; i < oldWord.Length; ++i)
            {
                key = (int)(stringKey[i]- firstLetter)+1;//Просто меняем ключ сдвига цезаря и используем метод родителя
                newWord += encryptCh(oldWord.ElementAt(i));
            }                
            return newWord;
        }
        //Аналогично методу цезаю
        public new string getWord(int index)
        {
            if (index >= answers.Count)
                return "";
            return encrypt(index);
        }

        public new bool checkWord(in string word, int index)
        {
            return word == answers.ElementAt(index);
        }
        private unsafe Temp reinterpret_cast()//safe????
        {
            //сам аналог reinterpret_cast, однако не было сказано о возможности исключения
            var sourceRef = __makeref(cs);
            var dest = default(Temp);
            var destRef = __makeref(dest);
            *(IntPtr*)&destRef = *(IntPtr*)&sourceRef;
            return __refvalue(destRef, Temp);
        }
        //формирование ключа для варианта
        public string formKey(int index)
        {
            string oldWord = answers.ElementAt(index);
            stringKey = reinterpret_cast().answers.ElementAt(index);//через каст получаем ключ
            //выравниваем размер
            while (stringKey.Length < oldWord.Length)
                stringKey += stringKey;
            stringKey = stringKey.Substring(0, oldWord.Length);
            return stringKey;
        }
        public void setAnswer(List<string> answer)
        {
            answers = answer;
        }
    }
}
