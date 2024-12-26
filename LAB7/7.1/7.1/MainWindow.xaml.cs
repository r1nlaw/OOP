using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FileOperationsWPF
{
    public partial class MainWindow : Window
    {
        private List<string> createdFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Выбор текстового файла
        private void BrowseTextFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Выберите текстовый файл"
            };

            if (dialog.ShowDialog() == true)
            {
                TextFilePathTextBox.Text = dialog.FileName;
            }
        }

        // Выбор двоичного файла
        private void BrowseBinaryFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Binary Files (*.dat)|*.dat|All Files (*.*)|*.*",
                Title = "Выберите двоичный файл"
            };

            if (dialog.ShowDialog() == true)
            {
                BinaryFilePathTextBox.Text = dialog.FileName;
            }
        }

        // Создание текстового файла
        private void CreateTextFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = TextFilePathTextBox.Text;

            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                        OutputTextBox.Text += $"Файл {filePath} создан.\n";
                        AddFileToList(filePath);
                    }
                    else
                    {
                        OutputTextBox.Text += $"Файл {filePath} уже существует.\n";
                    }
                }
                else
                {
                    OutputTextBox.Text += "Укажите путь к файлу.\n";
                }
            }
            catch (Exception ex)
            {
                OutputTextBox.Text += $"Ошибка при создании файла: {ex.Message}\n";
            }
        }

        // Запись текста в файл
        private void WriteTextToFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = TextFilePathTextBox.Text;
            string textToWrite = TextToWriteTextBox.Text;

            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(textToWrite))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine(textToWrite);
                    }
                    OutputTextBox.Text += "Текст записан в файл.\n";
                }
                catch (Exception ex)
                {
                    OutputTextBox.Text += $"Ошибка при записи текста: {ex.Message}\n";
                }
            }
            else
            {
                OutputTextBox.Text += "Укажите путь к файлу и текст для записи.\n";
            }
        }

        // Чтение текста из файла
        private void ReadTextFromFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = TextFilePathTextBox.Text;

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string content = reader.ReadToEnd();
                        OutputTextBox.Text += "Содержимое файла:\n";
                        OutputTextBox.Text += content + "\n";
                    }
                }
                catch (Exception ex)
                {
                    OutputTextBox.Text += $"Ошибка при чтении файла: {ex.Message}\n";
                }
            }
            else
            {
                OutputTextBox.Text += "Укажите путь к существующему файлу.\n";
            }
        }

        // Запись двоичных данных в файл
        private void WriteBinaryData_Click(object sender, RoutedEventArgs e)
        {
            string filePath = BinaryFilePathTextBox.Text;

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    using (BinaryWriter writer = new BinaryWriter(fs))
                    {
                        writer.Write(42); // Пример целого числа
                        writer.Write(3.14); // Пример числа с плавающей запятой
                        writer.Write("Пример строки"); // Пример строки
                    }
                    OutputTextBox.Text += $"Двоичные данные записаны в файл {filePath}.\n";
                    AddFileToList(filePath);
                }
                catch (Exception ex)
                {
                    OutputTextBox.Text += $"Ошибка при записи двоичных данных: {ex.Message}\n";
                }
            }
            else
            {
                OutputTextBox.Text += "Укажите путь к файлу.\n";
            }
        }

        // Чтение двоичных данных из файла
        private void ReadBinaryData_Click(object sender, RoutedEventArgs e)
        {
            string filePath = BinaryFilePathTextBox.Text;

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int intValue = reader.ReadInt32();
                        double doubleValue = reader.ReadDouble();
                        string stringValue = reader.ReadString();

                        OutputTextBox.Text += "Прочитанные двоичные данные:\n";
                        OutputTextBox.Text += $"Целое число: {intValue}\n";
                        OutputTextBox.Text += $"Число с плавающей запятой: {doubleValue}\n";
                        OutputTextBox.Text += $"Строка: {stringValue}\n";
                    }
                }
                catch (Exception ex)
                {
                    OutputTextBox.Text += $"Ошибка при чтении двоичных данных: {ex.Message}\n";
                }
            }
            else
            {
                OutputTextBox.Text += "Укажите путь к существующему файлу.\n";
            }
        }

        // Создание нового файла
        private void CreateFile_Click(object sender, RoutedEventArgs e)
        {
            string fileName = NewFileNameTextBox.Text;

            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

                try
                {
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                        OutputTextBox.Text += $"Файл {filePath} создан.\n";
                        AddFileToList(filePath);
                    }
                    else
                    {
                        OutputTextBox.Text += $"Файл {filePath} уже существует.\n";
                    }
                }
                catch (Exception ex)
                {
                    OutputTextBox.Text += $"Ошибка при создании файла: {ex.Message}\n";
                }
            }
            else
            {
                OutputTextBox.Text += "Укажите имя файла.\n";
            }
        }

        // Добавление файла в список
        private void AddFileToList(string filePath)
        {
            if (!createdFiles.Contains(filePath))
            {
                createdFiles.Add(filePath);
                FileListBox.Items.Add(filePath);
            }
        }

        // Информация о файле
        private void ShowFileInfo_Click(object sender, RoutedEventArgs e)
        {
            string filePath = TextFilePathTextBox.Text;

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    OutputTextBox.Text += $"Информация о файле {filePath}:\n";
                    OutputTextBox.Text += $"Размер: {fileInfo.Length} байт\n";
                    OutputTextBox.Text += $"Дата создания: {fileInfo.CreationTime}\n";
                    OutputTextBox.Text += $"Дата последнего изменения: {fileInfo.LastWriteTime}\n";
                }
                catch (Exception ex)
                {
                    OutputTextBox.Text += $"Ошибка при получении информации о файле: {ex.Message}\n";
                }
            }
            else
            {
                OutputTextBox.Text += "Укажите путь к существующему файлу.\n";
            }
        }
    }
}