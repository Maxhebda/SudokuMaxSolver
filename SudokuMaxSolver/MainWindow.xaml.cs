using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Canvas canvas = new Canvas();
            canvas.Width = 450;
            canvas.Height = 450;
            canvas.Background = Brushes.White;
            stackPanelMain.Children.Add(canvas);
            Line[] myTmpLine = new Line[4];             //line for main board

            // generate 9x9 buttons
            for (byte y = 0; y < 9; y++)
            {
                for (byte x = 0; x < 9; x++)
                {
                    buttonMain[y, x] = new Button();
                    buttonMain[y, x].Content = (board.get(y,x)==0)?"": "" + board.get(y, x);
                    buttonMain[y, x].Width = 50;
                    buttonMain[y, x].Height = 50;
                    buttonMain[y, x].Click += bMain_Click;
                    buttonMain[y, x].Name = "b" + y + x;
                    buttonMain[y, x].FontSize = 14;
                    Canvas.SetLeft(buttonMain[y, x], x*50);
                    Canvas.SetTop(buttonMain[y, x], y*50);
                    canvas.Children.Add(buttonMain[y, x]);
                }
            }

            // generate 4 lines for the main board
            byte myTmpLineCounter = 0;
            for (byte i = 0; i < 4;  i++)
            {
                myTmpLine[i] = new Line();
                myTmpLine[i].X1 = i == 0 ? 149 : i == 1 ? 299 : 0;
                myTmpLine[i].X2 = i == 0 ? 149 : i == 1 ? 299 : 450;
                myTmpLine[i].Y1 = i <= 1 ? 0   : i == 2 ? 149 : 299;
                myTmpLine[i].Y2 = i <= 1 ? 450 : i == 2 ? 149 : 299;
                myTmpLine[myTmpLineCounter].Stroke = System.Windows.Media.Brushes.Black;
                myTmpLine[myTmpLineCounter].StrokeThickness = 3;
                canvas.Children.Add(myTmpLine[myTmpLineCounter++]);
            }

            this.Height = dockPanelMain.Height;
        }
        void generatePopup(byte yMain, byte xMain)
        {
            UniformGrid grid = new UniformGrid();
            grid.Columns = 3;
            grid.Width = 100;
            grid.Height = 100;
            grid.Background = Brushes.White;
            for (byte iPopup = 0; iPopup < 9; iPopup++)
            {
                buttonPopup[iPopup] = new Button();
                buttonPopup[iPopup].Content = iPopup + 1;
                buttonPopup[iPopup].Width = 30;
                buttonPopup[iPopup].Height = 30;
                buttonPopup[iPopup].Name = "p" + (iPopup + 1) + "_" + yMain + xMain;        // name button in popup is "p3_00"
                buttonPopup[iPopup].Margin = new Thickness(3);
                buttonPopup[iPopup].Click += bPopup_Click;
                grid.Children.Add(buttonPopup[iPopup]);
            }

            popupMain.Child = grid;
            popupMain.PlacementTarget = (Button)buttonMain[yMain,xMain];
            popupMain.Name = "p" + yMain + xMain;       // name popup is "p00"
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
            // number y and x of our button
            byte y = byte.Parse(((Button)sender).Name[1] + "");
            byte x = byte.Parse(((Button)sender).Name[2] + "");

            // double click close popup
            if (popupMain.Name == "p" + y + x && popupMain.IsOpen==true)
            {
                popupMain.IsOpen = false;
                popupMain.Name = null;
                return;
            }

            // close old popup if is open
            if (popupMain.IsOpen == true) popupMain.IsOpen = false;

            // board.set(y, x, x);
            // buttonMain[y, x].Content = "a";
            List<byte> l = board.valuesInSquare(y,x);
            for (int i=0; i<l.Count; i++)
            {
                Debug.Write(l[i] + ", ");
            }
            Debug.WriteLine(" ");

            generatePopup(y, x);    //show the popup after clicking the button
        }
    }
}
