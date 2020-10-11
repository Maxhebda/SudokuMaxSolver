using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Threading;
using System;
using System.Windows.Input;

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

        bool lookForSolutions = true;   //variable to look for solutions by all means in the loop
        bool lookForSolutionsAfterCandidatesBlocked = false;   //variable to look for a solution after candidates are blocked

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

            // hidden right stackpanel
            showRightStackPanelWithSolutions(false);

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
            if (popupMain.IsOpen == true)
            {
                popupMain.IsOpen = false;
            }

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
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

            popupMain.IsOpen = false;
        }

        private void menuGenerujNowaPlansze_Click(object sender, RoutedEventArgs e)
        {
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

            this.Cursor = Cursors.Wait;
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
            this.Cursor = Cursors.Arrow;
        }

        private void menuNowaPlansza_Click(object sender, RoutedEventArgs e)
        {
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

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
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

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
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

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
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

            //close popup if is open
            popupMain.IsOpen = false;

            blockVisible();
            refreshBoard();
        }
        private void blockVisible()
        {
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

            for (byte y = 0; y < 9; y++)
                for (byte x = 0; x < 9; x++)
                {
                    if (board.get(y, x) != 0)
                    {
                        board.setReadOnly(y, x, true);
                    }
                }
        }
        private void menuOdblokujWidoczne_Click(object sender, RoutedEventArgs e)
        {
            //close right stackpanel with solutions
            showRightStackPanelWithSolutions(false);

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
            blockVisible();
            lookForSolutions = true;
            lookForSolutionsAfterCandidatesBlocked = true;

            List<SolutionInformation> listManualSolution = new List<SolutionInformation>();
            SolutionInformation tmp;

            Debug.WriteLine("########################################################");
            Debug.WriteLine("Manual solving test...");

            //--- start while
            while (lookForSolutions)
            {
                lookForSolutions = false;
                Debug.WriteLine("Test [01] The only possible...");
                //test only posible
                tmp = new SolutionInformation();
                tmp = Sudoku_AI.ManualSolver01_TheOnlyPossible(ref board);

                if (tmp.Get_pointsChanged().Count > 0)
                {
                    Debug.Write("[01]Changes : ");
                    for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    listManualSolution.Add(tmp);
                    Debug.WriteLine("z listy po dodaniu : ilosc =" + listManualSolution[listManualSolution.Count-1].Get_pointsChanged().Count);
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                Debug.WriteLine("Test [02] Single candidate in row...");
                //test single in row
                tmp = new SolutionInformation();
                tmp = Sudoku_AI.ManualSolver02_SingleCandidateInRow(ref board);
                if (tmp.Get_pointsChanged().Count > 0)
                {
                    Debug.Write("[02]Changes : ");
                    for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    listManualSolution.Add(tmp);
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                Debug.WriteLine("Test [03] Single candidate in column...");
                //test single in column
                tmp = new SolutionInformation();
                tmp = Sudoku_AI.ManualSolver03_SingleCandidateInColumn(ref board);
                if (tmp.Get_pointsChanged().Count > 0)
                {
                    Debug.Write("[03]Changes : ");
                    for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    listManualSolution.Add(tmp);
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                Debug.WriteLine("Test [04] Single candidate in square...");
                //test single in square
                tmp = new SolutionInformation();
                tmp = Sudoku_AI.ManualSolver04_SingleCandidateInSquare(ref board);
                if (tmp.Get_pointsChanged().Count > 0)
                {
                    Debug.Write("[04]Changes : ");
                    for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    listManualSolution.Add(tmp);
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                Debug.WriteLine("Test [05] Twins in square...");
                //test twins in square
                List<SolutionInformation> listTmp = new List<SolutionInformation>();
                listTmp = Sudoku_AI.ManualSolver05_TwinsInSquare(ref board);
                if (listTmp.Count > 0)
                {
                    foreach (var item in listTmp)
                    {
                        switch (item.Get_typeOfSolution())
                        {
                            case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method01_TheOnlyPossible:
                                {
                                    if (item.Get_pointsChanged().Count > 0)
                                    {
                                        Debug.WriteLine("Test [05] The only possible...");
                                        Debug.Write("[05]Brothers : ");
                                        foreach (var item2 in item.Get_pointsDetected())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                        Debug.Write("[05]Changes : ");
                                        foreach (var item2 in item.Get_pointsChanged())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                    }
                                }
                                break;
                            case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method02_SingleCandidateInRow:
                                {
                                    if (item.Get_pointsChanged().Count > 0)
                                    {
                                        Debug.WriteLine("Test [05] Single candidate in row...");
                                        Debug.Write("[05]Brothers : ");
                                        foreach (var item2 in item.Get_pointsDetected())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                        Debug.Write("[05]Changes : ");
                                        foreach (var item2 in item.Get_pointsChanged())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                    }
                                }
                                break;
                            case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method03_SingleCandidateInColumn:
                                {
                                    if (item.Get_pointsChanged().Count > 0)
                                    {
                                        Debug.WriteLine("Test [05] Single Candidate in column...");
                                        Debug.Write("[05]Brothers : ");
                                        foreach (var item2 in item.Get_pointsDetected())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                        Debug.Write("[05]Changes : ");
                                        foreach (var item2 in item.Get_pointsChanged())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                    }
                                }
                                break;
                            case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method04_SingleCandidateInSquare:
                                {
                                    if (item.Get_pointsChanged().Count > 0)
                                    {
                                        Debug.WriteLine("Test [05] Single candidate in square...");
                                        Debug.Write("[05]Brothers : ");
                                        foreach (var item2 in item.Get_pointsDetected())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                        Debug.Write("[05]Changes : ");
                                        foreach (var item2 in item.Get_pointsChanged())
                                        {
                                            Debug.Write("(" + item2.Y + "," + item2.X + "->" + item2.Value + ") ");
                                        }
                                        Debug.WriteLine("");
                                    }
                                }
                                break;
                        }
                    }
                    foreach (SolutionInformation iter in listTmp)
                    {
                        listManualSolution.Add(iter);
                    }
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                Debug.WriteLine("Test [08] Double Forcing Chains...");
                //test Double Forcing Chains
                tmp = new SolutionInformation();
                tmp = Sudoku_AI.ManualSolver08_DoubleForcingChains(ref board);
                if (tmp.Get_pointsChanged().Count > 0)
                {
                    Debug.Write("[08]Start Chain pos : ");
                    for (int i = 0; i < tmp.Get_pointsDetected().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsDetected()[i].Y + "," + tmp.Get_pointsDetected()[i].X + "->" + tmp.Get_pointsDetected()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    Debug.Write("[08]Changes : ");
                    for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    listManualSolution.Add(tmp);
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                Debug.WriteLine("Test [09] Triple Forcing Chains...");
                //test Triple Forcing Chains
                tmp = new SolutionInformation();
                tmp = Sudoku_AI.ManualSolver09_TripleForcingChains(ref board);
                if (tmp.Get_pointsChanged().Count > 0)
                {
                    Debug.Write("[09]Start Chain pos : ");
                    for (int i = 0; i < tmp.Get_pointsDetected().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsDetected()[i].Y + "," + tmp.Get_pointsDetected()[i].X + "->" + tmp.Get_pointsDetected()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    Debug.Write("[09]Changes : ");
                    for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                    {
                        Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                    }
                    Debug.WriteLine("");
                    listManualSolution.Add(tmp);
                    lookForSolutions = true;
                    lookForSolutionsAfterCandidatesBlocked = true;
                }

                //do lionfish only when NO solutions are found
                if (lookForSolutions == false && lookForSolutionsAfterCandidatesBlocked)
                {
                    Debug.WriteLine("Test [06] X Wings...");
                    //test X Wings
                    tmp = new SolutionInformation();
                    tmp = Sudoku_AI.ManualSolver06_XWings(ref board);
                    if (tmp.Get_pointsChanged().Count > 0)
                    {
                        Debug.Write("[06]XWings pos : ");
                        for (int i = 0; i < tmp.Get_pointsDetected().Count; i++)
                        {
                            Debug.Write("(" + tmp.Get_pointsDetected()[i].Y + "," + tmp.Get_pointsDetected()[i].X + "->" + tmp.Get_pointsDetected()[i].Value + ") ");
                        }
                        Debug.WriteLine("");
                        Debug.Write("[06]Blocked : ");
                        for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                        {
                            Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                        }
                        Debug.WriteLine("");
                        listManualSolution.Add(tmp);

                        lookForSolutions = true;
                        lookForSolutionsAfterCandidatesBlocked = false;
                    }

                    Debug.WriteLine("Test [07] Y Wings...");
                    //test Y Wings
                    tmp = new SolutionInformation();
                    tmp = Sudoku_AI.ManualSolver06_XWings(ref board);
                    if (tmp.Get_pointsChanged().Count > 0)
                    {
                        Debug.Write("[07]YWings pos : ");
                        for (int i = 0; i < tmp.Get_pointsDetected().Count; i++)
                        {
                            Debug.Write("(" + tmp.Get_pointsDetected()[i].Y + "," + tmp.Get_pointsDetected()[i].X + "->" + tmp.Get_pointsDetected()[i].Value + ") ");
                        }
                        Debug.WriteLine("");
                        Debug.Write("[07]Blocked : ");
                        for (int i = 0; i < tmp.Get_pointsChanged().Count; i++)
                        {
                            Debug.Write("(" + tmp.Get_pointsChanged()[i].Y + "," + tmp.Get_pointsChanged()[i].X + "->" + tmp.Get_pointsChanged()[i].Value + ") ");
                        }
                        Debug.WriteLine("");
                        listManualSolution.Add(tmp);

                        lookForSolutions = true;
                        lookForSolutionsAfterCandidatesBlocked = false;
                        
                    }
                }

            }   //--- stop while

            showRightStackPanelWithSolutions(true);
            Debug.WriteLine("Ilosc rozwiazan na liscie : " + listManualSolution.Count);

            //--- right stackpanel clear
            RightStackPanel_Clear();

            //--- right stackpanel adding
            string descriptionTmp;
            string changedTmp;
            foreach (var item in listManualSolution)
            {
                descriptionTmp = "";
                changedTmp = "";
                switch(item.Get_typeOfSolution())
                {
                    case SolutionInformation.TypeOfSolution.Method01_TheOnlyPossible:
                        {
                            descriptionTmp = "Jedyna możliwa cyfra jaką możesz wpisać w pole : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method02_SingleCandidateInRow:
                        {
                            descriptionTmp = "Pojedynczy kandydat w rzędzie : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method03_SingleCandidateInColumn:
                        {
                            descriptionTmp = "Pojedynczy kandydat w kolumnie : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method04_SingleCandidateInSquare:
                        {
                            descriptionTmp = "Pojedynczy kandydat w kwadracie : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method01_TheOnlyPossible:
                        {
                            descriptionTmp = "Bliźniaki '" + item.Get_pointsDetected()[0].Value + "' w polach ";
                            foreach (var itemDetected in item.Get_pointsDetected())
                            {
                                descriptionTmp += $"({itemDetected.Y}, {itemDetected.X}) ";
                            }
                            descriptionTmp += "pozwoliły znaleźć jedyną możliwą cyfrę w polach : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method02_SingleCandidateInRow:
                        {
                            descriptionTmp = "Bliźniaki '" + item.Get_pointsDetected()[0].Value + "' w polach ";
                            foreach (var itemDetected in item.Get_pointsDetected())
                            {
                                descriptionTmp += $"({itemDetected.Y}, {itemDetected.X}) ";
                            }
                            descriptionTmp += "pozwoliły znaleźć pojedynczego kandydata w rzędzie : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method03_SingleCandidateInColumn:
                        {
                            descriptionTmp = "Bliźniaki '" + item.Get_pointsDetected()[0].Value + "' w polach ";
                            foreach (var itemDetected in item.Get_pointsDetected())
                            {
                                descriptionTmp += $"({itemDetected.Y}, {itemDetected.X}) ";
                            }
                            descriptionTmp += "pozwoliły znaleźć pojedynczego kandydata w kolumnie : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method05_Twins_for_Method04_SingleCandidateInSquare:
                        {
                            descriptionTmp = "Bliźniaki '" + item.Get_pointsDetected()[0].Value + "' w polach ";
                            foreach (var itemDetected in item.Get_pointsDetected())
                            {
                                descriptionTmp += $"({itemDetected.Y},{itemDetected.X}) ";
                            }
                            descriptionTmp += "pozwoliły znaleźć pojedynczego kandydata w kwadracie : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method06_XWings:
                        {
                            descriptionTmp = "Pozioma Skrzydlica '" + item.Get_pointsDetected()[0].Value + "' w polach ";
                            foreach (var itemDetected in item.Get_pointsDetected())
                            {
                                descriptionTmp += $"({itemDetected.Y},{itemDetected.X}) ";
                            }
                            descriptionTmp += "zablokowała '"+ item.Get_pointsDetected()[0].Value + "' w kolumnach : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method07_YWings:
                        {
                            descriptionTmp = "Pionowa Skrzydlica '" + item.Get_pointsDetected()[0].Value + "' w polach ";
                            foreach (var itemDetected in item.Get_pointsDetected())
                            {
                                descriptionTmp += $"({itemDetected.Y},{itemDetected.X}) ";
                            }
                            descriptionTmp += "zablokowała '" + item.Get_pointsDetected()[0].Value + "' w rzędach : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method08_DoubleForcingChains:
                        {
                            descriptionTmp = "Forsowny podwójny łańcuch " + item.Get_pointsDetected()[0].Value + "," + item.Get_pointsDetected()[1].Value + " w polu (" + item.Get_pointsDetected()[0].Y + "," + item.Get_pointsDetected()[0].X + ") pozwolił wpisać nam : ";
                        }
                        break;
                    case SolutionInformation.TypeOfSolution.Method09_TripleForcingChains:
                        {
                            descriptionTmp = "Forsowny potrójny łańcuch " + item.Get_pointsDetected()[0].Value + "," + item.Get_pointsDetected()[1].Value + "," + item.Get_pointsDetected()[2].Value + " w polu (" + item.Get_pointsDetected()[0].Y + "," + item.Get_pointsDetected()[0].X + ") pozwolił wpisać nam : ";
                        }
                        break;
                }

                if (item.Get_pointsChanged() != null)
                {
                    foreach (var itemChanged in item.Get_pointsChanged())
                    {
                        changedTmp += $"({itemChanged.Y},{itemChanged.X})->{itemChanged.Value} ";
                    }
                }

                RightStackPanel_Add(descriptionTmp + changedTmp);
                
            }

            refreshBoard();
        }

        private void RightStackPanel_Clear()
        {
            RightlistBoxSolutions.Items.Clear();
        }

        private void RightStackPanel_Add(String item)
        {
            RightlistBoxSolutions.Items.Add($"{RightlistBoxSolutions.Items.Count + 1,3}" + '\t' + item);
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //close popup if is open
            popupMain.IsOpen = false;

            showRightStackPanelWithSolutions(false);
            //RightlistBoxSolutions.Items.Add("sss");
        }

        private void testXWings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        //hide popup if the menu click
        public void PreviewMouseLeftButtonDownInMenu_Click(object sender, EventArgs e)
        {
            popupMain.IsOpen = false;
        }

        private void showRightStackPanelWithSolutions(bool OnOff)
        {
            if (OnOff)
            {
                this.ResizeMode = System.Windows.ResizeMode.CanResize;
                this.MaxWidth = 1000;
                this.Width = 1000;
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                this.dockPanelMain.Width = 980;
            }
            else
            {
                this.ResizeMode = System.Windows.ResizeMode.CanResize;
                this.MaxWidth = 480;
                this.Width = 480;
                this.ResizeMode = System.Windows.ResizeMode.NoResize;
                this.dockPanelMain.Width = 460;
            }
        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (popupMain.IsOpen)
            {
                // number y and x on the our main button
                byte y = byte.Parse((popupMain).Name[1] + "");
                byte x = byte.Parse((popupMain).Name[2] + "");
                Debug.WriteLine("key ="+e.Key+", wsp [yx]="+y+", "+x);

                //esc = close popup
                if (e.Key == System.Windows.Input.Key.Escape)
                {
                    popupMain.IsOpen = false;
                    return;
                }

                byte keyPressed;
                if (KeyEventArgsToByte(e) != null)
                {
                    keyPressed = (byte)KeyEventArgsToByte(e);

                    if (keyPressed==0 && board.get(y,x)!=0 && board.getReadOnly(y,x)==false)
                    {
                        board.set(y, x, 0);
                        buttonMain[y, x].Content = "";

                        // hidden popup
                        popupMain.IsOpen = false;

                        // hidden right stackpanel
                        showRightStackPanelWithSolutions(false);
                    }
                    if (board.isInColumn(y,x,keyPressed) || board.isInRow(y,x,keyPressed) || board.isInSquare(y,y,keyPressed))
                    {
                        // do nothing
                    }
                    else
                    {
                        board.set(y, x, keyPressed);
                        buttonMain[y, x].Content = (keyPressed == 0) ? "" : "" + keyPressed;
                        Debug.WriteLine(keyPressed);

                        // hidden popup
                        popupMain.IsOpen = false;

                        // hidden right stackpanel
                        showRightStackPanelWithSolutions(false);
                    }
                }
            }
        }

        //method converts key to byte               null = do nothing
        private byte? KeyEventArgsToByte(KeyEventArgs key)
        {
            if (key.Key == System.Windows.Input.Key.D1 || key.Key == System.Windows.Input.Key.NumPad1) return 1;
            if (key.Key == System.Windows.Input.Key.D2 || key.Key == System.Windows.Input.Key.NumPad2) return 2;
            if (key.Key == System.Windows.Input.Key.D3 || key.Key == System.Windows.Input.Key.NumPad3) return 3;
            if (key.Key == System.Windows.Input.Key.D4 || key.Key == System.Windows.Input.Key.NumPad4) return 4;
            if (key.Key == System.Windows.Input.Key.D5 || key.Key == System.Windows.Input.Key.NumPad5) return 5;
            if (key.Key == System.Windows.Input.Key.D6 || key.Key == System.Windows.Input.Key.NumPad6) return 6;
            if (key.Key == System.Windows.Input.Key.D7 || key.Key == System.Windows.Input.Key.NumPad7) return 7;
            if (key.Key == System.Windows.Input.Key.D8 || key.Key == System.Windows.Input.Key.NumPad8) return 8;
            if (key.Key == System.Windows.Input.Key.D9 || key.Key == System.Windows.Input.Key.NumPad9) return 9;
            if (key.Key == System.Windows.Input.Key.D0 || key.Key == System.Windows.Input.Key.NumPad0 || key.Key == System.Windows.Input.Key.X) return 0;

            //null = do nothing
            return null;
        }

        private void Button_Zwin_Click(object sender, RoutedEventArgs e)
        {
            showRightStackPanelWithSolutions(false);
        }
    }
}
