using System;
using System.Text;
using System.Windows;
using StressTest;
using System.Linq;

namespace StressTestApp
{
    public partial class MainWindow : Window
    {
        // Массив для хранения результатов тестов
        private TestCaseResult[] results;

        // Массив возможных причин неудачи
        private static readonly string[] failureReasons =
        {
            "Неправильный монтаж",
            "Недостаточная прочность материала",
            "Коррозия металла",
            "Перегрузка конструкции",
            "Износ материала"
        };

        public MainWindow()
        {
            InitializeComponent();
            results = new TestCaseResult[10]; // Массив для 10 тестов
        }

        // Обработчик изменения выбора
        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                // Проверяем, что выбранный элемент не пустой
                if (MaterialsListBox.SelectedItem == null || CrossSectionsListBox.SelectedItem == null || TestResultsListBox.SelectedItem == null)
                {
                    TestDetailsLabel.Content = "Please select all options.";
                    return;
                }

                // Получение выбранного материала
                Material selectedMaterial = (Material)Enum.Parse(typeof(Material), ((System.Windows.Controls.ListBoxItem)MaterialsListBox.SelectedItem).Content.ToString());

                // Получение выбранного поперечного сечения
                CrossSection selectedCrossSection = (CrossSection)Enum.Parse(typeof(CrossSection), ((System.Windows.Controls.ListBoxItem)CrossSectionsListBox.SelectedItem).Content.ToString());

                // Получение выбранного результата теста
                TestResult selectedTestResult = (TestResult)Enum.Parse(typeof(TestResult), ((System.Windows.Controls.ListBoxItem)TestResultsListBox.SelectedItem).Content.ToString());

                // Создание строки для отображения
                StringBuilder selectionStringBuilder = new StringBuilder();

                // Добавление материала в строку
                selectionStringBuilder.Append($"Material: {selectedMaterial}, ");

                // Добавление поперечного сечения в строку
                selectionStringBuilder.Append($"Cross Section: {selectedCrossSection}, ");

                // Добавление результата теста в строку
                selectionStringBuilder.Append($"Test Result: {selectedTestResult}");

                // Отображение результата в Label
                TestDetailsLabel.Content = selectionStringBuilder.ToString();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show("Error occurred: " + ex.Message);
            }
        }

        // Обработчик клика по кнопке "Run Tests"
        private void RunTests_Click(object sender, RoutedEventArgs e)
        {
            // Заполняем массив результатов тестов
            Random rand = new Random();

            for (int i = 0; i < results.Length; i++)
            {
                // Генерация случайного результата
                var result = (TestResult)rand.Next(0, 2); // Рандомный выбор между Pass и Fail

                // Генерация случайной причины для неудачного теста
                string reason = null;
                if (result == TestResult.Fail)
                {
                    // Если результат Fail, выбираем случайную причину из массива
                    reason = failureReasons[rand.Next(failureReasons.Length)];
                }

                results[i] = new TestCaseResult(result, reason);
            }

            // Обновляем интерфейс
            int passCount = 0;
            int failCount = 0;
            ReasonsListBox.Items.Clear(); // Очищаем список причин

            foreach (var testCase in results)
            {
                if (testCase.Result == TestResult.Pass)
                {
                    passCount++;
                }
                else
                {
                    failCount++;
                    // Добавляем причину в список только если она не пустая
                    if (!string.IsNullOrEmpty(testCase.ReasonForFailure))
                    {
                        ReasonsListBox.Items.Add(testCase.ReasonForFailure);
                    }
                }
            }

            // Обновляем метки с количеством успешных и неудачных тестов
            TestDetailsLabel.Content = $"Passed: {passCount}, Failed: {failCount}";
        }
    }
}
