using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private Ellipse[] balls;

        private IBallManager ballManager;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded; // Подписка на событие Loaded
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            balls = new Ellipse[] { first_ball, third_ball, fifth_ball, second_ball, fourth_ball };
            ballManager = new BallManagerTask(MyCanvas, logs_label, balls);
            ballManager.StartMovingBalls();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ballManager.StopMovingBalls();
        }

        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            ballManager.StopMovingBalls();
        }
    }
}