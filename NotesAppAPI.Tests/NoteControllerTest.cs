using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;
using System;
using System.Collections.Generic;

namespace NotesAppAPI.Tests
{
    public class NoteControllerTests
    {
        private readonly Mock<NoteRepository> _noteRepositoryMock;
        private readonly NoteController _controller;

        public NoteControllerTests()
        {
            _noteRepositoryMock = new Mock<NoteRepository>();
            _controller = new NoteController(_noteRepositoryMock.Object);
        }

        [Fact]
        public void GetAllNotes_ReturnsOk_WhenNotesExist()
        {
            // Arrange
            var notes = new List<Note>
            {
                new Note("Title1", "Content1", 1),
                new Note("Title2", "Content2", 2)
            };
            _noteRepositoryMock.Setup(repo => repo.GetAllNotes()).Returns(notes);

            // Act
            var result = _controller.GetAllNotes();

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetAllNotes_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            _noteRepositoryMock.Setup(repo => repo.GetAllNotes()).Throws(new Exception("No notes found"));

            // Act
            var result = _controller.GetAllNotes();

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetNoteById_ReturnsOk_WhenNoteExists()
        {
            // Arrange
            int noteId = 1;
            var note = new Note("Title1", "Content1", 1);
            _noteRepositoryMock.Setup(repo => repo.GetNoteById(noteId)).Returns(note);

            // Act
            var result = _controller.GetNoteById(noteId);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetNoteById_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            int noteId = 1;
            _noteRepositoryMock.Setup(repo => repo.GetNoteById(noteId)).Throws(new Exception("Note not found"));

            // Act
            var result = _controller.GetNoteById(noteId);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void CreateNote_ReturnsOk_WhenNoteCreated()
        {
            // Arrange
            int userId = 1;
            string title = "New Note";
            string content = "Note content";

            // Act
            var result = _controller.CreateNote(userId, title, content);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void CreateNote_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int userId = 1;
            string title = "New Note";
            string content = "Note content";
            _noteRepositoryMock.Setup(repo => repo.CreateNote(It.IsAny<Note>())).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.CreateNote(userId, title, content);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void UpdateNote_ReturnsOk_WhenNoteUpdated()
        {
            // Arrange
            int noteId = 1;
            string newTitle = "Updated Title";
            string newContent = "Updated Content";

            // Act
            var result = _controller.UpdateNote(noteId, newTitle, newContent);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateNote_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int noteId = 1;
            string newTitle = "Updated Title";
            string newContent = "Updated Content";
            _noteRepositoryMock.Setup(repo => repo.Update(noteId, newTitle, newContent)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.UpdateNote(noteId, newTitle, newContent);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void DeleteNote_ReturnsOk_WhenNoteDeleted()
        {
            // Arrange
            int noteId = 1;

            // Act
            var result = _controller.DeleteNote(noteId);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteNote_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int noteId = 1;
            _noteRepositoryMock.Setup(repo => repo.Delete(noteId)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.DeleteNote(noteId);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}
