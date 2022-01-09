using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Net;

namespace ApiChecks
{
    [TestFixture]
    public class APIChecksClass
    {
        private static string _baseUrl;
        private static RestClient _client;

        [OneTimeSetUp]
        public void TestClassInitialize()
        {
            _baseUrl = "https://localhost:44367/api/Todo";
            _client = new RestClient(_baseUrl);
        }

        [Test]
        public void VerifyGetAllTodoItemsReturns200()
        {

            //act
            IRestResponse response = _client.Execute(Helpers.GetAllTodoItemsRequest());

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET all todo items did not return a success status code; it returned {response.StatusCode}");
        }

        [Test]
        public void VerifyGetTodoItemWithId1ReturnsId1()
        {
            //Arrange
            var expectedId = 1;
            var request = Helpers.GetSingleTodoItemRequest(expectedId);

            //Act
            IRestResponse<TodoItem> response = _client.Execute<TodoItem>(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET todo item with {expectedId} did not return a success status code; it returned {response.StatusCode}");

            Assert.AreEqual(expectedId, response.Data.Id, $"GET todo with id {expectedId} returned {response.Data.Id} instead");

            StringAssert.AreEqualIgnoringCase("Walk the dog", response.Data.Name, $"Name should have been 'Walk the dog', but was {response.Data.Name}");
        }

        [Test]
        public void VerifyPostWithAllValidValuesReturns201()
        {
            //Arrange
            TodoItem expectedItem = Helpers.GetTestTodoItem();
            var request = Helpers.PostTodoItemRequest(expectedItem);
            
            //Act
            IRestResponse response = _client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Post new todo item should have returned 201, but returned {response.StatusCode} instead");
        }

        [Test, TestCaseSource(typeof(TestDataClass), "PutTestData")]
        public string VerifyPut(TodoItem item)
        {
            //Arrange
            var request = Helpers.PutTodoItemRequest(1, item);

            //Act
            IRestResponse response = _client.Execute(request);

            //Assert
            return response.StatusCode.ToString();
        }
    }

    public class TestDataClass
    {
        public static IEnumerable PutTestData
        {
            get
            {
                yield return new TestCaseData(Helpers.GetTestTodoItem()).Returns("NoContent");
                yield return new TestCaseData(Helpers.GetTestTodoItem(name: "")).Returns("BadRequest");
                yield return new TestCaseData(Helpers.GetTestTodoItem(name: "")).Returns("BadRequest");
            }
        }
    }

}