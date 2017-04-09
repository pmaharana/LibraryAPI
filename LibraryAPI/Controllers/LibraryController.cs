using LibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryAPI.Controllers
{   

    public class LibraryController : ApiController
    {
        //the connection string to my SQL table
        const string connectionString =
                @"Server=localhost\SQLEXPRESS;Database=LibraryAPI;Trusted_Connection=True;";

        



        [HttpGet]
        public IEnumerable<Library> GetLibraryInfo()
        {
            return Library.GetAllBooks(connectionString);
        }

        [HttpGet]
        public IHttpActionResult GetBook(int id)
        {
            var book = Library.GetAllBooks(connectionString).FirstOrDefault(f => f.Id == id);
            return Ok(book);
        }

        [HttpPut]
        public IHttpActionResult CreateBook([FromBody]Library libraryBook)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var text = @"insert into [Catalog] (Title, Author, YearPublished) 
                    values  (@Title, @Author, @YearPublished)";

                var cmd = new SqlCommand(text, connection);
                
                cmd.Parameters.AddWithValue("@Title", libraryBook.Title);
                cmd.Parameters.AddWithValue("@Author", libraryBook.Author);
                cmd.Parameters.AddWithValue("@YearPublished", libraryBook.YearPublished);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }

            return Ok(libraryBook);
        }








    }

}
