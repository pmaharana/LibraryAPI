using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryAPI.Models;

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


    }
}


