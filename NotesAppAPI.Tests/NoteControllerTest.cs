using System;
using NotesAppAPI.Controllers;
using NotesAppAPI.DataAccess.EF.Repositories;

namespace NotesAppAPI.Tests
{
    public class NoteControllerTest
    {

        private readonly NoteRepository _noteRepository;
        private readonly NoteController _noteController; 

        [Fact]
        public void Get_WhenCalled_ReturnOkResult()
        {
            //Act
            var okResult = _noteController.GetNoteById(1);

            //Assert
            Assert.NotNull(okResult);
        }
    }
}