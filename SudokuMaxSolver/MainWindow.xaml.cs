using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SudokuMaxSolver
{
    public partial class MainWindow : Window
    {
        ToolTip toolTip = new ToolTip();
        BoardTab board = new BoardTab();
        Button[,] buttonMain = new Button[9, 9];        //main board
        Popup popupMain = new Popup();

        //delete
        Popup p = new Popup();
        TextBox text = new TextBox();

        Button[] buttonPopup = new Button[9];           //small board popup

        public MainWindow()
        {
            InitializeComponent();
            generateBoard();
            refreshBoard();
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
                    buttonMain[y, x].Content = (board.get(y, x) == 0) ? "" : "" + board.get(y, x);
                    buttonMain[y, x].Width = 50;
                    buttonMain[y, x].Height = 50;
                    buttonMain[y, x].Click += bMain_Click;
                    buttonMain[y, x].MouseRightButtonDown += bPopup_RightClick;     //close popup if you click right mouse
                    buttonMain[y, x].Name = "b" + y + x;
                    buttonMain[y, x].FontSize = 14;
                    Canvas.SetLeft(buttonMain[y, x], x * 50);
                    Canvas.SetTop(buttonMain[y, x], y * 50);
                    canvas.Children.Add(buttonMain[y, x]);
                }
            }

            // generate 4 lines for the main board
            byte myTmpLineCounter = 0;
            for (byte i = 0; i < 4; i++)
            {
                myTmpLine[i] = new Line();
                myTmpLine[i].X1 = i == 0 ? 149 : i == 1 ? 299 : 0;
                myTmpLine[i].X2 = i == 0 ? 149 : i == 1 ? 299 : 450;
                myTmpLine[i].Y1 = i <= 1 ? 0 : i == 2 ? 149 : 299;
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
            grid.MouseRightButtonDown += bPopup_RightClick;     //close popup if you click right mouse
            for (byte iPopup = 0; iPopup < 9; iPopup++)
            {
                // show "x" if you click to number
                buttonPopup[iPopup] = new Button();
                if (board.get(yMain, xMain) == iPopup + 1)
                {
                    buttonPopup[iPopup].Content = "X";
                    buttonPopup[iPopup].Name = "p0_" + yMain + xMain;        // name button in popup is "p0_00"
                    if (board.getReadOnly(yMain, xMain))
                    {
                        buttonPopup[iPopup].IsEnabled = false;
                    }

                }
                else
                {
                    if (board.getReadOnly(yMain,xMain) || board.isInRow(yMain, xMain, (byte)(iPopup + 1)) || board.isInColumn(yMain, xMain, (byte)(iPopup + 1)) || board.isInSquare(yMain, xMain, (byte)(iPopup + 1)))
                    {
                        buttonPopup[iPopup].IsEnabled = false;
                    }
                    else
                    {
                        buttonPopup[iPopup].IsEnabled = true;
                    }
                    buttonPopup[iPopup].Content = iPopup + 1;
                    buttonPopup[iPopup].Name = "p" + (iPopup + 1) + "_" + yMain + xMain;        // name button in popup is "p3_00"
                }
                buttonPopup[iPopup].FontWeight = FontWeights.Normal;
                buttonPopup[iPopup].Width = 30;
                buttonPopup[iPopup].Height = 30;
                buttonPopup[iPopup].Margin = new Thickness(3);
                buttonPopup[iPopup].Click += bPopup_Click;
                buttonPopup[iPopup].MouseRightButtonDown += bPopup_RightClick;      //close popup if you click right mouse
                grid.Children.Add(buttonPopup[iPopup]);
            }

            //set popup
            popupMain.Child = grid;
            popupMain.PlacementTarget = (Button)buttonMain[yMain, xMain];
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

            // number y and x on the our main button and number popup to change
            byte y = byte.Parse(((Button)sender).Name[3] + "");
            byte x = byte.Parse(((Button)sender).Name[4] + "");
            byte p = byte.Parse(((Button)sender).Name[1] + "");

            //change of number on the main board
            board.set(y, x, p);
            buttonMain[y, x].Content = (p == 0) ? "" : "" + p;
            //Debug.WriteLine(((Button)sender).Name);
        }
        private void bPopup_RightClick(object sender, RoutedEventArgs e)
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
            if (popupMain.Name == "p" + y + x && popupMain.IsOpen == true)
            {
                popupMain.IsOpen = false;
                popupMain.Name = "p";
                return;
            }

            // close old popup if is open
            if (popupMain.IsOpen == true) popupMain.IsOpen = false;

            //if button is read only then do not open it
            if (!board.getReadOnly(y,x))
            {
                generatePopup(y, x);    //show the popup after clicking the button
            }
        }
        private void refreshBoard()     //show board
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    buttonMain[y, x].Content = (board.get(y, x) == 0) ? "" : "" + board.get(y, x);
                    if (board.getReadOnly(y, x))
                    {
                        buttonMain[y, x].FontWeight = FontWeights.Bold;
                        buttonMain[y, x].Background = Brushes.Aqua;
                    }
                    else
                    {
                        buttonMain[y, x].FontWeight = FontWeights.Regular;
                        buttonMain[y, x].Background = Brushes.AliceBlue;
                    }
                }
        }

        private void menuProgram_Click(object sender, RoutedEventArgs e)
        {
           popupMain.IsOpen = false;
        }

        private void menuGenerujNowaPlansze_Click(object sender, RoutedEventArgs e)
        {
            Sudoku_AI newSudoku = new Sudoku_AI();
            switch (((MenuItem)sender).Name)
            {
                case "menuTrywialna":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.Trywialna);
                    break;

                case "menuBardzoLatwa":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.BardzoLatwa);
                    break;

                case "menuLatwa":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.Latwa);
                    break;

                case "menuPrzecietna":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.Przecietna);
                    break;

                case "menuDosycTrudna":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.DosycTrudna);
                    break;

                case "menuTrudna":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.Trudna);
                    break;

                case "menuBardzoTrudna":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.BardzoTrudna);
                    break;

                case "menuDiaboliczna":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.Diaboliczna);
                    break;

                case "menuNiemozliwa":
                    newSudoku.generateNewBoard(Sudoku_AI.difficultyLevel.Niemozliwa);
                    break;
            }
            board.load(newSudoku);
            refreshBoard();
        }

        private void menuNowaPlansza_Click(object sender, RoutedEventArgs e)
        {
            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    buttonMain[y, x].Content = "";
                }
            board.clear();
            refreshBoard();
        }

        private void menuWyczysc_Click(object sender, RoutedEventArgs e)
        {
            //close popup if is open
            popupMain.IsOpen = false;

            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    if (!board.getReadOnly(y,x))
                    {
                        board.set(y, x, 0);
                    }
                }
            refreshBoard();
        }

        private void menuRozwiazBrutalnie_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Number od solutions : " + Sudoku_AI.autoSolver_numberOfSolutions(board));

            if (Sudoku_AI.autoSolver_brutal(ref board))
            {
                Debug.WriteLine("\nIs solved!");
                refreshBoard();
            }
            else
            {
                Debug.WriteLine("\nnot solved");
            }

        }

        private void menuZablokujWidoczne_Click(object sender, RoutedEventArgs e)
        {
            //close popup if is open
            popupMain.IsOpen = false;

            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    if (board.get(y,x)!=0)
                    {
                        board.setReadOnly(y, x, true);
                    }
                }
            refreshBoard();
        }
        private void menuOdblokujWidoczne_Click(object sender, RoutedEventArgs e)
        {
            //close popup if is open
            popupMain.IsOpen = false;

            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    if (board.get(y, x) != 0)
                    {
                        board.setReadOnly(y, x,false);
                    }
                }
            refreshBoard();
        }
        private void menuRozwiazManualnie_Click(object sender, RoutedEventArgs e)
        {
            //close popup if is open
            popupMain.IsOpen = false;

            SolutionInformation tmp = new SolutionInformation();

            Debug.WriteLine("Manual solving test...");  
            
            //test only posible
            tmp = Sudoku_AI.ManualSolver01_TheOnlyPossible(ref board);
            if (tmp.Count() > 0)
            {
                for (int i = 0; i < tmp.Count(); i++)
                {
                    Debug.WriteLine("I found a solution: (" + tmp.Get_Y(i) + "," + tmp.Get_X(i) + ")->" + tmp.Get_Value(i) + " (" + tmp.Get_Destription(i) + ")");
                }
            }

            //test single in row
            tmp.Clear();
            tmp = Sudoku_AI.ManualSolver02_SingleCandidateInRow(ref board);
            if (tmp.Count() > 0)
            {
                for (int i = 0; i < tmp.Count(); i++)
                {
                    Debug.WriteLine("I found a solution: (" + tmp.Get_Y(i) + "," + tmp.Get_X(i) + ")->" + tmp.Get_Value(i) + " (" + tmp.Get_Destription(i) + ")");
                }
            }

            //test single in column
            tmp.Clear();
            tmp = Sudoku_AI.ManualSolver03_SingleCandidateInColumn(ref board);
            if (tmp.Count()>0)
            {
                for( int i = 0 ; i < tmp.Count(); i++)
                {
                    Debug.WriteLine("I found a solution: (" + tmp.Get_Y(i) + "," + tmp.Get_X(i) + ")->" + tmp.Get_Value(i) + " (" + tmp.Get_Destription(i) + ")");
                }
            }
            
            //test single in square
            tmp.Clear();
            tmp = Sudoku_AI.ManualSolver04_SingleCandidateInSquare(ref board);
            if (tmp.Count() > 0)
            {
                for (int i = 0; i < tmp.Count(); i++)
                {
                    Debug.WriteLine("I found a solution: (" + tmp.Get_Y(i) + "," + tmp.Get_X(i) + ")->" + tmp.Get_Value(i) + " (" + tmp.Get_Destription(i) + ")");
                }
            }


            refreshBoard();
        }
    }
}
