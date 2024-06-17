/**
 * Mark Sarasua
 * Dr. Alrifai
 * CS 4253
 * Final Exam Alternative Project
 * CrossWordPuzzleLogic.cs
 * 
 */

using System;
using System.Collections.Generic;

namespace ACMFinalExamProblem
{
    enum DIRECTION 
    {
        R, L, D, U
    };

    public class SearchResults 
    {
        public int P { get; set; }  // puzzle number
        public int D { get; set; }  // direction of letters
        public int R { get; set; }  // first letter row number 
        public int C { get; set; }  // first letter column number
        public string W { get; set; }   // word searched for
        public bool SET { get; set; }   // fields set bool
        public int Rows { get; set; }   // number of rows
        public int Cols { get; set; }   // number of columns

        public string PuzzleID() 
        { 
            return $"Word search puzzle #{P}:"; 
        }

        public string WordLocation() 
        {
            if(SET == true) 
            {
                string RR = (R < 10) ? $" {R}" : $"{R}";
                string CC = (C < 10) ? $" {C}" : $"{C}";
                return $"{(DIRECTION)D} {RR} {CC} {W}";
            }

            return $"{W} Not Found";
        }
    }
    
    public class Puzzle
    {
        public string P { get; set; }   // word search puzzle number
        public string R { get; set; }   // number of rows in puzzle grid
        public string C { get; set; }   // number of columns in puzzle grid
        public string S { get; set; }   // number of words to search for
        public string Letters { get; set; } // raw puzzle letter set
        public string Words { get; set; }   // raw word set
        public string Puzzles { get; set; } // number of puzzles
        
        private string[][] _PuzzleCols;
        private string[][] _PuzzleRows;
        private string[][] _Words;

        public SearchResults Locate(string word) 
        {
            if (word == null || this._Words == null)
            {
                throw new ArgumentNullException();
            }

            int ind = Convert.ToInt32(word);
            
            SearchResults search_results = new SearchResults();
            
            for (int i = 0; i < 2; i++)
            {
                search_results = SearchPuzzle(_Words[ind], (i == 1) ? "true" : "false");
                
                if (search_results.SET)
                {
                    search_results.Rows = Convert.ToInt32(this.R);
                    search_results.Cols = Convert.ToInt32(this.C);
                    return search_results;
                }
            }
            
            return search_results;
        }

        public string[][] WordList() 
        {
            if (this.S == null || this.Words == null)
            {
                throw new ArgumentNullException();
            }

            int S = Convert.ToInt32(this.S);
            
            string[][] word_list = new string[S][];
            string[] words = Words.Split(',');
            int i = 0;
            
            foreach (string word in words)
            {
                string[] temp = new string[word.Length];
                char[] letters = word.ToCharArray();
                int j = 0;
                
                foreach (char letter in letters)
                {
                    temp[j++] = letter.ToString();
                }
                
                word_list[i++] = temp;
            }
            
            _Words = word_list;
            return word_list;
        }

        public string[][] Grid() 
        {
            if (this.C == null || this.R == null || this.Letters == null)
            {
                throw new ArgumentNullException();
            }

            int C = Convert.ToInt32(this.C);
            int R = Convert.ToInt32(this.R);
            
            string[][] columns = new string[C][];
            string[][] rows = new string[R][];
            
            char[] letters = Letters.ToCharArray();
            
            int col = 0;
            int row = 0;
            
            foreach (char letter in letters)
            {
                int col_mod = col % C;
                int new_row = C - 1;
                
                if (col < C)
                {
                    columns[col] = new string[R];
                }
                
                if (col_mod == 0)
                {
                    rows[row] = new string[C];
                }
                
                columns[col_mod][row] = letter.ToString();
                rows[row][col_mod] = letter.ToString();
                
                if (col_mod == new_row)
                {
                    row++;
                }
                
                col++;
            }
            
            _PuzzleCols = columns;
            _PuzzleRows = rows;
            
            return rows;
        }
        
        private SearchResults SearchPuzzle(string[] word, string vertical) 
        {
            if (word == null || vertical == null)
            {
                throw new ArgumentNullException();
            }

            bool vert = Convert.ToBoolean(vertical);

            string[][] letters = vert ? _PuzzleCols : _PuzzleRows;
            int index = 0;
            
            foreach (string[] row in letters)
            {
                int[] results = WordLocation(row, word, vertical);
                
                if (results != null && results[1] != -1)
                {
                    SearchResults search_results = new SearchResults
                    {
                        P = Convert.ToInt32(this.P),
                        D = results[1],
                        W = String.Join("", word)
                    };
                    
                    switch (Convert.ToInt32(vert))
                    {
                        case 0:
                            search_results.R = index + 1;
                            search_results.C = results[0] + 1;
                            break;
                        
                        case 1:
                            search_results.R = results[0] + 1;
                            search_results.C = index + 1;
                            break;
                    }
                    
                    search_results.SET = true;
                    return search_results;
                }
                
                index++;
            }
            
            return Empty(word);
        }

        private SearchResults Empty(string[] word) 
        {
            if (word == null)
            {
                throw new ArgumentNullException();
            }

            SearchResults empty = new SearchResults
            {
                W = String.Join("", word),
                SET = false
            };

            return empty;
        }

        private int[] WordLocation(string[] letters, string[] word, string vertical)
        {
            if (letters == null || word == null || vertical == null)
            {
                throw new ArgumentNullException();
            }
            
            List<int> index = StartPosition(letters, word[0]);
            
            if (index.Count != 0)
            {
                int[] location = new int[2];

                for (int i = 0; i < index.Count; i++) 
                {
                    location[0] = index[i];
                    location[1] = WordDirection(letters, word, $"{index[i]}", vertical);

                    if (location[1] != -1) 
                    {
                        return location;
                    }
                }
            }

            return null;
        }

        private int WordDirection(string[] letters, string[] word, string index, string vertical) 
        {
            if (letters == null || word == null || index == null || vertical == null)
            {
                throw new ArgumentNullException();
            }

            bool vert = Convert.ToBoolean(vertical);
            
            if (!IsForward(letters, word, index))
            {
                if (IsBackward(letters, word, index))
                {
                    return vert ? 3 : 1;
                }
            }
            else
            {
                return vert ? 2 : 0;
            }
            
            return -1;
        }
        
        private List<int> StartPosition(string[] letters, string letter) 
        {
            if (letters == null || letter == null)
            {
                throw new ArgumentNullException();
            }

            List<int> positions = new List<int>();

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == letter)
                {
                    positions.Add(i);
                }
            }
            
            return positions;
        }

        private bool IsForward(string[] letters, string[] word, string index) 
        {
            if (letters == null || word == null || index == null)
            {
                throw new ArgumentNullException();
            }
            
            int n = letters.Length;
            int ind = Convert.ToInt32(index);
            
            for (int i = ind, j = 0; i < (word.Length + ind); i++, j++)
            {
                if (letters[i % n] != word[j])
                {
                    return false;
                }
            }
            
            return true;
        }

        private bool IsBackward(string[] letters, string[] word, string index) 
        {
            if (letters == null || word == null || index == null) 
            {
                throw new ArgumentNullException();
            }
            
            int n = letters.Length;
            int ind = Convert.ToInt32(index);
            
            for (int i = ind, j = 0; i > (ind - word.Length); i--, j++)
            {
                if (letters[(i < 0) ? i + n : i] != word[j])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
