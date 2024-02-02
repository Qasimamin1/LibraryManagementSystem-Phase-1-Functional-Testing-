using NUnit.Framework;
using LibraryManagementSystemTesting.Models;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using LibraryManagementSystemTesting.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManagementSystemTesting.Controllers;
using NUnit.Framework.Internal;

namespace LibraryManagementSystemTesting.FunctionalTesting
{
    [TestFixture]
    //our test case is to ensure that when a valid book ID is provided,
    //the method correctly removes the corresponding book from the database
    //and redirects to the "Index" action. 
    public class BooksDeleteTests
    {
        private LibraryDbContext _context; // Using your actual DbContext

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

        [Test]
        public async Task DeleteConfirmed_ShouldRemoveBookFromDatabase_WhenValidIdIsProvided()
        {
            // Arrange
            var book = new BooksAdmin
            {
                // Set properties as needed to create a book
                BookId = 1,
                BookCode = "B001",
                BookName = "Test Book",
                BookPrice = 9.99
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Create a new instance of the controller and set the context
            var controller = new BooksAdminController(_context);

            // Act
            var deleteResult = await controller.DeleteConfirmed(book.BookId) as RedirectToActionResult;
            //var deleteResult = await controller.DeleteConfirmed(999) as RedirectToActionResult;
            // Assert
           
            // This line asserts that the 'deleteResult' should not be null, 
            deleteResult.Should().NotBeNull();

            // check that the 'ActionName' property of 'deleteResult' is equal
            // to "Index," verifying that after deleting a book, the action redirects
            // to the "Index" action.
            
            deleteResult.ActionName.Should().Be("Index");

            // Check if the book is removed from the database
            //These lines collectively ensure that the deletion action redirects
            //to the "Index" action and that the book has been removed from the
            //database as expected.
            
            var deletedBook = await _context.Books.FindAsync(book.BookId);
            deletedBook.Should().BeNull();
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup code if necessary
            _context.Dispose();
        }
    }
}



//var deleteResult = await controller.DeleteConfirmed(999) as RedirectToActionResult;