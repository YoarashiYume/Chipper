using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Сipher
{
    public partial class Form1 : Form
    {

        private int maxVariant;
        private Caesar caesar = null;
        private Vigenere vigenere = null;
        private Gammirovaniye gamm = null;
        private List<Button> lstBtnCalc = null;
        private List<Label> labalList = null;
        private List<List<TextBox>> textsList = null;
        private List<TextBox> formulaText = null;
        private List<List<TextBox>> textsGammList = null;
        private int variant = 0; //выбранный вариант

        private void loadKeyboard()
        {
            //автоматическое заполнение интерфейсной клавиатуры
            for (int i = 0; i < lstBtnCalc.Count; ++i)
            {
                lstBtnCalc.ElementAt(i).Text = (char)((int)'А' + i) + "\r\n" + (i+1).ToString();
                //и установка соответствующих событий при нажатии
                lstBtnCalc.ElementAt(i).Click += new EventHandler(delegate (Object o, EventArgs a)
                {
                    //добавляем букву в необходимый бокс, при нажатии
                    txtCaesarInput.Text += ((Button)o).Text.ElementAt(0);
                });
            }
            
        }
        private void disableBoxInVigenere()
        {
            //деакутивация всех textBox на вкладке с алгоритмом Виженера
            foreach (var item in textsList)
                foreach (var el in item)
                    el.Enabled = false;
        }
        private void changeKeyboardStatus(bool isEnable = true)
        {
            foreach (var item in lstBtnCalc)
                item.Enabled = isEnable;
        }
        private void configSecondVigenere()
        {
            //настройка бкосов на вкладке с алгоритмом Виженера
            for (int i = 2; i < textsList.Count; ++i)
                foreach (var el in textsList[i])
                {
                    el.KeyPress += new KeyPressEventHandler(delegate (Object o, KeyPressEventArgs e)
                    {
                        //изначально допускаем только цифры и "горячие клавишт"
                        if (!char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                        {
                            if (e.KeyChar >= 'А' && e.KeyChar <= 'я')
                            {
                                //но если кириллица, то заменяем содержимое бокса соответствующим ASCII кодом
                                var txtBox = (TextBox)o;
                                txtBox.Text = ((int)Char.ToUpper(e.KeyChar) - (int)'А'+1).ToString();
                                txtBox.SelectionStart = txtBox.Text.Length;
                                txtBox.SelectionLength = 0;
                            }
                            e.Handled = true;
                        }
                    });
                    el.TextChanged += new EventHandler(delegate (Object o, EventArgs a)
                    {
                        //ограничиваем содержимое значениями от 0 до 32
                        var txtBox = (TextBox)o;
                        if (txtBox.Text.Length == 0)
                            return;
                        int value = Int32.Parse(txtBox.Text);
                        if (value == 0)
                            value = 1;
                        while (value > 32)
                            value /= 10;
                        txtBox.Text = value.ToString();
                        txtBox.SelectionStart = txtBox.Text.Length;
                        txtBox.SelectionLength = 0;
                    });
                }
        }


        private void configGamm()
        {
            //настройка бкосов на вкладке с алгоритмом  модульного гаммирования,аналогично настройке боксов Виженера
            for (int i = 1; i < textsGammList.Count-1; ++i)
                foreach (var el in textsGammList[i])
                {
                    el.KeyPress += new KeyPressEventHandler(delegate (Object o, KeyPressEventArgs e)
                    {
                        if (!char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                        {
                            if (e.KeyChar >= 'А' && e.KeyChar <= 'я')
                            {
                                var txtBox = (TextBox)o;
                                txtBox.Text = ((int)Char.ToUpper(e.KeyChar) - (int)'А' + 1).ToString();
                                txtBox.SelectionStart = txtBox.Text.Length;
                                txtBox.SelectionLength = 0;
                            }
                            e.Handled = true;
                        }
                    });
                    el.TextChanged += new EventHandler(delegate (Object o, EventArgs a)
                    {
                        var txtBox = (TextBox)o;
                        if (txtBox.Text.Length == 0)
                            return;
                        int value = Int32.Parse(txtBox.Text);
                        if (value == 0)
                            value = 1;
                        while (value > 32)
                            value /= 10;
                        txtBox.Text = value.ToString();
                        txtBox.SelectionStart = txtBox.Text.Length;
                        txtBox.SelectionLength = 0;
                    });
                }
        }
        private void configFormula()
        {
            //настраиваем текстбоксы с последовательностью
            foreach (var el in formulaText)
            {
                el.KeyPress += new KeyPressEventHandler(delegate (Object o, KeyPressEventArgs e)
                {
                    //допускаем только цифры
                    if (!char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                        e.Handled = true;
                });
            }
        }
        private void disableBoxInGamm()
        {
            //деакутивация всех textBox на вкладке с алгоритмом модульного гаммирования
            foreach (var item in textsGammList)
                foreach (var el in item)
                    el.Enabled = false;
            foreach (var item in formulaText)
                item.Enabled = false;
        }
        private void loadClasses()
        {
            //загружаем ответы в классы
            Loader l = new Loader();
            caesar = new Caesar();
            l.loadCaesar(ref caesar);
            gamm = new Gammirovaniye();
            l.loadGamm(ref gamm);

            vigenere = new Vigenere(ref caesar);
            l.loadVigenere(ref vigenere);

            maxVariant = l.maxVariant();
        }

        public Form1()
        {
            //инициализация и настройка компонентов
            InitializeComponent();
            loadClasses();
            btnCeaser.Enabled = false;
            btnVigenere.Enabled = false;
            btnGammirovanie.Enabled = false;
            mainLabel.Text = "Введите свой порядковый номер в журнале № (влияет на начальный вариант)";
            lstBtnCalc = new List<Button>() {
                button1, button2, button3, button4, button5, button6, button7, button8,
                button9, button10, button11, button12, button13, button14, button15, button16,
                button17, button18, button19, button20, button21, button22, button23, button24,
                button25, button26, button27, button28, button29, button30, button31, button32 };
            textsList = new List<List<TextBox>>() { null,null, null, null, null };
            textsList[0] = new List < TextBox>{ textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox7,textBox8,textBox9,textBox10,
                textBox11,textBox12,textBox13,textBox14,textBox15,textBox16,textBox17,textBox18};
            textsList[1] = new List<TextBox>{ textBox19,textBox20,textBox21,textBox22,textBox23,textBox24,textBox25,textBox26,textBox27,textBox28,
                textBox29,textBox30,textBox31,textBox32,textBox33,textBox34,textBox35,textBox36};
            textsList[2] = new List<TextBox>{ textBox37,textBox38,textBox39,textBox40,textBox41,textBox42,textBox43,textBox44,textBox45,textBox46,
                textBox47,textBox48,textBox49,textBox50,textBox51,textBox52,textBox53,textBox54};
            textsList[3] = new List<TextBox>{ textBox55,textBox56,textBox57,textBox58,textBox59,textBox60,textBox61,textBox62,textBox63,textBox64,
                textBox65,textBox66,textBox67,textBox68,textBox69,textBox70,textBox71,textBox72};
            textsList[4] = new List<TextBox>{ textBox73,textBox74,textBox75,textBox76,textBox77,textBox78,textBox79,textBox80,textBox81,textBox82,
                textBox83,textBox84,textBox85,textBox86,textBox87,textBox88,textBox89,textBox90};
            labalList = new List<Label> { label6, label7, label8, label9, label10, label11, label12, label13, label14,
            label15,label16,label17,label18,label23,label24,label25,label26,label27};
            formulaText =new List<TextBox>{ textBox91,textBox92,textBox93,textBox94,textBox95,textBox96,textBox97,textBox98,textBox99,textBox100,
                textBox101,textBox102,textBox103, textBox104,textBox105,textBox106,textBox107,textBox108};

            textsGammList = new List<List<TextBox>>() { null, null, null, null, null };
            textsGammList[0] = new List<TextBox>() {textBox109,textBox110,textBox111,textBox112,textBox113,textBox114,textBox115,textBox116,textBox117,textBox118,
                textBox119,textBox120,textBox121,textBox122,textBox123,textBox124,textBox125,textBox126 };
            textsGammList[1] = new List<TextBox>() {textBox127,textBox128,textBox129,textBox130,textBox131,textBox132,textBox133,textBox134,textBox135,textBox136,
                textBox137,textBox138,textBox139,textBox140,textBox141,textBox142,textBox143,textBox144 };
            textsGammList[2] = new List<TextBox>() {textBox145,textBox146,textBox147,textBox148,textBox149,textBox150,textBox151,textBox152,textBox153,textBox154,
                textBox155,textBox156,textBox157,textBox158,textBox159,textBox160,textBox161,textBox162 };
            textsGammList[3] = new List<TextBox>() {textBox163,textBox164,textBox165,textBox166,textBox167,textBox168,textBox169,textBox170,textBox171,textBox172,
                textBox173,textBox174,textBox175,textBox176,textBox177,textBox178,textBox179,textBox180 };
            textsGammList[4] = new List<TextBox>() {textBox181,textBox182,textBox183,textBox184,textBox185,textBox186,textBox187,textBox188,textBox189,textBox190,
                textBox191,textBox192,textBox193,textBox194,textBox195,textBox196,textBox197,textBox198 };
            lblVigenereTask.Text = "Используя шифр Виженера, расшифруйте данную криптограмму,\r\n взяв в качестве ключа слово из пункта 1.";
            loadKeyboard();
            configSecondVigenere();
            configGamm();
            configFormula();
            setFormula();
        }

        private void btnCeaser_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            tabControl2.SelectedTab = tabControl2.TabPages[0];
            lblCaesarTask.Text = "1.Используя шифр Цезаря, расшифруйте слово по данной криптограмме.\r\n";
            lblCaesarTask.Text += caesar.getWord(variant);
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            variant = Int32.Parse(variantText.Text) - 1;
            if (variant < 0)
            {
                MessageBox.Show("Некоррентный номер", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //подстраиваем введенный вариант до возможного
            variant = variant % maxVariant;
            btnAccept.Enabled = false;
            btnCeaser.Enabled = true;
            btnVigenere.Enabled = true;
            btnGammirovanie.Enabled = true;
            changeKeyboardStatus();
            lblVariant.Text = "Текущий вариант №" + (variant+1);
            if (variant == 0)
                btnPrevV.Enabled = false;
            if (variant == maxVariant)
                btnNextV.Enabled = false;
        }

        private void setFormula()
        {
            //Создание надписей у псевдослучайной последовательности
            label6.Text = "k1=";
            for (int i = 1; i < labalList.Count; ++i)
                labalList[i].Text = 'k'.ToString() + (i + 1) + "= (k" + i + "*7+1) mod 32= ";
        }

        private void variantText_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ограничение на ввод цифр для варианта
            if (!char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnCaesarCheck_Click(object sender, EventArgs e)
        {
            if (caesar.checkWord(txtCaesarInput.Text, variant))
                txtCaesarRes.Text = "Верно";
            else
                txtCaesarRes.Text = "Неверно";
        }

        private void txtCaesarInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Настройка текстового поля на вкладке с шифром Цезаря, для ручного ввода
            if (e.KeyChar >= 'А' && e.KeyChar <= 'я')
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
                e.Handled = false;
                return;
            }
            if ( !Char.IsControl(e.KeyChar))
                    e.Handled = true;
        }

        private void btnVigenere_Click(object sender, EventArgs e)
        {
            //переключение на алгоритм Виженера
            changeKeyboardStatus(false);
            disableBoxInVigenere();
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            tabControl2.SelectedTab = tabControl2.TabPages[1];

        }

        private void btnGetCryp_Click(object sender, EventArgs e)
        {
            //получение ключа и криптограммы, алгоритм Виженера
           string tempWord = vigenere.formKey(variant);
            for (int i = 0; i < tempWord.Length; ++i)
            {   //включение необходимого кол-ва текстбоксов и вывод ключа
                textsList[1][i].Text = tempWord[i].ToString();
                textsList[2][i].Enabled = true;
                textsList[3][i].Enabled = true;
                textsList[4][i].Enabled = true;
            }
            tempWord = vigenere.getWord(variant);
            for (int j = 0; j < tempWord.Length; ++j)
                textsList[0][j].Text = tempWord.ElementAt(j).ToString();            
        }

        private void btnResultVigenere_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < textsList[2].Count; ++i)
            {//для проверки результата должны быть заполнены все доступные боксы "открытого текста"
                if (textsList[2][i].Enabled == false)
                    break;
                if (textsList[4][i].Text.Length == 0)
                {
                    MessageBox.Show("Заполните все текстовые поля открытого текста", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string word = "";
            for (int i = 0; i < textsList[4].Count; ++i)
            {
                if (!textsList[4][i].Enabled)
                    break;
                word += (char)(Int32.Parse(textsList[4][i].Text) - 1 + 'А');
            }
            txtAnswer.Text = word;
        }

        private void btnCheckVigenere_Click(object sender, EventArgs e)
        {
            if (vigenere.checkWord(txtAnswer.Text, variant))
                txtResultVigenere.Text = "Верно";
            else
                txtResultVigenere.Text = "Неверно";
        }

        private void btnGammirovanie_Click(object sender, EventArgs e)
        {
            //переключение на алгоритм модульного гаммирования
            changeKeyboardStatus(false);
            lblGammirovanieTask.Text = "Расшифруйте слово по данной криптограмме с помощью шифра модульного \n" +
                "гаммирования, используя генератор: ki + 1 = (7ki + 1)mod32, где i ϵ { 1,2,3,...}\n, k1 = "+ gamm.getK0(variant) +
                " для получения псевдослучайной последовательности.";
            disableBoxInGamm();
            btnAnswerGamm.Enabled = false;
            btnCheckGamm.Enabled = false;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
            tabControl2.SelectedTab = tabControl2.TabPages[2];

        }
        private void enableTextInGamm(int count)
        {
            //включение необходимого кол-ва текстбоксов, алгоритм модульного гаммирования
            for (int i = 1; i < count; ++i)
                formulaText[i].Enabled = true;
            for (int i = 1; i < 4; ++i)
                for (int j = 0; j < count; ++j)
                    textsGammList[i][j].Enabled = true;
        }
        private void btnGetCrypto_Click(object sender, EventArgs e)
        {
            string word;//получение криптограммы и значения k1
            txtGammGryp.Text = word =gamm.getWord(variant);
            formulaText[0].Text = gamm.getK0(variant).ToString();
            enableTextInGamm(word.Length);
        }

        private void btnOT_Click(object sender, EventArgs e)
        {
            if (txtGammGryp.Text.Length == 0)
                return;//если криптография не была получена, то заканчиваем проверку
            foreach (var item in textsGammList[3])
            {//для проверки результата должны быть заполнены все доступные боксы " Ч.П.О.Т"
                if (!item.Enabled)
                    break;
                if (item.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все текстовые в строке Ч.П.О.Т", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            for (int i = 0; i < textsGammList[4].Count; ++i)
            {
                //преобразование ASCII в буквы
                if (!textsGammList[3][i].Enabled)
                    break;
                textsGammList[4][i].Text = ((char)(Int32.Parse(textsGammList[3][i].Text) - 1 + 'А')).ToString();
            }
            btnAnswerGamm.Enabled = true;
            btnCheckGamm.Enabled = true;
        }

        private void btnAnswerGamm_Click(object sender, EventArgs e)
        {
            //сбор ответа из отельных текстбоксов
            string word = "";
            for (int i = 0; i < textsGammList[4].Count; ++i)
            {
                if (!textsGammList[3][i].Enabled)
                    break;
                word += textsGammList[4][i].Text;
            }
            txtAnswerGamm.Text = word;
        }

        private void btnCheckGamm_Click(object sender, EventArgs e)
        {
            if (gamm.checkWord(txtAnswerGamm.Text, variant))
                txtResultGamm.Text = "Верно";
            else
                txtResultGamm.Text = "Неверно";
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            //очистка всех тесктбоксов
            foreach (var item in textsList)
                foreach (var el in item)
                    el.Clear();
            foreach (var item in textsList)
                foreach (var el in item)
                    el.Clear();
            foreach (var item in formulaText)
                item.Clear();
            foreach (var item in textsGammList)
                foreach (var el in item)
                    el.Clear();
            txtResultGamm.Clear();
            txtAnswerGamm.Clear();
            txtAnswer.Clear();
            txtResultVigenere.Clear();
            txtCaesarInput.Clear();
            txtCaesarRes.Clear();
            txtGammGryp.Clear();
            disableBoxInGamm();
            disableBoxInVigenere();
            btnAnswerGamm.Enabled = false;
            btnCheckGamm.Enabled = false;
        }

        /*
         * Тут переключение страниц и варианта
         * при достижении какой-либо границы - блокируются соответствующие кнопки
         * при отходе - разблокируются
         *          */
        private void btnPrevWindow_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabControl2.TabPages[0])
                tabControl1.SelectedTab = tabControl1.TabPages[0];
            else
                tabControl2.SelectedTab = tabControl2.TabPages[tabControl2.SelectedIndex - 1];
            if (!btnNextWindow.Enabled)
                btnNextWindow.Enabled = true;
            if (tabControl2.SelectedIndex == 0)
                changeKeyboardStatus(true);
        }

        private void btnNextWindow_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedTab = tabControl2.TabPages[tabControl2.SelectedIndex + 1];
            if (tabControl2.SelectedIndex == 2)
                btnNextWindow.Enabled = false;
            if (tabControl2.SelectedIndex != 0)
                changeKeyboardStatus(false);
        }
        //+ при смене варианта происходит обновление задания
        private void btnPrevV_Click(object sender, EventArgs e)
        {
            btnResetAll_Click(sender, e);
            --variant;
            lblVariant.Text = "Текущий вариант №" + (variant + 1);
            lblGammirovanieTask.Text = "Расшифруйте слово по данной криптограмме с помощью шифра модульного \n" +
                "гаммирования, используя генератор: ki + 1 = (7ki + 1)mod32, где i ϵ { 1,2,3,...}\n, k1 = " + gamm.getK0(variant) +
                " для получения псевдослучайной последовательности.";
            lblCaesarTask.Text = "1.Используя шифр Цезаря, расшифруйте слово по данной криптограмме.\r\n";
            lblCaesarTask.Text += caesar.getWord(variant);
            if (variant == 0)
                btnPrevV.Enabled = false;
            if (!btnNextV.Enabled)
                btnNextV.Enabled = true;

        }

        private void btnNextV_Click(object sender, EventArgs e)
        {
            btnResetAll_Click(sender, e);
            ++variant;
            lblVariant.Text = "Текущий вариант №" + (variant + 1);
            lblGammirovanieTask.Text = "Расшифруйте слово по данной криптограмме с помощью шифра модульного \n" +
                "гаммирования, используя генератор: ki + 1 = (7ki + 1)mod32, где i ϵ { 1,2,3,...}\n, k1 = " + gamm.getK0(variant) +
                " для получения псевдослучайной последовательности.";
            lblCaesarTask.Text = "1.Используя шифр Цезаря, расшифруйте слово по данной криптограмме.\r\n";
            lblCaesarTask.Text += caesar.getWord(variant);
            if (variant+1 == maxVariant)
                btnNextV.Enabled = false;
            if (!btnPrevV.Enabled)
                btnPrevV.Enabled = true;
        }
    }
}
