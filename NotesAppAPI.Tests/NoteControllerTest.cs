using System;
using Microsoft.AspNetCore.Mvc;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Models;
using NotesAppAPI.DataAccess.EF.Repositories;

namespace NotesAppAPI.Tests
{
    public class NoteControllerTest
    {

        private readonly NoteRepository _noteRepository;
        private readonly NoteController _noteController; 


        //Test GetallNotes
        [Fact]
        public void Get_WhenCalled_ReturnOkResult()
        {
            //Act
            var okResult = _noteController.GetAllNotes();

            //Assert
            Assert.NotNull(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnAllNotes()
        {
            //Act
            var okResult = _noteController.GetAllNotes() as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<Note>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }


        //Test GetNoteById
        [Fact]
        public void GetById_UknownIdPassed_ReturnsNotFoundResult()
        {
            //Act
            var notFoundResult = _noteController.GetNoteById(0);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);

        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnOkResult()
        {
            //Arrange
            var TestId = 1;

            //Act
            var okResult = _noteController.GetNoteById(TestId);

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingId_ReturnRightNote()
        {
            //Arrange 
            var TestId = 1;

            //Act
            var okResult = _noteController.GetNoteById(TestId) as OkObjectResult;

            //Assert 
            Assert.IsType<Note>(okResult);
            Assert.Equal(TestId, (okResult.Value as Note).NoteId);
        }

        //Test addNote
        [Fact]
        public void Add_InvalidObjectPassed_ReturnBadRequest()
        {
            //Arrange
        }

    }
}