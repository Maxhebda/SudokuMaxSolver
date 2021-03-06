# SudokuMaxSolver
Project written in c# Visual Studio / WPF

<img src="./projectScreenImage/SudokuMaxSolver6.png" width=600/>

Download applications here -> [SudokuMaxSolver.exe](./SudokuMaxSolver/bin/Debug/SudokuMaxSolver.exe)

* It is a unique project on a global scale.
* I saw only one of these on the website.
* The project is created only by me and I do not use any outside help because such help does not exist.
* I am looking for solutions and creating my own algorithms for manual Sudoku solving.
* Completed algorithm using the "twins" method [link1](https://www.sudokudragon.com/guidehiddentwins.htm) [link2](http://dwojcik.ugu.pl/sudoku/basic/ns.php)
* Completed algorithm using the "XY-Wings" methods [link](http://dwojcik.ugu.pl/sudoku/tough/xywing.php)
* I am currently working on creating an algorithm using the "Naked pairs" methods [link](https://www.learn-sudoku.com/naked-pairs.html)
* In the future, I plan to rewrite the program for android.

<img src="./projectScreenImage/SudokuMaxSolver2.png" width=300/>

## The program is under construction :
### Was completed
* dynamic generation of buttons (9x9) and popup (3x3) on the window
* generating sudoku with a certain degree of difficulty (easy..impossible)
* generating the correct board (has only one solution)
* short menu with icons
* brute force sudoku solution (add separate static function to be used in other projects)
* sudoku solution (brute force)
* sudoku solution with steps and description :
- [x] method 01 - TheOnlyPossible [link]((http://dwojcik.ugu.pl/sudoku/basic/sp.php))
- [x] method 02 - SingleCandidateInRow [link](http://dwojcik.ugu.pl/sudoku/basic/sc.php)
- [x] method 03 - SingleCandidateInColumn
- [x] method 04 - SingleCandidateInSquare
- [x] method 05 - Twins [link](https://www.sudokudragon.com/guidehiddentwins.htm)
- [x] method 06 - X-Wings (blocking candidates)
- [x] method 07 - Y-Wings (blocking candidates) [link](http://dwojcik.ugu.pl/sudoku/tough/xywing.php)
- [x] method 08 - Double Forcing Chains [link](http://dwojcik.ugu.pl/sudoku/extreme/fchain.php)
- [x] method 09 - Triple Forcing Chains [link](http://hodoku.sourceforge.net/en/tech_chains.php)
- [x] method 10 - Naked pairs in row (blocking candidates) [link](https://www.learn-sudoku.com/naked-pairs.html)
- [x] method 11 - Naked pairs in column (blocking candidates)
- [x] method 12 - Naked pairs in square (blocking candidates)

Forcing chanin method example :
<img src="./projectScreenImage/forcingChainExample.png" width=200/>

### What else needs to be done
* board analysis (checking how many solutions there are (method is finished)), determining the degree of difficulty)
* sudoku solution with steps and description
* I have to come up with new methods for solving difficult SUDOKU and then develop algorithms

## Screen from 23.10.2020r :
<img src="./projectScreenImage/SudokuMaxSolver1.png" width=250/>  <img src="./projectScreenImage/SudokuMaxSolver3.png" width=250/>  <img src="./projectScreenImage/SudokuMaxSolver5.png" width=500/>  <img src="./projectScreenImage/SudokuMaxSolver4.png" width=500/>