using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.BackColor = Color.Green;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            // Получаем текущий текст из TextBox

            string text = textBox1.Text;




            // Проверяем, является ли текст корректным вещественным числом со знаком
            if (!IsValidNumeric(text))
            {
                // Если текст некорректен, удаляем последний символ
                textBox1.Text = text.Substring(0, text.Length - 1);
                // Устанавливаем курсор в конец текста
                textBox1.SelectionStart = textBox1.Text.Length;

                MessageBox.Show("Введите корректное вещественное число!!!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.BackColor = Color.Green;


            }


        }

        private bool IsValidNumeric(string text)
        {
            // Разрешенные символы: цифры, точка, минус, плюс
            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '.' && c != '-' && c != '+')
                {
                    textBox1.BackColor = Color.Red;
                    return false;
                }

            }
            if (text.IndexOf('0') == 0)
            {
                // Проверяем, что после '0' сразу следует точка или строка заканчивается на '0'
                if (text.Length > 1 && text[1] != '.' && char.IsDigit(text[1]))
                {
                    textBox1.BackColor = Color.Red;
                    return false;
                }
            }

            // Проверяем, что минус и плюс находятся только в начале строки
            if (text.IndexOf('-') > 0 || text.IndexOf('+') > 0)
            {
                textBox1.BackColor = Color.Red;
                return false;
            }

            // Проверяем, что точка встречается только один раз
            if (text.IndexOf('.') != text.LastIndexOf('.'))
            {
                textBox1.BackColor = Color.Red;
                return false;
            }

            return true;
        }
        


    }
}
