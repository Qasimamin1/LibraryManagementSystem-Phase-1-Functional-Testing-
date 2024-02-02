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
    // there are total three test cases.
    [TestFixture]
    public class BooksDetailsTests
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
        public async Task Details_ShouldReturnViewWithBook_WhenIdIsValid()
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
            var result = await controller.Details(book.BookId) as ViewResult;

            // Assert
            result.Should().NotBeNull(); // Check if the result is not null
            result.Model.Should().BeOfType<BooksAdmin>(); // Check if the model in the result is of type BooksAdmin
            var model = result.Model as BooksAdmin;
            model.Should().BeEquivalentTo(book, options => options.ComparingByMembers<BooksAdmin>());
            // Check if the model in the result is equivalent to the expected book using FluentAssertions
        }

        [Test]
        public async Task Details_ShouldReturnNotFound_WhenIdIsNull()
        {
            // Arrange
            var controller = new BooksAdminController(_context);

            // Act
            var result = await controller.Details(null) as NotFoundResult;

            // Assert
            result.Should().NotBeNull(); // Check if the result is not null
        }

        [Test]
        public async Task Details_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var controller = new BooksAdminController(_context);

            // Act
            var result = await controller.Details(99) as NotFoundResult;

            // Assert
            result.Should().NotBeNull(); // Check if the result is not null
        }

        [TearDown]
        public void TearDown()
        {
            // Cleanup code if necessary
            _context.Dispose();
        }
    }
}
