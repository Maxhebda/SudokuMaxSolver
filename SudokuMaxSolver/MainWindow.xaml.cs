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
        Button[,] buttonMain = new Button[9, 9];        //main board
        Popup popupMain = new Popup();
        Button[] buttonPopup = new Button[9];           //small board popup

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
        void generatePopup(byte yMain, byte xMain)
        {
            UniformGrid grid = new UniformGrid();
            grid.Columns = 3;
            grid.Width = 100;
            grid.Height = 100;
            for (byte iPopup = 0; iPopup < 9; iPopup++)
            {
                buttonPopup[iPopup] = new Button();
                buttonPopup[iPopup].Content = iPopup + 1;
                buttonPopup[iPopup].Width = 30;
                buttonPopup[iPopup].Height = 30;
                buttonPopup[iPopup].Name = "p" + (iPopup + 1);
                buttonPopup[iPopup].Click += bPopup_Click;
                grid.Children.Add(buttonPopup[iPopup]);
            }

            popupMain.Child = grid;
            popupMain.PlacementTarget = (Button)buttonMain[yMain,xMain];
            popupMain.Placement = PlacementMode.Right;
            popupMain.Width = 102;
            popupMain.Height = 102;
            popupMain.VerticalOffset = -10;
            popupMain.HorizontalOffset = -10;
            popupMain.IsOpen = true;
        }

        private void bPopup_Click(object sender, RoutedEventArgs e)
        {
            // hidden popup
            popupMain.IsOpen = false;
        }

        private void bMain_Click(object sender, RoutedEventArgs e)
        {
            // close old popup if is open
            if (popupMain.IsOpen == true) popupMain.IsOpen = false;

            // number y and x of our button
            byte y = byte.Parse(((Button)sender).Name[1] + "");
            byte x = byte.Parse(((Button)sender).Name[2] + "");
            board.set(y, x, x);
            buttonMain[y, x].Content = "a";
            Console.WriteLine(board.get(y, x));

            generatePopup(y, x);    //show the popup after clicking the button
        }
    }
}
