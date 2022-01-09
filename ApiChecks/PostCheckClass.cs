using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Net;

namespace ApiChecks
{
    [TestFixture]
    public class PostChecksClass : ApiChecksBase
    {
        private TodoItem testItem;
       
        [TearDown]
        public void TestDataCleanUp()
        {
            IRestResponse response = _client.Execute(Helpers.DeleteTodoItemRequest(testItem.Id));
            if(response.StatusCode != HttpStatusCode.NoContent)
            {
                Console.WriteLine($"unable to delete {testItem} - {response.StatusCode}");
            }
        }
       
        [Test]
        public void VerifyPostWithAllValidValuesReturns201()
        {
            //Arrange
            TodoItem expectedItem = Helpers.GetTestTodoItem();
            var request = Helpers.PostTodoItemRequest(expectedItem);
            
            //Act
            IRestResponse<TodoItem> response = _client.Execute<TodoItem>(request);
            testItem = response.Data;

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Post new todo item should have returned 201, but returned {response.StatusCode} instead");
        }
    }
}