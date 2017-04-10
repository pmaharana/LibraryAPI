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
        public IEnumerable<Book> GetLibraryInfo()
        {
            return Book.GetAllBooks(connectionString);
        }

        //[HttpGet]
        //public IHttpActionResult GetBook(int id)
        //{
        //    var book = Book.GetBook(connectionString, id);
        //    return Ok(book);
        //}

        [HttpPut]
        public IHttpActionResult CreateBook([FromBody]Book libraryBook)
        {
            Book.AddABook(connectionString, libraryBook);
            return Ok(libraryBook);

        }

        [HttpPost]
        public IHttpActionResult UpdateBook([FromBody]Book libraryBook)
        {
            Book.UpdateABook(connectionString, libraryBook);
            return Ok(Book.GetAllBooks(connectionString));  //returns all the results so I can see whats updated
        }

        [HttpDelete]
        public IHttpActionResult DeleteBook([FromBody]Book libraryBook)
        {
            Book.DeleteABook(connectionString, libraryBook);
            return Ok(Book.GetAllBooks(connectionString));
        }



    }





}
