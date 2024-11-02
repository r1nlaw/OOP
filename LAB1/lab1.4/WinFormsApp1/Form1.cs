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

            // �������� ������� ����� �� TextBox

            string text = textBox1.Text;




            // ���������, �������� �� ����� ���������� ������������ ������ �� ������
            if (!IsValidNumeric(text))
            {
                // ���� ����� �����������, ������� ��������� ������
                textBox1.Text = text.Substring(0, text.Length - 1);
                // ������������� ������ � ����� ������
                textBox1.SelectionStart = textBox1.Text.Length;

                MessageBox.Show("������� ���������� ������������ �����!!!", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.BackColor = Color.Green;


            }


        }

        private bool IsValidNumeric(string text)
        {
            // ����������� �������: �����, �����, �����, ����
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
                // ���������, ��� ����� '0' ����� ������� ����� ��� ������ ������������� �� '0'
                if (text.Length > 1 && text[1] != '.' && char.IsDigit(text[1]))
                {
                    textBox1.BackColor = Color.Red;
                    return false;
                }
            }

            // ���������, ��� ����� � ���� ��������� ������ � ������ ������
            if (text.IndexOf('-') > 0 || text.IndexOf('+') > 0)
            {
                textBox1.BackColor = Color.Red;
                return false;
            }

            // ���������, ��� ����� ����������� ������ ���� ���
            if (text.IndexOf('.') != text.LastIndexOf('.'))
            {
                textBox1.BackColor = Color.Red;
                return false;
            }

            return true;
        }
        


    }
}
