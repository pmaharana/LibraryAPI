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

        //[HttpGet]
        //public IHttpActionResult GetBook(int id)
        //{
        //    var book = Library.GetAllBooks(connectionString).FirstOrDefault(f => f.Id == id);
        //    return Ok(book);
        //}

        [HttpPut]
        public IHttpActionResult CreateBook([FromBody]Library libraryBook)
        {
            Library.AddABook(connectionString, libraryBook);
            return Ok(libraryBook);
          
        }

        [HttpPost]
        public IHttpActionResult UpdateBook([FromBody]Library libraryBook)
        {
            Library.UpdateABook(connectionString, libraryBook);
            return Ok(Library.GetAllBooks(connectionString));  //returns all the results so I can see whats updated
        }









    }

}
