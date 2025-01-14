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
    public class TagControllerTests
    {
        private readonly Mock<TagRepository> _tagRepositoryMock;
        private readonly TagController _controller;

        public TagControllerTests()
        {
            _tagRepositoryMock = new Mock<TagRepository>();
            _controller = new TagController(_tagRepositoryMock.Object);
        }

        [Fact]
        public void GetAllTags_ReturnsOk_WhenTagsExist()
        {
            // Arrange
            var tags = new List<Tag>
            {
                new Tag("Tag1"),
                new Tag("Tag2")
            };
            _tagRepositoryMock.Setup(repo => repo.GetAllTags()).Returns(tags);

            // Act
            var result = _controller.GetAllTags();

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetAllTags_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            _tagRepositoryMock.Setup(repo => repo.GetAllTags()).Throws(new Exception("No tags found"));

            // Act
            var result = _controller.GetAllTags();

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void GetTagById_ReturnsOk_WhenTagExists()
        {
            // Arrange
            int tagId = 1;
            var tag = new Tag("Tag1");
            _tagRepositoryMock.Setup(repo => repo.GetTagById(tagId)).Returns(tag);

            // Act
            var result = _controller.GetTagById(tagId);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public void GetTagById_ReturnsNotFound_WhenExceptionThrown()
        {
            // Arrange
            int tagId = 1;
            _tagRepositoryMock.Setup(repo => repo.GetTagById(tagId)).Throws(new Exception("Tag not found"));

            // Act
            var result = _controller.GetTagById(tagId);

            // Assert
            var notFoundResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void CreateTag_ReturnsOk_WhenTagCreated()
        {
            // Arrange
            string tagName = "New Tag";

            // Act
            var result = _controller.CreateTag(tagName);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void CreateTag_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            string tagName = "New Tag";
            _tagRepositoryMock.Setup(repo => repo.CreateTag(It.IsAny<Tag>())).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.CreateTag(tagName);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void UpdateTag_ReturnsOk_WhenTagUpdated()
        {
            // Arrange
            int tagId = 1;
            string newTagName = "Updated Tag";

            // Act
            var result = _controller.UpdateTag(tagId, newTagName);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void UpdateTag_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int tagId = 1;
            string newTagName = "Updated Tag";
            _tagRepositoryMock.Setup(repo => repo.UpdateTag(tagId, newTagName)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.UpdateTag(tagId, newTagName);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void DeleteTag_ReturnsOk_WhenTagDeleted()
        {
            // Arrange
            int tagId = 1;

            // Act
            var result = _controller.DeleteTag(tagId);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteTag_ReturnsUnauthorized_WhenExceptionThrown()
        {
            // Arrange
            int tagId = 1;
            _tagRepositoryMock.Setup(repo => repo.DeleteTag(tagId)).Throws(new Exception("Unauthorized"));

            // Act
            var result = _controller.DeleteTag(tagId);

            // Assert
            var unauthorizedResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}
