# SudokuMaxSolver
Project written in c # Visual Studio / C# / WPF

<img src="./projectScreenImage/SudokuMaxSolver2.png" width=300/>

Download applications here -> [SudokuMaxSolver.exe](./SudokuMaxSolver/bin/Debug/SudokuMaxSolver.exe)

* It is a unique project on a global scale.
* I saw only one of these on the website.
* The project is created only by me and I do not use any outside help because such help does not exist.
* I am looking for solutions and creating my own algorithms for manual Sudoku solving.
* I am currently working on creating an algorithm using the "twins" method [1](https://www.sudokudragon.com/guidehiddentwins.htm) [2](http://dwojcik.ugu.pl/sudoku/basic/ns.php) (algorithm 80% complete)
* In the future, I plan to rewrite the program for android.

## The program is under construction :
### Was completed
* dynamic generation of buttons (9x9) and popup (3x3) on the window
* generating sudoku with a certain degree of difficulty (easy..impossible)
* generating the correct board (has only one solution)
* short menu with icons
* brute force sudoku solution (add separate static function to be used in other projects)
* sudoku solution (brute force)
* sudoku solution with steps and description :
- [x] method 01 - TheOnlyPossible [1]((http://dwojcik.ugu.pl/sudoku/basic/sp.php))
- [x] method 02 - SingleCandidateInRow [1](http://dwojcik.ugu.pl/sudoku/basic/sc.php)
- [x] method 03 - SingleCandidateInColumn
- [x] method 04 - SingleCandidateInSquare
- [ ] method 05 - Twins [1](https://www.sudokudragon.com/guidehiddentwins.htm) (algorithm 80% complete)
- [ ] method 06 - XY-Wing [1](http://dwojcik.ugu.pl/sudoku/tough/xywing.php)
### What else needs to be done
* board analysis (checking how many solutions there are (method is finished)), determining the degree of difficulty)
* sudoku solution with steps and description
* I have to come up with new methods for solving difficult SUDOKU and then develop algorithms

## Screen from 04.09.2020 :
<img src="./projectScreenImage/SudokuMaxSolver1.png" width=250/> <img src="./projectScreenImage/SudokuMaxSolver3.png" width=250/>