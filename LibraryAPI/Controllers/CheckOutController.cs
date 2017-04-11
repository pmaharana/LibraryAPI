using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryAPI.Models;
using System.Data.SqlClient;

namespace LibraryAPI.Controllers
{
    public class CheckOutController : ApiController
    {

        const string connectionString =
                @"Server=localhost\SQLEXPRESS;Database=LibraryAPI;Trusted_Connection=True;";


        //[HttpPost]
        //public IHttpActionResult Checkout(int id)
        //{
        //    // select to see if the book was already checkout
        //    // if its checked out return a message syaing it checked out and will be bcak at the due date
        //    return Ok(new { Message = "already checked out", DueBackDate = DateTime.Now.AddDays(10) });

        //    // else 
        //    // update to check the book the out
        //}


        //[HttpGet]
        //public IHttpActionResult GetBook(int bookId)
        //{
        //    var book = Book.GetABook(connectionString, bookId);
        //    return Ok(book);
        //}

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var book = Book.GetAllBooks(connectionString).FirstOrDefault(f => f.Id == id); 
            return Ok(book);
        }

        [HttpPost]
        public IHttpActionResult CheckBookOut(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var books = new Book();
                var text = "SELECT * FROM [Catalog] WHERE Id = @Id";
                var cmd = new SqlCommand(text, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books = new Book(reader);
                }
                connection.Close();
                if (books.IsCheckedOut == "True")
                {
                    return Ok(new { Message = "Sorry, this book has already been checked out" });
                }
                else
                {
                    var query = @"UPDATE [Catalog] SET IsCheckedOut=@IsCheckedOut, DueBackDate=@DueBackDate where Id=@Id";
                    var sqlCmd = new SqlCommand(query, connection);
                    sqlCmd.Parameters.AddWithValue("@IsCheckedOut", "True");
                    sqlCmd.Parameters.AddWithValue("@DueBackDate", DateTime.Now.AddDays(10).);
                    sqlCmd.Parameters.AddWithValue("@Id",id);
                    connection.Open();
                    sqlCmd.ExecuteNonQuery();
                    connection.Close();
                    return Ok(new { Message = "You have checked out a book! It's due back on", books.DueBackDate });
                }
            }
        }

    }
}


