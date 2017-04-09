using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibraryAPI.Models
{
    public class Library
    {
        private SqlDataReader reader;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public string Genre { get; set; }
        public string IsCheckedOut { get; set; }
        public string LastCheckedOutDate { get; set; }
        //public DateTime DueBackDate { get; set; }

        public Library()  //base constructor
        {

        }

        public Library(SqlDataReader reader)  //adding to Library constructor
        {
            Id = (int)reader[0];
            Title = reader[1].ToString();
            Author = reader[2].ToString();
            YearPublished = (int)reader[3];
            Genre = reader[4].ToString();
            IsCheckedOut = reader[5].ToString();
            LastCheckedOutDate = reader[6].ToString();

        }


        public static List<Library> GetAllBooks(string connectionString)
        {
            

            var books = new List<Library>();  //created a new list to store each book added
            using (var connection = new SqlConnection(connectionString))
            {
                var text = @"select Id, Title, Author, YearPublished, Genre, IsCheckedOut, LastCheckedOutDate
            from Catalog";
                var sqlCommand = new SqlCommand(text, connection);
                connection.Open();
                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    var book = new Library(reader);
                    books.Add(book);
                }
                connection.Close();
                return books;

            }
        }
    }

    
}