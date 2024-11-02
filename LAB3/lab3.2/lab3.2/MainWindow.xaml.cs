using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static UIHandler;
using static MMatrix;

namespace Matrix
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeGrid(grid1, 1, 1);
            InitializeGrid(grid2, 1, 1);
            InitializeGrid(grid3, 1, 1);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                double[,] a = new double[Row1.SelectedIndex + 1, Column1.SelectedIndex + 1];
                double[,] b = new double[Row_matrix2.SelectedIndex + 1, Column2.SelectedIndex + 1];
                InitializeGrid(grid1, Column1.SelectedIndex + 1, Row1.SelectedIndex + 1);
                InitializeGrid(grid2, Column2.SelectedIndex + 1, Row_matrix2.SelectedIndex + 1);
            }
            catch { return; }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            double[,] a = new double[Row1.SelectedIndex + 1, Column1.SelectedIndex + 1];
            double[,] b = new double[Column1.SelectedIndex + 1, Column2.SelectedIndex + 1];
            a = getValuesFromGrid(grid1);
            b = getValuesFromGrid(grid2);
            try
            {
                double[,] c = Multiply(a, b);
                DisplayResult(grid3, c);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}