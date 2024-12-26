using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp3
{
    public interface IBallManager
    {
        void StartMovingBalls();
        void StopMovingBalls();
    }
    public class BallManagerTask:IBallManager
    {
        private Ellipse[] balls;
        private int[] xPositions;
        private int[] yPositions;
        private int[] xDirections;
        private int[] yDirections;
        private CancellationTokenSource cancellationTokenSource;
        private readonly object lockObject = new object();
        private readonly Canvas canvas;
        private readonly Label logLabel;

        public BallManagerTask(Canvas canvas, Label logLabel, Ellipse[] balls)
        {
            this.balls = balls;
            this.canvas = canvas;
            this.logLabel = logLabel;
            InitializeBalls(balls);
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void InitializeBalls(Ellipse[] balls)
        {
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
        }

        public async void StartMovingBalls()
        {
            Task[] tasks = new Task[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                int index = i;
                tasks[i] = MoveBall(index, cancellationTokenSource.Token);
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation
            }
        }

        private async Task MoveBall(int index, CancellationToken token)
        {
            int stepCount = 0;
            while (!token.IsCancellationRequested)
            {
                lock (lockObject)
                {
                    xPositions[index] += xDirections[index] * 10;
                    yPositions[index] += yDirections[index] * 10;

                    if (xPositions[index] <= 0 || xPositions[index] >= canvas.ActualWidth - balls[index].Width)
                    {
                        xDirections[index] *= -1;
                    }
                    if (yPositions[index] <= 0 || yPositions[index] >= canvas.ActualHeight - balls[index].Height)
                    {
                        yDirections[index] *= -1;
                    }

                    double positionRatio = (double)xPositions[index] / canvas.ActualWidth; 
                    byte r = (byte)(255 * (1 - positionRatio));
                    byte g = (byte)(255 * (1 - positionRatio) / 2);
                    byte b = (byte)(255 * positionRatio);      
                    balls[index].Fill = new SolidColorBrush(Color.FromRgb(r, g, b));
                }

                if (stepCount % 100 == 0)
                {
                    await canvas.Dispatcher.InvokeAsync(() =>
                    {
                        logLabel.Content += $"Ball {index}: x = {xPositions[index]}, y = {yPositions[index]}\n";
                        AnalyzeThreadState(index);
                    });
                }

                await canvas.Dispatcher.InvokeAsync(() =>
                {
                    Canvas.SetLeft(balls[index], xPositions[index]);
                    Canvas.SetTop(balls[index], yPositions[index]);
                });

                await Task.Delay(5, token);
                stepCount++;
            }
        }
        private void AnalyzeThreadState(int index)
        {
            Thread currentThread = Thread.CurrentThread;
            canvas.Dispatcher.Invoke(() =>
            {
                logLabel.Content += $"Thread {index}: State = {currentThread.ThreadState}, IsAlive = {currentThread.IsAlive}\n";
            });
        }

        public void StopMovingBalls()
        {
            cancellationTokenSource.Cancel();
        }
    }

    //public class BallManagerThread : IBallManager
    //{
    //    private Ellipse[] balls;
    //    private int[] xPositions;
    //    private int[] yPositions;
    //    private int[] xDirections;
    //    private int[] yDirections;
    //    private Thread[] threads;
    //    private readonly object lockObject = new object();
    //    private readonly Canvas canvas;
    //    private readonly Label logLabel;
    //    private bool stopRequested = false;

    //    public BallManagerThread(Canvas canvas, Label logLabel, Ellipse[] balls)
    //    {
    //        this.balls = balls;
    //        this.canvas = canvas;
    //        this.logLabel = logLabel;
    //        InitializeBalls(balls);
    //    }

    //    private void InitializeBalls(Ellipse[] balls)
    //    {
    //        xPositions = new int[balls.Length];
    //        yPositions = new int[balls.Length];
    //        xDirections = new int[balls.Length];
    //        yDirections = new int[balls.Length];
    //        threads = new Thread[balls.Length];

    //        var random = new Random();

    //        for (int i = 0; i < balls.Length; i++)
    //        {
    //            xPositions[i] = (int)Canvas.GetLeft(balls[i]);
    //            yPositions[i] = (int)Canvas.GetTop(balls[i]);

    //            xDirections[i] = random.Next(0, 2) == 0 ? -1 : 1;
    //            yDirections[i] = random.Next(0, 2) == 0 ? -1 : 1;
    //        }
    //    }

    //    public void StartMovingBalls()
    //    {
    //        for (int i = 0; i < balls.Length; i++)
    //        {
    //            int index = i;
    //            threads[i] = new Thread(() => MoveBall(index));
    //            threads[i].Priority = (ThreadPriority)(i % 3); // Устанавливаем приоритет потока
    //            threads[i].Start();
    //        }
    //    }

    //    private void MoveBall(int index)
    //    {
    //        try
    //        {
    //            while (!stopRequested)
    //            {
    //                lock (lockObject)
    //                {
    //                    xPositions[index] += xDirections[index] * 5;
    //                    yPositions[index] += yDirections[index] * 5;

    //                    if (xPositions[index] <= 0 || xPositions[index] >= canvas.ActualWidth - balls[index].Width)
    //                    {
    //                        xDirections[index] *= -1;
    //                    }
    //                    if (yPositions[index] <= 0 || yPositions[index] >= canvas.ActualHeight - balls[index].Height)
    //                    {
    //                        yDirections[index] *= -1;
    //                    }
    //                }

    //                // Обновляем позиции в UI-потоке
    //                Application.Current.Dispatcher.InvokeAsync(() =>
    //                {
    //                    Canvas.SetLeft(balls[index], xPositions[index]);
    //                    Canvas.SetTop(balls[index], yPositions[index]);
    //                });

    //                Thread.Sleep(50); // Добавляем задержку
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Логируем ошибку в UI-потоке
    //            Application.Current.Dispatcher.InvokeAsync(() =>
    //            {
    //                logLabel.Content += $"Error in thread {index}: {ex.Message}\n";
    //            });
    //        }
    //    }

    //    public void StopMovingBalls()
    //    {
    //        stopRequested = true;
    //        foreach (var thread in threads)
    //        {
    //            if (thread != null && thread.IsAlive)
    //            {
    //                thread.Join(); // Ожидаем завершения потока
    //            }
    //        }
    //    }
    //}

}
