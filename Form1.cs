using System;
using System.Windows.Forms;

namespace Работа_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Показываем элементы для radioButton1
                label9.Visible = true;
                maskedTextBox2.Visible = true;

                // Скрываем элементы для radioButton2
                label8.Visible = false;
                maskedTextBox1.Visible = false;
            }
        }
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                // Показываем элементы для radioButton2
                label8.Visible = true;
                maskedTextBox1.Visible = true;

                // Скрываем элементы для radioButton1
                label9.Visible = false;
                maskedTextBox2.Visible = false;
            }
        }
         private void button1_Click(object sender, EventArgs e)
        {
            string First = textBox1.Text;
            string Second = textBox2.Text;
            string Last = textBox3.Text;
            string Phone = maskedTextBox2.Text.Trim();
            string Birth = maskedTextBox1.Text.Trim();

            // Проверка пустых значений для имени, фамилии и отчества
            if (string.IsNullOrWhiteSpace(First) || string.IsNullOrWhiteSpace(Second) || string.IsNullOrWhiteSpace(Last))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            string login = string.Empty;

            // Генерация логина заказчика, если выбран radioButton1
            if (radioButton1.Checked)
            {
                // Номер телефона обязателен для заказчика
                if (string.IsNullOrWhiteSpace(Phone) || Phone.Length < 3)
                {
                    MessageBox.Show("Номер телефона должен быть заполнен и содержать хотя бы 3 цифры.");
                    return;
                }

                login = GenerateCustomerLogin(First, Second, Last, Phone);
                textBox4.Text = login;
            }

            // Генерация логина сотрудника, если выбран radioButton2
            if (radioButton2.Checked)
            {
                // Дата рождения не обязательна для сотрудника, но если она есть, она используется
                login = GenerateEmployeeLogin(First, Second, Last, Birth);
                textBox4.Text = login;
            }

            // Генерация пароля
            string password = GeneratePassword();
            textBox5.Text = password;
        }

        // Генерация логина для сотрудника
        private string GenerateEmployeeLogin(string firstName, string lastName, string middleName, string birthDate)
        {
            // Проверка обязательных полей для сотрудника
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(middleName))
            {
                MessageBox.Show("Все поля для сотрудника должны быть заполнены.");
                return string.Empty;
            }

            try
            {
                string login = lastName[0].ToString().ToUpper() +
                               firstName[0].ToString().ToUpper() +
                               middleName[0].ToString().ToUpper();

                // Получаем последние 2 цифры года рождения из строки
                if (!string.IsNullOrWhiteSpace(birthDate) && birthDate.Length >= 10)
                {
                    string yearOfBirth = birthDate.Substring(6, 4);  // Год рождения (yyyy)
                    string yearSuffix = yearOfBirth.Substring(2, 2); // последние две цифры года
                    login += yearSuffix;
                }

                return login;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при генерации логина для сотрудника: " + ex.Message);
                return string.Empty;
            }
        }

        // Генерация логина для заказчика
        private string GenerateCustomerLogin(string firstName, string lastName, string middleName, string phone)
        {
            // Проверка обязательных полей для заказчика
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(middleName) || string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Все поля для заказчика должны быть заполнены.");
                return string.Empty;
            }

            
                string login = lastName[0].ToString().ToUpper() +firstName[0].ToString().ToUpper() + middleName[0].ToString().ToUpper();
                // Получаем последние 3 цифры номера телефона
                string phoneSuffix = phone.Length >= 3 ? phone.Substring(phone.Length - 3, 3) : phone;
                login += phoneSuffix;
                return login;
        }

        private string GeneratePassword()
        {
            Random random = new Random();

            // 2 строчные буквы
            char firstChar = (char)random.Next('a', 'z' + 1);
            char secondChar = (char)random.Next('a', 'z' + 1);

            // 4 заглавные буквы
            char thirdChar = (char)random.Next('A', 'Z' + 1);
            char fourthChar = (char)random.Next('A', 'Z' + 1);
            char fifthChar = (char)random.Next('A', 'Z' + 1);
            char sixthChar = (char)random.Next('A', 'Z' + 1);

            // 2 случайные цифры
            char seventhChar = (char)random.Next('0', '9' + 1);
            char eighthChar = (char)random.Next('0', '9' + 1);

            // Формирование пароля в нужном формате
            return $"{firstChar}{secondChar}{thirdChar}{fourthChar}{seventhChar}{eighthChar}{fifthChar}{sixthChar}";
        }
    }
}

