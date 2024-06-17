/**
 * Mark Sarasua
 * Dr. Alrifai
 * CS 4253
 * Final Exam Alternative Project
 * Database.cs
 * 
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ACMFinalExamProblem
{
    internal class Database
    {
        private const string _Server = "LAPTOP-37A20S3C";
        private const string _Database = "CrossWordPuzzles";
        private const string _Table = "Puzzles";
        private string _Sql_Connection = string.Empty;
        private int _Puzzles = 0;

        public Database() 
        {
            try 
            {
                _Sql_Connection = $"Data Source={_Server}; Initial Catalog={_Database}; Integrated Security=True;";

                using(SqlConnection connection = new SqlConnection(_Sql_Connection)) 
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand
                    (
                        $"SELECT COUNT(*) FROM {_Table}",
                        connection
                    );

                    _Puzzles = (int)command.ExecuteScalar();
                    command.Dispose();
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine( ex.ToString() );
            }
        }
        
        public List<Puzzle> LoadPuzzleData() 
        {
            try
            {
                List<object> puzzle_data = new List<object>();

                using(SqlConnection connection = new SqlConnection(_Sql_Connection))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand
                    (
                        $"SELECT * FROM {_Table}",
                        connection
                    );

                    SqlDataReader data_reader = command.ExecuteReader();

                    while (data_reader.Read())
                    {
                        List<object> row = new List<object>();

                        for (int i = 0; i < data_reader.FieldCount; i++) 
                        {
                            row.Add(data_reader.GetValue(i));
                        }

                        puzzle_data.Add(row);
                    }

                    data_reader.Close();
                    command.Dispose();
                }

                return ExtractPuzzlesFromQueryResults(puzzle_data);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private List<Puzzle> ExtractPuzzlesFromQueryResults(List<object> query_results) 
        {
            List<Puzzle> puzzles = new List<Puzzle>();

            foreach (object row in query_results) 
            {
                Puzzle puzzle = new Puzzle
                {
                    Puzzles = $"{_Puzzles}",
                    P = (row as List<object>)[0].ToString(),
                    R = (row as List<object>)[1].ToString(),
                    C = (row as List<object>)[2].ToString(),
                    S = (row as List<object>)[3].ToString(),
                    Letters = (row as List<object>)[4].ToString(),
                    Words = (row as List<object>)[5].ToString(),
                };

                puzzles.Add(puzzle);
            }

            return puzzles;
        }
    }
}
