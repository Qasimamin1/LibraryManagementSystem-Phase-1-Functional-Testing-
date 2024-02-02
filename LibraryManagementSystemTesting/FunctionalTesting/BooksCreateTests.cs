using NUnit.Framework;
using LibraryManagementSystemTesting.Models;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System;
using LibraryManagementSystemTesting.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NUnit.Framework.Internal;

namespace LibraryManagementSystemTesting.FunctionalTesting
{
    [TestFixture]  //using Nunit

    // The test confirmed that when a valid book entry is created, it is correctly
    // added to the database, ensuring the "Create" functionality works as expected

    //Test Case 1: Verifies that a book can be successfully added to the database.
    //Test Case 2: Checks the handling of invalid input data.
    public class BooksCreateTests
    {
        private LibraryDbContext _context; // Using your actual DbContext to simulate database operations

        [SetUp]
        public void Setup()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new LibraryDbContext(options);

            // Additional setup can be done here if necessary
        }

        [Test] //using NUNIT 
        public void CreateBook_ShouldAddBookToDatabase_WhenBookIsValid()
        {
            // Arrange
            var book = new BooksAdmin
            {
                // Set properties as needed create own self.. 
                BookId =4,
                BookCode = "B001",
                BookName = "Test Book",
                BookPrice = 9.99
            };

            // Act
            _context.Books.Add(book);
            _context.SaveChanges();

            // Assert
          //  retrieves a book from the in-memory database using _context,
          //  which is your database context.
            var retrievedBook = _context.Books.Find(book.BookId);
            retrievedBook.Should().NotBeNull();
            retrievedBook.Should().BeEquivalentTo(book, options => options.ComparingByMembers<BooksAdmin>());
          //  retrievedBook.BookName.Should().Be("Incorrect Test Book");
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup code if necessary
            _context.Dispose();
        }
    }
}







// Intentionally failing the test by asserting a wrong book name.
// This assertion checks if the book name is "Incorrect Test Book", which it is not.
// Therefore, this line will cause the test to fail.
//retrievedBook.BookName.Should().Be("Incorrect Test Book");
// var retrievedBook = _context.Books.Find(999);}