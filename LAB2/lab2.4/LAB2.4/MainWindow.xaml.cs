using System;
using System.Windows;
using System.Windows.Controls;

namespace MatrixMultiplication
{
    public partial class MainWindow : Window
    {
        private double[,] matrix1; 
        private double[,] matrix2; 
        private double[,] result;   

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes(); // Инициализация выпадающих списков для выбора размеров матриц
        }

        private void InitializeComboBoxes()
        {
            
            for (int i = 1; i <= 10; i++)
            {
                comboBoxRows1.Items.Add(i);
                comboBoxColumns1.Items.Add(i);
                comboBoxRows2.Items.Add(i);
                comboBoxColumns2.Items.Add(i);
            }
            comboBoxRows1.SelectedIndex = 1; // Установка начальных значений
            comboBoxColumns1.SelectedIndex = 1;
            comboBoxRows2.SelectedIndex = 1;
            comboBoxColumns2.SelectedIndex = 1;
        }

        // Обработчик кнопки для ввода значений матриц
        private void buttonInputValues_Click(object sender, RoutedEventArgs e)
        {
            int rows1 = comboBoxRows1.SelectedIndex + 1; // Получение количества строк для первой матрицы
            int columns1 = comboBoxColumns1.SelectedIndex + 1; // Получение столбцов 
            int rows2 = comboBoxRows2.SelectedIndex + 1; // Получение количества строк для второй матрицы
            int columns2 = comboBoxColumns2.SelectedIndex + 1; // Получение столбцов 

            // Проверка на совместимость матриц для умножения
            if (columns1 != rows2)
            {
                MessageBox.Show("Количество столбцов в Матрице 1 должно быть равно количеству строк в Матрице 2.");
                return; // Выход, если размеры не соответствуют
            }

            // Инициализация матриц нужного размера
            matrix1 = new double[rows1, columns1];
            matrix2 = new double[rows2, columns2];

            // Инициализация текстовых полей для ввода значений матриц
            InitializeMatrix(grid1, matrix1);
            InitializeMatrix(grid2, matrix2);
        }

        // Обработчик кнопки для вычисления результата умножения
        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение значений из текстовых полей для каждой матрицы
                getValuesFromGrid(grid1, matrix1);
                getValuesFromGrid(grid2, matrix2);

                // Умножение матриц
                MultiplyMatrices();
                // Отображение результата умножения
                initializeGrid(grid3, result);
            }
            catch (FormatException ex)
            {
                // Обработка ошибки ввода, если введены некорректные значения
                MessageBox.Show("Ошибка ввода. Пожалуйста, убедитесь, что все значения являются числами.");
            }
            catch (Exception ex)
            {
                // Общая обработка исключений
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        // Метод для инициализации матрицы и создания текстовых полей
        private void InitializeMatrix(Grid grid, double[,] matrix)
        {
            grid.Children.Clear(); // Очистка предыдущих элементов
            grid.ColumnDefinitions.Clear(); // Очистка предыдущих определений столбцов
            grid.RowDefinitions.Clear(); // Очистка предыдущих определений строк

            int rows = matrix.GetLength(0); // Получение количества строк
            int columns = matrix.GetLength(1); // Получение количества столбцов

            // Создание определений столбцов
            for (int x = 0; x < columns; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            // Создание определений строк
            for (int y = 0; y < rows; y++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            // Заполнение матрицы случайными значениями и создание текстовых полей для ввода
            Random rand = new Random();
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    TextBox t = new TextBox();
                    t.Text = (rand.NextDouble() * 10).ToString("F2"); // Заполнение случайным значением
                    t.VerticalAlignment = VerticalAlignment.Center;
                    t.HorizontalAlignment = HorizontalAlignment.Center;
                    t.SetValue(Grid.RowProperty, y); // Установка позиции текстового поля
                    t.SetValue(Grid.ColumnProperty, x);
                    grid.Children.Add(t); // Добавление текстового поля в сетку
                }
            }
        }

        // Метод для считывания значений из текстовых полей в матрицу
        private void getValuesFromGrid(Grid grid, double[,] matrix)
        {
            int columns = grid.ColumnDefinitions.Count; // Определение количества столбцов
            int rows = grid.RowDefinitions.Count; // Определение количества строк

            // Считывание значений из текстовых полей
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c]; // Получение текстового поля
                int row = Grid.GetRow(t); // Получение строки
                int column = Grid.GetColumn(t); // Получение столбца
                matrix[row, column] = double.Parse(t.Text); // Преобразование текста в число и сохранение в матрицу
            }
        }

        // Метод для умножения матриц
        private void MultiplyMatrices()
        {
            int m1rows = matrix1.GetLength(0); // Количество строк первой матрицы
            int m1columns_m2rows = matrix1.GetLength(1); // Количество столбцов первой матрицы / строк второй
            int m2columns = matrix2.GetLength(1); // Количество столбцов второй матрицы

            result = new double[m1rows, m2columns]; // Инициализация матрицы результата

            // Умножение матриц
            for (int row = 0; row < m1rows; row++)
            {
                for (int column = 0; column < m2columns; column++)
                {
                    double accumulator = 0; // Сумматор для хранения текущего значения
                    for (int cell = 0; cell < m1columns_m2rows; cell++)
                    {
                        accumulator += matrix1[row, cell] * matrix2[cell, column]; // Умножение и накопление
                    }
                    result[row, column] = accumulator; // Сохранение результата
                }
            }
        }

        // Метод для инициализации сетки результата
        private void initializeGrid(Grid grid, double[,] matrix)
        {
            if (grid != null)
            {
                grid.Children.Clear(); // Очистка предыдущих элементов
                grid.ColumnDefinitions.Clear(); // Очистка предыдущих определений столбцов
                grid.RowDefinitions.Clear(); // Очистка предыдущих определений строк

                int columns = matrix.GetLength(1); // Получение количества столбцов
                int rows = matrix.GetLength(0); // Получение количества строк

                // Создание определений столбцов
                for (int x = 0; x < columns; x++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }

                // Создание определений строк
                for (int y = 0; y < rows; y++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }

                // Заполнение сетки значениями результата
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        double cell = matrix[y, x]; // Получение значения из матрицы
                        TextBox t = new TextBox();
                        t.Text = cell.ToString("F2"); // Форматирование значения
                        t.VerticalAlignment = VerticalAlignment.Center;
                        t.HorizontalAlignment = HorizontalAlignment.Center;
                        t.SetValue(Grid.RowProperty, y); // Установка позиции текстового поля
                        t.SetValue(Grid.ColumnProperty, x);
                        grid.Children.Add(t); // Добавление текстового поля в сетку
                    }
                }
            }
        }
    }
}
