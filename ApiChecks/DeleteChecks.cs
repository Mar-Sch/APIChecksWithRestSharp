﻿using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Net;

namespace ApiChecks
{    
    [TestFixture]
    public class DeleteChecks : ApiChecksBase
    {
        private TodoItem testItem;

        [SetUp]
        public void TestDataSetup()
        {
            TodoItem item = Helpers.GetTestTodoItem(name: $"DeleteChecks items {new DateTime().Ticks}");
            var request = Helpers.PostTodoItemRequest(item);

            //Act
            IRestResponse<TodoItem> response = _client.Execute<TodoItem>(request);
            testItem = response.Data;
        }

        [Test]
        public void VerifyDeleteWithValidIdReturns204()
        {
            //Arrange
            var request = Helpers.DeleteTodoItemRequest(testItem.Id);
            //Act
            IRestResponse response = _client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, $"Delete item with ID {testItem.Id} should have returned NoContent, but it returned {response.StatusCode} instead");
        }
    }
}