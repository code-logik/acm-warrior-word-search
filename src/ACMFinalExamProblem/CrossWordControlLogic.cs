/**
 * Mark Sarasua
 * Dr. Alrifai
 * CS 4253
 * Final Exam Alternative Project
 * CrossWordControlLogic.cs
 * 
 */

using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ACMFinalExamProblem
{
    partial class MainWindow    
    {
        private const string _Letter_HighLight_URI = "pack://application:,,,/Assets/Images/letter-frame-highlight.png";
        private const string _Letter_URI = "pack://application:,,,/Assets/Images/letter-frame.png";
        private const string _Canvas_URI = "pack://application:,,,/Assets/Images/canvas.png";
        private readonly int[] _Row_Range = { 3, 12 };
        private readonly int[] _Column_Range = { 3, 20 };

        private ControlTemplate _Control_Template_WordListButton = Application.Current.FindResource("WordListButton") as ControlTemplate;

        private ImageBrush _Letter_HighLight = null;
        private ImageBrush _Letter_Frame = null;
        private ImageBrush _Canvas = null;

        private Puzzle _Puzzle = null;
        private List<int> _Words_Found = null;
        private int _WordList_Count_Limit = 11;

        private Puzzle LoadPuzzle(int puzzle_number)
        {
            try
            {
                return _Puzzles[puzzle_number - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private Label CreateOutputLabel(string output) 
        {
            if (output == null) 
            {
                throw new ArgumentNullException();
            }

            Label label = new Label
            {
                Padding = new Thickness(10, 0, 0, 0),
                Foreground = Brushes.WhiteSmoke,
                FontFamily = new FontFamily("Cascadia Mono"),
                Margin = new Thickness(0, 5, 0, 5),
                Width = 200,
                Content = output
            };

            return label;
        }
        
        private void WordListButton_Click(object sender, RoutedEventArgs e) 
        {
            bool locate = true;
            int index = Convert.ToInt32((sender as Button).Tag);

            foreach (var word in _Words_Found) 
            {
                if (index == word) 
                {
                    locate = false;
                }
            }

            if (locate == true)
            {
                SearchResults results = _Puzzle.Locate($"{index}");
                Label label = CreateOutputLabel(results.WordLocation());
                OutputBaseStackPanel.Children.Add(label);
                PuzzleLetterHighlight(results);
                _Words_Found.Add(index);
            }
        }

        private Button CreateWordButton(string word, string index)
        {
            if (word == null || index == null) 
            {
                throw new ArgumentNullException();
            }

            Button button = new Button
            {
                Template = _Control_Template_WordListButton,
                Content = word,
                FontFamily = new FontFamily("Cascadia Mono"),
                Tag = index
            };

            button.Click += WordListButton_Click;

            return button;
        }

        private void CreateWordListButtons(string[][] word_list) 
        {
            if (word_list == null) 
            {
                throw new ArgumentNullException();
            }

            int limiter = (word_list.Length > _WordList_Count_Limit) ? _WordList_Count_Limit : word_list.Length;

            for (int i = 0; i < limiter; i++)
            {
                Button button = CreateWordButton(string.Join("", word_list[i]), $"{i}");

                if (i != (word_list.Length - 1)) 
                {
                    button.Margin = new Thickness(0, 0, 0, 5);
                }

                WordListBaseStackPanel.Children.Add(button);
            }
        }
        
        private void PuzzleLetterHighlight(SearchResults results) 
        {
            if (results == null) 
            {
                throw new ArgumentNullException();
            }
            
            if (CrossWordBaseGrid != null)
            {
                if (_Letter_HighLight == null) 
                {
                    _Letter_HighLight = LoadImageAsset(_Letter_HighLight_URI);
                }

                Grid crossword_canvas = CrossWordBaseGrid.Children[0] as Grid;
                Grid crossword_letter_grid = crossword_canvas.Children[0] as Grid;
                int index = ((results.R - 1) * results.Cols) + (results.C - 1);
                
                for (int i = 0; i < results.W.Length; i++)
                {
                    Grid crossword_letter = crossword_letter_grid.Children[index] as Grid;
                    Label crossword_letter_label = crossword_letter.Children[0] as Label;
                    crossword_letter.Background = _Letter_HighLight;
                    crossword_letter_label.Foreground = Brushes.Black;
                    crossword_letter_label.FontWeight = FontWeights.Bold;

                    switch (results.D)
                    {
                        case 0:
                            index += 1;
                            break;
                        case 1:
                            index -= 1;
                            break;
                        case 2:
                            index += results.Cols;
                            break;
                        case 3:
                            index -= results.Cols;
                            break;
                    }
                }
            }
        }
        
        private void CreateCrossWordPuzzle(int puzzle_number)
        {
            try
            {
                _Puzzle = LoadPuzzle(puzzle_number);
                
                if (_Puzzle != null)
                {
                    if (_Words_Found != null) 
                    {
                        _Words_Found.Clear();
                    }

                    _Words_Found = new List<int>();
                    WordListBaseStackPanel.Children.Clear();
                    OutputBaseStackPanel.Children.Clear();
                    CrossWordBaseGrid.Children.Clear();

                    PuzzleNumberLabel.Content = $"Word search puzzle #{_Puzzle.P}:";

                    int rows = Convert.ToInt32(_Puzzle.R);
                    int cols = Convert.ToInt32(_Puzzle.C);
                    
                    Grid canvas = CrossWordCanvas(rows, cols);
                    Grid letters = CreateCrossWordGrid(rows, cols);
                    
                    PopulateGridPuzzleLetters(letters, _Puzzle.Grid());

                    canvas.Children.Add(letters);
                    CrossWordBaseGrid.Children.Add(canvas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private Grid PuzzleLetter(string name, string letter)
        {
            if (name == null || letter == null) 
            {
                throw new ArgumentNullException();
            }

            if (_Letter_Frame == null)
            {
                _Letter_Frame = LoadImageAsset(_Letter_URI);
            }

            Grid grid = new Grid
            {
                Name = name,
                Background = _Letter_Frame,
                Height = 25,
                Width = 25
            };

            Label label = new Label
            {
                Content = letter,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = System.Windows.Media.Brushes.WhiteSmoke,
                FontWeight = FontWeights.Regular
            };

            grid.Children.Add(label);

            return grid;
        }

        private void PopulateGridPuzzleLetters(Grid grid, string[][] letters)
        {
            if (grid == null || letters == null) 
            {
                throw new ArgumentNullException();
            }

            int row = 0;

            foreach (string[] group in letters)
            {
                for (int i = 0; i < group.Length; i++)
                {
                    Grid letter = PuzzleLetter($"r{row}c{i}", group[i]);
                    Grid.SetRow(letter, row);
                    Grid.SetColumn(letter, i);
                    grid.Children.Add(letter);
                }

                row++;
            }
        }

        private Grid CreateCrossWordGrid(int rows, int cols)
        {
            if 
            (
                rows < _Row_Range[0] || rows > _Row_Range[1] || 
                cols < _Column_Range[0] || cols > _Column_Range[1]
            )
            {
                throw new ArgumentNullException();
            }

            Grid grid = new Grid
            {
                Name = "CrossWordLetterGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition cd = new ColumnDefinition
                {
                    Width = new GridLength(25)
                };

                grid.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < rows; i++)
            {
                RowDefinition rd = new RowDefinition
                {
                    Height = new GridLength(25)
                };

                grid.RowDefinitions.Add(rd);
            }

            return grid;
        }

        private ImageBrush LoadImageAsset(string uri)
        {
            if (uri == null) 
            {
                throw new ArgumentNullException();
            }
            
            ImageSource source = new BitmapImage(new Uri(uri));

            ImageBrush brush = new ImageBrush
            {
                ImageSource = source,
                Stretch = Stretch.Fill
            };

            return brush;
        }

        private Grid CrossWordCanvas(int rows, int cols)
        {
            if 
            (
                rows < _Row_Range[0] || rows > _Row_Range[1] ||
                cols < _Column_Range[0] || cols > _Column_Range[1]
            )
            {
                throw new ArgumentNullException();
            }

            if (_Canvas == null) 
            {
                _Canvas = LoadImageAsset(_Canvas_URI);
            }

            Grid grid = new Grid
            {
                Name = "CrossWordCanvas",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = (cols * 25) + 50,
                Height = (rows * 25) + 50
            };

            return grid;
        }
    }
}
