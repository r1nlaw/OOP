using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;


class UIHandler
{
    public static void InitializeGrid(Grid grid, int columns, int rows)
    {
        if (grid == null)
            return;

        grid.Children.Clear();
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();

        // Создание объекта Random для генерации случайных чисел
        Random random = new Random();

        // Добавляем столбцы
        for (int x = 0; x < columns; x++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        }

        // Добавляем строки
        for (int y = 0; y < rows; y++)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }

        // Заполнение ячеек случайными числами
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // Генерация случайного числа (вещественного или целого)
                double randomValue = random.NextDouble() * 10; // Вещественное число от 0 до 10
                                                               // или можно использовать random.Next(0, 100) для целых чисел от 0 до 99

                TextBox textBox = new TextBox
                {
                    Text = randomValue.ToString("F2"), // Округляем до двух знаков после запятой
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };

                Border border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Child = textBox
                };

                border.SetValue(Grid.RowProperty, y);
                border.SetValue(Grid.ColumnProperty, x);

                grid.Children.Add(border);
            }
        }
    }

    public static void DisplayResult(Grid grid, double[,] matrix)
    {
        if (grid == null || matrix == null)
            throw new ArgumentException("Grid or array is null");

        // Очищаем содержимое грида
        grid.Children.Clear();
        grid.ColumnDefinitions.Clear();
        grid.RowDefinitions.Clear();

        int rows = matrix.GetLength(0); // Получаем количество строк массива
        int columns = matrix.GetLength(1); // Получаем количество столбцов массива

        // Добавляем строки и столбцы в грид
        for (int x = 0; x < columns; x++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int y = 0; y < rows; y++)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        }

        // Заполняем грид значениями из массива
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                TextBox textBox = new TextBox
                {
                    Text = matrix[y, x].ToString("F2"), // Округляем до двух знаков после запятой
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                };

                Border border = new Border
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Child = textBox
                };

                // Устанавливаем положение TextBox в гриде
                border.SetValue(Grid.RowProperty, y);
                border.SetValue(Grid.ColumnProperty, x);

                // Добавляем элемент в грид
                grid.Children.Add(border);
            }
        }
    }
    public static double[,] getValuesFromGrid(Grid grid)
    {
        if (grid == null || grid.RowDefinitions.Count == 0 || grid.ColumnDefinitions.Count == 0)
            throw new ArgumentException("Сетка либо равна нулю, либо неправильно инициализирована");

        int rows = grid.RowDefinitions.Count;
        int columns = grid.ColumnDefinitions.Count;

        double[,] values = new double[rows, columns]; // Инициализируем двумерный массив

        foreach (UIElement element in grid.Children)
        {
            if (element is Border border && border.Child is TextBox textBox)
            {
                int row = Grid.GetRow(border);
                int column = Grid.GetColumn(border);

                if (double.TryParse(textBox.Text, out double result))
                {
                    values[row, column] = result; // Присваиваем значение
                }
                else
                {
                    values[row, column] = 0; // Если парсинг не удался
                }
            }
        }

        return values;
    }
}