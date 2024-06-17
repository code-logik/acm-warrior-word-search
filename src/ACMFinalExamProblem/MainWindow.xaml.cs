/**
 * Mark Sarasua
 * Dr. Alrifai
 * CS 4253
 * Final Exam Alternative Project
 * MainWindow.xaml.cs
 * 
 */

using System;
using System.Collections.Generic;
using System.Windows;

namespace ACMFinalExamProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database _Database = new Database();
        private List<Puzzle> _Puzzles = new List<Puzzle>();

        public MainWindow()
        {
            InitializeComponent();
            _Puzzles = _Database.LoadPuzzleData();
            LoadStartUpPuzzle();
        }

        private void LoadStartUpPuzzle() 
        {
            CreateCrossWordPuzzle(1);
            string[][] list = _Puzzle.WordList();
            CreateWordListButtons(list);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCrossWordPuzzle(Convert.ToInt32(_Puzzle.P));
            string[][] list = _Puzzle.WordList();
            CreateWordListButtons(list);
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int current = Convert.ToInt32(_Puzzle.P);
            int puzzles = Convert.ToInt32(_Puzzle.Puzzles);
            int random = -1;

            while (random == -1 || random == current) 
            {
                random = rand.Next(1, puzzles + 1);
            }
            
            CreateCrossWordPuzzle(random);
            string[][] list = _Puzzle.WordList();
            CreateWordListButtons(list);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            int current_puzzle = Convert.ToInt32(_Puzzle.P);
            int previous_puzzle = (current_puzzle == 1) ? 10 : current_puzzle - 1;
            CreateCrossWordPuzzle(previous_puzzle);
            string[][] list = _Puzzle.WordList();
            CreateWordListButtons(list);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            int current_puzzle = Convert.ToInt32(_Puzzle.P);
            int next_puzzle = (current_puzzle == 10) ? 1 : current_puzzle + 1;
            CreateCrossWordPuzzle(next_puzzle);
            string[][] list = _Puzzle.WordList();
            CreateWordListButtons(list);
        }
    }
}
