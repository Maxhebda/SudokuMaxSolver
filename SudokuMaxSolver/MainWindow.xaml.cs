using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuMaxSolver
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BoardTab board = new BoardTab();
        Button[,] buttonMain = new Button[9, 9];

        public MainWindow()
        {
            InitializeComponent();
            generateBoard();
        }

        void generateBoard()
        {   
            UniformGrid grid = new UniformGrid();
            grid.Width = 450;
            grid.Height = 450;
            grid.Columns = 9;
            stackPanelMain.Children.Add(grid);
            // generate 9x9 buttons
            for (byte y = 0; y < 9; y++)
            {
                for (byte x = 0; x < 9; x++)
                {
                    buttonMain[y, x] = new Button();
                    buttonMain[y, x].Content = "0";
                    buttonMain[y, x].Width = 50;
                    buttonMain[y, x].Height = 50;
                    buttonMain[y, x].Click += bMain_Click;
                    buttonMain[y, x].Name = "b" + y + x;
                    buttonMain[y, x].FontSize = 14;
                    grid.Children.Add(buttonMain[y, x]);
                }
            }
            this.Height = dockPanelMain.Height;
        }

        private void bMain_Click(object sender, RoutedEventArgs e)
        {
            // number y and x of our button
            byte y = byte.Parse(((Button)sender).Name[1] + "");
            byte x =byte.Parse(((Button)sender).Name[2] + "");
            board.set(y, x, x);
            buttonMain[y, x].Content = "a";
            Console.WriteLine(board.get(y, x));
        }
    }
}
