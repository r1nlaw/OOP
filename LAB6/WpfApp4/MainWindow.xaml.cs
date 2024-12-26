using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        private Ellipse[] balls;
        private int[] xPositions;
        private int[] yPositions;
        private int[] xDirections;
        private int[] yDirections;
        private CancellationTokenSource cancellationTokenSource;
        private Thread[] threads; // Массив потоков
        private Barrier barrier; // Синхронизация потоков

        public MainWindow()
        {
            InitializeComponent();
            InitializeBalls();
            cancellationTokenSource = new CancellationTokenSource();
            StartMovingBalls();
        }

        private void InitializeBalls()
        {
            balls = new Ellipse[] { first_ball, third_ball, fifth_ball, second_ball, fourth_ball };

            xPositions = new int[balls.Length];
            yPositions = new int[balls.Length];
            xDirections = new int[balls.Length];
            yDirections = new int[balls.Length];

            var random = new Random();

            for (int i = 0; i < balls.Length; i++)
            {
                xPositions[i] = (int)Canvas.GetLeft(balls[i]);
                yPositions[i] = (int)Canvas.GetTop(balls[i]);

                xDirections[i] = random.Next(0, 2) == 0 ? -1 : 1;
                yDirections[i] = random.Next(0, 2) == 0 ? -1 : 1;
            }

            barrier = new Barrier(balls.Length, (b) =>
            {
                // Анализ состояния потоков или действий после синхронизации
                Dispatcher.Invoke(() =>
                {
                    logs_label.Content = $"All balls reached synchronization point at {DateTime.Now}\n";
                });
            });
        }

        private void StartMovingBalls()
        {
            threads = new Thread[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                int index = i;
                threads[i] = new Thread(() => MoveBall(index, cancellationTokenSource.Token));
                threads[i].Start();
            }
        }

        private void MoveBall(int index, CancellationToken token)
        {
            try
            {
                int stepCount = 0;
                while (!token.IsCancellationRequested)
                {
                    xPositions[index] += xDirections[index] * 5;
                    yPositions[index] += yDirections[index] * 5;

                    // Обработка столкновений с границами
                    if (xPositions[index] <= 0 || xPositions[index] >= MainCanvas.ActualWidth - balls[index].Width)
                    {
                        xDirections[index] *= -1;
                    }
                    if (yPositions[index] <= 0 || yPositions[index] >= MainCanvas.ActualHeight - balls[index].Height)
                    {
                        yDirections[index] *= -1;
                    }

                    // Обновляем позиции на экране
                    Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(balls[index], xPositions[index]);
                        Canvas.SetTop(balls[index], yPositions[index]);
                    });

                    // Синхронизация после каждого шага
                    barrier.SignalAndWait(); // Потоки ждут друг друга

                    // Задержка для имитации движения
                    Thread.Sleep(50);
                    stepCount++;
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибок
                Dispatcher.Invoke(() =>
                {
                    logs_label.Content += $"Error in thread {index}: {ex.Message}\n";
                });
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cancellationTokenSource.Cancel(); // Отменяем все потоки

            // Ожидаем завершения всех потоков с помощью Join
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel(); // Отменяем все потоки

            // Ожидаем завершения всех потоков с помощью Join
            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
    }
}
