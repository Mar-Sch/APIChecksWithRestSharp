using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections;
using System.Net;

namespace ApiChecks
{
    [TestFixture]
    public class APIChecksClass : ApiChecksBase
    {        
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
                yield return new TestCaseData(Helpers.GetTestTodoItem()).Returns("NoContent").SetName("happy path");
                yield return new TestCaseData(Helpers.GetTestTodoItem(name: "")).Returns("BadRequest").SetName("blank name");
                yield return new TestCaseData(Helpers.GetTestTodoItem(name: "")).Returns("BadRequest").SetName("missing name field");
            }
        }
    }

}