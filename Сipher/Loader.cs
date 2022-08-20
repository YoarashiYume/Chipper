using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сipher
{
    /*Загрузчик ответов. Сказать больше нечего.
     * ответы представлены в виде списка, каждая строка соответствует номеру варианту
     */

    class Loader
    {
        private static List<string> caesarAnswer = new List<string>()
        {//ответы на задания с шифром цезаря
            "КРИПТОГРАФИЯ",
            "ИНФОРМАЦИЯ",
            "БЕЗОПАСНОСТЬ",
            "КОНФИДЕНЦИАЛЬНОСТЬ",
            "ПОЛЬЗОВАТЕЛЬ",
            "АДМИНИСТРАТОР",
            "ПРОТОКОЛ",
            "УЯЗВИМОСТЬ",
            "ФУНКЦИЯ",
            "ЗЛОУМЫШЛЕННИК",
            "МАРШРУТИЗАТОР",
            "ИДЕНТИФИКАЦИЯ",
            "СЕРТИФИКАЦИЯ",
            "ЛИЦЕНЗИЯ",
            "ГЕНЕРАТОР",
            "АНТИВИРУС",
            "АНАЛИЗ",
            "ДОСТУП",
            "ШИФРОГРАММА",
            "ДЕШИФРОВЩИК",
            "БИГРАММА",
            "ГАММИРОВАНИЕ",
            "ЭНИГМА",
            "МАТРИЦА",
            "АЛГОРИТМ"
        };
        private static List<string> vigenereAnswer = new List<string>()
        {//ответы на задания с шифром виженера
            "ИНФОРМАЦИЯ",
            "БЕЗОПАСНОСТЬ",
            "КОНФИДЕНЦИАЛЬНОСТЬ ",
            "ПОЛЬЗОВАТЕЛЬ",
            "АДМИНИСТРАТОР",
            "ПРОТОКОЛ",
            "УЯЗВИМОСТЬ",
            "ФУНКЦИЯ",
            "ЗЛОУМЫШЛЕННИК",
            "МАРШРУТИЗАТОР",
            "ИДЕНТИФИКАЦИЯ",
            "СЕРТИФИКАЦИЯ",
            "ЛИЦЕНЗИЯ",
            "ГЕНЕРАТОР",
            "АНТИВИРУС",
            "АНАЛИЗ",
            "ДОСТУП",
            "ШИФРОГРАММА",
            "ДЕШИФРОВЩИК",
            "БИГРАММА",
            "ГАММИРОВАНИЕ",
            "ЭНИГМА",
            "МАТРИЦА",
            "АЛГОРИТМ",
            "КРИПТОГРАФИЯ"
        };
        private static List<string> gammAnswer = new List<string>()
        {//ответы на задания с шифром модульного гаммирования
            "КРИПТОГРАФИЯ",
            "ИНФОРМАЦИЯ",
            "БЕЗОПАСНОСТЬ",
            "КОНФИДЕНЦИАЛЬНОСТЬ",
            "ПОЛЬЗОВАТЕЛЬ",
            "АДМИНИСТРАТОР",
            "ПРОТОКОЛ",
            "УЯЗВИМОСТЬ",
            "ФУНКЦИЯ",
            "ЗЛОУМЫШЛЕННИК",
            "МАРШРУТИЗАТОР",
            "ИДЕНТИФИКАЦИЯ",
            "СЕРТИФИКАЦИЯ",
            "ЛИЦЕНЗИЯ",
            "ГЕНЕРАТОР",
            "АНТИВИРУС",
            "АНАЛИЗ",
            "ДОСТУП",
            "ШИФРОГРАММА",
            "ДЕШИФРОВЩИК",
            "БИГРАММА",
            "ГАММИРОВАНИЕ",
            "ЭНИГМА",
            "МАТРИЦА",
            "АЛГОРИТМ"
        };
        //соответствующие загрузчики
        public void loadCaesar(ref Caesar cs)
        {
            cs.setAnswer(caesarAnswer);
        }
        public void loadGamm(ref Gammirovaniye ga)
        {
            ga.setAnswer(gammAnswer);
        }
        public void loadVigenere(ref Vigenere vi)
        {
            vi.setAnswer(vigenereAnswer);
        }
        public int maxVariant()
        {//определяем максимально возможный варинт - наименьший список ответоа
            if (gammAnswer.Count < vigenereAnswer.Count)
                return gammAnswer.Count < caesarAnswer.Count ? gammAnswer.Count : caesarAnswer.Count;
            return vigenereAnswer.Count < caesarAnswer.Count ? vigenereAnswer.Count : caesarAnswer.Count;
        }
    }
}
