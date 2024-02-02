using NUnit.Framework;
using LibraryManagementSystemTesting.Models;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using LibraryManagementSystemTesting.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LibraryManagementSystemTesting.Controllers;

namespace LibraryManagementSystemTesting.FunctionalTesting
{
    [TestFixture]
    //It ensures that the action redirects to the book list (Index action)
    //after a successful edit and verifies
    //the accuracy of the updated book data in the database.
    public class BooksEditTests
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

           
        }

        [Test]
        public async Task EditBook_ShouldUpdateBookInDatabase_WhenModelStateIsValid()
        {
            // Arrange
            var book = new BooksAdmin
            {
                // Set properties as needed to create a book
                BookId = 2 ,
                BookCode = "B001",
                BookName = "Test Book",
                BookPrice = 9.99
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Create a new instance of the controller and set the context
            var controller = new BooksAdminController(_context);

            // Act
            var editResult = await controller.Edit(book.BookId, book) as RedirectToActionResult;

            // Assert
            editResult.Should().NotBeNull();
            editResult.ActionName.Should().Be("Index");

            // Check if the book is updated in the database
            var updatedBook = await _context.Books.FindAsync(book.BookId);
            updatedBook.Should().NotBeNull();
            updatedBook.Should().BeEquivalentTo(book, options => options.ComparingByMembers<BooksAdmin>());
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup code if necessary
            _context.Dispose();
        }
    }
}
