using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Net;

namespace ApiChecks
{    
    [TestFixture]
    public class DeleteChecks
    {
        private TodoItem testItem;

        [SetUp]
        public void TestDataSetup()
        {
            TodoItem item = new TodoItem
            {
                Name = $"DeleteChecks items {new DateTime().Ticks}",
                DateDue = new DateTime(2022, 02, 15),
                IsComplete = false
            };
            var client = new RestClient("https://localhost:44367/api/Todo");
            var request = new RestRequest(Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");

            //Act
            IRestResponse<TodoItem> response = client.Execute<TodoItem>(request);
            testItem = response.Data;
        }


        [Test]
        public void VerifyDeleteWithValidIdReturns204()
        {
            //Arrange
            var client = new RestClient($"https://localhost:44367/api/Todo/{testItem.Id}");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("CanAccess", "true");
            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, $"Delete item with ID {testItem.Id} should have returned NoContent, but it returned {response.StatusCode} instead");
        }
    }
}